using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ProjektWPF.Models
{
    using System;
    using System.Windows.Threading;

    public class PersonalTimer
    {
        private DispatcherTimer timer;
        private int? timeInSeconds;

        public event EventHandler Tick;
        public event EventHandler TimerFinished;

        public PersonalTimer()
        {
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        public int? TimeInSeconds
        {
            get { return timeInSeconds; }
            set
            {
                timeInSeconds = value;
                OnTimeChanged();
            }
        }

        public bool IsRunning => timer.IsEnabled;

        public string FormattedTime
        {
            get
            {
                if (timeInSeconds.HasValue)
                {
                    var ts = TimeSpan.FromSeconds(timeInSeconds.Value);
                    return ts.ToString(@"mm\:ss");
                }
                return "00:00";
            }
        }

        public void Start()
        {
            if (timer != null && !timer.IsEnabled)
            {
                timer.Start();
            }
        }

        public void Stop()
        {
            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }
        }

        public void Toggle()
        {
            if (timer != null)
            {
                if (timer.IsEnabled)
                {
                    Stop();
                }
                else
                {
                    Start();
                }
            }
        }

        public void Restart(int? timeInMinutes)
        {
            Stop();
            TimeInSeconds = timeInMinutes * 60;
           // Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (TimeInSeconds.HasValue && TimeInSeconds.Value > 0)
            {
                TimeInSeconds--;
                Tick?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Stop();
                TimerFinished?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnTimeChanged()
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }
    }

}
