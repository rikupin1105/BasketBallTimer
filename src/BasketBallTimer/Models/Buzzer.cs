namespace BasketBallTimer.Models
{
    public class Buzzer
    {
        private System.Media.SoundPlayer? player = null;
        string SoundFile = "Buzzer.wav";
        public void Play()
        {
            player = new(SoundFile);
            player.Play();
        }
        public void Stop()
        {
            if (player is not null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }
    }
}
