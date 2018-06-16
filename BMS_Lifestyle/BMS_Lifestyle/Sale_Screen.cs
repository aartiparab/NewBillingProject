using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace BMS_Lifestyle
{
    public partial class Sale_Screen : Form
    {

        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectDb ObjCon = new ConnectDb();
        SqlDataReader dr = null;
        string username, commandT, save, purchasePK, company, company_state, gst_percent = "", amtwdiscount,sms_user1,sms_pass1,sms_sid1;
        int affect;
        AutoCompleteStringCollection Party = new AutoCompleteStringCollection();
        AutoCompleteStringCollection Tax = new AutoCompleteStringCollection();
        AutoCompleteStringCollection state1 = new AutoCompleteStringCollection();
        AutoComplete objAccName = new AutoComplete();
        Double TVatAmt = 0, GST_percent,discamt = 0;
        Double transport_gst = 0;
        int flag = 0;
        public Sale_Screen(string username, string company)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //CustomerEdit obj = new CustomerEdit(username, company);
            //obj.MdiParent = this.ParentForm;
            //obj.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sale_Screen p = new Sale_Screen(username, company);
            p.MdiParent = this.ParentForm;
            p.Show();
            this.Close();
        }

        private void Sale_Screen_Load(object sender, EventArgs e)
        {
            try
            {
                cmbCustName.Focus();
                string str5 = "select [State],[SMS_USER],[SMS_PASS],[SMS_SID] FROM [Company_Master] where [Company_ID] = '" + company + "'";
                dr = ObjCon.getData(str5);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        company_state = dr[0].ToString();
                        sms_user1 = dr[1].ToString();
                        sms_pass1 = dr[2].ToString();
                        sms_sid1 =dr[3].ToString();
                    }

                }
                dr.Close();
                fillParty();
                cmbPaymentMode.SelectedIndex = 0;
                panelcashcard.Visible = false;
                this.ActiveControl = cmbCustName;
            }
            catch { }
            
        }

        public void fillParty()
        {
            try
            {
                //dr.Close();
              string commandT5 = "select distinct [CustName] from [Customer]";
                dr = ObjCon.getData(commandT5);
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        Party.Add(dr[0].ToString());
                    }

                }
                dr.Close();
                cmbCustName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbCustName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbCustName.AutoCompleteCustomSource = Party;


                commandT = "select distinct [Name] from [Salesman_Master]";
                dr = ObjCon.getData(commandT);
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        cmbSalesman.Items.Add(dr[0].ToString());
                    }

                }
                dr.Close();

                string commandT1 = "select distinct [State_Code] from [State_Code_Master] order by [State_Code]";
                dr = ObjCon.getData(commandT1);
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        cmbState.Items.Add(dr[0].ToString().ToUpper());
                    }

                }
                dr.Close();
                //cmbState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //cmbState.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //cmbState.AutoCompleteCustomSource = state1;
                cmbState.SelectedIndex = 26;
            }
            catch { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Customer obj = new Customer(username,company);
            //obj.MdiParent = this.ParentForm;
            //obj.Show();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rows = Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1;
            DataGridViewRowsRemovedEventArgs ee = null;
            double cgst, sgst, igst, GST_Amount;
            string stock = null,Bcode1;
            int flag = 0,flag_cust =1;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column1")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "group")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "size")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "unit")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "skucode")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "mrp")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "amount1")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "gst_amt")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "GST_per")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "new_mrp")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "shipping_wt1")
            {
                SendKeys.Send("{TAB}");
            }
            
            if (e.ColumnIndex == 0)
            {
                if (cmbState.Text == "")
                {
                    MessageBox.Show("State cannot be blank");
                }
                try
                {
                    // Check stock of item  
                    dr.Close();
                    commandT = "exec Stock_Barcodewise '" + dataGridView1.Rows[e.RowIndex -1].Cells["barcode"].Value.ToString() + "','"+company+"'";
                    dr = ObjCon.getData(commandT);
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            stock = dr[0].ToString();
                        }
                    }
                    dr.Close();

                    //if stock is greater than zero 
                    if (double.Parse(stock) > 0)
                    {
                        int count = 0;
                        int row = dataGridView1.RowCount - 2; 

                        string[] bcode = new string[row + 1];
                        string[] qty = new string[row + 1];

                        for (int i = 0; i <= row; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value != null && e.RowIndex - 1 != i)
                            {
                                bcode[i] = dataGridView1.Rows[i].Cells["barcode"].Value.ToString();
                                qty[i] = dataGridView1.Rows[i].Cells["qty1"].Value.ToString();
                            }
                        }
                        for (int i = 0; i <= row; i++)
                        {
                            if (dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() == bcode[i])
                            {
                                count = count + int.Parse(qty[i].ToString());
                            }
                        }
                        if (int.Parse(stock) > count)
                        {
                            try
                            {
                                //dr.Close();
                                string checkcust = "select [Barcode] from [MRP_for_Customer] where [Barcode] = '" + dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() + "' and [CustomerName] = '" + cmbCustName.Text + "'";
                                dr = ObjCon.getData(checkcust);
                                if (dr.HasRows)
                                {
                                    flag_cust = 2;
                                }

                                dr.Close();

                                if (flag_cust == 2)
                                {
                                    if (dataGridView1.Rows[e.RowIndex - 1].Cells["Column1"].Value == null || dataGridView1.Rows[e.RowIndex - 1].Cells["Column1"].Value == "")
                                    {
                                        commandT = "SELECT i.[ItemName],i.[Item_Group],i.[Item_Unit],cast(m.[MRP] as decimal(18,2)) as MRP_value," +
                                                    "isnull(cast(t.[Tax_per] as decimal(18,2)),0) as Tax_per,i.[shipping_wt],i.[Item_No],i.[SKU_Code] " +
                                                    "FROM [Item] i left join [Tax_Master] t on i.[Tax_Name] = t.[Tax_Name] " +
                                                    "left join [MRP_for_Customer] m on i.[Barcode_No] = m.Barcode where " +
                                                    "i.[Barcode_No] = '" + dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() + "' and m.[CustomerName] = '" + cmbCustName.Text + "'";
                                        dr = ObjCon.getData(commandT);
                                        if (dr.HasRows == true)
                                        {
                                            while (dr.Read())
                                            {
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["Column1"].Value = dr["ItemName"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["group"].Value = dr["Item_Group"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["size"].Value = dr["Item_No"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["unit"].Value = dr["Item_Unit"].ToString();
                                                try
                                                {
                                                    if (dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value.ToString() != "")
                                                    {
                                                        if (dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value.ToString() != null)
                                                        {
                                                        }
                                                    }
                                                    else
                                                    {
                                                        dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value = "0";
                                                    }
                                                }
                                                catch
                                                {
                                                    dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value = "0";
                                                }
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value = "1";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["mrp"].Value = dr["MRP_value"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value = dr["Tax_per"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["shipping_wt1"].Value = dr["shipping_wt"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["amtWogst"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["amtwogst1"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["skucode"].Value = dr["SKU_Code"].ToString();
                                            }
                                        }
                                        dr.Close();
                                    }
                                }
                                else if (flag_cust == 1)
                                {
                                    if (dataGridView1.Rows[e.RowIndex - 1].Cells["Column1"].Value == null || dataGridView1.Rows[e.RowIndex - 1].Cells["Column1"].Value == "")
                                    {
                                        commandT = "SELECT i.[ItemName],i.[Item_Group],i.[Item_Unit],cast(i.[MRP_value] as decimal(18,2)) as MRP_value," +
                                                        "isnull(cast(t.[Tax_per] as decimal(18,2)),0) as Tax_per,i.[shipping_wt],i.[Item_No],i.[SKU_Code] " +
                                                        "FROM [Item] i left join [Tax_Master] t on i.[Tax_Name] = t.[Tax_Name] " +
                                                        " where i.[Barcode_No] = '" + dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() + "'";
                                        dr = ObjCon.getData(commandT);
                                        if (dr.HasRows == true)
                                        {
                                            while (dr.Read())
                                            {
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["Column1"].Value = dr["ItemName"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["group"].Value = dr["Item_Group"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["size"].Value = dr["Item_No"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["unit"].Value = dr["Item_Unit"].ToString();
                                                try
                                                {
                                                    if (dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value.ToString() != "")
                                                    {
                                                        if (dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value.ToString() != null)
                                                        {
                                                        }
                                                    }
                                                    else
                                                    {
                                                        dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value = "0";
                                                    }
                                                }
                                                catch
                                                {
                                                    dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value = "0";
                                                }
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value = "1";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["mrp"].Value = dr["MRP_value"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value = dr["Tax_per"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["shipping_wt1"].Value = dr["shipping_wt"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["amtWogst"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["amtwogst1"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["skucode"].Value = dr["SKU_Code"].ToString();
                                            }
                                        }
                                        dr.Close();
                                    }
                                }
                                if (cmbState.Text == company_state)
                                {
                                    try
                                    {

                                        string str8 = "SELECT isnull(cast([cgst_tax] as decimal(18,2)),0) as cgst_tax,isnull(cast([sgst_tax] as decimal(18,2)),0) as sgst_tax FROM [Item] i where i.[Barcode_No] =  '" + dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() + "'";
                                        dr = ObjCon.getData(str8);
                                        if (dr.HasRows)
                                        {
                                            while (dr.Read())
                                            {
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_per"].Value = dr["cgst_tax"].ToString();
                                                if (dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_per"].Value.ToString() == "")
                                                {
                                                    if (dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_per"].Value == null)
                                                    {
                                                        dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_per"].Value = "0";
                                                    }
                                                }

                                                dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_per"].Value = dr["sgst_tax"].ToString();
                                                if (dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_per"].Value.ToString() == "")
                                                {
                                                    if (dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_per"].Value == null)
                                                    {
                                                        dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_per"].Value = "0";
                                                    }
                                                }
                                                // dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value = dr["Tax_per"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["igst_per"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["igst_amt"].Value = "0";
                                            }
                                        }
                                        dr.Close();
                                    }
                                    catch { }
                                }
                                else
                                {
                                    try
                                    {
                                        string str9 = "SELECT isnull(cast([igst_tax] as decimal(18,2)),0) as igst_tax FROM [Item] i where i.[Barcode_No] = '" + dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() + "'";
                                        dr = ObjCon.getData(str9);
                                        if (dr.HasRows)
                                        {
                                            while (dr.Read())
                                            {

                                                dataGridView1.Rows[e.RowIndex - 1].Cells["igst_per"].Value = dr["igst_tax"].ToString();
                                                // dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value = dr["Tax_per"].ToString();
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_per"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_amt"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_per"].Value = "0";
                                                dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_amt"].Value = "0";
                                                if (dataGridView1.Rows[e.RowIndex - 1].Cells["igst_per"].Value.ToString() == "")
                                                {
                                                    if (dataGridView1.Rows[e.RowIndex - 1].Cells["igst_per"].Value == null) 
                                                    {
                                                        dataGridView1.Rows[e.RowIndex - 1].Cells["igst_per"].Value = "0";
                                                    }
                                                }
                                            }
                                        }
                                        dr.Close();
                                    }
                                    catch { }
                                }
                                try {
                                    
                                    //---------------Reverse Calculation---------------------------------------
                                    if (cmbState.Text == company_state) 
                                    {
                                        dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["mrp"].Value.ToString()) * 100) / (100 + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))));
                                        
                                        dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value = String.Format("{0:0.00}", ((((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value.ToString()))) / 100)));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) - Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value.ToString()));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString())) / 100));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["new_mrp"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value.ToString()) / Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()))));

                                        GST_Amount = Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value);

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_amt"].Value = String.Format("{0:0.00}", (((GST_Amount) / 2)));
                                        cgst = Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_amt"].Value.ToString());

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_amt"].Value = String.Format("{0:0.00}", (((GST_Amount) / 2)));
                                        sgst = Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_amt"].Value.ToString());

                                        dataGridView1_RowsRemoved(dataGridView1, ee);
                                    }
                                    else 
                                    {
                                        dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["mrp"].Value.ToString()) * 100) / (100 + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value = String.Format("{0:0.00}", ((((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value.ToString()))) / 100)));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) - Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value.ToString()));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString())) / 100));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()));

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["new_mrp"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value.ToString()) / Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()))));

                                        GST_Amount = Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value);

                                        dataGridView1.Rows[e.RowIndex - 1].Cells["igst_amt"].Value = String.Format("{0:0.00}", (GST_Amount));
                                        igst = Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["igst_amt"].Value.ToString());

                                        dataGridView1_RowsRemoved(dataGridView1, ee);
                                    }
                                }
                                catch { }
                            }
                            catch { }
                        }
                        else
                        { }
                    }
                    else
                    {
                        MessageBox.Show("This Item is out of stock.");
                    }
                }
                catch { }
            }

            if (e.ColumnIndex == 9)
            {
                try
                {
                    if (cmbState.Text == company_state)
                    {
                        dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["mrp"].Value.ToString()) * 100) / (100 + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value = String.Format("{0:0.00}", ((((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value.ToString()))) / 100)));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) - Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value.ToString()));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString())) / 100));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["new_mrp"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value.ToString()) / Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()))));

                        GST_Amount = Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value);

                        dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_amt"].Value = String.Format("{0:0.00}", (((GST_Amount) / 2)));
                        cgst = Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_amt"].Value.ToString());

                        dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_amt"].Value = String.Format("{0:0.00}", (((GST_Amount) / 2)));
                        sgst = Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_amt"].Value.ToString());

                        dataGridView1_RowsRemoved(dataGridView1, ee);
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["mrp"].Value.ToString()) * 100) / (100 + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value = String.Format("{0:0.00}", ((((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value.ToString()))) / 100)));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) - Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value.ToString()));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString())) / 100));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["new_mrp"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value.ToString()) / Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()))));

                        GST_Amount = Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value);

                        dataGridView1.Rows[e.RowIndex - 1].Cells["igst_amt"].Value = String.Format("{0:0.00}", (GST_Amount));
                        igst = Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["igst_amt"].Value.ToString());

                        dataGridView1_RowsRemoved(dataGridView1, ee);
                    }
                }
                catch { }
            }

            if(e.RowIndex > 0)
            {
                int row = dataGridView1.RowCount - 2;
                string[] bcode = new string[row + 1];

                for (int i = 0; i <= row; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value != null && e.RowIndex-1 != i)
                    {
                        bcode[i] = dataGridView1.Rows[i].Cells["barcode"].Value.ToString();
                    }
                }
                for (int i = 0; i <= row; i++)
                {

                    if (dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() == bcode[i])
                    {
                        dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["Column1"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["group"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["size"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["unit"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["mrp"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["shipping_wt1"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["amtWogst"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["amtwogst1"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["skucode"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value = "";
                    }
                }
            }

            if (e.ColumnIndex == 7)
            {
                try
                {
                    // Check stock of item  
                    dr.Close();
                    commandT = "exec Stock_Barcodewise '" + dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() + "','" + company + "'";
                    dr = ObjCon.getData(commandT);
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            stock = dr[0].ToString();
                        }
                    }
                    dr.Close();

                    //if stock is greater than zero 
                    if (double.Parse(stock) > 0)
                    {
                        int count = 0;
                        int row = dataGridView1.RowCount - 2;

                        string[] bcode = new string[row + 1];
                        string[] qty = new string[row + 1];

                        for (int i = 0; i <= row; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value != null && e.RowIndex != i)
                            {
                                bcode[i] = dataGridView1.Rows[i].Cells["barcode"].Value.ToString();
                                qty[i] = dataGridView1.Rows[i].Cells["qty1"].Value.ToString();
                            }
                        }

                        for (int i = 0; i <= row; i++)
                        {

                            if (dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() == bcode[i])
                            {
                                count = count + int.Parse(qty[i].ToString());
                            }
                        }
                        if (int.Parse(stock) >= count)
                        {
                            try
                            {
                                if (cmbState.Text == company_state)
                                {
                                    dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["mrp"].Value.ToString()) * 100) / (100 + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value = String.Format("{0:0.00}", ((((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value.ToString()))) / 100)));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) - Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value.ToString()));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString())) / 100));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["new_mrp"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value.ToString()) / Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()))));

                                    GST_Amount = Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value);

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_amt"].Value = String.Format("{0:0.00}", (((GST_Amount) / 2)));
                                    cgst = Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["cgst_amt"].Value.ToString());

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_amt"].Value = String.Format("{0:0.00}", (((GST_Amount) / 2)));
                                    sgst = Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["sgst_amt"].Value.ToString());

                                    dataGridView1_RowsRemoved(dataGridView1, ee);
                                }
                                else
                                {
                                    dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["mrp"].Value.ToString()) * 100) / (100 + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value = String.Format("{0:0.00}", ((((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Disc_amt"].Value.ToString()))) / 100)));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) - Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["discamt1"].Value.ToString()));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString())) / 100));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amtWOgst"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()));

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["new_mrp"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value.ToString()) / Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()))));

                                    GST_Amount = Convert.ToDouble(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value);

                                    dataGridView1.Rows[e.RowIndex - 1].Cells["igst_amt"].Value = String.Format("{0:0.00}", (GST_Amount));
                                    igst = Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["igst_amt"].Value.ToString());

                                    dataGridView1_RowsRemoved(dataGridView1, ee);
                                }
                            }
                            catch { }
                        }
                        else 
                        {
                            MessageBox.Show("Eneterd quantity exceed from stock quantity");
                        }
                    }
                }
                catch { }
            }
            dataGridView1_RowsRemoved(dataGridView1, ee);
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Double Mtr = 0, CgstAmt = 0, IgstAmt = 0, SgstAmt = 0, QTY1 = 0, discamt12 = 0, tWT = 0; ;
            int rows = Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1;

            for (int i = 0; i < rows; i++)
            {
                try
                {
                    if (dataGridView1.Rows[i].Cells["qty1"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["qty1"].Value.ToString() != "")
                        {
                            QTY1 += Double.Parse(dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["amount1"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["amount1"].Value.ToString() != "")
                        {
                            Mtr += Double.Parse(dataGridView1.Rows[i].Cells["amount1"].Value.ToString());
                        }
                    }

                    if (dataGridView1.Rows[i].Cells["discamt1"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["discamt1"].Value.ToString() != "")
                        {
                            discamt12 += Double.Parse(dataGridView1.Rows[i].Cells["discamt1"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["cgst_amt"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["cgst_amt"].Value.ToString() != "")
                        {
                            CgstAmt += Double.Parse(dataGridView1.Rows[i].Cells["cgst_amt"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["igst_amt"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["igst_amt"].Value.ToString() != "")
                        {
                            IgstAmt += Double.Parse(dataGridView1.Rows[i].Cells["igst_amt"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["sgst_amt"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["sgst_amt"].Value.ToString() != "")
                        {
                            SgstAmt += Double.Parse(dataGridView1.Rows[i].Cells["sgst_amt"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["shipping_wt1"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["shipping_wt1"].Value.ToString() != "")
                        {
                            tWT += Double.Parse(dataGridView1.Rows[i].Cells["shipping_wt1"].Value.ToString());
                        }
                    }
                }
                catch { }
            }

            txtweightGm.Text = tWT.ToString();
            txtweightKG.Text = (tWT * 0.001).ToString();

            textBox3.Text = String.Format("{0:0.00}", double.Parse(rows.ToString()));
            txttotqty.Text = String.Format("{0:0.00}", double.Parse(QTY1.ToString()));
            lblTot_Amt.Text = String.Format("{0:0.00}", double.Parse(Mtr.ToString()));
            lblcgstamt.Text = String.Format("{0:0.00}", double.Parse(CgstAmt.ToString()));
            lbligstamt.Text = String.Format("{0:0.00}", double.Parse(IgstAmt.ToString()));
            lblsgstamt.Text = String.Format("{0:0.00}", double.Parse(SgstAmt.ToString()));
            TVatAmt = (Double.Parse(lblcgstamt.Text) + Double.Parse(lbligstamt.Text) + Double.Parse(lblsgstamt.Text));
            TxtDiscAmt.Text = String.Format("{0:0.00}", double.Parse(discamt12.ToString()));
            lblTotalgst.Text = (TVatAmt).ToString();
            lblNet_Amt.Text = Math.Round((Double.Parse(lblTot_Amt.Text) + Double.Parse((TVatAmt).ToString()) - Double.Parse(TxtDiscAmt.Text)), 0).ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbCustName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString().ToUpper();
                e.FormattingApplied = true;
            }
        }

        protected void Caption_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text != null && (sender as TextBox).Text.Trim() != "")
            { }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //int colIndex;
            //int rowIndex;
            //objAccName.productname.Clear();

            //var txtBox = e.Control as TextBox;

            //colIndex = dataGridView1.CurrentCell.ColumnIndex;
            //rowIndex = dataGridView1.CurrentCell.RowIndex;

            //if (colIndex == dataGridView1.Rows[rowIndex].Cells["barcode"].ColumnIndex)
            //{
            //    if (e.Control is TextBox)
            //    {
            //        if (txtBox != null)
            //        {

            //            txtBox.CharacterCasing = CharacterCasing.Upper;
            //            txtBox.TextChanged += new EventHandler(Caption_TextChanged);
            //            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //            txtBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //            string commandT = "select distinct [barcode] FROM [SubPurchase] where [Returned] = 'No'";
            //            objAccName.filltextbox(commandT);
            //            txtBox.AutoCompleteCustomSource = objAccName.productname;
            //        }
            //    }
            //} 
        }

        public bool Validation()
        {
            
            if (cmbCustName.Text == "")
            {
                MessageBox.Show("Customer Name cannot be blank.");
                return false;
            }
            if (cmbSalesman.Text == "")
            {
                MessageBox.Show("Saleman Name cannot be blank.");
                return false;
            }
            if (dataGridView1.RowCount <= 1)
            {
                MessageBox.Show("Item Detail section cannot be blank.");
            }
            return true;
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Validation() == true)
            {
                string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();

                if (ans == "Yes")
                {
                    try
                    {
                        try
                        {
                            string str5 = "select dbo.[GenSalesBillNo] ('" + dtpBill_Date.Value.ToString("MM/dd/yyyy") + "','" + company + "')";
                            dr = ObjCon.getData(str5);
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    txtSaleBill.Text = dr[0].ToString();
                                }
                            }
                            dr.Close();
                        }
                        catch
                        {

                        }
                        string conString = "Data Source=.;Initial Catalog=Neemali_Natural;User ID=sa;Password=preet@1234";
                        con = new SqlConnection(conString);
                        con.Open();

                        if (cmbPaymentMode.Text == "Cash")
                        {
                            cmd = new SqlCommand("SalesBillSave", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@SalesBillno", txtSaleBill.Text));
                            cmd.Parameters.Add(new SqlParameter("@CustName", cmbCustName.Text));
                            cmd.Parameters.Add(new SqlParameter("@Mobile", txtMob.Text));
                            cmd.Parameters.Add(new SqlParameter("@Email", txtemail.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_state", cmbState.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_GST_No", txtgstno.Text));
                            cmd.Parameters.Add(new SqlParameter("@BillDate", dtpBill_Date.Value.ToString("MM/dd/yyyy")));
                            cmd.Parameters.Add(new SqlParameter("@PaymentMode", cmbPaymentMode.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalAmt", lblTot_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Discount_amt", TxtDiscAmt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Cgst_Amt", lblcgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Sgst_Amt", lblsgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Igst_Amt", lbligstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalGST", lblTotalgst.Text));
                            cmd.Parameters.Add(new SqlParameter("@NetAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Balance", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@RecAmt", "0.00"));
                            cmd.Parameters.Add(new SqlParameter("@Advance", "0"));
                            cmd.Parameters.Add(new SqlParameter("@Company", company));
                            cmd.Parameters.Add(new SqlParameter("@DeleteFlag", "No"));
                            cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                            cmd.Parameters.Add(new SqlParameter("@Salesman", cmbSalesman.Text));
                            cmd.Parameters.Add(new SqlParameter("@CashAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@CardAmt", "0"));
                            cmd.Parameters.Add(new SqlParameter("@TWt_ingm", txtweightGm.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_inKg", txtweightKG.Text));
                            cmd.ExecuteNonQuery();
                        }
                        else if (cmbPaymentMode.Text == "Credit/Debit Card")
                        {
                            cmd = new SqlCommand("SalesBillSave", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@SalesBillno", txtSaleBill.Text));
                            cmd.Parameters.Add(new SqlParameter("@CustName", cmbCustName.Text));
                            cmd.Parameters.Add(new SqlParameter("@Mobile", txtMob.Text));
                            cmd.Parameters.Add(new SqlParameter("@Email", txtemail.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_state", cmbState.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_GST_No", txtgstno.Text));
                            cmd.Parameters.Add(new SqlParameter("@BillDate", dtpBill_Date.Value.ToString("MM/dd/yyyy")));
                            cmd.Parameters.Add(new SqlParameter("@PaymentMode", cmbPaymentMode.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalAmt", lblTot_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Discount_amt", TxtDiscAmt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Cgst_Amt", lblcgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Sgst_Amt", lblsgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Igst_Amt", lbligstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalGST", lblTotalgst.Text));
                            cmd.Parameters.Add(new SqlParameter("@NetAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Balance", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@RecAmt", "0.00"));
                            cmd.Parameters.Add(new SqlParameter("@Advance", "0"));
                            cmd.Parameters.Add(new SqlParameter("@Company", company));
                            cmd.Parameters.Add(new SqlParameter("@DeleteFlag", "No"));
                            cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                            cmd.Parameters.Add(new SqlParameter("@Salesman", cmbSalesman.Text));
                            cmd.Parameters.Add(new SqlParameter("@CashAmt", "0"));
                            cmd.Parameters.Add(new SqlParameter("@CardAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_ingm", txtweightGm.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_inKg", txtweightKG.Text));
                            cmd.ExecuteNonQuery();
                        }
                        else if (cmbPaymentMode.Text == "Cash & Card")
                        {
                            cmd = new SqlCommand("SalesBillSave", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@SalesBillno", txtSaleBill.Text));
                            cmd.Parameters.Add(new SqlParameter("@CustName", cmbCustName.Text));
                            cmd.Parameters.Add(new SqlParameter("@Mobile", txtMob.Text));
                            cmd.Parameters.Add(new SqlParameter("@Email", txtemail.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_state", cmbState.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_GST_No", txtgstno.Text));
                            cmd.Parameters.Add(new SqlParameter("@BillDate", dtpBill_Date.Value.ToString("MM/dd/yyyy")));
                            cmd.Parameters.Add(new SqlParameter("@PaymentMode", cmbPaymentMode.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalAmt", lblTot_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Discount_amt", TxtDiscAmt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Cgst_Amt", lblcgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Sgst_Amt", lblsgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Igst_Amt", lbligstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalGST", lblTotalgst.Text));
                            cmd.Parameters.Add(new SqlParameter("@NetAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Balance", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@RecAmt", "0.00"));
                            cmd.Parameters.Add(new SqlParameter("@Advance", "0"));
                            cmd.Parameters.Add(new SqlParameter("@Company", company));
                            cmd.Parameters.Add(new SqlParameter("@DeleteFlag", "No"));
                            cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                            cmd.Parameters.Add(new SqlParameter("@Salesman", cmbSalesman.Text));
                            cmd.Parameters.Add(new SqlParameter("@CashAmt", txtcashamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@CardAmt", txtcardamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_ingm", txtweightGm.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_inKg", txtweightKG.Text));
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd = new SqlCommand("SalesBillSave", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@SalesBillno", txtSaleBill.Text));
                            cmd.Parameters.Add(new SqlParameter("@CustName", cmbCustName.Text));
                            cmd.Parameters.Add(new SqlParameter("@Mobile", txtMob.Text));
                            cmd.Parameters.Add(new SqlParameter("@Email", txtemail.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_state", cmbState.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_GST_No", txtgstno.Text));
                            cmd.Parameters.Add(new SqlParameter("@BillDate", dtpBill_Date.Value.ToString("MM/dd/yyyy")));
                            cmd.Parameters.Add(new SqlParameter("@PaymentMode", cmbPaymentMode.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalAmt", lblTot_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Discount_amt", TxtDiscAmt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Cgst_Amt", lblcgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Sgst_Amt", lblsgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Igst_Amt", lbligstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalGST", lblTotalgst.Text));
                            cmd.Parameters.Add(new SqlParameter("@NetAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Balance", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@RecAmt", "0.00"));
                            cmd.Parameters.Add(new SqlParameter("@Advance", "0"));
                            cmd.Parameters.Add(new SqlParameter("@Company", company));
                            cmd.Parameters.Add(new SqlParameter("@DeleteFlag", "No"));
                            cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                            cmd.Parameters.Add(new SqlParameter("@Salesman", cmbSalesman.Text));
                            cmd.Parameters.Add(new SqlParameter("@CashAmt", "0"));
                            cmd.Parameters.Add(new SqlParameter("@CardAmt", "0"));
                            cmd.Parameters.Add(new SqlParameter("@TWt_ingm", txtweightGm.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_inKg", txtweightKG.Text));
                            cmd.ExecuteNonQuery();
                        }
                        try
                        {
                            string strfk = "select ISNULL(MAX([SalesBillPk]),0) FROM [Sales]";
                            dr = ObjCon.getData(strfk);
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    purchasePK = dr[0].ToString();
                                }
                            }
                            dr.Close();
                        }
                        catch { }

                        int row = Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1;
                        for (int i = 0; i < row; i++)
                        {
                            try
                            {

                                cmd = new SqlCommand("SubSaleBillSave", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add(new SqlParameter("@Purchase_Fk", purchasePK));
                                cmd.Parameters.Add(new SqlParameter("@SubSalesBillno", txtSaleBill.Text));
                                cmd.Parameters.Add(new SqlParameter("@ItemName", dataGridView1.Rows[i].Cells["Column1"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Barcode", dataGridView1.Rows[i].Cells["barcode"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Group1", dataGridView1.Rows[i].Cells["group"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@size", dataGridView1.Rows[i].Cells["size"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@unit1", dataGridView1.Rows[i].Cells["unit"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Qty", dataGridView1.Rows[i].Cells["qty1"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@MRP1", dataGridView1.Rows[i].Cells["mrp"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Amt", dataGridView1.Rows[i].Cells["amount1"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@AmtWoGST", dataGridView1.Rows[i].Cells["amtWogst"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Discount", dataGridView1.Rows[i].Cells["Disc_amt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@cgsttax", dataGridView1.Rows[i].Cells["cgst_per"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@cgstamt", dataGridView1.Rows[i].Cells["cgst_amt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@sgsttax", dataGridView1.Rows[i].Cells["sgst_per"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@sgstamt", dataGridView1.Rows[i].Cells["sgst_amt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@igsttax", dataGridView1.Rows[i].Cells["igst_per"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@igstamt", dataGridView1.Rows[i].Cells["igst_amt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@GST_amt", dataGridView1.Rows[i].Cells["gst_amt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@GST_per", dataGridView1.Rows[i].Cells["GST_per"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Newmrp1", dataGridView1.Rows[i].Cells["new_mrp"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@totalamount", dataGridView1.Rows[i].Cells["Tamt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Returned", "No"));
                                cmd.Parameters.Add(new SqlParameter("@Flag", "No"));
                                cmd.Parameters.Add(new SqlParameter("@Company", company));
                                cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                                cmd.Parameters.Add(new SqlParameter("@Disc_AMT", dataGridView1.Rows[i].Cells["discamt1"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Ship_weight1", dataGridView1.Rows[i].Cells["shipping_wt1"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@SKU_Code", dataGridView1.Rows[i].Cells["skucode"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Rate", dataGridView1.Rows[i].Cells["rate1"].Value.ToString()));
                                cmd.ExecuteNonQuery();
                            }
                            catch
                            {
                            }
                        }
                        if (cmbCustName.Text != "")
                        {
                            if (flag == 0)
                            {
                                string save3 = "INSERT INTO [Customer]([CustName],[Mobile],[EmailId],[Company],[Gst_No],[State],[Enter_by],[Enter_date])VALUES" +
                                    "('" + cmbCustName.Text + "','" + txtMob.Text + "','" + txtemail.Text + "','" + company + "','" + txtgstno.Text + "','" + cmbState.Text + "','" + username + "','"+System.DateTime.Now.ToString("MM/dd/yyyy")+"')";
                                affect = ObjCon.affect(save3);
                            }
                        }

                        MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //SMS
                        //if (chkSMS.Checked == true)
                        //{
                        //    string NO = txtMob.Text;
                        //    sendsms1(NO);
                        //}
                        clear();
                    }
                    catch { }
                }
            }
        }
        public void sendsms1(string NO)
        {
            string result = "";
            string sendToPhoneNumber = NO;
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                   string smsContain = "";
                // string s=txtEnqNo.Text.Replace(@"\", "%5C%5C");
               // string s = txtEnqNo.Text.Replace(@"\", "%2F");
                   smsContain = "Hello " + cmbCustName.Text + ", Thank you for your purchase! Your total amount is Rs. " + lblNet_Amt.Text + ". Please visit us again and have a nice day!";

                smsContain = smsContain.Replace("&", "+%26+");
               

                String url = "http://fast.admarksolution.com/vendorsms/pushsms.aspx?user=" + sms_user1 + "&password=" + sms_pass1 + "&msisdn=91" + sendToPhoneNumber + "&sid=" + sms_sid1 + "&msg=" + smsContain + "&fl=0&gwid=2";
                
                 
                request = WebRequest.Create(url);

                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new System.IO.StreamReader(stream, ec);

                result = reader.ReadToEnd();

                reader.Close();
                stream.Close();
            }
            catch (System.Exception exp)
            {
                MessageBox.Show("error" + exp.ToString());
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }


        public void clear()
        {
            cmbCustName.Text = "";
            txtemail.Text = "";
            txtgstno.Text = "";
            txtMob.Text = "";
            cmbSalesman.Text = "";
            cmbPaymentMode.Text = "";
            cmbState.Text = "";
            textBox3.Text = "0";
            txttotqty.Text = "0";
            lblcgstamt.Text = "0";
            lblsgstamt.Text = "0";
            lbligstamt.Text = "0";
            lblTot_Amt.Text = "0";
            lblTotalgst.Text = "0";
            lblNet_Amt.Text = "0";
            TxtDiscAmt.Text = "0";
            dataGridView1.Rows.Clear();
            cmbSalesman.Text = "";
            txtweightKG.Text = "";
            txtweightGm.Text = "";

        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (Validation() == true)
            {
                string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();

                if (ans == "Yes")
                {
                    try
                    {
                        try
                        {
                            string str5 = "select dbo.[GenSalesBillNo] ('" + dtpBill_Date.Value.ToString("MM/dd/yyyy") + "','" + company + "')";
                            dr = ObjCon.getData(str5);
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    txtSaleBill.Text = dr[0].ToString();
                                }
                            }
                            dr.Close();
                        }
                        catch
                        {

                        }

                        string conString = "Data Source=.;Initial Catalog=Neemali_Natural;User ID=sa;Password=preet@1234";
                        con = new SqlConnection(conString);
                        con.Open();
                        if (cmbPaymentMode.Text == "Cash")
                        {
                            cmd = new SqlCommand("SalesBillSave", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@SalesBillno", txtSaleBill.Text));
                            cmd.Parameters.Add(new SqlParameter("@CustName", cmbCustName.Text));
                            cmd.Parameters.Add(new SqlParameter("@Mobile", txtMob.Text));
                            cmd.Parameters.Add(new SqlParameter("@Email", txtemail.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_state", cmbState.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_GST_No", txtgstno.Text));
                            cmd.Parameters.Add(new SqlParameter("@BillDate", dtpBill_Date.Value.ToString("MM/dd/yyyy")));
                            cmd.Parameters.Add(new SqlParameter("@PaymentMode", cmbPaymentMode.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalAmt", lblTot_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Discount_amt", TxtDiscAmt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Cgst_Amt", lblcgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Sgst_Amt", lblsgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Igst_Amt", lbligstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalGST", lblTotalgst.Text));
                            cmd.Parameters.Add(new SqlParameter("@NetAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Balance", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@RecAmt", "0.00"));
                            cmd.Parameters.Add(new SqlParameter("@Advance", "0"));
                            cmd.Parameters.Add(new SqlParameter("@Company", company));
                            cmd.Parameters.Add(new SqlParameter("@DeleteFlag", "No"));
                            cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                            cmd.Parameters.Add(new SqlParameter("@Salesman", cmbSalesman.Text));
                            cmd.Parameters.Add(new SqlParameter("@CashAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@CardAmt", "0"));
                            cmd.Parameters.Add(new SqlParameter("@TWt_ingm", txtweightGm.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_inKg", txtweightKG.Text));

                            cmd.ExecuteNonQuery();
                        }
                        else if (cmbPaymentMode.Text == "Credit/Debit Card")
                        {
                            cmd = new SqlCommand("SalesBillSave", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@SalesBillno", txtSaleBill.Text));
                            cmd.Parameters.Add(new SqlParameter("@CustName", cmbCustName.Text));
                            cmd.Parameters.Add(new SqlParameter("@Mobile", txtMob.Text));
                            cmd.Parameters.Add(new SqlParameter("@Email", txtemail.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_state", cmbState.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_GST_No", txtgstno.Text));
                            cmd.Parameters.Add(new SqlParameter("@BillDate", dtpBill_Date.Value.ToString("MM/dd/yyyy")));
                            cmd.Parameters.Add(new SqlParameter("@PaymentMode", cmbPaymentMode.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalAmt", lblTot_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Discount_amt", TxtDiscAmt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Cgst_Amt", lblcgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Sgst_Amt", lblsgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Igst_Amt", lbligstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalGST", lblTotalgst.Text));
                            cmd.Parameters.Add(new SqlParameter("@NetAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Balance", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@RecAmt", "0.00"));
                            cmd.Parameters.Add(new SqlParameter("@Advance", "0"));
                            cmd.Parameters.Add(new SqlParameter("@Company", company));
                            cmd.Parameters.Add(new SqlParameter("@DeleteFlag", "No"));
                            cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                            cmd.Parameters.Add(new SqlParameter("@Salesman", cmbSalesman.Text));
                            cmd.Parameters.Add(new SqlParameter("@CashAmt", "0"));
                            cmd.Parameters.Add(new SqlParameter("@CardAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_ingm", txtweightGm.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_inKg", txtweightKG.Text));

                            cmd.ExecuteNonQuery();
                        }
                        else if (cmbPaymentMode.Text == "Cash & Card")
                        {
                            cmd = new SqlCommand("SalesBillSave", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@SalesBillno", txtSaleBill.Text));
                            cmd.Parameters.Add(new SqlParameter("@CustName", cmbCustName.Text));
                            cmd.Parameters.Add(new SqlParameter("@Mobile", txtMob.Text));
                            cmd.Parameters.Add(new SqlParameter("@Email", txtemail.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_state", cmbState.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_GST_No", txtgstno.Text));
                            cmd.Parameters.Add(new SqlParameter("@BillDate", dtpBill_Date.Value.ToString("MM/dd/yyyy")));
                            cmd.Parameters.Add(new SqlParameter("@PaymentMode", cmbPaymentMode.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalAmt", lblTot_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Discount_amt", TxtDiscAmt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Cgst_Amt", lblcgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Sgst_Amt", lblsgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Igst_Amt", lbligstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalGST", lblTotalgst.Text));
                            cmd.Parameters.Add(new SqlParameter("@NetAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Balance", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@RecAmt", "0.00"));
                            cmd.Parameters.Add(new SqlParameter("@Advance", "0"));
                            cmd.Parameters.Add(new SqlParameter("@Company", company));
                            cmd.Parameters.Add(new SqlParameter("@DeleteFlag", "No"));
                            cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                            cmd.Parameters.Add(new SqlParameter("@Salesman", cmbSalesman.Text));
                            cmd.Parameters.Add(new SqlParameter("@CashAmt", txtcashamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@CardAmt", txtcardamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_ingm", txtweightGm.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_inKg", txtweightKG.Text));

                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd = new SqlCommand("SalesBillSave", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@SalesBillno", txtSaleBill.Text));
                            cmd.Parameters.Add(new SqlParameter("@CustName", cmbCustName.Text));
                            cmd.Parameters.Add(new SqlParameter("@Mobile", txtMob.Text));
                            cmd.Parameters.Add(new SqlParameter("@Email", txtemail.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_state", cmbState.Text));
                            cmd.Parameters.Add(new SqlParameter("@cust_GST_No", txtgstno.Text));
                            cmd.Parameters.Add(new SqlParameter("@BillDate", dtpBill_Date.Value.ToString("MM/dd/yyyy")));
                            cmd.Parameters.Add(new SqlParameter("@PaymentMode", cmbPaymentMode.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalAmt", lblTot_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Discount_amt", TxtDiscAmt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Cgst_Amt", lblcgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Sgst_Amt", lblsgstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Igst_Amt", lbligstamt.Text));
                            cmd.Parameters.Add(new SqlParameter("@TotalGST", lblTotalgst.Text));
                            cmd.Parameters.Add(new SqlParameter("@NetAmt", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@Balance", lblNet_Amt.Text));
                            cmd.Parameters.Add(new SqlParameter("@RecAmt", "0.00"));
                            cmd.Parameters.Add(new SqlParameter("@Advance", "0"));
                            cmd.Parameters.Add(new SqlParameter("@Company", company));
                            cmd.Parameters.Add(new SqlParameter("@DeleteFlag", "No"));
                            cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                            cmd.Parameters.Add(new SqlParameter("@Salesman", cmbSalesman.Text));
                            cmd.Parameters.Add(new SqlParameter("@CashAmt", "0"));
                            cmd.Parameters.Add(new SqlParameter("@CardAmt", "0"));
                            cmd.Parameters.Add(new SqlParameter("@TWt_ingm", txtweightGm.Text));
                            cmd.Parameters.Add(new SqlParameter("@TWt_inKg", txtweightKG.Text));

                            cmd.ExecuteNonQuery();
                        }

                        try
                        {
                            string strfk = "select ISNULL(MAX([SalesBillPk]),0) FROM [Sales]";
                            dr = ObjCon.getData(strfk);
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    purchasePK = dr[0].ToString();
                                }
                            }
                            dr.Close();
                        }
                        catch { }

                        int row = Convert.ToInt32(dataGridView1.RowCount.ToString()) - 1;
                        for (int i = 0; i < row; i++)
                        {
                            try
                            {

                                cmd = new SqlCommand("SubSaleBillSave", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add(new SqlParameter("@Purchase_Fk", purchasePK));
                                cmd.Parameters.Add(new SqlParameter("@SubSalesBillno", txtSaleBill.Text));
                                cmd.Parameters.Add(new SqlParameter("@ItemName", dataGridView1.Rows[i].Cells["Column1"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Barcode", dataGridView1.Rows[i].Cells["barcode"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Group1", dataGridView1.Rows[i].Cells["group"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@size", dataGridView1.Rows[i].Cells["size"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@unit1", dataGridView1.Rows[i].Cells["unit"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Qty", dataGridView1.Rows[i].Cells["qty1"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@MRP1", dataGridView1.Rows[i].Cells["mrp"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Amt", dataGridView1.Rows[i].Cells["amount1"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@AmtWoGST", dataGridView1.Rows[i].Cells["amtWogst"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Discount", dataGridView1.Rows[i].Cells["Disc_amt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@cgsttax", dataGridView1.Rows[i].Cells["cgst_per"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@cgstamt", dataGridView1.Rows[i].Cells["cgst_amt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@sgsttax", dataGridView1.Rows[i].Cells["sgst_per"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@sgstamt", dataGridView1.Rows[i].Cells["sgst_amt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@igsttax", dataGridView1.Rows[i].Cells["igst_per"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@igstamt", dataGridView1.Rows[i].Cells["igst_amt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@GST_amt", dataGridView1.Rows[i].Cells["gst_amt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@GST_per", dataGridView1.Rows[i].Cells["GST_per"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Newmrp1", dataGridView1.Rows[i].Cells["new_mrp"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@totalamount", dataGridView1.Rows[i].Cells["Tamt"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Returned", "No"));
                                cmd.Parameters.Add(new SqlParameter("@Flag", "No"));
                                cmd.Parameters.Add(new SqlParameter("@Company", company));
                                cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                                cmd.Parameters.Add(new SqlParameter("@Disc_AMT", dataGridView1.Rows[i].Cells["discamt1"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Ship_weight1", dataGridView1.Rows[i].Cells["shipping_wt1"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@SKU_Code", dataGridView1.Rows[i].Cells["skucode"].Value.ToString()));
                                cmd.Parameters.Add(new SqlParameter("@Rate", dataGridView1.Rows[i].Cells["rate1"].Value.ToString()));
                                cmd.ExecuteNonQuery();
                            }
                            catch
                            {
                            }
                        }

                        if (cmbCustName.Text != "")
                        {
                            if (flag == 0)
                            {
                                string save3 = "INSERT INTO [Customer]([CustName],[Mobile],[EmailId],[Company],[Gst_No],[State],[Enter_by],[Enter_date])VALUES" +
                                    "('" + cmbCustName.Text + "','" + txtMob.Text + "','" + txtemail.Text + "','" + company + "','" + txtgstno.Text + "','" + cmbState.Text + "','" + username + "','"+System.DateTime.Now.ToString("MM/dd/yyyy")+"')";
                                affect = ObjCon.affect(save3);
                            }
                        }

                        MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //SMS
                        //if (chkSMS.Checked == true)
                        //{
                        //    string NO = txtMob.Text;
                        //    sendsms1(NO);
                        //}

                        clear();
                        try
                        {
                            //newSaleBillPrint1.SetDatabaseLogon("sa", "preet@1234", ".", "Neemali_Natural");
                            //newSaleBillPrint1.SetParameterValue("@Bill_No", txtSaleBill.Text);
                            //newSaleBillPrint1.SetParameterValue("@company", company);
                            //newSaleBillPrint1.PrintToPrinter(1, true, 1, 50);
                        }
                        catch { }
                    }
                    catch { }
                }
            }
        }

        private void TxtDiscAmt_TextChanged(object sender, EventArgs e)
        {
            //DataGridViewRowsRemovedEventArgs ee = null;
            //if (TxtDiscAmt.Text == "")
            //{
            //    TxtDiscAmt.Text = "0";
            //}
            //else if(TxtDiscAmt.Text != "" || TxtDiscAmt.Text != "0")
            //{
            //    dataGridView1_RowsRemoved(dataGridView1, ee);
            //}
            
        }

        private void cmbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPaymentMode.Text == "Cash & Card")
            {
                panelcashcard.Visible = true;
            }
            else {

                panelcashcard.Visible = false;
            }
        }

        private void cmbCustName_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void txtcashamt_TextChanged(object sender, EventArgs e)
        {
            double net_amt = Convert.ToDouble(lblNet_Amt.Text);
            txtcardamt.Text = String.Format("{0:0.00}", net_amt - double.Parse(txtcashamt.Text));
        }

        private void txtMob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void cmbCustName_Leave(object sender, EventArgs e)
        {
            if (cmbCustName.Text != null)
            {
                if (cmbCustName.Text != "")
                {
                    try
                    {

                        string commandt5 = "select [CustName] from [Customer] where Ltrim([CustName]) = '"+cmbCustName.Text+"'";
                        dr = ObjCon.getData(commandt5);
                        if (dr.HasRows)
                        {
                            flag = 1;
                        }
                        dr.Close();


                        if (flag == 1)
                        {
                            commandT = "select [Mobile],[EmailId],[Gst_No],[State] from [Customer] where [CustName] = '" + cmbCustName.Text + "'";
                            dr = ObjCon.getData(commandT);
                            if (dr.HasRows == true)
                            {
                                while (dr.Read())
                                {
                                    txtemail.Text = dr["EmailId"].ToString();
                                    txtMob.Text = dr["Mobile"].ToString();
                                    cmbState.Text = dr["State"].ToString().ToUpper();
                                    txtgstno.Text = dr["Gst_No"].ToString();
                                }
                                dr.Close();
                            }
                            txtemail.ReadOnly = true;
                            txtMob.ReadOnly = true;
                            txtgstno.ReadOnly = true;
                            cmbState.Enabled = false;
                        }
                        else
                        {
                            txtemail.Text = "";
                            txtMob.Text = "";
                            txtgstno.Text = "";
                            txtemail.ReadOnly = false;
                            txtMob.ReadOnly = false;
                            txtgstno.ReadOnly = false;
                            cmbState.Enabled = true;
                            cmbState.SelectedIndex = 26;
                        }
                    }
                    catch { }
                }
                else
                {
                    txtemail.ReadOnly = true;
                    txtMob.ReadOnly = true;
                    txtgstno.ReadOnly = true;
                    cmbState.Enabled = false;
                }
            }
            else
            {
                txtemail.ReadOnly = true;
                txtMob.ReadOnly = true;
                txtgstno.ReadOnly = true;
                cmbState.Enabled = false;
            }
       }

        private void chkSMS_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtgstno_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;

            if (txtgstno.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"\d{2}[A-Z]{5}\d{4}[A-Z]{1}[0-9A-Z]{1}[A-Z]{1}[0-9A-Z]{1}$");

                if (!mRegxExpression.IsMatch(txtgstno.Text.Trim()))
                {

                    MessageBox.Show("Invaid GST No");

                    txtgstno.Focus();
                    txtgstno.Text = "";
                    cmbState.Text = "";
                }
                else
                {
                    string gstin = txtgstno.Text;
                    string code = gstin.Substring(0, 2).ToString();


                    string sel = "Select [State_Code] from [State_Code_Master] where [State_Code] like '" + code + "%'";
                    dr = ObjCon.getData(sel);
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            cmbState.Text = dr["State_Code"].ToString().ToUpper().ToUpper();

                        }
                    }
                    dr.Close();
                }

            }
        }
    }
}
