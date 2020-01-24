using F0.Talks.AsyncAwait.NuGet.Http;
using System.Threading;
using System.Threading.Tasks;

namespace F0.Talks.AsyncAwait.NuGet.Services
{
    public static class NuGetService
    {
        public static async Task<int> GetAsync(string packageId, bool prerelease, CancellationToken cancellationToken)
        {
            using var client = new NuGetClient();

            int downloads = await client.GetAsync(packageId, prerelease, cancellationToken);

            return downloads;
        }
    }
}
