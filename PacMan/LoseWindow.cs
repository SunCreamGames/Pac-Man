using System.Windows.Forms;

namespace Pac_Man
{
    using System;

    public partial class LoseWindow : Form
    {
        private Form1 failedLevel;

        public LoseWindow(Form1 f)
        {
            InitializeComponent();
            failedLevel = f;
            f.Enabled = false;
        }


        private void LoseWindow_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F)
            {
                Hide();
                var levelWindow = new Form1();
                levelWindow.Show();
                levelWindow.Closed += (s, args) => { Close(); };
            }

            if (e.KeyCode == Keys.R)
            {
                Hide();

                var levelWindow = failedLevel;
                levelWindow.RestartLevel();
                levelWindow.Show();
                levelWindow.Enabled = true;
            }
        }
    }
}