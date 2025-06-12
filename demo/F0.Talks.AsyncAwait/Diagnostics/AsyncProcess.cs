using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace F0.Talks.AsyncAwait.Diagnostics;

public static class AsyncProcess
{
    public static async Task<ProcessResult> StartAsync(ProcessStartInfo startInfo, CancellationToken cancellationToken = default)
    {
        startInfo.UseShellExecute = false;
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;

        Process process = new()
        {
            StartInfo = startInfo,
            EnableRaisingEvents = true
        };

        List<string> standardOutput = [];
        TaskCompletionSource<ImmutableArray<string>> standardOutputResults = new();
        process.OutputDataReceived += OnOutputDataReceived;

        List<string> standardError = [];
        TaskCompletionSource<ImmutableArray<string>> standardErrorResults = new();
        process.ErrorDataReceived += OnErrorDataReceived;

        TaskCompletionSource<ProcessResult> processCompletion = new();
        process.Exited += OnProcessExited;

        bool started = process.Start();
        if (started)
        {
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
        }
        else
        {
            _ = processCompletion.TrySetException(new InvalidOperationException("Failed to start new process."));
        }

        await using (cancellationToken.Register((object? state) => processCompletion.TrySetCanceled(cancellationToken), null, false))
        {
            return await processCompletion.Task;
        }

        void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            string? data = e.Data;
            if (data is null)
            {
                process.OutputDataReceived -= OnOutputDataReceived;
                standardOutputResults.SetResult(standardOutput.ToImmutableArray());
            }
            else
            {
                standardOutput.Add(data);
            }
        }

        void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            string? data = e.Data;
            if (data is null)
            {
                process.ErrorDataReceived -= OnErrorDataReceived;
                standardErrorResults.SetResult(standardError.ToImmutableArray());
            }
            else
            {
                standardError.Add(data);
            }
        }

        async void OnProcessExited(object? sender, EventArgs e)
        {
            var process = sender as Process;
            Debug.Assert(process is not null);

            process.Exited -= OnProcessExited;

            await Task.WhenAll(standardOutputResults.Task, standardErrorResults.Task);

            ProcessResult result = new(process.ExitCode, standardOutputResults.Task.Result, standardErrorResults.Task.Result);

            process.Dispose();

            processCompletion.TrySetResult(result);
        }
    }
}

public sealed class ProcessResult
{
    internal ProcessResult(int exitCode, ImmutableArray<string> standardOutput, ImmutableArray<string> standardError)
    {
        ExitCode = exitCode;
        StandardOutput = standardOutput;
        StandardError = standardError;
    }

    public int ExitCode { get; }
    public ImmutableArray<string> StandardOutput { get; }
    public ImmutableArray<string> StandardError { get; }
}
