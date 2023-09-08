using BasketBallTimer.Models;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Windows.Input;

namespace BasketBallTimer.ViewModels
{

    public class ControllerViewModel : BindableBase
    {
        public ReactiveCommand StartStopCommand { get; set; } = new();
        public ReactiveCommand ResetDownCommand { get; set; } = new();
        public ReactiveCommand ResetUpCommand { get; set; } = new();
        public ReactiveCommand ResetDown14Command { get; set; } = new();
        public ReactiveCommand ResetUp14Command { get; set; } = new();
        public ReactiveCommand ShowHiddenCommand { get; set; } = new();
        public ReactiveCommand KeyDownCommand { get; set; } = new();
        public ReactiveCommand KeyUpCommand { get; set; } = new();
        public ReactiveCommand SecondEditCommand { get; set; } = new();

        private void KeyDown(KeyEventArgs? x)
        {
            if (x == null) return;

            if (x.Key == Key.J)
                ShotClock.ResetStartDown();
            if (x.Key==Key.K)
                ShotClock.ResetStart14Down();
        }
        private void KeyUp(KeyEventArgs? x)
        {
            if (x == null) return;

            if (x.Key == Key.J)
                ShotClock.ResetStartUp();
            if (x.Key == Key.K)
                ShotClock.ResetStart14Up();
        }

        private ShotClock ShotClock { get; set; }
        public ControllerViewModel()
        {
            ShotClock = new ShotClock();


            StartStopCommand.Subscribe(_ => ShotClock.StartStop());

            ResetDownCommand.Subscribe(_ => ShotClock.ResetStartDown());
            ResetUpCommand.Subscribe(_ => ShotClock.ResetStartUp());
            ResetDown14Command.Subscribe(_ => ShotClock.ResetStart14Down());
            ResetUp14Command.Subscribe(_ => ShotClock.ResetStart14Up());

            ShowHiddenCommand.Subscribe(_ => ShotClock.ShowHide());

            KeyDownCommand.Subscribe(x => KeyDown(x as KeyEventArgs));
            KeyUpCommand.Subscribe(x => KeyUp(x as KeyEventArgs));

            SecondEditCommand.Subscribe(b => ShotClock.SecondsEdit((bool)b!));

            Opacity = ShotClock.ObserveProperty(x => x.Opacity.Value).ToReactiveProperty();
            ShotClockDisplay = ShotClock.ObserveProperty(x => x.SecondsDisplay.Value).ToReactiveProperty();
        }

        public ReactiveProperty<string?> ShotClockDisplay { get; set; }
        public ReactiveProperty<int> Opacity { get; set; }
    }
}
