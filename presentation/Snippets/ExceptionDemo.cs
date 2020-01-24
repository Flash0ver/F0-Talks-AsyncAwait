using System;
using System.Threading.Tasks;

namespace Snippets
{
    public static class ExceptionDemo
    {
        #region Exception_Setup
        private static Task FromException()
        {
            return Task.FromException(new InvalidOperationException(nameof(FromException)));
        }

        private static async Task ThrowAsync()
        {
            await Task.Yield();

            throw new InvalidOperationException(nameof(ThrowAsync));
        }

        private static Task ThrowImmediately()
        {
            throw new InvalidOperationException(nameof(ThrowImmediately));
        }

        private static void WriteTaskInfo(Task task)
        {
            Console.WriteLine("---");
            Console.WriteLine($"{nameof(Task.IsCompletedSuccessfully)}={task.IsCompletedSuccessfully} | {nameof(Task.IsCompleted)}={task.IsCompleted}");
            if (task.IsFaulted)
            {
                foreach (Exception ex in task.Exception!.InnerExceptions)
                {
                    Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                }
            }
        }
        #endregion

        #region Exception_Code
        public static Task FailAsync()
        {
            Task task = ThrowAsync();

            try
            {
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Observed at least one exception!");
                Console.WriteLine($"{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Observed exception:");
                Console.WriteLine($"{ex.Message}");
            }

            WriteTaskInfo(task);
            return Task.CompletedTask;
        }

        private static async Task WriteAsync(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            await Task.Yield();

            Console.WriteLine($"ToString: {obj.ToString()}");
        }
        #endregion
    }
}
