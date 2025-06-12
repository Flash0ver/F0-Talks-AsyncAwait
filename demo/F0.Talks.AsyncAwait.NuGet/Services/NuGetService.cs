using F0.Talks.AsyncAwait.NuGet.Http;
using System.Threading;
using System.Threading.Tasks;

namespace F0.Talks.AsyncAwait.NuGet.Services;

public static class NuGetService
{
    public static async Task<long> GetAsync(string packageId, bool prerelease, CancellationToken cancellationToken)
    {
        using var client = new NuGetClient();

        long downloads = await client.GetAsync(packageId, prerelease, cancellationToken);

        return downloads;
    }
}
