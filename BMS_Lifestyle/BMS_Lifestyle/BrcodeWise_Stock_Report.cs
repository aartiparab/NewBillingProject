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
    public partial class BrcodeWise_Stock_Report : Form
    {
        string username, company;
        SqlCommand cmd = null;
        SqlConnection con = null;
        SqlDataReader dr = null;
        ConnectDb objCon = new ConnectDb();
        AutoCompleteStringCollection barcode = new AutoCompleteStringCollection();
        AutoCompleteStringCollection warehouse = new AutoCompleteStringCollection();
        AutoCompleteStringCollection pname = new AutoCompleteStringCollection();
        AutoCompleteStringCollection Sname = new AutoCompleteStringCollection();
        AutoCompleteStringCollection rack = new AutoCompleteStringCollection();
        public BrcodeWise_Stock_Report(string username, string company)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
        }

        private void BrcodeWise_Stock_Report_Load(object sender, EventArgs e)
        {
            try
            {
                barcode.Add("All");
                string strfill = "select distinct [EAC_Code] from [Item] union all select distinct [EAC_Code] from [Kit_Master]";
                dr = objCon.getData(strfill);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        barcode.Add(dr[0].ToString());
                    }
                }
                dr.Close();
                txtbarcode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtbarcode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtbarcode.AutoCompleteCustomSource = barcode;

                warehouse.Add("All");
                string strfill1 = "select distinct [WareHouse_Name] from [Rack_Master]";
                dr = objCon.getData(strfill1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        warehouse.Add(dr[0].ToString());
                    }
                }
                dr.Close();
                txtwarehouse.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtwarehouse.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtwarehouse.AutoCompleteCustomSource = warehouse;

                pname.Add("All");
                string strfill2 = "select distinct [P_Name] from [Item] union all select distinct [Kit_Name] from [Kit_Master]";
                dr = objCon.getData(strfill2);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pname.Add(dr[0].ToString());
                    }
                }
                dr.Close();
                txtp_name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtp_name.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtp_name.AutoCompleteCustomSource = pname;

                pname.Add("All");
                string strfil6 = "select distinct [S_Name] from [Item] union all select distinct [Kit_Name] from [Kit_Master]";
                dr = objCon.getData(strfil6);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Sname.Add(dr[0].ToString());
                    }
                }
                dr.Close();
                txtS_name.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtS_name.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtS_name.AutoCompleteCustomSource = Sname;

            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                stock_Barcodewise1.SetDatabaseLogon("sa", "preet@1234", "server", "BMSLifestyle");
                stock_Barcodewise1.SetParameterValue("@fromDate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                stock_Barcodewise1.SetParameterValue("@toDate", dateTimePicker2.Value.ToString("MM/dd/yyyy"));
                stock_Barcodewise1.SetParameterValue("@company", company);
                stock_Barcodewise1.SetParameterValue("@Barcode", txtbarcode.Text);
                stock_Barcodewise1.SetParameterValue("@P_Name", txtp_name.Text);
                stock_Barcodewise1.SetParameterValue("@S_Name", txtS_name.Text);
                stock_Barcodewise1.SetParameterValue("@rack", txtrack.Text);
                stock_Barcodewise1.SetParameterValue("@warehouse", txtwarehouse.Text);
                crystalReportViewer1.ReportSource = stock_Barcodewise1;
                crystalReportViewer1.Refresh();
            }
            catch
            { }
        }

        private void txtwarehouse_TextChanged(object sender, EventArgs e)
        {
            if (txtwarehouse.Text != "")
            {
                if (txtwarehouse.Text == "All")
                {
                    rack.Add("All");
                    string strfill3 = "select distinct [RackName] from [Rack_Master] ";
                    dr = objCon.getData(strfill3);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            rack.Add(dr[0].ToString());
                        }
                    }
                    dr.Close();
                    txtrack.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtrack.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtrack.AutoCompleteCustomSource = rack;
                }
                else 
                {
                    string strfill3 = "select distinct [RackName] from [Rack_Master] where [WareHouse_Name] = '" + txtwarehouse.Text + "'";
                    dr = objCon.getData(strfill3);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            rack.Add(dr[0].ToString());
                        }
                    }
                    dr.Close();
                    txtrack.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txtrack.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtrack.AutoCompleteCustomSource = rack;
                }
            }
        }
    }
}
