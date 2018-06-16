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
    public partial class Sale_Bill_PrintingScreen : Form
    {
        string username, company;
        SqlCommand cmd = null;
        SqlConnection con = null;
        SqlDataReader dr = null;
        ConnectDb objCon = new ConnectDb();
        public Sale_Bill_PrintingScreen(string username, string company)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
        }

        private void Sale_Bill_PrintingScreen_Load(object sender, EventArgs e)
        {
            try 
            {
                string str = "select [Challan_no] from [Sales] where [DeleteFlag] = 'No' order by [SalesBillPk] desc";
                dr = objCon.getData(str);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    { 
                        comboBox1.Items.Add(dr[0].ToString());
                    }
                }
                dr.Close();
            }
            catch
            {}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                challanRpt1.SetDatabaseLogon("sa", "preet@1234", "server", "BMSLifestyle");
                challanRpt1.SetParameterValue("@challanNo", comboBox1.Text.TrimEnd().TrimStart());
                crystalReportViewer1.ReportSource = challanRpt1;
                crystalReportViewer1.Refresh();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            challanRpt1.SetDatabaseLogon("sa", "preet@1234", "server", "BMSLifestyle");
            challanRpt1.SetParameterValue("@challanNo", comboBox1.Text.TrimEnd().TrimStart());
            challanRpt1.PrintToPrinter(1, true, 1, 50);
        }
    }
}
