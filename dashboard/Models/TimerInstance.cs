using System.Timers;
using Microsoft.EntityFrameworkCore;

namespace dashboard.Models
{
    public class TimerInstance
    {
        private Timer _timer;
        internal string _key = null;
        internal string _url = null;
        public int RefreshRate { get; set; } = 60;
        public virtual void RefreshData() { }

        public void timer_elapsed(object sender, ElapsedEventArgs e)
        {
            RefreshData();
        }

        private void init_timer()
        {
            _timer = new Timer(RefreshRate * 1000);
            _timer.AutoReset = true;
            _timer.Elapsed += new ElapsedEventHandler(timer_elapsed);
            _timer.Start();
        }

        public TimerInstance()
        {
            init_timer();
        }

        public TimerInstance(string key, string url)
        {
            _key = key;
            _url = url;
        }
    }
}