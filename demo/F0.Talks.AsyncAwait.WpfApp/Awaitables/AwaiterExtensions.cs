namespace F0.Talks.AsyncAwait.WpfApp.Awaitables;

internal static class AwaiterExtensions
{
    public static ControlAwaiter GetAwaiter(this Control control)
    {
        return new ControlAwaiter(control);
    }
}
