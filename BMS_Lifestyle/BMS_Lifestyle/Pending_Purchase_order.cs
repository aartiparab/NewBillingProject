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
    public partial class Pending_Purchase_order : Form
    {
        string username, company, connectionString;
        SqlCommand command;
        SqlConnection connection;
        string PONo1;
        public Pending_Purchase_order(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Pending_Purchase_order_Load(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                
                command = new SqlCommand("Select [PO_No],convert(varchar,[PO_Date],103) as PO_Date,[SupName],cast([TQty] as decimal(18,0)) as TQty"+
                                         ",cast([Tbal_qty] as decimal(18,0)) as Tbal_qty, cast([NetAmt] as decimal(18,2)) as NetAmt,[Narration] "+
                                         "FROM [Purchase_Order] where [Tbal_qty] > '0' and [PO_No] not in (select [PO_No1] from [Temp_Purchase])", connection);
                
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
                    MessageBox.Show("No Purchase Order generate");
                    this.BeginInvoke(new MethodInvoker(Close));
                }
                reader.Close();
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns[0].DefaultCellStyle.SelectionBackColor = Color.Red;
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

                PurchaseEntry obj = new PurchaseEntry(username, company, connectionString, PONo1);
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
