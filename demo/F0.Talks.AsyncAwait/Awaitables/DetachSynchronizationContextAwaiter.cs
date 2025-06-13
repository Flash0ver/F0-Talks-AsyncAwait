using System.Runtime.CompilerServices;

namespace F0.Talks.AsyncAwait.Awaitables;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "Demo")]
public struct DetachSynchronizationContextAwaiter : ICriticalNotifyCompletion
{
    public bool IsCompleted => SynchronizationContext.Current == null;

    public void OnCompleted(Action continuation)
    {
        _ = ThreadPool.QueueUserWorkItem((object? state) => continuation(), null);
    }

    public void UnsafeOnCompleted(Action continuation)
    {
        _ = ThreadPool.UnsafeQueueUserWorkItem((object? state) => continuation(), null);
    }

    public void GetResult()
    {
        //no-op
    }

    public DetachSynchronizationContextAwaiter GetAwaiter()
    {
        return this;
    }
}
