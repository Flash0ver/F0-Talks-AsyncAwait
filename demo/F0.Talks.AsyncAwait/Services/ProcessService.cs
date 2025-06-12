using F0.Talks.AsyncAwait.Diagnostics;

namespace F0.Talks.AsyncAwait.Services;

public static class ProcessService
{
    public static async Task RunAsync(string fileName, string arguments, CancellationToken cancellationToken = default)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo(fileName, arguments);

        Task<ProcessResult> task = AsyncProcess.StartAsync(startInfo, cancellationToken);

        ProcessResult result = await task;

        PrintOutput(result);
        PrintError(result);
        PrintExitCode(result);

        static void PrintOutput(ProcessResult result)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (string output in result.StandardOutput)
            {
                Console.WriteLine(output);
            }
            Console.ResetColor();
        }

        static void PrintError(ProcessResult result)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string error in result.StandardError)
            {
                Console.WriteLine(error);
            }
            Console.ResetColor();
        }

        static void PrintExitCode(ProcessResult result)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Process exited with code {result.ExitCode}.");
            Console.ResetColor();
        }
    }
}
