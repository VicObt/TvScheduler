﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Controls;
using ADOX;
using System.Data.OleDb;

namespace TvScheduler
{
    public partial class SoapSchedules : Form
    {
        public SoapSchedules()
        {
            InitializeComponent();
        }
        int soapID;
        private static string path = Application.StartupPath + "\\Data\\TvScheduler.mdb";
        private static string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + path + ";";
        
        //public static string connectionString = "Data Source=./SQLEXPRESS;AttachDbFilename=|DataDirectory|/TvScheduler.mdf; Initial Catalog=TvScheduler;Integrated Security=True;";
        public static string SelectedTable = string.Empty;  
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
           // fdlg.InitialDirectory = @"c:\";
            fdlg.FileName = txtFileName.Text;
            fdlg.Filter = "Excel Sheet(*.xls)|*.xls|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = fdlg.FileName;
                Import();
                Application.DoEvents();
            }
        }
        private void Import()
        {
            if (txtFileName.Text.Trim() != string.Empty)
            {
                try
                {
                    string[] strTables = GetTableExcel(txtFileName.Text);

                    frmSelectTables objSelectTable = new frmSelectTables(strTables);
                    objSelectTable.ShowDialog(this);
                    objSelectTable.Dispose();
                    if ((SelectedTable != string.Empty) && (SelectedTable != null))
                    {
                        DataTable dt = GetDataTableExcel(txtFileName.Text, SelectedTable);
                        dataGridView1.DataSource = dt.DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        public static DataTable GetDataTableExcel(string strFileName, string Table)
        {
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + strFileName + "; Extended Properties = \"Excel 8.0;HDR=Yes;IMEX=1\";");
            conn.Open();
            string strQuery = "SELECT * FROM [" + Table + "]";
            System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter(strQuery, conn);
            System.Data.DataSet ds = new System.Data.DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }
        public static string[] GetTableExcel(string strFileName)
        {
            string[] strTables = new string[100];
            Catalog oCatlog = new Catalog();
            ADOX.Table oTable = new ADOX.Table();
            ADODB.Connection oConn = new ADODB.Connection();
            oConn.Open("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + strFileName + "; Extended Properties = \"Excel 8.0;HDR=Yes;IMEX=1\";", "", "", 0);
            oCatlog.ActiveConnection = oConn;
            if (oCatlog.Tables.Count > 0)
            {
                int item = 0;
                foreach (ADOX.Table tab in oCatlog.Tables)
                {
                    if (tab.Type == "TABLE")
                    {
                        strTables[item] = tab.Name;
                        item++;
                    }
                }
            }
            return strTables;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                this.Close();
            }
            catch (Exception)
            {    
                throw;
            } 
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OleDbConnection Cnn = new OleDbConnection(connectionString);
            OleDbTransaction tr = null;

            try
            {
                Cnn.Open();
                tr = Cnn.BeginTransaction();

                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {

                    DataGridViewRow row = dataGridView1.SelectedRows[i];
                    string oName = row.Cells[0].Value.ToString() + "," + row.Cells[1].Value.ToString() + "," + row.Cells[2].Value.ToString() + "," + row.Cells[3].Value.ToString() + "," + row.Cells[4].Value.ToString() + "," + row.Cells[5].Value.ToString();
                    string[] oMovieName = oName.Split(',');
                   
                    string myText = "INSERT INTO TimeLine (Title,ShowTypeID) VALUES ('" + oMovieName[1].ToString() + "','" + soapID + "')";
                    OleDbCommand cmd = new OleDbCommand(myText, Cnn);
                    cmd.Transaction = tr;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "SELECT @@IDENTITY";
                    object TimeLineobj;
                    TimeLineobj = cmd.ExecuteScalar();
                    int TimeLineid = int.Parse(TimeLineobj.ToString());

                    string myText1 = "INSERT INTO Details (ShowID,Duration,ExpiryDate,Rating) VALUES ('" + TimeLineid + "','" + oMovieName[5].ToString() + "','" + oMovieName[4].ToString() + "','PG')";
                    OleDbCommand cmd1 = new OleDbCommand(myText1, Cnn);
                    cmd1.Transaction = tr;
                    cmd1.ExecuteNonQuery();
                }

                tr.Commit();
                MessageBox.Show("Sucessfully Added to Time Line");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Oops an error occured" + " " + ex.Message);
                tr.Rollback();
            }
            finally
            {
                Cnn.Close();
            }
        }
        private void SoapSchedules_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns.Clear();
            OleDbConnection Cnn = new OleDbConnection(connectionString);
            OleDbDataReader myreader = null;
            try
            {
                Cnn.Open();
                string myText = "SELECT * from ShowType WHERE ShowTypeName = 'SoapOpera'";
                OleDbCommand cmd = new OleDbCommand(myText, Cnn);
                myreader = cmd.ExecuteReader();

                if (myreader.Read())
                {
                    soapID = myreader.GetInt32(0);
                }
            }


            catch (Exception ex)
            {

                MessageBox.Show("Oops an error occured" + " " + ex.Message);
            }
            finally
            {
                Cnn.Close();
                myreader.Close();
            }
        }
        }
    }

