using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TvScheduler
{
    public partial class Main : Form
    {
        //public static string connectionString ="Data Source=./SQLEXPRESS;AttachDbFilename="C:/Workspace/Biz Projects\MyTv\TvScheduler\TvScheduler\TvScheduler.mdf";Integrated Security=True;User Instance=True"; 
        //public static string connectionString = "Data Source=./SQLEXPRESS; AttachDbFilename=|DataDirectory|/TvScheduler.mdf.mdf; Integrated Security=True";
       
        public Main()
        {
            InitializeComponent();
        }

        private void movieTimeLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //showID = movieTimeLineToolStripMenuItem.Text;
            try
            {
                MovieShedules movieShed = new MovieShedules();
                movieShed.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oops an Error occured");
            }
        }
        //public int 
        private void soapTimeLineToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                SoapSchedules soapSched = new SoapSchedules();
                soapSched.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oops an Error occured");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        

        private void monthsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TimeLine movTL = new TimeLine();
                movTL.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oops an Error occured");
            }
        }
        private void statisticsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Statistics sta = new Statistics();
                sta.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Oops an Error occured");
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }      
      
    }
}
