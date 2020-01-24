using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snippets
{
    public static class PitfallDemo
    {
        #region Hidden_AsyncVoid
        public static Task AsyncVoid()
        {
            Action operation = () => GetAsync();

            operation();

            return Task.CompletedTask;
        }
        #endregion

        #region UnnecessaryContinuation
        public static async Task ContinuationMethod()
        {
            await GetAsync();
        }

        public static Task ContinuationLambda()
        {
            Func<Task> operation = async () => await GetAsync();

            return operation();
        }
        #endregion

        #region UnnecessaryThreadPool
        public static Task CreateTask()
        {
            var task = Task.Run(() => { });

            return task;
        }
        #endregion

        private static Task GetAsync()
        {
            return Task.CompletedTask;
        }

        private static Task GetAsync(CancellationToken cancellationToken)
        {
            return Task.FromCanceled(cancellationToken);
        }

        #region FakeAsync
        public static async Task FakeAsync()
        {
            await Task.Run(CpuBoundWork);
        }

        private static void CpuBoundWork()
        {
            Thread.Sleep(10_000);
        }
        #endregion
    }
}
