using System.Windows.Forms;

namespace Pac_Man
{
    using System;

    public partial class WinWindow : Form
    {
        private Form1 level;
        public WinWindow(Form1 parent)
        {
            InitializeComponent();
            level = parent;
        }


        private void WinWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F)
            {
               
                Hide();
                level = new Form1();
                level.Show();
                level.Closed += (s, args) => { Close(); };
            }
        }
    }
}