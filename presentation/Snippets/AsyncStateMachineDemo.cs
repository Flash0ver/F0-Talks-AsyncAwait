#region AsyncStateMachine
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Snippets
{
    public static class AsyncStateMachineDemo
    {
        public static async Task MethodAsync(TimeSpan delay, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Hello .NET Community Austria!");

            Console.WriteLine($"Delay: {delay}");
            await Task.Delay(delay, cancellationToken);

            Console.WriteLine("Goodbye .NET Community Austria!");
        }
    }
}
#endregion
