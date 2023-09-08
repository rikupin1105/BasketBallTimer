namespace BasketBallTimer.Models
{
    public interface IShotClock
    {
        void StartStop();
        void ResetStartDown();
        void ResetStartUp();
        void ResetStart14Down();
        void ResetStart14Up();
        void ShowHide();
        void SecondsEdit(bool Add);
    }
}
