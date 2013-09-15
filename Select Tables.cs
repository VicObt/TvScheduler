using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TvScheduler
{
    public partial class frmSelectTables : Form
    {
        public frmSelectTables(string[] StrTable)
        {
            InitializeComponent();
            Tables = StrTable;
        }
        public frmSelectTables(DataTable dt)
        {
            InitializeComponent();
            dtTable = dt;
            DataTables = true;
        }

        DataTable dtTable = new DataTable();
        string tableName = string.Empty;
        string[] Tables;
        bool DataTables = false;
       
        private void frmSelectTables_Load(object sender, EventArgs e)
        {
            if (!DataTables)
            {
                if (Tables != null)
                {
                    for (int tables = 0; tables < Tables.Length; tables++)
                    {
                        try
                        {
                            ListViewItem lv = new ListViewItem();
                            lv.Text = Tables[tables].ToString();
                            lv.Tag = tables;
                            lstViewTables.Items.Add(lv);
                        }
                        catch (Exception)
                        {
                            //MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
            }
            else
            {
                if (dtTable.Rows.Count > 0)
                {
                    for (int tables = 0; tables < dtTable.Rows.Count; tables++)
                    {
                        try
                        {
                            ListViewItem lv = new ListViewItem();
                            lv.Text = dtTable.Rows[tables][0].ToString();
                            lv.Tag = dtTable.Rows[tables][0];
                            lstViewTables.Items.Add(lv);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lstViewTables.Items.Count > 0)
            {
                if (tableName != string.Empty)
                {
                    SoapSchedules.SelectedTable = tableName;
                    MovieShedules.SelectedTable = tableName;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Select a Table");
                }
            }
            else
            {
                this.Close();
            }
        }
        private void lstViewTables_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //tableName = lstViewTables.Items[lstViewTables.SelectedIndices].ToString();
        }
        private void lstViewTables_ItemSelectionChanged_1(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            tableName = e.Item.Text;
        }

    }
}