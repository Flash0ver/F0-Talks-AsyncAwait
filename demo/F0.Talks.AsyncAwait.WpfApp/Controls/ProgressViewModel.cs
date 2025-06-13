using F0.ComponentModel;
using F0.Windows.Input;

namespace F0.Talks.AsyncAwait.WpfApp.Controls;

internal sealed class ProgressViewModel : ViewModel
{
    private int _maximum;
    public int Maximum
    {
        get => _maximum;
        set => SetProperty(ref _maximum, value);
    }

    private int _progress;
    public int Progress
    {
        get => _progress;
        set => SetProperty(ref _progress, value);
    }

    private CancellationTokenSource? _cts;
    private CancellationTokenSource? CTS
    {
        get => _cts;
        set
        {
            if (TrySetProperty(ref _cts, value))
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
            await ProcessAsyncSequence(CreateAsyncSequence(), CTS.Token, reporter).ConfigureAwait(true);
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

        await foreach (int item in asyncSequence.WithCancellation(cancellationToken))
        {
            current++;
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken).ConfigureAwait(true);
            progress.Report(current);
        }
    }
}
