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
    public partial class Incomp_PO_PurchaseEntry : Form
    {
        string username, company, connectionString, PONo1;
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectDb ObjCon = new ConnectDb();
        SqlDataReader dr = null;
        SqlDataReader dr1 = null;
        int affect, flag1 = 0;
        string purchaseOrderPK, Sup_id;
        AutoComplete objAccName = new AutoComplete();
        string company_state, companyname, commandT;
        int sup_id, sum, m = 0;
        string T_BalQTY;

        public Incomp_PO_PurchaseEntry(string username, string company,string connectionString,string PONo1)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
            this.PONo1 = PONo1;
        }

        private void Incomp_PO_PurchaseEntry_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txt_EacCode;
            panelcashcard.Visible = false;
            panelcheque.Visible = false;
            cmbPayMode.SelectedIndex = 0;
            DataGridViewRowsRemovedEventArgs ee = null;
            try
            {
                string str1 = "select [Name] FROM [WareHouse_Master] order by [GID] asc";
                dr = ObjCon.getData(str1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbwarehouse.Items.Add(dr[0].ToString());
                    }
                }
                dr.Close();
            }
            catch
            { }
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("exec [Incomplete_Po] '" + PONo1 + "'", con);
                
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtpur_billno.Text = dr["Pur_BillNo1"].ToString();
                        dateTimePicker1.Text = dr["Pur_BillDate1"].ToString();
                        txtPONo.Text = dr["PO_No1"].ToString();
                        dtpPO_Date.Text = dr["Po_Date1"].ToString();
                        cmbSupName.Text = dr["SupName1"].ToString();
                        sup_id = Convert.ToInt32(dr["SupId1"].ToString());
                        txtMob.Text = dr["Mobile1"].ToString();
                        cmbState.Text = dr["sup_state1"].ToString();
                        txtgstno.Text = dr["sup_gst_no1"].ToString();
                        txtemail.Text = dr["sup_email1"].ToString();
                        txtNarration.Text = dr["Narration1"].ToString();
                        txttotqty.Text = dr["TQty1"].ToString();
                        txtTPOQty.Text = dr["TPoQty1"].ToString();
                        T_BalQTY = dr["TBalPoQty1"].ToString();
                        lblTot_Amt.Text = dr["TotalAmt1"].ToString();
                        lblTotalgst.Text = dr["TotalGST1"].ToString();
                        lblcgstamt.Text = dr["CGSTAmt1"].ToString();
                        lblsgstamt.Text = dr["SGSTAmt1"].ToString();
                        lbligstamt.Text = dr["IGSTAmt1"].ToString();
                        lblNet_Amt.Text = dr["NetAmt1"].ToString();
                        cmbPayMode.Text = dr["PaymentMode1"].ToString();
                        txtCashamt.Text = dr["CashAmt1"].ToString();
                        txtCardamt.Text = dr["CardAmt1"].ToString();
                        txtCheque.Text = dr["ChequeNo1"].ToString();
                        cmbwarehouse.Text = dr["Warehouse1"].ToString();
                    }
                }
                dr.Close();
            }
            catch
            { }
            
            try
            {
                dataGridView1_RowsRemoved(dataGridView1, ee);
                int count = 0;

                cmd = new SqlCommand("exec [Incomplete_Sub_Po] '" + PONo1 + "'", con);
                //cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[count].Cells["barcode1"].Value = dr["EAC_code1"].ToString();
                        dataGridView1.Rows[count].Cells["Column1"].Value = dr["P_Name1"].ToString();
                        dataGridView1.Rows[count].Cells["group"].Value = dr["Groups1"].ToString();
                        dataGridView1.Rows[count].Cells["unit"].Value = dr["unit11"].ToString();
                        dataGridView1.Rows[count].Cells["qty1"].Value = dr["Qty1"].ToString();
                        dataGridView1.Rows[count].Cells["Po_Qty"].Value = dr["PoQty1"].ToString();
                        dataGridView1.Rows[count].Cells["bal_po_qty"].Value = dr["BalpoQty1"].ToString();
                        dataGridView1.Rows[count].Cells["rate1"].Value = dr["Rate1"].ToString();
                        dataGridView1.Rows[count].Cells["disc"].Value = dr["Disc_per1"].ToString();
                        dataGridView1.Rows[count].Cells["disc_amt"].Value = dr["Disc_amt1"].ToString();
                        dataGridView1.Rows[count].Cells["amount1"].Value = dr["Amount1"].ToString();
                        dataGridView1.Rows[count].Cells["GST_per"].Value = dr["GST_per1"].ToString();
                        dataGridView1.Rows[count].Cells["gst_amt"].Value = dr["gst_amt1"].ToString();
                        dataGridView1.Rows[count].Cells["Tamt"].Value = dr["total_amount1"].ToString();
                        dataGridView1.Rows[count].Cells["cgstper1"].Value = dr["cgsttax1"].ToString();
                        dataGridView1.Rows[count].Cells["sgstper1"].Value = dr["sgsttax1"].ToString();
                        dataGridView1.Rows[count].Cells["igstper1"].Value = dr["igsttax1"].ToString();
                        dataGridView1.Rows[count].Cells["cgstamt1"].Value = dr["S_cgstamt1"].ToString();
                        dataGridView1.Rows[count].Cells["sgstamt1"].Value = dr["S_sgstamt1"].ToString();
                        dataGridView1.Rows[count].Cells["igstamt1"].Value = dr["S_igstamt1"].ToString();
                        dataGridView1.Rows[count].Cells["rack1"].Value = dr["Rack1"].ToString();
                        count++;
                    }
                }
                dr.Close();
                dataGridView1_RowsRemoved(dataGridView1, ee);
            }
            catch
            {
                con.Close();
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rows = Convert.ToInt32(dataGridView1.RowCount.ToString()) - 2;
            DataGridViewRowsRemovedEventArgs ee = null;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "barcode1")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column1")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "group")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "unit")
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
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Tamt")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Po_Qty")
            {
                SendKeys.Send("{TAB}");
            }
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "qty1")
            //{
            //    SendKeys.Send("{TAB}");
            //}
            if (dataGridView1.Columns[e.ColumnIndex].Name == "GST_per")
            {
                SendKeys.Send("{TAB}");
            }

            if (e.ColumnIndex == 5)
            {
                try
                {
                    if (dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString() != "" || dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString() != "0")
                    {
                        if (cmbState.Text == company_state)
                        {
                            dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc"].Value.ToString())) / 100));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()))) - (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value.ToString()))));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))) / 100));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["cgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["sgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["cgstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["sgstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString())));
                        }
                        else
                        {
                            dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc"].Value.ToString())) / 100));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()))) - (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value.ToString()))));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))) / 100));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["igstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString())));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["igstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString())));

                            dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString())));
                        }
                        dataGridView1_RowsRemoved(dataGridView1, ee);
                    }
                }
                catch { }
            }
            if (e.ColumnIndex == 7)
            {
                try
                {
                    if (cmbState.Text == company_state)
                    {
                        dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc"].Value.ToString())) / 100));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()))) - (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value.ToString()))));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))) / 100));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["cgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()) / 2));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["sgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()) / 2));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["cgstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()) / 2));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["sgstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()) / 2));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString())));
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc"].Value.ToString())) / 100));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()))) - (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value.ToString()))));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))) / 100));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["igstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString())));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["igstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString())));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString())));
                    }
                    dataGridView1_RowsRemoved(dataGridView1, ee);
                }
                catch { }
            }
            if (e.ColumnIndex == 8)
            {
                try
                {
                    if (cmbState.Text == company_state)
                    {
                        dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc"].Value.ToString())) / 100));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()))) - (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value.ToString()))));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))) / 100));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["cgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()) / 2));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["sgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()) / 2));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["cgstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()) / 2));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["sgstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString()) / 2));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString())));
                    }
                    else
                    {
                        dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc"].Value.ToString())) / 100));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()))) - (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value.ToString()))));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString()))) / 100));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["igstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["GST_per"].Value.ToString())));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["igstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString())));

                        dataGridView1.Rows[e.RowIndex - 1].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["gst_amt"].Value.ToString())));
                    }
                    dataGridView1_RowsRemoved(dataGridView1, ee);
                }
                catch { }
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Double Mtr = 0, CgstAmt = 0, IgstAmt = 0, SgstAmt = 0, QTY1 = 0, GSTAMT = 0;
            Double TPoQty = 0, BalpoQty = 0;
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
                    if (dataGridView1.Rows[i].Cells["cgstamt1"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["cgstamt1"].Value.ToString() != "")
                        {
                            CgstAmt += Double.Parse(dataGridView1.Rows[i].Cells["cgstamt1"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["igstamt1"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["igstamt1"].Value.ToString() != "")
                        {
                            IgstAmt += Double.Parse(dataGridView1.Rows[i].Cells["igstamt1"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["sgstamt1"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["sgstamt1"].Value.ToString() != "")
                        {
                            SgstAmt += Double.Parse(dataGridView1.Rows[i].Cells["sgstamt1"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["gst_amt"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["gst_amt"].Value.ToString() != "")
                        {
                            GSTAMT += Double.Parse(dataGridView1.Rows[i].Cells["gst_amt"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["Po_Qty"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["Po_Qty"].Value.ToString() != "")
                        {
                            TPoQty += Double.Parse(dataGridView1.Rows[i].Cells["Po_Qty"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["bal_po_qty"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["bal_po_qty"].Value.ToString() != "")
                        {
                            BalpoQty += Double.Parse(dataGridView1.Rows[i].Cells["bal_po_qty"].Value.ToString());
                        }
                    }
                }
                catch { }
            }

            txtTPOQty.Text = String.Format("{0:0.00}", double.Parse(TPoQty.ToString()));
            txtBalQty.Text = String.Format("{0:0.00}", double.Parse(BalpoQty.ToString()));
            textBox3.Text = String.Format("{0:0.00}", double.Parse(rows.ToString()));
            txttotqty.Text = String.Format("{0:0.00}", double.Parse(QTY1.ToString()));
            lblTot_Amt.Text = String.Format("{0:0.00}", double.Parse(Mtr.ToString()));
            lblcgstamt.Text = String.Format("{0:0.00}", double.Parse(CgstAmt.ToString()));
            lbligstamt.Text = String.Format("{0:0.00}", double.Parse(IgstAmt.ToString()));
            lblsgstamt.Text = String.Format("{0:0.00}", double.Parse(SgstAmt.ToString()));
            lblTotalgst.Text = String.Format("{0:0.00}", double.Parse(GSTAMT.ToString()));
            lblNet_Amt.Text = String.Format("{0:0.00}", (Double.Parse(lblTot_Amt.Text) + Double.Parse(lblTotalgst.Text)), 2);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString().ToUpper();
                e.FormattingApplied = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_EacCode_Leave(object sender, EventArgs e)
        {
            if (txt_EacCode.Text != "")
            {
                try
                {
                    DataGridViewRowsRemovedEventArgs ee = null;
                    sum = 0;
                    double balsku = 0;

                    string i = txt_EacCode.Text.TrimEnd().TrimStart();

                    for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                    {
                        if (i == dataGridView1.Rows[j].Cells["barcode1"].Value.ToString())
                        {
                            m = j;

                            int a = (Convert.ToInt32(dataGridView1.Rows[m].Cells["qty1"].Value.ToString()));

                            int c = (Convert.ToInt32(dataGridView1.Rows[m].Cells["Po_Qty"].Value.ToString()));

                            int d = (Convert.ToInt32(dataGridView1.Rows[m].Cells["bal_po_qty"].Value.ToString()));

                            if (d > 0)
                            {
                                if (a < c)
                                {
                                    sum = sum + Convert.ToInt32(dataGridView1.Rows[m].Cells["qty1"].Value.ToString());

                                    balsku = balsku + Convert.ToInt32(dataGridView1.Rows[m].Cells["bal_po_qty"].Value.ToString());

                                    if (sum < c)
                                    {
                                        dataGridView1.Rows[m].Cells["qty1"].Value = (Convert.ToInt32(dataGridView1.Rows[m].Cells["qty1"].Value.ToString()) + 1).ToString();

                                        dataGridView1.Rows[m].Cells["Bal_Po_Qty"].Value = (Convert.ToInt32(dataGridView1.Rows[m].Cells["bal_po_qty"].Value.ToString()) - 1).ToString();

                                        if (dataGridView1.Rows[m].Cells["rate1"].Value.ToString() != "" || dataGridView1.Rows[m].Cells["rate1"].Value.ToString() != "0")
                                        {
                                            if (cmbState.Text == company_state)
                                            {
                                                dataGridView1.Rows[m].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[m].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[m].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[m].Cells["disc"].Value.ToString())) / 100));

                                                dataGridView1.Rows[m].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[m].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[m].Cells["rate1"].Value.ToString()))) - (Double.Parse(dataGridView1.Rows[m].Cells["disc_amt"].Value.ToString()))));

                                                dataGridView1.Rows[m].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[m].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[m].Cells["GST_per"].Value.ToString()))) / 100));

                                                dataGridView1.Rows[m].Cells["cgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[m].Cells["GST_per"].Value.ToString()) / 2));

                                                dataGridView1.Rows[m].Cells["sgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[m].Cells["GST_per"].Value.ToString()) / 2));

                                                dataGridView1.Rows[m].Cells["cgstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[m].Cells["gst_amt"].Value.ToString()) / 2));

                                                dataGridView1.Rows[m].Cells["sgstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[m].Cells["gst_amt"].Value.ToString()) / 2));

                                                dataGridView1.Rows[m].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[m].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[m].Cells["gst_amt"].Value.ToString())));
                                            }
                                            else
                                            {
                                                dataGridView1.Rows[m].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[m].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[m].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[m].Cells["disc"].Value.ToString())) / 100));

                                                dataGridView1.Rows[m].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[m].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[m].Cells["rate1"].Value.ToString()))) - (Double.Parse(dataGridView1.Rows[m].Cells["disc_amt"].Value.ToString()))));

                                                dataGridView1.Rows[m].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[m].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[m].Cells["GST_per"].Value.ToString()))) / 100));

                                                dataGridView1.Rows[m].Cells["igstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[m].Cells["GST_per"].Value.ToString())));

                                                dataGridView1.Rows[m].Cells["igstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[m].Cells["gst_amt"].Value.ToString())));

                                                dataGridView1.Rows[m].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[m].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[m].Cells["gst_amt"].Value.ToString())));
                                            }

                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Quantity is exceeding...", "OK", MessageBoxButtons.OK);
                                    }
                                    this.ActiveControl = txt_EacCode;
                                }
                                else
                                {
                                    MessageBox.Show("Quantity is exceeding...", "OK", MessageBoxButtons.OK);
                                    this.ActiveControl = txt_EacCode;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Out of Stock...", "OK", MessageBoxButtons.OK);
                                this.ActiveControl = txt_EacCode;
                            }
                        }
                    }
                    txt_EacCode.Text = "";
                    dataGridView1_RowsRemoved(dataGridView1, ee);
                }
                catch
                {
                    this.ActiveControl = txt_EacCode;
                }
                txt_EacCode.Text = "";
            }
        }

        public void Clear()
        {
            dataGridView1.Rows.Clear();
            txtpur_billno.Text = "";
            txtPONo.Text = "";
            cmbState.Text = "";
            cmbSupName.Text = "";
            cmbPayMode.Text = "";
            cmbwarehouse.Text = "";
            txtemail.Text = "";
            txtMob.Text = "";
            txtgstno.Text = "";
            dateTimePicker1.Text = "";
            dtpPO_Date.Text = "";
            txtNarration.Text = "";
            textBox3.Text = "0";
            txttotqty.Text = "0";
            txtTPOQty.Text = "0";
            txtBalQty.Text = "0";
            lblcgstamt.Text = "0";
            lblsgstamt.Text = "0";
            lbligstamt.Text = "0";
            lblTot_Amt.Text = "0";
            lblTotalgst.Text = "0";
            lblNet_Amt.Text = "0";
        }

        protected void Caption_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text != null && (sender as TextBox).Text.Trim() != "")
            { }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int colIndex;
            int rowIndex;
            objAccName.productname.Clear();

            var txtBox = e.Control as TextBox;

            colIndex = dataGridView1.CurrentCell.ColumnIndex;
            rowIndex = dataGridView1.CurrentCell.RowIndex;

            if (colIndex == dataGridView1.Rows[rowIndex].Cells["rack1"].ColumnIndex)
            {
                if (e.Control is TextBox)
                {
                    if (txtBox != null)
                    {

                        txtBox.CharacterCasing = CharacterCasing.Upper;
                        txtBox.TextChanged += new EventHandler(Caption_TextChanged);
                        txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        string commandT = "select distinct [RackName] from [Rack_Master] where [WareHouse_Name] = '" + cmbwarehouse.Text.TrimEnd().TrimStart() + "'";
                        objAccName.filltextbox(commandT);
                        txtBox.AutoCompleteCustomSource = objAccName.productname;
                    }
                }
            }
        }

        private void cmbPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPayMode.Text == "Cash & Card")
            {
                panelcashcard.Visible = true;
                panelcheque.Visible = false;
            }
            if (cmbPayMode.Text == "Cheque")
            {
                panelcheque.Visible = true;
                panelcashcard.Visible = false;
            }
            if (cmbPayMode.Text == "Cash" || cmbPayMode.Text == "Card")
            {
                panelcheque.Visible = false;
                panelcashcard.Visible = false;
            }
        }

        private void txtCashamt_TextChanged(object sender, EventArgs e)
        {
        }

        public bool txtValidation()
        {
            if (txtpur_billno.Text == "")
            {
                MessageBox.Show("Enter Purchase bill No.");
                this.ActiveControl = txtpur_billno;
                return false;
            }
            if (dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show("Item Details cannot be blank.");
                return false;
            }
            if (cmbwarehouse.Text == "")
            {
                MessageBox.Show("Select Warehouse.");
                this.ActiveControl = cmbwarehouse;
                return false;
            }

            if (cmbPayMode.Text == "Cash & Card" && txtCashamt.Text == "")
            {
                MessageBox.Show("Enter Cash amount.");
                this.ActiveControl = txtCashamt;
                return false;
            }
            if (cmbPayMode.Text == "Cheque" && txtCheque.Text == "")
            {
                MessageBox.Show("Enter Cheque No.");
                this.ActiveControl = txtCheque;
                return false;
            }
            try
            {
                int rows = dataGridView1.Rows.Count - 1;
                for (int j = 0; j < rows; j++)
                {
                    if ((dataGridView1.Rows[j].Cells["rack1"].Value == "" || dataGridView1.Rows[j].Cells["rack1"].Value == null) && dataGridView1.Rows[j].Cells["qty1"].Value != "0")
                    {
                        MessageBox.Show("Select rack in " + j + " row.");
                        return false;
                    }
                }
            }
            catch { }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtValidation() == true)
            {
                if (txtBalQty.Text != "0.00")
                {
                    MessageBox.Show("This Purchase Order is incomplete. Purchase will save on temporary basis.", "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();
                    if (ans == "Yes")
                    {
                        try
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                SqlCommand command = connection.CreateCommand();
                                SqlTransaction transaction;
                                transaction = connection.BeginTransaction("SampleTransaction");

                                try
                                {
                                    //Delete From TempPurchase
                                    command = new SqlCommand("delete from [Temp_Purchase] where [Pur_BillNo1] = '" + txtpur_billno.Text.TrimEnd().TrimStart() + "'", connection, transaction);
                                    command.CommandType = CommandType.Text;
                                    command.ExecuteNonQuery();

                                    //Delete from Temp_SubPurchase
                                    command = new SqlCommand("delete from [Temp_SubPurchase] where [Pur_BillNo1] = '" + txtpur_billno.Text.TrimEnd().TrimStart() + "'", connection, transaction);
                                    command.CommandType = CommandType.Text;
                                    command.ExecuteNonQuery();

                                    //TempPurchaseSave
                                    command = new SqlCommand("TempPurchaseSave", connection, transaction);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@Pur_BillNo1", txtpur_billno.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Pur_BillDate1", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@PO_No1", txtPONo.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Po_Date1", dtpPO_Date.Value.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@SupName1", cmbSupName.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@SupId1", sup_id);
                                    command.Parameters.AddWithValue("@Mobile1", txtMob.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@sup_state1", cmbState.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@sup_gst_no1", txtgstno.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@sup_email1", txtemail.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Warehouse1", cmbwarehouse.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@TQty1", txttotqty.Text);
                                    command.Parameters.AddWithValue("@TPoQty1", txtTPOQty.Text);
                                    command.Parameters.AddWithValue("@TBalPoQty1", txtBalQty.Text);
                                    command.Parameters.AddWithValue("@TotalAmt1", lblTot_Amt.Text);
                                    command.Parameters.AddWithValue("@TotalGST1", lblTotalgst.Text);
                                    command.Parameters.AddWithValue("@CGSTAmt1", lblcgstamt.Text);
                                    command.Parameters.AddWithValue("@SGSTAmt1", lblsgstamt.Text);
                                    command.Parameters.AddWithValue("@IGSTAmt1", lbligstamt.Text);
                                    command.Parameters.AddWithValue("@NetAmt1", lblNet_Amt.Text);
                                    command.Parameters.AddWithValue("@Advance1", lblNet_Amt.Text);
                                    command.Parameters.AddWithValue("@RecAmt1", lblNet_Amt.Text);
                                    command.Parameters.AddWithValue("@Company1", company);
                                    command.Parameters.AddWithValue("@EnterBy1", username);
                                    command.Parameters.AddWithValue("@PaymentMode1", cmbPayMode.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("Narration1", txtNarration.Text.TrimEnd().TrimStart());
                                    if (cmbPayMode.Text == "Cash")
                                    {
                                        command.Parameters.AddWithValue("@CashAmt1", lblNet_Amt.Text);
                                        command.Parameters.AddWithValue("@CardAmt1", "0");
                                        command.Parameters.AddWithValue("@ChequeNo1", "");
                                    }
                                    if (cmbPayMode.Text == "Card")
                                    {
                                        command.Parameters.AddWithValue("@CashAmt1", "0");
                                        command.Parameters.AddWithValue("@CardAmt1", lblNet_Amt.Text);
                                        command.Parameters.AddWithValue("@ChequeNo1", "");
                                    }
                                    if (cmbPayMode.Text == "Cash & Card")
                                    {
                                        command.Parameters.AddWithValue("@CashAmt1", txtCashamt.Text);
                                        command.Parameters.AddWithValue("@CardAmt1", txtCardamt.Text);
                                        command.Parameters.AddWithValue("@ChequeNo1", "");
                                    }
                                    if (cmbPayMode.Text == "Cheque")
                                    {
                                        command.Parameters.AddWithValue("@CashAmt1", "0");
                                        command.Parameters.AddWithValue("@CardAmt1", "0");
                                        command.Parameters.AddWithValue("@ChequeNo1", txtCheque.Text.TrimEnd().TrimStart());
                                    }
                                    command.ExecuteNonQuery();


                                    //update bal qty in po
                                    command = new SqlCommand("update [Purchase_Order] set [Tbal_qty] = '" + txtBalQty.Text + "' where [PO_No] = '" + txtPONo.Text.TrimEnd().TrimStart() + "'", connection, transaction);
                                    command.CommandType = CommandType.Text;
                                    command.ExecuteNonQuery();


                                    //TempSubPurchaseSave
                                    for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count - 1); i++)
                                    {
                                        command = new SqlCommand("Temp_SubPurchaseSave", connection, transaction);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@Pur_BillNo1", txtpur_billno.Text.TrimEnd().TrimStart());
                                        command.Parameters.AddWithValue("@PO_No1", txtPONo.Text.TrimEnd().TrimStart());
                                        command.Parameters.AddWithValue("@P_Name1", dataGridView1.Rows[i].Cells["Column1"].Value.ToString());
                                        command.Parameters.AddWithValue("@EAC_code1", dataGridView1.Rows[i].Cells["barcode1"].Value.ToString());
                                        command.Parameters.AddWithValue("@Groups1", dataGridView1.Rows[i].Cells["group"].Value.ToString());
                                        command.Parameters.AddWithValue("@unit11", dataGridView1.Rows[i].Cells["unit"].Value.ToString());
                                        if (dataGridView1.Rows[i].Cells["rack1"].Value == "" || dataGridView1.Rows[i].Cells["rack1"].Value == null)
                                        {
                                            command.Parameters.AddWithValue("@Rack1", "");
                                        }
                                        else
                                        {
                                            command.Parameters.AddWithValue("@Rack1", dataGridView1.Rows[i].Cells["rack1"].Value.ToString());
                                        }
                                        command.Parameters.AddWithValue("@P_QTY1", dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                                        command.Parameters.AddWithValue("@Qty1", dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                                        command.Parameters.AddWithValue("@PoQty1", dataGridView1.Rows[i].Cells["Po_Qty"].Value.ToString());
                                        command.Parameters.AddWithValue("@BalpoQty1", dataGridView1.Rows[i].Cells["bal_po_qty"].Value.ToString());
                                        command.Parameters.AddWithValue("@Rate1", dataGridView1.Rows[i].Cells["rate1"].Value.ToString());
                                        command.Parameters.AddWithValue("@Disc_per1", dataGridView1.Rows[i].Cells["disc"].Value.ToString());
                                        command.Parameters.AddWithValue("@Disc_amt1", dataGridView1.Rows[i].Cells["disc_amt"].Value.ToString());
                                        command.Parameters.AddWithValue("@Amount1", dataGridView1.Rows[i].Cells["amount1"].Value.ToString());
                                        command.Parameters.AddWithValue("@cgsttax1", dataGridView1.Rows[i].Cells["cgstper1"].Value.ToString());
                                        command.Parameters.AddWithValue("@cgstamt1", dataGridView1.Rows[i].Cells["cgstamt1"].Value.ToString());
                                        command.Parameters.AddWithValue("@sgsttax1", dataGridView1.Rows[i].Cells["sgstper1"].Value.ToString());
                                        command.Parameters.AddWithValue("@sgstamt1", dataGridView1.Rows[i].Cells["sgstamt1"].Value.ToString());
                                        command.Parameters.AddWithValue("@igsttax1", dataGridView1.Rows[i].Cells["igstper1"].Value.ToString());
                                        command.Parameters.AddWithValue("@igstamt1", dataGridView1.Rows[i].Cells["igstamt1"].Value.ToString());
                                        command.Parameters.AddWithValue("@gst_amt1", dataGridView1.Rows[i].Cells["gst_amt"].Value.ToString());
                                        command.Parameters.AddWithValue("@GST_per1", dataGridView1.Rows[i].Cells["GST_per"].Value.ToString());
                                        command.Parameters.AddWithValue("@total_amount1", dataGridView1.Rows[i].Cells["Tamt"].Value.ToString());
                                        command.Parameters.AddWithValue("@Company1", company);
                                        command.Parameters.AddWithValue("@EnterBy1", username);
                                        command.ExecuteNonQuery();

                                        //update bal qty in sub po
                                        command = new SqlCommand("update [SubPurchaseOrder] set [Bal_Qty] = '" + dataGridView1.Rows[i].Cells["bal_po_qty"].Value.ToString() + "' where " +
                                                                 "[SubPO_No] = '" + txtPONo.Text.TrimEnd().TrimStart() + "' and [EAC_Code] = '" + dataGridView1.Rows[i].Cells["barcode1"].Value.ToString() + "'", connection, transaction);
                                        command.CommandType = CommandType.Text;
                                        command.ExecuteNonQuery();

                                    }

                                    transaction.Commit();
                                    MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Clear();
                                }
                                catch
                                {
                                    try
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show("Save was unsuccessful...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Clear();
                                    }
                                    catch (Exception ex2)
                                    { }
                                }
                            }
                        }
                        catch { }
                    }
                }

                else if (txtBalQty.Text == "0.00")
                {
                    MessageBox.Show("This Purchase Order is complete.", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();
                    if (ans == "Yes")
                    {
                        try
                        {
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                SqlCommand command = connection.CreateCommand();
                                SqlTransaction transaction;
                                transaction = connection.BeginTransaction("SampleTransaction");

                                try
                                {
                                    //Deleteflag update in TempPurchase
                                    command = new SqlCommand("Update [Temp_Purchase] set [deleteflag1] = 'Yes' where [Pur_BillNo1] = '" + txtpur_billno.Text.TrimEnd().TrimStart() + "'", connection, transaction);
                                    command.CommandType = CommandType.Text;
                                    command.ExecuteNonQuery();

                                    //Deleteflag update in Temp_SubPurchase
                                    command = new SqlCommand("Update [Temp_SubPurchase] set [DeleteFlag1] = 'Yes' where [Pur_BillNo1] = '" + txtpur_billno.Text.TrimEnd().TrimStart() + "'", connection, transaction);
                                    command.CommandType = CommandType.Text;
                                    command.ExecuteNonQuery();

                                    //PurchaseSave
                                    command = new SqlCommand("PurchaseSave", connection, transaction);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@Pur_BillNo", txtpur_billno.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Pur_BillDate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@PO_No", txtPONo.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Po_Date", dtpPO_Date.Value.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@SupName", cmbSupName.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@SupId", sup_id);
                                    command.Parameters.AddWithValue("@Mobile", txtMob.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@sup_state", cmbState.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@sup_gst_no", txtgstno.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@sup_email", txtemail.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Warehouse", cmbwarehouse.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@PaymentMode", cmbPayMode.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@TQty", txttotqty.Text);
                                    command.Parameters.AddWithValue("@TPoQty", txtTPOQty.Text);
                                    command.Parameters.AddWithValue("@TBalPoQty", txtBalQty.Text);
                                    command.Parameters.AddWithValue("@TotalAmt", lblTot_Amt.Text);
                                    command.Parameters.AddWithValue("@TotalGST", lblTotalgst.Text);
                                    command.Parameters.AddWithValue("@CGSTAmt", lblcgstamt.Text);
                                    command.Parameters.AddWithValue("@SGSTAmt", lblsgstamt.Text);
                                    command.Parameters.AddWithValue("@IGSTAmt", lbligstamt.Text);
                                    command.Parameters.AddWithValue("@NetAmt", lblNet_Amt.Text);
                                    command.Parameters.AddWithValue("@Advance", lblNet_Amt.Text);
                                    command.Parameters.AddWithValue("@RecAmt", lblNet_Amt.Text);
                                    command.Parameters.AddWithValue("@Company", company);
                                    command.Parameters.AddWithValue("@EnterBy", username);
                                    command.Parameters.AddWithValue("Narration", txtNarration.Text.TrimEnd().TrimStart());
                                    if (cmbPayMode.Text == "Cash")
                                    {
                                        command.Parameters.AddWithValue("@CashAmt", lblNet_Amt.Text);
                                        command.Parameters.AddWithValue("@CardAmt", "0");
                                        command.Parameters.AddWithValue("@ChequeNo", "");
                                    }
                                    if (cmbPayMode.Text == "Card")
                                    {
                                        command.Parameters.AddWithValue("@CashAmt", "0");
                                        command.Parameters.AddWithValue("@CardAmt", lblNet_Amt.Text);
                                        command.Parameters.AddWithValue("@ChequeNo", "");
                                    }
                                    if (cmbPayMode.Text == "Cash & Card")
                                    {
                                        command.Parameters.AddWithValue("@CashAmt", txtCashamt.Text);
                                        command.Parameters.AddWithValue("@CardAmt", txtCardamt.Text);
                                        command.Parameters.AddWithValue("@ChequeNo", "");
                                    }
                                    if (cmbPayMode.Text == "Cheque")
                                    {
                                        command.Parameters.AddWithValue("@CashAmt", "0");
                                        command.Parameters.AddWithValue("@CardAmt", "0");
                                        command.Parameters.AddWithValue("@ChequeNo", txtCheque.Text.TrimEnd().TrimStart());
                                    }
                                    command.ExecuteNonQuery();

                                    //update bal qty in po
                                    command = new SqlCommand("update [Purchase_Order] set [Tbal_qty] = '" + txtBalQty.Text + "' where [PO_No] = '" + txtPONo.Text.TrimEnd().TrimStart() + "'", connection, transaction);
                                    command.CommandType = CommandType.Text;
                                    command.ExecuteNonQuery();

                                    //SubPurchaseSave
                                    for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count - 1); i++)
                                    {
                                        command = new SqlCommand("SubPurchaseSave", connection, transaction);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@Pur_BillNo", txtpur_billno.Text.TrimEnd().TrimStart());
                                        command.Parameters.AddWithValue("@PO_No", txtPONo.Text.TrimEnd().TrimStart());
                                        command.Parameters.AddWithValue("@P_Name", dataGridView1.Rows[i].Cells["Column1"].Value.ToString());
                                        command.Parameters.AddWithValue("@EAC_code", dataGridView1.Rows[i].Cells["barcode1"].Value.ToString());
                                        command.Parameters.AddWithValue("@Groups", dataGridView1.Rows[i].Cells["group"].Value.ToString());
                                        command.Parameters.AddWithValue("@unit1", dataGridView1.Rows[i].Cells["unit"].Value.ToString());
                                        if (dataGridView1.Rows[i].Cells["rack1"].Value.ToString() == "" || dataGridView1.Rows[i].Cells["rack1"].Value.ToString() == null)
                                        {
                                            command.Parameters.AddWithValue("@Rack", "");
                                        }
                                        else
                                        {
                                            command.Parameters.AddWithValue("@Rack", dataGridView1.Rows[i].Cells["rack1"].Value.ToString());
                                        }
                                        command.Parameters.AddWithValue("@P_QTY", dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                                        command.Parameters.AddWithValue("@Qty", dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                                        command.Parameters.AddWithValue("@PoQty", dataGridView1.Rows[i].Cells["Po_Qty"].Value.ToString());
                                        command.Parameters.AddWithValue("@BalpoQty", dataGridView1.Rows[i].Cells["bal_po_qty"].Value.ToString());
                                        command.Parameters.AddWithValue("@Rate", dataGridView1.Rows[i].Cells["rate1"].Value.ToString());
                                        command.Parameters.AddWithValue("@Disc_per", dataGridView1.Rows[i].Cells["disc"].Value.ToString());
                                        command.Parameters.AddWithValue("@Disc_amt", dataGridView1.Rows[i].Cells["disc_amt"].Value.ToString());
                                        command.Parameters.AddWithValue("@Amount", dataGridView1.Rows[i].Cells["amount1"].Value.ToString());
                                        command.Parameters.AddWithValue("@cgsttax", dataGridView1.Rows[i].Cells["cgstper1"].Value.ToString());
                                        command.Parameters.AddWithValue("@cgstamt", dataGridView1.Rows[i].Cells["cgstamt1"].Value.ToString());
                                        command.Parameters.AddWithValue("@sgsttax", dataGridView1.Rows[i].Cells["sgstper1"].Value.ToString());
                                        command.Parameters.AddWithValue("@sgstamt", dataGridView1.Rows[i].Cells["sgstamt1"].Value.ToString());
                                        command.Parameters.AddWithValue("@igsttax", dataGridView1.Rows[i].Cells["igstper1"].Value.ToString());
                                        command.Parameters.AddWithValue("@igstamt", dataGridView1.Rows[i].Cells["igstamt1"].Value.ToString());
                                        command.Parameters.AddWithValue("@gst_amt", dataGridView1.Rows[i].Cells["gst_amt"].Value.ToString());
                                        command.Parameters.AddWithValue("@GST_per", dataGridView1.Rows[i].Cells["GST_per"].Value.ToString());
                                        command.Parameters.AddWithValue("@total_amount", dataGridView1.Rows[i].Cells["Tamt"].Value.ToString());
                                        command.Parameters.AddWithValue("@Company", company);
                                        command.Parameters.AddWithValue("@EnterBy", username);
                                        command.ExecuteNonQuery();

                                        //update bal qty in sub po
                                        command = new SqlCommand("update [SubPurchaseOrder] set [Bal_Qty] = '" + dataGridView1.Rows[i].Cells["bal_po_qty"].Value.ToString() + "' where " +
                                                                 "[SubPO_No] = '" + txtPONo.Text.TrimEnd().TrimStart() + "' and [EAC_Code] = '" + dataGridView1.Rows[i].Cells["barcode1"].Value.ToString() + "'", connection, transaction);
                                        command.CommandType = CommandType.Text;
                                        command.ExecuteNonQuery();
                                    }

                                    transaction.Commit();
                                    MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Clear();
                                }
                                catch
                                {
                                    try
                                    {
                                        transaction.Rollback();
                                        MessageBox.Show("Save was unsuccessful...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Clear();
                                    }
                                    catch (Exception ex2)
                                    { }
                                }
                            }
                        }
                        catch { }
                    }
                }  //else if End
                else { }
            }
        }

        private void cmbwarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbwarehouse.Text != "")
            {
                try
                {
                    rack1.Items.Clear();
                   
                    string strrack = "select distinct [RackName] from [Rack_Master] where [WareHouse_Name] = '" + cmbwarehouse.Text.TrimEnd().TrimStart() + "'";
                    dr1 = ObjCon.getData(strrack);
                    if (dr1.HasRows)
                    {
                        while (dr1.Read())
                        {
                            rack1.Items.Add(dr1[0].ToString());
                        }
                    }
                    dr1.Close();
                }
                catch
                { }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
