#region AsyncStateMachine
using System;
using System.Threading.Tasks;

namespace Snippets
{
    public static class AsyncStateMachineDemo
    {
        public static async Task MethodAsync()
        {
            Console.WriteLine("Hello .NET Community Austria!");
            await Task.Delay(TimeSpan.FromSeconds(2));
            Console.WriteLine("Goodbye .NET Community Austria!");
        }
    }
}
#endregion
