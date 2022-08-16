using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Snippets
{
    public static class TplDemo
    {
        #region Task_Void
        public static Task Get_Task_Void()
        {
            var task = Task.Run(() => { Thread.Sleep(1_000); });

            if (task.IsCompletedSuccessfully)
            {
                Console.WriteLine(task.Status == TaskStatus.RanToCompletion);
            }

            if (task.IsCanceled)
            {
                Console.WriteLine(task.Status == TaskStatus.Canceled);
            }

            if (task.IsFaulted)
            {
                Console.WriteLine(task.Status == TaskStatus.Faulted);
                Console.WriteLine(task.Exception);
            }

            return task;
        }
        #endregion

        #region Task_TResult
        public static Task<int> Get_Task_TResult()
        {
            var task = Task.Run(() => { Thread.Sleep(1_000); return 263_414_974; });

            if (task.IsCompletedSuccessfully)
            {
                Console.WriteLine(task.Result);
            }

            if (task.IsFaulted)
            {
                Console.WriteLine(task.Exception!.InnerExceptions.Select(e => e.Message));
            }

            return task;
        }
        #endregion

        #region ValueTask_Void
        public static async ValueTask Get_ValueTask_Void()
        {
            // warning: may be awaited more than just once
            ValueTask valueTask = AsyncEnumerable.Range(0, 10).GetAsyncEnumerator().DisposeAsync();

            // OK, if only consumed once
            await valueTask;

            // OK, if only consumed once
            await valueTask.ConfigureAwait(false);

            // OK, if only consumed once
            // a Task will never transition from a complete to an incomplete state
            Task task = valueTask.AsTask();
            await task;
            await task;
            await task;
        }
        #endregion

        #region ValueTask_TResult
        public static async ValueTask<bool> Get_ValueTask_TResult()
        {
            ValueTask<bool> valueTask = AsyncEnumerable.Range(0, 10).GetAsyncEnumerator().MoveNextAsync();

            // error: consumed more than just once
            bool result1 = await valueTask;
            bool result2 = await valueTask;

            // error: consumed concurrently
            _ = Task.Run(async () => await valueTask);
            _ = Task.Run(async () => await valueTask);

            // error: IValueTaskSource / IValueTaskSource<TResult> implementations do not need to support blocking until completion
            // that operation may be a race condition
            bool result = valueTask.GetAwaiter().GetResult();

            // OK, if only consumed once
            if (valueTask.IsCompletedSuccessfully)
            {
                result = valueTask.Result;
            }

            return result;
        }
        #endregion

        #region AsyncStream
        public static async IAsyncEnumerable<string> Get_AsyncStream([EnumeratorCancellation] CancellationToken token = default)
        {
            var stopwatch = Stopwatch.StartNew();
            await foreach (int item in GenerateSequence(token))
            {
                yield return $"Got {item} after {stopwatch.Elapsed}";
            }
        }

        private static async IAsyncEnumerable<int> GenerateSequence([EnumeratorCancellation] CancellationToken token = default)
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(100), token);
                yield return i;
            }
        }
        #endregion

        #region Awaiter
        public static async Task Get_Awaiter()
        {
            //Console.WriteLine(DateTimeOffset.Now);
            //await TimeSpan.FromSeconds(30);
            //Console.WriteLine(DateTimeOffset.Now);
        }
        #endregion

        #region CancellationTokenSource
        public static void Get_CancellationTokenSource()
        {
            var cts = new CancellationTokenSource();

            cts.Cancel();
            cts.CancelAfter(TimeSpan.FromSeconds(10));
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            CancellationToken token = cts.Token;
        }
        #endregion

        #region TaskCompletionSource
        public static Task Get_TaskCompletionSource()
        {
            var cts = new TaskCompletionSource<int>();

            return cts.Task;
        }
        #endregion
    }

    //public static class Extensions
    //{
    //    public static TaskAwaiter GetAwaiter(this TimeSpan timeSpan)
    //    {
    //        return Task.Delay(timeSpan).GetAwaiter();
    //    }
    //}
}
