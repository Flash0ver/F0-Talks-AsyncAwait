using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Snippets
{
    public static class CancellationDemo
    {
        #region Cancellation_Operation
        public static async Task Caller()
        {
            using var cts = new CancellationTokenSource();

            Task task = Callee();
            //cts.Cancel();

            try
            {
                await task;
                Console.WriteLine("Operation completed successfully.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Operation was canceled.");
            }

            Console.WriteLine($"{nameof(Task.IsCompleted)}={task.IsCompleted} | {nameof(Task.IsCompletedSuccessfully)}={task.IsCompletedSuccessfully} | {nameof(Task.IsCanceled)}={task.IsCanceled}");
        }

        public static async Task Callee()
        {
            Console.WriteLine("noncancelable");
            await ReadDataAsync("first");
            await ReadDataAsync("second");
            await ReadDataAsync("third");
        }

        public static async Task Callee(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await ReadDataAsync("first");

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await ReadDataAsync("second");
        }
        #endregion

        #region Cancellation_AsyncStream1
        public static async ValueTask Consumer1()
        {
            using var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(3));

            await foreach (int item in Producer(cts.Token))
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static async IAsyncEnumerable<int> Producer([EnumeratorCancellation] CancellationToken token = default)
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), token);
                yield return i;
            }
        }
        #endregion

        #region Cancellation_AsyncStream2
        public static async ValueTask Consumer2()
        {
            IAsyncEnumerable<int> asyncStream = AsyncEnumerable.Range(0, 10);
            await Consumer(asyncStream);
        }

        public static async ValueTask Consumer<T>(IAsyncEnumerable<T> asyncStream)
            //where T : notnull
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));

            await foreach (T item in asyncStream.WithCancellation(cts.Token))
            {
                Console.WriteLine(item!.ToString());
            }
        }
        #endregion

        private static async Task ReadDataAsync(string text)
        {
            await Task.Yield();

            Console.WriteLine($"{nameof(ReadDataAsync)}({text}) completed.");
        }
    }
}
