using System;

namespace BasketBallTimer.Models
{
    public class MyTimer
    {
        private System.Timers.Timer timer;
        private long lastTime;

        public double Interval;
        public event System.Timers.ElapsedEventHandler Elapsed;

        public MyTimer(System.Timers.ElapsedEventHandler elapsed)
        {
            Elapsed = elapsed;

            timer = new System.Timers.Timer();
            timer.AutoReset = true;
            timer.Interval = 1;
            timer.Elapsed += (o, e) =>
            {
                if ((DateTime.Now.Ticks / 10000 - lastTime) >= Interval)
                {
                    System.Threading.ThreadPool.QueueUserWorkItem((state) => Elapsed(o, e));
                    lastTime += (Int64)Interval;
                }
            };
        }

        public void Start()
        {
            lastTime = DateTime.Now.Ticks / 10000;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }
    }
}
