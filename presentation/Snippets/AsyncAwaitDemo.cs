using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Snippets
{
    public static class AsyncAwaitDemo
    {
        public static Task Method1Async()
        {
            using var httpClient = new HttpClient();
            #region Async
            Func<Task> asyncOperation = async () =>
            {
                Task<string> async = httpClient.GetStringAsync("https://www.meetup.com/dotnet-austria/events/263414974/");
                await async;
            };
            #endregion
            return asyncOperation();
        }

        public static Task Method2Async()
        {
            using var httpClient = new HttpClient();
            #region Await
            Func<Task> asyncOperation = async () =>
            {
                Task<string> async = httpClient.GetStringAsync("https://www.meetup.com/dotnet-austria/events/263414974/");
                await async;
            };
            #endregion
            return asyncOperation();
        }
    }
}
