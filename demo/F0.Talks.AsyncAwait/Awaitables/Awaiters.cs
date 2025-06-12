namespace F0.Talks.AsyncAwait.Awaitables;

public static class Awaiters
{
    public static DetachSynchronizationContextAwaiter DetachCurrentSyncContext()
    {
        return new DetachSynchronizationContextAwaiter();
    }
}
