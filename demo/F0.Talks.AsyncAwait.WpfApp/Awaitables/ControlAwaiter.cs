using System;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace F0.Talks.AsyncAwait.WpfApp.Awaitables
{
    public struct ControlAwaiter : INotifyCompletion
    {
        private readonly Control control;

        public ControlAwaiter(Control control)
        {
            this.control = control;
        }

        public bool IsCompleted => control.Dispatcher.CheckAccess();

        public void OnCompleted(Action continuation)
        {
            control.Dispatcher.Invoke(continuation);
        }

        public void GetResult()
        {
            //no-op
        }
    }
}
