using System;
using System.Threading.Tasks;

namespace Snippets
{
    public static class ProgressDemo
    {
        #region Progress
        public static async Task ReportAsync()
        {
            IProgress<float> progress = new Progress<float>(p => Console.WriteLine($"{p:P}"));
            await ProcessAsync(10, progress);
        }

        public static async Task ProcessAsync(int length, IProgress<float> progress)
        {
            for (int i = 0; i < length; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(0.5));
                progress.Report((i + 1.0f) / length);
            }
        }
        #endregion
    }
}
