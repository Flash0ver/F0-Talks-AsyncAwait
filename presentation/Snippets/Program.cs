using System.Threading.Tasks;

namespace Snippets
{
    internal static class Program
    {
        internal static async Task<int> Main(
            string? region = null,
            string? session = null,
            string? package = null,
            string? project = null,
            string[]? args = null)
        {
            //region = "Progress";

            Task task = region switch
            {
                "ControlFlow" => TapDemo.WriteTitleAsync(),
                "AsyncStateMachine" => AsyncStateMachineDemo.MethodAsync(),
                "Throw" => ThrowDemo.ThrowAsync(),
                "AsyncLock" => new AsyncLockDemo().AccessSharedResourceAsync(),
                "Cancellation_Operation" => CancellationDemo.Caller(),
                "Cancellation_AsyncStream1" => CancellationDemo.Consumer1().AsTask(),
                "Cancellation_AsyncStream2" => CancellationDemo.Consumer2().AsTask(),
                "ContinueWith" => ContinuationDemo.ContinueWith(),
                "AwaitTheOperation" => ContinuationDemo.AwaitTheOperation(),
                "AwaitTheResult" => ContinuationDemo.AwaitTheResult(),
                "Combinators_All" => ContinuationDemo.Combinators_All(),
                "Combinators_Any" => ContinuationDemo.Combinators_Any(),
                "Progress" => ProgressDemo.ReportAsync(),
                "SynchronizationContext" => TapDemo.RunAsync(),
                "Hidden_AsyncVoid" => PitfallDemo.AsyncVoid(),
                "UnnecessaryContinuation" => PitfallDemo.ContinuationMethod(),
                "UnnecessaryThreadPool" => PitfallDemo.CreateTask(),
                "Exception_Code" => ExceptionDemo.FailAsync(),

                "Task_Void" => TplDemo.Get_Task_Void(),
                "Task_TResult" => TplDemo.Get_Task_TResult(),
                "ValueTask_Void" => TplDemo.Get_ValueTask_Void().AsTask(),
                "ValueTask_TResult" => TplDemo.Get_ValueTask_TResult().AsTask(),
                "AsyncStream" => TplDemo.Get_AsyncStream().GetAsyncEnumerator().MoveNextAsync().AsTask(),
                "Awaiter" => TplDemo.Get_Awaiter(),

                null => Task.CompletedTask,
                _ => Task.CompletedTask
            };

            //try
            //{
            //    await task;
            //}
            //catch (TaskCanceledException ex)
            //{
            //    Console.WriteLine("> Task canceled:");
            //    Console.WriteLine(ex.Message);
            //}
            //catch (OperationCanceledException ex)
            //{
            //    Console.WriteLine("> Operation canceled:");
            //    Console.WriteLine(ex.Message);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("> Operation faulted:");
            //    Console.WriteLine(ex.Message);
            //}

            await task;

            return 0;
        }
    }
}
