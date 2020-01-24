#region Throw
using System;
using System.Threading.Tasks;

namespace Snippets
{
    public static class ThrowDemo
    {
        public static async Task ThrowAsync()
        {
            await Task.Yield();

            throw new InvalidOperationException();
        }
    }
}
#endregion
