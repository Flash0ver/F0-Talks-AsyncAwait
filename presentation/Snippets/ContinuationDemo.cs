﻿using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snippets
{
    public static class ContinuationDemo
    {
        #region ContinueWith
        public static Task ContinueWith()
        {
            Task<string> task = GetTitleAsync();
            Task continuation = task.ContinueWith(antecedent => Console.WriteLine($"{antecedent.Result}"));
            return continuation;
        }
        #endregion

        #region AwaitTheOperation
        public static async Task AwaitTheOperation()
        {
            Task<string> task = GetTitleAsync();
            Console.WriteLine($"{task.IsCompleted}");
            await task;
            Console.WriteLine($"{task.IsCompleted}");
            Console.WriteLine($"{task.Result}");
        }
        #endregion

        #region AwaitTheResult
        public static async Task AwaitTheResult()
        {
            string title = await GetTitleAsync();
            Console.WriteLine(title);
        }
        #endregion

        #region Combinators_All
        public static async Task Combinators_All()
        {
            Task<string> groupTask = GetTitleAsync("https://www.meetup.com/dotnet-austria/");
            Task<string> eventTask = GetTitleAsync("https://www.meetup.com/dotnet-austria/events/263414974/");

            Task<string[]> completion = Task.WhenAll(groupTask, eventTask);
            Console.WriteLine(completion.IsCompleted);
            string[] result = await completion;
            Console.WriteLine($"{groupTask.IsCompleted}; {eventTask.IsCompleted}");

            Console.WriteLine(string.Join(Environment.NewLine, result));

        }
        #endregion

        #region Combinators_Any
        public static async Task Combinators_Any()
        {
            Task<string> groupTask = GetTitleAsync("https://www.meetup.com/dotnet-austria/");
            Task<string> eventTask = GetTitleAsync("https://www.meetup.com/dotnet-austria/events/263414974/");

            Task<Task<string>> one = Task.WhenAny(groupTask, eventTask);
            string result = await one.Unwrap();

            Console.WriteLine(result);
        }
        #endregion

        private static Task<string> GetTitleAsync()
        {
            return GetTitleAsync("https://www.meetup.com/dotnet-austria/events/263414974/", TimeSpan.Zero);
        }

        private static Task<string> GetTitleAsync(string address)
        {
            return GetTitleAsync(address, TimeSpan.Zero);
        }

        private static Task<string> GetTitleAsync(string address, int delay)
        {
            return GetTitleAsync(address, TimeSpan.FromMilliseconds(delay));
        }

        private static async Task<string> GetTitleAsync(string address, TimeSpan delay)
        {
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument document = await context.OpenAsync(address);

            await Task.Delay(delay);
            return document.Title;
        }

        private static void WriteThreadInfo()
        {
            string text = $"Current Thread Id: {Thread.CurrentThread.ManagedThreadId} - {nameof(Thread.CurrentThread.IsBackground)}={Thread.CurrentThread.IsBackground} - {nameof(Thread.CurrentThread.IsThreadPoolThread)}={Thread.CurrentThread.IsThreadPoolThread}";
            Console.WriteLine(text);
        }
    }
}
