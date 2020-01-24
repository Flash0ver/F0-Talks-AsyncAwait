using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace F0.Talks.AsyncAwait.Diagnostics
{
    public static class AsyncProcess
    {
        public static async Task<ProcessResult> StartAsync(ProcessStartInfo startInfo, CancellationToken cancellationToken = default)
        {
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            var process = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true
            };

            var standardOutput = new List<string>();
            var standardOutputResults = new TaskCompletionSource<string[]>();
            process.OutputDataReceived += OnOutputDataReceived;

            var standardError = new List<string>();
            var standardErrorResults = new TaskCompletionSource<string[]>();
            process.ErrorDataReceived += OnErrorDataReceived;

            var processCompletion = new TaskCompletionSource<ProcessResult>();
            process.Exited += OnProcessExited;

            bool started = process.Start();
            if (started)
            {
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            else
            {
                processCompletion.TrySetException(new InvalidOperationException("Failed to start new process."));
            }

            using (cancellationToken.Register(() => processCompletion.TrySetCanceled(cancellationToken)))
            {
                return await processCompletion.Task;
            }

            void OnOutputDataReceived(object sender, DataReceivedEventArgs e)
            {
                string data = e.Data;
                if (data is null)
                {
                    process.OutputDataReceived -= OnOutputDataReceived;
                    standardOutputResults.SetResult(standardOutput.ToArray());
                }
                else
                {
                    standardOutput.Add(data);
                }
            }

            void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
            {
                string data = e.Data;
                if (data is null)
                {
                    process.ErrorDataReceived -= OnErrorDataReceived;
                    standardErrorResults.SetResult(standardError.ToArray());
                }
                else
                {
                    standardError.Add(data);
                }
            }

            async void OnProcessExited(object sender, EventArgs e)
            {
                var process = sender as Process;

                process!.Exited -= OnProcessExited;

                await Task.WhenAll(standardOutputResults.Task, standardErrorResults.Task);

                var result = new ProcessResult(process.ExitCode, standardOutputResults.Task.Result, standardErrorResults.Task.Result);

                process.Dispose();

                processCompletion.TrySetResult(result);
            }
        }
    }

    public sealed class ProcessResult
    {
        public int ExitCode { get; }
        public string[] StandardOutput { get; }
        public string[] StandardError { get; }

        internal ProcessResult(int exitCode, string[] standardOutput, string[] standardError)
        {
            ExitCode = exitCode;
            StandardOutput = standardOutput;
            StandardError = standardError;
        }
    }
}
