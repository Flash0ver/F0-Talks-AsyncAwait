using System.Runtime.CompilerServices;

namespace F0.Talks.AsyncAwait.Awaitables;

public static class AwaiterExtensions
{
    public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
    {
        return Task.Delay(timeSpan).GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this int milliseconds)
    {
        return Task.Delay(TimeSpan.FromMilliseconds(milliseconds)).GetAwaiter();
    }

    public static TaskAwaiter GetAwaiter(this IEnumerable<Task> tasks)
    {
        return Task.WhenAll(tasks).GetAwaiter();
    }
}
