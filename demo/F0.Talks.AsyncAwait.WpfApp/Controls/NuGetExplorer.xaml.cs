﻿using F0.Talks.AsyncAwait.Awaitables;
using F0.Talks.AsyncAwait.NuGet.Services;
using F0.Talks.AsyncAwait.WpfApp.Awaitables;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace F0.Talks.AsyncAwait.WpfApp.Controls
{
    public partial class NuGetExplorer : UserControl
    {
        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        public NuGetExplorer()
        {
            InitializeComponent();
        }

        private async void OnGetDownloads(object sender, RoutedEventArgs e)
        {
            string packageId = PackageId.Text;
            Task<int> task = NuGetService.GetAsync(packageId, true, cts.Token);
            try
            {
                int totalDownloads = await task.ConfigureAwait(false);
                await this;
                TotalDownloads.Text = $"{totalDownloads}";
            }
            catch (OperationCanceledException ex)
            {
                await Dispatcher.BeginInvoke((Action)(() => TotalDownloads.Text = ex.Message));
            }

        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}
