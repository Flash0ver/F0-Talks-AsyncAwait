using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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

        private static async Task WriteAsync(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            await Task.Yield();

            Console.WriteLine($"ToString: {obj}");
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

        #region Exception_Asynchronous
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
        #endregion

        #region Exception_Synchronous
        public static async Task FailSync()
        {
            Task<int> task = GetRandomNumberAsync(new HttpClient());

            Console.WriteLine($"{nameof(task.Status)}: {task.Status}");
            int number = await task;

            Console.WriteLine(number);
        }

        private static async Task<int> GetRandomNumberAsync(HttpClient client)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            string json = await client.GetStringAsync("http://www.randomnumberapi.com/api/v1.0/random?min=0&max=9&count=1");

            using JsonDocument document = JsonDocument.Parse(json);
            JsonElement root = document.RootElement;
            JsonElement number = root.EnumerateArray().Single();
            return number.GetInt32();
        }
        #endregion
    }
}
