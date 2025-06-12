using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace F0.Talks.AsyncAwait.Awaitables;

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
