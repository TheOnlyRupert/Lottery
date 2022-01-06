using System;
using System.Diagnostics;
using System.Timers;

namespace Lottery.Source {
    public class CountDownTimer : IDisposable {
        private readonly Stopwatch _stpWatch = new Stopwatch();
        private readonly Timer timer = new Timer();
        private TimeSpan _max = TimeSpan.FromMilliseconds(30000);
        public Action CountDownFinished;
        public Action TimeChanged;

        public CountDownTimer(int min, int sec) {
            SetTime(min, sec);
            StepMs = 1000;
        }

        public bool IsRunning => timer.Enabled;

        public int StepMs {
            get => (int)timer.Interval;
            set => timer.Interval = value;
        }

        public TimeSpan TimeLeft => _max.TotalMilliseconds - _stpWatch.ElapsedMilliseconds > 0
            ? TimeSpan.FromMilliseconds(_max.TotalMilliseconds - _stpWatch.ElapsedMilliseconds)
            : TimeSpan.FromMilliseconds(0);

        private bool _mustStop => _max.TotalMilliseconds - _stpWatch.ElapsedMilliseconds < 0;

        public string TimeLeftStr => TimeLeft.ToString(@"\mm\:ss");

        public string TimeLeftMsStr => TimeLeft.ToString(@"mm\:ss\.fff");

        public void Dispose() {
            timer.Dispose();
        }

        private void TimerTick(object sender, EventArgs e) {
            TimeChanged?.Invoke();

            if (_mustStop) {
                CountDownFinished?.Invoke();
                _stpWatch.Stop();
                timer.Enabled = false;
            }
        }

        public void SetTime(TimeSpan ts) {
            _max = ts;
            TimeChanged?.Invoke();
        }

        public void SetTime(int min, int sec = 0) {
            SetTime(TimeSpan.FromSeconds(min * 60 + sec));
        }

        public void Start() {
            timer.Start();
            _stpWatch.Start();
        }

        public void Pause() {
            timer.Stop();
            _stpWatch.Stop();
        }

        public void Stop() {
            Reset();
            Pause();
        }

        public void Reset() {
            _stpWatch.Reset();
        }

        public void Restart() {
            _stpWatch.Reset();
            timer.Start();
        }
    }
}