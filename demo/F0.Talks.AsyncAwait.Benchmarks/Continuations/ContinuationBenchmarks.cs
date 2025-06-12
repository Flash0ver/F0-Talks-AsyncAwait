using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using System.Threading.Tasks;

namespace F0.Talks.AsyncAwait.Benchmarks.Continuations;

[MemoryDiagnoser]
[Config(typeof(ContinuationConfig))]
public class ContinuationBenchmarks
{
    [Benchmark]
    public Task<int> CompletedTask_Synchronous()
    {
        int number = 42;

        return GenerateSynchronously(number);
    }

    [Benchmark]
    public async Task<int> CompletedTask_UnnecessaryAsync()
    {
        int number = 42;

        return await GenerateSynchronously(number);
    }

    [Benchmark]
    public Task<int> IncompleteTask_Synchronous()
    {
        int number = 42;

        return GenerateAsynchronously(number);
    }

    [Benchmark]
    public async Task<int> IncompleteTask_UnnecessaryAsync()
    {
        int number = 42;

        return await GenerateAsynchronously(number);
    }

    private static Task<int> GenerateSynchronously(int number)
    {
        return Task.FromResult(number);
    }

    private static async Task<int> GenerateAsynchronously(int number)
    {
        await Task.Yield();
        return number;
    }

    private class ContinuationConfig : ManualConfig
    {
        public ContinuationConfig()
        {
            AddColumn(new TagColumn("Scenario", name => name.Split('_')[0]));
            AddColumn(new TagColumn("Usage", name => name.Split('_')[1]));
        }
    }
}
