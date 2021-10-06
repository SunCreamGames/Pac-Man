using System.Timers;

namespace Model.PacMan
{
    public class MyTimer
    {
        private Timer timer;
        private int miliSecs;

        public MyTimer()
        {
            miliSecs = 0;
            timer = new Timer(1) {AutoReset = true};
        }

        private void OnMilisecondPast(object sender, ElapsedEventArgs e)
        {
            miliSecs++;
        }

        public void Start()
        {
            miliSecs = 0;
            timer.Elapsed += OnMilisecondPast;
            timer.Enabled = true;
            timer.Start();
        }

        public int End()
        {
            timer.Stop();
            timer.Elapsed -= OnMilisecondPast;
            return miliSecs;

        }
    }
}