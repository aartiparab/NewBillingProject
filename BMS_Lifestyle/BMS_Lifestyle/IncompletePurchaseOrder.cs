using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BMS_Lifestyle
{
    public partial class IncompletePurchaseOrder : Form
    {
        string username, company, connectionString;
        SqlCommand command;
        SqlConnection connection;
        string PONo1;

        public IncompletePurchaseOrder(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void IncompletePurchaseOrder_Load(object sender, EventArgs e)
        {
            try
            {

                connection = new SqlConnection(connectionString);
                connection.Open();

                command = new SqlCommand("Select [PO_No1],convert(varchar,[Po_Date1],103) as PO_Date,[SupName1],cast([TQty1] as decimal(18,0)) as TQty,"+
                                         "cast([TPoQty1]as decimal(18,0)) as TPoQty, cast([TBalPoQty1] as decimal(18,0)) as Tbal_qty, "+
                                         "cast([NetAmt1] as decimal(18,2)) as NetAmt FROM [Temp_Purchase] where [TBalPoQty1] > '0' and [deleteflag1] ='No'", connection);

                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("No Purchase Order incomplete");
                    this.BeginInvoke(new MethodInvoker(Close));
                }
                reader.Close();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.GreenYellow;
            }
            catch { }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                PONo1 = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                this.AutoScaleMode = AutoScaleMode.Font;
                this.WindowState = FormWindowState.Normal;

                Incomp_PO_PurchaseEntry obj = new Incomp_PO_PurchaseEntry(username, company, connectionString, PONo1);
                obj.MdiParent = this.ParentForm;
                obj.Show();
            }
            catch { }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "PO_Date")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "TQty")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Tbal_qty")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "NetAmt")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Narration")
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
