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
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;
using Microsoft.Win32;

namespace BMS_Lifestyle
{
    public partial class Purchase_Order : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectDb ObjCon = new ConnectDb();
        SqlDataReader dr = null;
        string username,company,connectionString;
        int affect,flag1 = 0;
        string purchaseOrderPK, Sup_id;
        AutoComplete objAccName = new AutoComplete();
        string company_state, companyname,commandT;
       
        public Purchase_Order(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company=company;
            this.connectionString = connectionString;
        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            try
            {
                string str5 = "select [State],[Company_Name] FROM [Company_Master] where [Company_ID] = '" + company + "'";
                dr = ObjCon.getData(str5);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        company_state = dr[0].ToString().ToUpper();
                        companyname = dr[1].ToString().ToUpper();
                    }

                }
                dr.Close();

                commandT = "select [SupName] from [Supplier] order by [SupId] asc";
                dr = ObjCon.getData(commandT);
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        cmbSupName.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();

                string commandT1 = "select [State_Code] from [State_Code_Master] order by [State_Code] asc";
                dr = ObjCon.getData(commandT1);
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        cmbState.Items.Add(dr[0].ToString().ToUpper());
                    }

                }
                dr.Close();
                cmbState.SelectedIndex = 26;
            }
            catch
            { }
        }
                         
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rows = Convert.ToInt32(dataGridView1.RowCount.ToString()) - 2;
            DataGridViewRowsRemovedEventArgs ee = null;
            
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
            
            if (dataGridView1.Columns[e.ColumnIndex].Name == "gst_amt")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "GST_per")
            {
                SendKeys.Send("{TAB}");
            }
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "Tamt")
            //{
            //    SendKeys.Send("{ENTER}");
            //}
           
            if (e.RowIndex > 0)
            {
                try
                {
                    int row = dataGridView1.RowCount - 1;

                    string[] bcode = new string[row + 1];

                    for (int i = 0; i < row; i++)
                    {
                        if (dataGridView1.Rows[i].Cells["barcode1"].Value != null && e.RowIndex != i)
                        {
                            bcode[i] = dataGridView1.Rows[i].Cells["barcode1"].Value.ToString();
                        }
                    }
                    for (int i = 0; i < row; i++)
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells["barcode1"].Value.ToString() == bcode[i])
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["barcode1"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["group"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["unit"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["qty1"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value = "";
                            dataGridView1.Rows[e.RowIndex].Cells["Tamt"].Value = "";
                        }
                    }
                }
                catch { }
            }

            if (e.ColumnIndex == 1)
            {
                if (cmbState.Text == "")
                {
                    MessageBox.Show("State cannot be blank");
                }
                else
                {
                    try
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value == null || dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value == "")
                        {
                            commandT =  "SELECT i.[P_Name],i.[Item_Group],i.[Item_Unit],isnull(cast(t.[Tax_per] as decimal(18,2)),0) as Tax_per FROM [Item] i left join "+ 
                                        "[Tax_Master] t on i.[Tax_Name] = t.[Tax_Name] where i.[EAC_Code] = '" + dataGridView1.Rows[e.RowIndex].Cells["barcode1"].Value.ToString() + "'";
                            
                            dr = ObjCon.getData(commandT);
                            if (dr.HasRows == true)
                            {
                                while (dr.Read())
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value = dr["P_Name"].ToString();
                                    dataGridView1.Rows[e.RowIndex].Cells["group"].Value = dr["Item_Group"].ToString();
                                    dataGridView1.Rows[e.RowIndex].Cells["unit"].Value = dr["Item_Unit"].ToString();
                                    dataGridView1.Rows[e.RowIndex].Cells["qty1"].Value = "0";
                                    dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value = "0";
                                    dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value = "0";
                                    dataGridView1.Rows[e.RowIndex].Cells["Tamt"].Value = "0";
                                    dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value = dr["Tax_per"].ToString();
                                    dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value = "0";
                                }
                            }
                            dr.Close(); 
                        }
                    }
                    catch
                    { }
                }
            }
   
            if (e.ColumnIndex == 5)
            {
                try
                {
                    if(dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString() != "" || dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString() != "0" )
                    {
                        if(cmbState.Text == company_state)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString()))));

                            dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()))) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["cgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["sgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["cgstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["sgstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));
                        }
                        else
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString()))));

                            dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()))) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["igstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString())));

                            dataGridView1.Rows[e.RowIndex].Cells["igstamt1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));

                            dataGridView1.Rows[e.RowIndex].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));
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
                        if(cmbState.Text == company_state)
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString()))));

                            dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()))) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["cgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["sgstper1"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()) / 2));
                            
                            dataGridView1.Rows[e.RowIndex].Cells["cgstamt1"].Value = String.Format("{0:0.00}",(Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["sgstamt1"].Value = String.Format("{0:0.00}",(Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["igstper1"].Value = "0";
                            dataGridView1.Rows[e.RowIndex].Cells["igstamt1"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));
                        }
                        else
                        {
                            dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["qty1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString()))));

                            dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()))) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["igstper1"].Value = String.Format("{0:0.00}",(Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString())));

                            dataGridView1.Rows[e.RowIndex].Cells["igstamt1"].Value = String.Format("{0:0.00}",(Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));
                            
                            dataGridView1.Rows[e.RowIndex].Cells["cgstper1"].Value = "0";
                            dataGridView1.Rows[e.RowIndex].Cells["cgstamt1"].Value = "0";
                            dataGridView1.Rows[e.RowIndex].Cells["sgstper1"].Value = "0";
                            dataGridView1.Rows[e.RowIndex].Cells["sgstamt1"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));
                        }
                        dataGridView1_RowsRemoved(dataGridView1, ee);
                }
                catch { }
            }
            
            dataGridView1_RowsRemoved(dataGridView1, ee);
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Double Mtr = 0,CgstAmt = 0, IgstAmt = 0, SgstAmt = 0,QTY1 = 0, GSTAMT = 0;
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
                }
                catch { }
            }
           
            textBox3.Text = String.Format("{0:0.00}", double.Parse(rows.ToString()));
            txttotqty.Text = String.Format("{0:0.00}", double.Parse(QTY1.ToString()));
            lblTot_Amt.Text = String.Format("{0:0.00}", double.Parse(Mtr.ToString()));
            lblcgstamt.Text = String.Format("{0:0.00}", double.Parse(CgstAmt.ToString()));
            lbligstamt.Text = String.Format("{0:0.00}", double.Parse(IgstAmt.ToString()));
            lblsgstamt.Text = String.Format("{0:0.00}", double.Parse(SgstAmt.ToString()));
            lblTotalgst.Text = String.Format("{0:0.00}", double.Parse(GSTAMT.ToString()));
            lblNet_Amt.Text =String.Format("{0:0.00}",(Double.Parse(lblTot_Amt.Text) + Double.Parse(lblTotalgst.Text)), 2);
        }

        public bool Validation()
        {
            if (cmbSupName.Text == "")
            {
                MessageBox.Show("Select Supplier Name.");
                this.ActiveControl = cmbSupName;
                return false;
            }

            if (dataGridView1.RowCount <= 1)
            {
                MessageBox.Show("Item Detail section cannot be blank.");
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation() == true)
            {
                string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();
                if (ans == "Yes")
                {
                    try
                    {
                        string str5 = "select [dbo].[GenPurchaseOrderNo] ('" + dtpPO_Date.Value.ToString("MM/dd/yyyy") + "','" + company + "')";
                        dr = ObjCon.getData(str5);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtPONo.Text = dr[0].ToString();
                            }
                        }
                        dr.Close();
                    }
                    catch
                    { }
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
                                //PurchaseOrderSave
                                command = new SqlCommand("PurchaseOrderSave", connection, transaction);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@PO_No",txtPONo.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@PO_Date",dtpPO_Date.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@SupName",cmbSupName.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@SupId",Convert.ToInt32(Sup_id));
                                command.Parameters.AddWithValue("@Mobile",txtMob.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@sup_state",cmbState.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@sup_gst_no",txtgstno.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@sup_email",txtemail.Text.TrimEnd().TrimStart()); 
                                command.Parameters.AddWithValue("@Narration",txtNarration.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@TQty", txttotqty.Text);
                                command.Parameters.AddWithValue("@Tbal_qty", txttotqty.Text);
                                command.Parameters.AddWithValue("@TotalAmt",lblTot_Amt.Text); 
                                command.Parameters.AddWithValue("@TotalGST",lblTotalgst.Text); 
                                command.Parameters.AddWithValue("@CGSTAmt",lblcgstamt.Text); 
                                command.Parameters.AddWithValue("@SGSTAmt",lblsgstamt.Text); 
                                command.Parameters.AddWithValue("@IGSTAmt",lbligstamt.Text); 
                                command.Parameters.AddWithValue("@NetAmt",lblNet_Amt.Text); 
                                command.Parameters.AddWithValue("@Balance","0");
                                command.Parameters.AddWithValue("@Advance",lblNet_Amt.Text);
                                command.Parameters.AddWithValue("@RecAmt", lblNet_Amt.Text); 
                                command.Parameters.AddWithValue("@deleteflag","No");
                                command.Parameters.AddWithValue("@Company",company); 
                                command.Parameters.AddWithValue("@EnterBy",username);
                                command.ExecuteNonQuery();

                                //SubPurchaseOrderSave
                                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count - 1); i++)
                                {
                                    command = new SqlCommand("SubPurchaseOrderSave", connection, transaction);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@SubPO_No",txtPONo.Text.TrimEnd().TrimStart());
		                            command.Parameters.AddWithValue("@P_Name",dataGridView1.Rows[i].Cells["Column1"].Value.ToString());
		                            command.Parameters.AddWithValue("@EAC_Code",dataGridView1.Rows[i].Cells["barcode1"].Value.ToString().ToUpper());
		                            command.Parameters.AddWithValue("@Groups",dataGridView1.Rows[i].Cells["group"].Value.ToString());
		                            command.Parameters.AddWithValue("@unit1",dataGridView1.Rows[i].Cells["unit"].Value.ToString());
		                            command.Parameters.AddWithValue("@PO_QTY",dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                                    command.Parameters.AddWithValue("@Bal_Qty", dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
		                            command.Parameters.AddWithValue("@Rate",dataGridView1.Rows[i].Cells["rate1"].Value.ToString());
		                            command.Parameters.AddWithValue("@Amount",dataGridView1.Rows[i].Cells["amount1"].Value.ToString());
		                            command.Parameters.AddWithValue("@cgsttax",dataGridView1.Rows[i].Cells["cgstper1"].Value.ToString());
		                            command.Parameters.AddWithValue("@cgstamt",dataGridView1.Rows[i].Cells["cgstamt1"].Value.ToString());
		                            command.Parameters.AddWithValue("@sgsttax",dataGridView1.Rows[i].Cells["sgstper1"].Value.ToString());
		                            command.Parameters.AddWithValue("@sgstamt",dataGridView1.Rows[i].Cells["sgstamt1"].Value.ToString());
		                            command.Parameters.AddWithValue("@igsttax",dataGridView1.Rows[i].Cells["igstper1"].Value.ToString());
		                            command.Parameters.AddWithValue("@igstamt",dataGridView1.Rows[i].Cells["igstamt1"].Value.ToString());
		                            command.Parameters.AddWithValue("@gst_amt",dataGridView1.Rows[i].Cells["gst_amt"].Value.ToString());
		                            command.Parameters.AddWithValue("@GST_per",dataGridView1.Rows[i].Cells["GST_per"].Value.ToString());
		                            command.Parameters.AddWithValue("@total_amount",dataGridView1.Rows[i].Cells["Tamt"].Value.ToString());
		                            command.Parameters.AddWithValue("@Company",company);
		                            command.Parameters.AddWithValue("@EnterBy",username);
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
        }

        public void Clear()
        {
            
            cmbState.Text = "";
            txtemail.Text = "";
            txtMob.Text = "";
            txtgstno.Text = "";
            txtNarration.Text = "";
            txttotqty.Text = "0";
            textBox3.Text = "0";
            lblcgstamt.Text = "0";
            lblsgstamt.Text = "0";
            lbligstamt.Text = "0";
            lblTot_Amt.Text = "0";
            lblTotalgst.Text = "0";
            lblNet_Amt.Text = "0";
            dataGridView1.Rows.Clear();
            cmbSupName.Text = "";
            dtpPO_Date.Text = "";
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
                     
        }

        protected void Caption_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text != null && (sender as TextBox).Text.Trim() != "")
            { }
        }


        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString().ToUpper();
                e.FormattingApplied = true;
            }
        }

          
        private void txtSupBillNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbSupName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtMob_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

     

        private void dtpBill_Date_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbPaymentMode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtemail_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void TxtDiscPerc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCourier_Charges_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
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
                        string str5 = "select [dbo].[GenPurchaseOrderNo] ('" + dtpPO_Date.Value.ToString("MM/dd/yyyy") + "','" + company + "')";
                        dr = ObjCon.getData(str5);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtPONo.Text = dr[0].ToString();
                            }
                        }
                        dr.Close();
                    }
                    catch
                    {
                    }

                    try
                    {
                        dr.Close();
                        string conString = "Data Source=SERVER;Initial Catalog=BMSLifestyle;User ID=sa;Password=preet@1234";
                        con = new SqlConnection(conString);
                        con.Open();
                        cmd = new SqlCommand("PurchaseOrderSave", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add(new SqlParameter("@PONo", txtPONo.Text));
                        cmd.Parameters.Add(new SqlParameter("@SupName", cmbSupName.Text));
                        cmd.Parameters.Add(new SqlParameter("@Mobile", txtMob.Text));
                        cmd.Parameters.Add(new SqlParameter("@sup_state", cmbState.Text.ToUpper()));
                        cmd.Parameters.Add(new SqlParameter("@sup_gst_no", txtgstno.Text));
                        cmd.Parameters.Add(new SqlParameter("@sup_email", txtemail.Text));
                        cmd.Parameters.Add(new SqlParameter("@PODate", dtpPO_Date.Value.ToString("MM/dd/yyyy")));
                        cmd.Parameters.Add(new SqlParameter("@PaymentMode", ""));
                        cmd.Parameters.Add(new SqlParameter("@TotalAmt", lblTot_Amt.Text));
                        cmd.Parameters.Add(new SqlParameter("@TotalGST", lblTotalgst.Text));
                        cmd.Parameters.Add(new SqlParameter("@CGSTAmt", lblcgstamt.Text));
                        cmd.Parameters.Add(new SqlParameter("@SGSTAmt", lblsgstamt.Text));
                        cmd.Parameters.Add(new SqlParameter("@IGSTAmt", lbligstamt.Text));
                        cmd.Parameters.Add(new SqlParameter("@NetAmt", lblNet_Amt.Text));
                        cmd.Parameters.Add(new SqlParameter("@Balance", lblNet_Amt.Text));
                        cmd.Parameters.Add(new SqlParameter("@Advance", "0"));
                        cmd.Parameters.Add(new SqlParameter("@RecAmt", "0.00"));
                        //cmd.Parameters.Add(new SqlParameter("@TWt_ingm", txtweightGm.Text));
                        //cmd.Parameters.Add(new SqlParameter("@TWt_inKg", txtweightKG.Text));
                        cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                        cmd.Parameters.Add(new SqlParameter("@Company", company));
                        cmd.Parameters.Add(new SqlParameter("@deleteflag", "No"));
                        cmd.Parameters.Add(new SqlParameter("@narration", txtNarration.Text));

                        cmd.ExecuteNonQuery();

                        try
                        {
                            string strfk = "select ISNULL(MAX([PO_Pk]),0) FROM [Purchase_Order]";
                            dr = ObjCon.getData(strfk);
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    purchaseOrderPK = dr[0].ToString();
                                }
                            }
                            dr.Close();
                        }
                        catch { }

                        int row = Convert.ToInt32(dataGridView1.RowCount.ToString()) - 2;
                        for (int i = 0; i <= row; i++)
                        {
                            cmd = new SqlCommand("SubPurchaseOrderSave", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@PO_Fk ", purchaseOrderPK));
                            cmd.Parameters.Add(new SqlParameter("@SubPONo ", txtPONo.Text));
                            cmd.Parameters.Add(new SqlParameter("@ItemName", dataGridView1.Rows[i].Cells["Column1"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@EACCode", dataGridView1.Rows[i].Cells["barcode1"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@SKU_code", dataGridView1.Rows[i].Cells["skucode"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@Ship_weight", dataGridView1.Rows[i].Cells["shipping_wt1"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@Groups", dataGridView1.Rows[i].Cells["group"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@size", dataGridView1.Rows[i].Cells["size"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@unit1", dataGridView1.Rows[i].Cells["unit"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@P_QTY", dataGridView1.Rows[i].Cells["qty1"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@Qty", dataGridView1.Rows[i].Cells["qty1"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@Rate", dataGridView1.Rows[i].Cells["rate1"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@Disc_per", dataGridView1.Rows[i].Cells["disc"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@new_rate", dataGridView1.Rows[i].Cells["newrate1"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@Amt", dataGridView1.Rows[i].Cells["amount1"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@cgsttax", dataGridView1.Rows[i].Cells["cgst_per"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@cgstamt", dataGridView1.Rows[i].Cells["cgst_amt"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@sgsttax", dataGridView1.Rows[i].Cells["sgst_per"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@sgstamt", dataGridView1.Rows[i].Cells["sgst_amt"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@igsttax", dataGridView1.Rows[i].Cells["igst_per"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@igstamt", dataGridView1.Rows[i].Cells["igst_amt"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@gst_amt", dataGridView1.Rows[i].Cells["gst_amt"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@GST_per", dataGridView1.Rows[i].Cells["GST_per"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@total_amount", dataGridView1.Rows[i].Cells["Tamt"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@MRP_Sale", dataGridView1.Rows[i].Cells["MRP"].Value.ToString()));
                            cmd.Parameters.Add(new SqlParameter("@Returned", "No"));
                            cmd.Parameters.Add(new SqlParameter("@Company", company));
                            cmd.Parameters.Add(new SqlParameter("@EnterBy", username));
                            cmd.Parameters.Add(new SqlParameter("@DeleteFlag", "No"));


                            cmd.ExecuteNonQuery();
                        }
                        if (cmbSupName.Text != "")
                        {
                            try
                            {
                                if (flag1 == 0)
                                {
                                    dr.Close();
                                    string save3 = "INSERT INTO [Supplier]([SupName],[Mobile],[EmailId],[GST_No],[state],[Enter_by],[Enter_date],[Company])VALUES" +
                                        "('" + cmbSupName.Text + "','" + txtMob.Text + "','" + txtemail.Text + "','" + txtgstno.Text + "','" + cmbState.Text + "','" + username + "',getDate(),'" + company + "')";
                                    affect = ObjCon.affect(save3);
                                }
                            }
                            catch { }
                        }
                        MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clear();
                      

                    }
                    catch { }
                }
            }
        }

        public void clear()
        {
            dataGridView1.Rows.Clear();
            cmbState.Text = "";
            cmbSupName.Text = "";
            txtMob.Text = "";
            txtemail.Text = "";
            dtpPO_Date.Text = "";
            lblTot_Amt.Text = "0";
            lblNet_Amt.Text = "0";
            txtPONo.Text = "";
            txtPONo.Text = "";
            lblsgstamt.Text = "0";
            lbligstamt.Text = "0";
            lblcgstamt.Text = "0";
            txtgstno.Text = "";
            textBox3.Text = "0";
            lblTotalgst.Text = "0";
            txtNarration.Text = "";
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
                            cmbState.Text = dr["State_Code"].ToString().ToUpper();

                        }
                    }
                    dr.Close();
                }

            }
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

        private void cmbSupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSupName.Text != "")
            {
                try
                {
                    commandT = "select [EmailId],[Mobile],[state],[GST_No],[SupId] from [Supplier] where [SupName]='" + cmbSupName.Text + "'";
                    dr = ObjCon.getData(commandT);
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            txtemail.Text = dr["EmailId"].ToString();
                            txtMob.Text = dr["Mobile"].ToString();
                            cmbState.Text = dr["state"].ToString();
                            txtgstno.Text = dr["GST_No"].ToString();
                            Sup_id = dr["SupId"].ToString();
                        } 
                    }
                    dr.Close();
                }
                catch { }
            }
        }
    }
}
