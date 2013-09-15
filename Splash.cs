using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TvScheduler
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }

        byte Z;

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 50;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Z += 2;
            if (Z >= 100)
            {
                this.Hide();
                Login namelog = new Login();
                namelog.Show();
                timer1.Enabled = false;
            }

            progressBar1.Value = Z;
        }
    }
}
