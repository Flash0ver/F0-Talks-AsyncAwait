namespace F0.Talks.AsyncAwait.Services;

public static class AsyncService
{
    public static async Task WriteAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Enter");
        var sw = Stopwatch.StartNew();

        await Task.Delay(TimeSpan.FromSeconds(0), cancellationToken);
        Console.WriteLine($"After {sw.Elapsed}");

        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        Console.WriteLine($"After {sw.Elapsed}");

        await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
        Console.WriteLine($"After {sw.Elapsed}");

        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        Console.WriteLine($"Exit after {sw.Elapsed}");
    }

    public static async Task WriteDetailedAsync(CancellationToken cancellationToken)
    {
        WriteThreadInfo("Enter");
        var sw = Stopwatch.StartNew();

        await Task.Delay(TimeSpan.FromSeconds(0), cancellationToken);
        WriteThreadInfo($"After {sw.Elapsed}");

        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        WriteThreadInfo($"After {sw.Elapsed}");

        await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
        WriteThreadInfo($"After {sw.Elapsed}");

        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        WriteThreadInfo($"Exit after {sw.Elapsed}");
    }

    internal static void WriteThreadInfo(string message)
    {
        string text = $"{message} - Thread Id: {Thread.CurrentThread.ManagedThreadId} - {nameof(Thread.CurrentThread.IsBackground)}={Thread.CurrentThread.IsBackground} - {nameof(Thread.CurrentThread.IsThreadPoolThread)}={Thread.CurrentThread.IsThreadPoolThread}";
        Console.WriteLine(text);
    }

    public static async Task<int> GetAsync(int value)
    {
        await Task.Yield();

        return value;
    }
}
