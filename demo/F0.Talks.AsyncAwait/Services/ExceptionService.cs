namespace F0.Talks.AsyncAwait.Services;

public static class ExceptionService
{
    public static Task ThrowImmediately()
    {
        throw new InvalidOperationException(nameof(ThrowImmediately));
    }

    public static async Task ThrowAsync()
    {
        await Task.Yield();

        throw new InvalidOperationException(nameof(ThrowAsync));
    }
}
