using F0.Talks.AsyncAwait.Awaitables;
using F0.Talks.AsyncAwait.NuGet.Services;
using F0.Talks.AsyncAwait.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace F0.Talks.AsyncAwait.ConsoleApp
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            WriteCommandLineArguments(args);

            await TimeSpan.FromSeconds(2);
            await Task.Delay(2_000);

            Console.WriteLine("Hello .NET Community Austria!");
            Console.WriteLine();

            await Awaiters.DetachCurrentSyncContext();

            await AsyncService.WriteAsync();
            await WriteNuGetDownloadsAsync();

            var cts = new CancellationTokenSource();
            cts.Cancel();
            try
            {
                await ProcessService.RunAsync("dotnet", "--version", cts.Token);
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("The operation was canceled.");
            }
        }

        private static async Task WriteNuGetDownloadsAsync()
        {
            string packageId = "Microsoft.Bcl.AsyncInterfaces";
            Task<int> task = NuGetService.GetAsync(packageId, true, default);
            int totalDownloads = await task;
            Console.WriteLine($"NuGet package '{packageId}' has {totalDownloads} total downloads");
        }

        private static void WriteCommandLineArguments(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine(string.Join(", ", args));
            }
        }
    }
}
