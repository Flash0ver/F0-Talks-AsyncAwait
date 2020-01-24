using System;
using System.Threading;

namespace Snippets
{
    public static class Logger
    {
        public static void WriteThreadInfo()
        {
            string text = $"Current Thread Id: {Thread.CurrentThread.ManagedThreadId} - {nameof(Thread.CurrentThread.IsBackground)}={Thread.CurrentThread.IsBackground} - {nameof(Thread.CurrentThread.IsThreadPoolThread)}={Thread.CurrentThread.IsThreadPoolThread}";
            Console.WriteLine(text);
        }
    }
}
