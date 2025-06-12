using System.Runtime.CompilerServices;

namespace F0.Talks.AsyncAwait.WpfApp.Awaitables;

public struct ControlAwaiter : INotifyCompletion
{
    private readonly Control _control;

    public ControlAwaiter(Control control)
    {
        _control = control;
    }

    public bool IsCompleted => _control.Dispatcher.CheckAccess();

    public void OnCompleted(Action continuation)
    {
        _control.Dispatcher.Invoke(continuation);
    }

    public void GetResult()
    {
        //no-op
    }
}
