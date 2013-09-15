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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Main mainform = new Main();
                Login log = new Login();
               
                this.Dispose();
                mainform.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ooops!An Error Occured While Loading");
            }
        }
    }
}
