using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace F0.Talks.AsyncAwait.NuGet.Http;

public sealed class NuGetClient : IDisposable
{
    private readonly HttpClient _client;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Ownership of HttpClientHandler is passed to HttpClient")]
    public NuGetClient()
    {
        var handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            CheckCertificateRevocationList = true,
        };

        _client = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://azuresearch-usnc.nuget.org/")
        };
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "TODO")]
    public async Task<long> GetAsync(string packageId, bool prerelease, CancellationToken cancellationToken)
    {
        await Task.CompletedTask.ConfigureAwait(false);

        string requestUri = $"query?q=PackageId:{packageId}&prerelease={prerelease}&semVerLevel=2.0.0";
        using HttpResponseMessage response = await _client.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
        await using Stream jsonStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

        cancellationToken.ThrowIfCancellationRequested();

        using JsonDocument document = await JsonDocument.ParseAsync(jsonStream, default, cancellationToken).ConfigureAwait(false);

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
        _client.Dispose();
    }
}
