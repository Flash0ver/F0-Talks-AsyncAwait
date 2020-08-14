using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System.Diagnostics;

namespace F0.Talks.AsyncAwait.Benchmarks
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            IConfig? config = Debugger.IsAttached
                ? new DebugInProcessConfig()
                : DefaultConfig.Instance;

            _ = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
        }
    }
}
