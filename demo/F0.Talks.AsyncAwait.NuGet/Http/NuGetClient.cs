using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace F0.Talks.AsyncAwait.NuGet.Http
{
    public sealed class NuGetClient : IDisposable
    {
        private readonly HttpClient client;

        public NuGetClient()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://azuresearch-usnc.nuget.org/")
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<long> GetAsync(string packageId, bool prerelease, CancellationToken cancellationToken)
        {
            await Task.CompletedTask.ConfigureAwait(false);

            string requestUri = $"query?q=PackageId:{packageId}&prerelease={prerelease}&semVerLevel=2.0.0";
            using HttpResponseMessage response = await client.GetAsync(requestUri).ConfigureAwait(false);
            using Stream jsonStream = await response.Content.ReadAsStreamAsync();

            cancellationToken.ThrowIfCancellationRequested();

            using JsonDocument document = JsonDocument.Parse(jsonStream);

            if (document.RootElement.GetProperty("totalHits").GetInt32() != 1)
            {
                throw new InvalidOperationException($"{nameof(packageId)} '{packageId}' is ambiguous.");
            }

            JsonElement data = document.RootElement.GetProperty("data").EnumerateArray().Single();
            long totalDownloads = data.GetProperty("totalDownloads").GetInt64();

            return totalDownloads;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
