using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Snippets
{
    public class AsyncLockDemo
    {
        #region AsyncLock
        private readonly SemaphoreSlim mutex = new SemaphoreSlim(1);
        private string resource = "Text";

        public async Task AccessSharedResourceAsync()
        {
            IEnumerable<Task> tasks = Enumerable.Range(0, 10)
                .Select(i => ProcessAsync(i));

            await Task.WhenAll(tasks);
        }

        public async Task ProcessAsync(int value)
        {
            resource = $"Text-{value}";

            await Task.Yield();

            Console.WriteLine(resource);
        }
        #endregion
    }
}
