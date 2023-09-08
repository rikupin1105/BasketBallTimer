using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Diagnostics;
using System.Windows.Threading;

namespace BasketBallTimer.Models
{
    public class ShotClock : BindableBase, IShotClock
    {
        MyTimer timer;
        Buzzer buzzer;

        public ReactiveProperty<string> SecondsDisplay { get; private set; } = new("24");
        public ReactiveProperty<int> Opacity { get; private set; } = new(1);


        private bool _visible = true;
        private bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;

                if (value) Opacity.Value = 1;
                else Opacity.Value = 0;
            }
        }

        private double _seconds = 24;
        private double Seconds
        {
            get => _seconds;
            set
            {
                _seconds = value;

                if (value <= 0) SecondsDisplay.Value = "0";
                else if (value < 5) SecondsDisplay.Value = (value).ToString("0.0");
                else SecondsDisplay.Value = Math.Ceiling(value).ToString("0");
            }
        }

        private bool _running = false;
        public ShotClock()
        {
            timer = new(dispatcherTimer_Tick);
            timer.Interval = 100;

            buzzer = new();
        }

        private void dispatcherTimer_Tick(object? sender, EventArgs e)
        {
            Seconds -= 0.1;

            if (Seconds <= 0.1)
            {
                buzzer.Play();

                timer.Stop();
                _running = false;
            }

            Debug.WriteLine(DateTime.Now.ToString("ss:ff"));
        }

        public void ResetStart14Down()
        {
            Visible = true;
            Seconds = 14;
            timer.Stop();
        }
        public void ResetStart14Up()
        {
            timer.Start();
            _running = true;
        }
        public void ResetStartDown()
        {
            Seconds = 24;
            timer.Stop();
            Visible = false;
        }
        public void ResetStartUp()
        {
            timer.Start();
            _running = true;
            Visible = true;
        }
        public void ShowHide()
        {
            Visible = !Visible;
        }
        public void StartStop()
        {
            if (_running)
            {
                timer.Stop();
                _running = false;
            }
            else
            {
                timer.Start();
                _running = true;
            }
        }

        public void SecondsEdit(bool Add)
        {
            if (Add)
            {
                if (Seconds < 5)
                {
                    Seconds += 0.1;
                }
                else
                {
                    Seconds += 1;
                    Seconds = Math.Ceiling(Seconds);
                }
            }
            else
            {
                if (Seconds <= 5)
                {
                    Seconds = Math.Max(0, Seconds-0.1);
                }
                else
                {
                    Seconds -= 1;
                    Seconds = Math.Ceiling(Seconds);
                }
            }
        }
    }
}
