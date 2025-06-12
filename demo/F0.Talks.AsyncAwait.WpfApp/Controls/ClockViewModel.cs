using F0.ComponentModel;
using System.Windows.Threading;

namespace F0.Talks.AsyncAwait.WpfApp.Controls;

internal sealed class ClockViewModel : ViewModel, IDisposable
{
    private readonly DispatcherTimer _timer;

    private string _time;
    public string Time
    {
        get => _time;
        set => SetProperty(ref _time, value);
    }

    public ClockViewModel()
    {
        _time = DateTime.Now.ToLongTimeString();

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(0.5)
        };
        _timer.Tick += OnTimerTick;
        _timer.Start();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        Time = DateTime.Now.ToLongTimeString();
    }

    void IDisposable.Dispose()
    {
        _timer.Stop();
        _timer.Tick -= OnTimerTick;
    }
}
