using F0.ComponentModel;
using F0.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace F0.Talks.AsyncAwait.WpfApp.Controls
{
    internal sealed class ProgressViewModel : ViewModel
    {
        private int maximum;
        public int Maximum
        {
            get => maximum;
            set => SetProperty(ref maximum, value);
        }

        private int progress;
        public int Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        private CancellationTokenSource? cts;
        private CancellationTokenSource? CTS
        {
            get => cts;
            set
            {
                if (TrySetProperty(ref cts, value))
                {
                    StartCommand.RaiseCanExecuteChanged();
                    CancelCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public IAsyncCommand StartCommand { get; }
        public IInputCommand CancelCommand { get; }

        public ProgressViewModel()
        {
            StartCommand = Command.Create(OnStart, CanStart);
            CancelCommand = Command.Create(OnCancel, CanCancel);
        }

        private async Task OnStart()
        {
            Progress = 0;
            CTS = new CancellationTokenSource();
            IProgress<int> reporter = new Progress<int>(p => Progress = p);
            try
            {
                await ProcessAsyncSequence(CreateAsyncSequence(), CTS.Token, reporter);
            }
            catch (TaskCanceledException)
            {
                reporter.Report(0);
            }
            CTS = null;
        }

        private bool CanStart()
        {
            return CTS == null;
        }

        private void OnCancel()
        {
            CTS!.Cancel();
        }

        private bool CanCancel()
        {
            return CTS != null;
        }

        private IAsyncEnumerable<int> CreateAsyncSequence()
        {
            Maximum = 5;
            return AsyncEnumerable.Range(0, 5);
        }

        private static async Task ProcessAsyncSequence(IAsyncEnumerable<int> asyncSequence, CancellationToken cancellationToken, IProgress<int> progress)
        {
            int current = 0;

            await foreach (int item in asyncSequence)
            {
                current++;
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                progress.Report(current);
            }
        }
    }
}
