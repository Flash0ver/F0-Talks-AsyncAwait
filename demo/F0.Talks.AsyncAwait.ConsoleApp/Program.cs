using F0.Talks.AsyncAwait.Awaitables;
using F0.Talks.AsyncAwait.NuGet.Services;
using F0.Talks.AsyncAwait.Services;
using System.Globalization;
using F0.Talks.AsyncAwait.ConsoleApp.Runtime;

namespace F0.Talks.AsyncAwait.ConsoleApp;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        using ConsoleLifetime lifetime = new();

        WriteCommandLineArguments(args);

        await TimeSpan.FromSeconds(2);
        await Task.Delay(2_000, CancellationToken.None);

        Console.WriteLine("Hello .NET Community Austria!");
        Console.WriteLine();

        await Awaiters.DetachCurrentSyncContext();

        await AsyncService.WriteAsync(CancellationToken.None);
        await WriteNuGetDownloadsAsync(CancellationToken.None);

        try
        {
            await ProcessService.RunAsync("dotnet", "--version", lifetime.CancellationToken);
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("The operation was canceled.");
        }
    }

    private static async Task WriteNuGetDownloadsAsync(CancellationToken cancellationToken)
    {
        string packageId = "Microsoft.Bcl.AsyncInterfaces";
        Task<long> task = NuGetService.GetAsync(packageId, true, cancellationToken);
        long totalDownloads = await task;
        string message = String.Create(CultureInfo.InvariantCulture, $"NuGet package '{packageId}' has {totalDownloads:N0} total downloads");
        Console.WriteLine(message);
    }

    private static void WriteCommandLineArguments(string[] args)
    {
        if (args.Length > 0)
        {
            Console.WriteLine(string.Join(", ", args));
        }
    }
}
