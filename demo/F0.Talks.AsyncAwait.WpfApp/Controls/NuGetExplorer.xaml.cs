﻿using F0.Talks.AsyncAwait.NuGet.Services;
using F0.Talks.AsyncAwait.WpfApp.Awaitables;
using System.Globalization;

namespace F0.Talks.AsyncAwait.WpfApp.Controls;

public partial class NuGetExplorer : UserControl, IDisposable
{
    private readonly CancellationTokenSource _cts = new();

    public NuGetExplorer()
    {
        InitializeComponent();
    }

    private async void OnGetDownloads(object sender, RoutedEventArgs e)
    {
        string packageId = PackageId.Text;
        Task<long> task = NuGetService.GetAsync(packageId, true, _cts.Token);
        try
        {
            long totalDownloads = await task.ConfigureAwait(false);
            string text = String.Create(CultureInfo.InvariantCulture, $"{totalDownloads:N0}");
            await this;
            TotalDownloads.Text = text;
        }
        catch (OperationCanceledException ex)
        {
            await Dispatcher.BeginInvoke(delegate () { TotalDownloads.Text = ex.Message; });
        }
    }

    private void OnCancel(object sender, RoutedEventArgs e)
    {
        _cts.Cancel();
    }

    void IDisposable.Dispose()
    {
        _cts.Dispose();
    }
}
