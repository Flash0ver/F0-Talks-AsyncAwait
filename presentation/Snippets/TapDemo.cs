using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Snippets
{
    public static class TapDemo
    {
        #region ControlFlow
        public static async Task WriteTitleAsync()
        {
            TraceMessage("Start synchronously");
            Task<string> operation = GetTitleAsync("https://www.meetup.com/dotnet-austria/events/263414974/");

            TraceMessage("Awaiting operation ...");
            string title = await operation;

            TraceMessage($"Title: '{title}'");
        }

        private static async Task<string> GetTitleAsync(string address)
        {
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            IBrowsingContext context = BrowsingContext.New(config);

            TraceMessage("Starting web request");
            Task<IDocument> operation = context.OpenAsync(address);

            TraceMessage("Awaiting web request ...");
            IDocument document = await operation;

            TraceMessage("... completed web request");
            return document.Title;
        }
        #endregion

        #region SynchronizationContext
        public static async Task RunAsync()
        {
            Task<string> task = GetDataAsync();
            Console.WriteLine("Retrieving data ...");

            string data = await task;

            Console.WriteLine("Processing data ...");
            await ProcessDataAsync(data);

            Console.WriteLine("Done");
        }
        #endregion

        private static void TraceMessage(string message, [CallerMemberName] string memberName = "")
        {
            Console.WriteLine($"{memberName}: {message}");
        }

        private static Task<string> GetDataAsync()
        {
            return Task.FromResult("Some data from a remote resource.");
        }

        private static Task ProcessDataAsync(string data)
        {
            Console.WriteLine(data);
            return Task.CompletedTask;
        }
    }
}
