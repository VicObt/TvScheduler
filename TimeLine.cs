using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Xml;
using System.Data.OleDb;


namespace TvScheduler
{
    public partial class TimeLine : Form
    {
        private BindingSource bindingSource = new BindingSource();

        public TimeLine()
        {
            InitializeComponent();
        }
        DataTable dt;
        OleDbDataAdapter da;
        private static string path = Application.StartupPath + "\\Data\\TvScheduler.mdb";
        private static string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + path + ";";
        OleDbConnection con = new OleDbConnection(connectionString);


        private void TimeLine_Load(object sender, EventArgs e)
        {
            con.Open();
            string myQuery = "SELECT Details.Duration, Details.ExpiryDate, Details.Rating, ShowType.ShowTypeName, TimeLine.Title FROM ((Details INNER JOIN TimeLine ON Details.ShowID = TimeLine.ID) INNER JOIN ShowType ON TimeLine.ShowTypeID = ShowType.TypeID)";
            OleDbCommand cmd = new OleDbCommand(myQuery, con);
            da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            cmd.Dispose();
            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
           
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            
            app.Visible = true;
           
            try
            {
                
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.ActiveSheet;
                
                worksheet.Name = "YourChosenSheetName";
               
                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }
                // storing Each row and column value to excel sheet
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }

                // save the application
                string fileName = String.Empty;
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Excel files |*.xls|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog1.FileName;
                    //Fixed-old code :11 para->add 1:Type.Missing
                    workbook.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                }
                else
                    return;

                // Exit from the application
                //app.Quit();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Oops! Something went wrong" + " " + ex.Message);
            }
            finally
            {
                app.Quit();
                workbook = null;
                app = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetData(string selectCommand)
        {
            try
            {
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(selectCommand, connectionString);
                OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(dataAdapter);
              
                DataTable table = new DataTable();
               
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dataAdapter.Fill(table);
                BindingSource databind = new BindingSource();
                databind.DataSource = table;
               
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            catch (SqlException)
            {
                MessageBox.Show("Oops an Error Occured could not load TimeLine");
            }
        }
    }
}
