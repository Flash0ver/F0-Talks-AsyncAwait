using F0.ComponentModel;
using System;
using System.Windows.Threading;

namespace F0.Talks.AsyncAwait.WpfApp.Controls
{
    internal sealed class ClockViewModel : ViewModel, IDisposable
    {
        private readonly DispatcherTimer timer;

        private string time;
        public string Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        public ClockViewModel()
        {
            time = DateTime.Now.ToLongTimeString();

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.5)
            };
            timer.Tick += OnTimerTick;
            timer.Start();
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            Time = DateTime.Now.ToLongTimeString();
        }

        void IDisposable.Dispose()
        {
            timer.Stop();
            timer.Tick -= OnTimerTick;
        }
    }
}
