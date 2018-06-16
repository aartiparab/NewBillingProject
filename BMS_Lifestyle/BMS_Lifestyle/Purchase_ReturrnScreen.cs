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

namespace BMS_Lifestyle
{
    public partial class Purchase_ReturrnScreen : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectDb ObjCon = new ConnectDb();
        SqlDataReader dr = null;
        SqlDataReader dr2 = null;
        string username;
        string commandT, save;
        int affect, sup_id;
        string company, connectionString;
        string purchasePK, ItemCode;
        AutoCompleteStringCollection Party = new AutoCompleteStringCollection();
        AutoCompleteStringCollection Tax = new AutoCompleteStringCollection();
        AutoCompleteStringCollection state1 = new AutoCompleteStringCollection();
        AutoComplete objAccName = new AutoComplete();
        Double TVatAmt = 0, GST_percent;
        Double transport_gst = 0;
        string company_state;
        string gst_percent = "";
        double stock_qty, return_qty;

        public Purchase_ReturrnScreen(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Purchase_ReturrnScreen_Load(object sender, EventArgs e)
        {
            try
            {
                string str5 = "select [State] FROM [Company_Master] where [Company_ID] = '" + company + "'";
                dr = ObjCon.getData(str5);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        company_state = dr[0].ToString().ToUpper();
                    }
                }
                dr.Close();
                fillParty();
            }
            catch
            { }
        }
        public void fillParty()
        {
            try
            {
                commandT = "select distinct [SupName] FROM [Purchase] where [deleteflag] = 'No'";
                dr = ObjCon.getData(commandT);
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        cmbSupName.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
            }
            catch { }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rows = Convert.ToInt32(dataGridView1.RowCount.ToString());
            DataGridViewRowsRemovedEventArgs ee = null;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "group")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "p_name")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "EACcode")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "unit")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "P_qty")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "qty1")
            {
                SendKeys.Send("{TAB}");
            }
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "amount1")
            //{
            //    SendKeys.Send("{TAB}");
            //}
            if (dataGridView1.Columns[e.ColumnIndex].Name == "gst_amt")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "GST_per")
            {
                SendKeys.Send("{TAB}");
            }
            

            if (e.ColumnIndex == 8)
            {
                try
                {
                    
                    stock_qty = Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["qty1"].Value.ToString());
                    return_qty = Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["R_qty"].Value.ToString());

                    if (stock_qty < return_qty)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["R_qty"].Value = "0";
                    }
                    else if (stock_qty >= return_qty)
                    {
                        double stock_qty1 = stock_qty - return_qty;
                        dataGridView1.Rows[e.RowIndex].Cells["st_qty"].Value = stock_qty1.ToString();
                    }
                    dataGridView1_RowsRemoved(dataGridView1, ee);  
                }
                catch { }
            }

            if (e.ColumnIndex == 8)
            {
                try
                {

                    if (dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString() != "" || dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString() != "0"
                        || dataGridView1.Rows[e.RowIndex].Cells["R_qty"].Value.ToString() != "" || dataGridView1.Rows[e.RowIndex].Cells["R_qty"].Value.ToString() != "0")
                    {
                        if (cmbState.Text == company_state)
                        {
                           // dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc"].Value.ToString())) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["R_qty"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString())))));

                            dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()))) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["cgst_per"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["sgst_per"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["cgst_amt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["sgst_amt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["igst_per"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["igst_amt"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));
                        }
                        else
                        {
                            //dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc"].Value.ToString())) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["R_qty"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString())))));

                            dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()))) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["igst_per"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString())));

                            dataGridView1.Rows[e.RowIndex].Cells["igst_amt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));

                            dataGridView1.Rows[e.RowIndex].Cells["cgst_per"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["sgst_per"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["cgst_amt"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["sgst_amt"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));
                        }
                        dataGridView1_RowsRemoved(dataGridView1, ee);
                    }
                }
                catch { }
            }
            if (e.ColumnIndex == 9)
            {
                try
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString() != "" || dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString() != "0"
                        || dataGridView1.Rows[e.RowIndex].Cells["R_qty"].Value.ToString() != "" || dataGridView1.Rows[e.RowIndex].Cells["R_qty"].Value.ToString() != "0")
                    {
                        if (cmbState.Text == company_state)
                        {
                            // dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc"].Value.ToString())) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value  = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["R_qty"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString())))));

                            dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value  = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()))) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["cgst_per"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["sgst_per"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["cgst_amt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["sgst_amt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString()) / 2));

                            dataGridView1.Rows[e.RowIndex].Cells["igst_per"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["igst_amt"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));
                        }
                        else
                        {
                            //dataGridView1.Rows[e.RowIndex - 1].Cells["disc_amt"].Value = String.Format("{0:0.00}", ((Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["rate1"].Value.ToString()) * Double.Parse(dataGridView1.Rows[e.RowIndex - 1].Cells["disc"].Value.ToString())) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["R_qty"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["rate1"].Value.ToString())))));

                            dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value = String.Format("{0:0.00}", (((Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString())) * (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString()))) / 100));

                            dataGridView1.Rows[e.RowIndex].Cells["igst_per"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["GST_per"].Value.ToString())));

                            dataGridView1.Rows[e.RowIndex].Cells["igst_amt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));

                            dataGridView1.Rows[e.RowIndex].Cells["cgst_per"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["sgst_per"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["cgst_amt"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["sgst_amt"].Value = "0";

                            dataGridView1.Rows[e.RowIndex].Cells["Tamt"].Value = String.Format("{0:0.00}", (Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["amount1"].Value.ToString()) + Double.Parse(dataGridView1.Rows[e.RowIndex].Cells["gst_amt"].Value.ToString())));
                        }
                        dataGridView1_RowsRemoved(dataGridView1, ee);
                    }
                }
                catch { }
            }
            dataGridView1_RowsRemoved(dataGridView1, ee);
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Double Mtr = 0, CgstAmt = 0, IgstAmt = 0, SgstAmt = 0, QTY1 = 0, tWT = 0; ;
            int rows = Convert.ToInt32(dataGridView1.RowCount.ToString());

            for (int i = 0; i < rows; i++)
            {
                try
                {
                    if (dataGridView1.Rows[i].Cells["R_qty"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["R_qty"].Value.ToString() != "")
                        {
                            QTY1 += Double.Parse(dataGridView1.Rows[i].Cells["R_qty"].Value.ToString());
                        }
                    }
                    if (dataGridView1.Rows[i].Cells["amount1"].Value != null)
                    {
                        if (dataGridView1.Rows[i].Cells["amount1"].Value.ToString() != "")
                        {
                            Mtr += Double.Parse(dataGridView1.Rows[i].Cells["amount1"].Value.ToString());
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
                }
                catch { }
            }

            textBox3.Text = String.Format("{0:0.00}", double.Parse(rows.ToString()));
            txttotqty.Text = String.Format("{0:0.00}", double.Parse(QTY1.ToString()));
            lblTot_Amt.Text = String.Format("{0:0.00}", double.Parse(Mtr.ToString()));
            lblcgstamt.Text = String.Format("{0:0.00}", double.Parse(CgstAmt.ToString()));
            lbligstamt.Text = String.Format("{0:0.00}", double.Parse(IgstAmt.ToString()));
            lblsgstamt.Text = String.Format("{0:0.00}", double.Parse(SgstAmt.ToString()));
            TVatAmt = (Double.Parse(lblcgstamt.Text) + Double.Parse(lbligstamt.Text) + Double.Parse(lblsgstamt.Text));
            lblTotalgst.Text = (TVatAmt).ToString();
            lblNet_Amt.Text = String.Format("{0:0.00}", (Double.Parse(lblTot_Amt.Text) + Double.Parse((TVatAmt).ToString())), 2);
        }

        public bool Validation()
        {
            if (txtSupBillNo.Text == "")
            {
                MessageBox.Show("Purchase Bill No. cannot be blank.");
                return false;
            }
            if (cmbwarehouse.Text == "")
            {
                MessageBox.Show("Warehouse cannot be blank.");
                return false;
            }
            if (dataGridView1.RowCount < 1)
            {
                MessageBox.Show("Item Detail section cannot be blank.");
            }
            try
            {
                int rows = dataGridView1.Rows.Count - 1;
                for (int j = 0; j < rows; j++)
                {
                    if (dataGridView1.Rows[j].Cells["R_qty"].Value == "0" || dataGridView1.Rows[j].Cells["R_qty"].Value == null)
                    {
                        MessageBox.Show("Return Qty cannot be 0.Check row no." + j + ".");
                        return false;
                    }
                }
            }
            catch { }
            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void txtSupBillNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                DataGridViewRowsRemovedEventArgs ee = null;
                
                string commandT1 = "SELECT [Pur_BillDate],isnull(cast([TotalAmt] as decimal(18,2)),0) as TotalAmt,isnull(cast([TotalGST] as decimal(18,2)),0) as TotalGST," +
                                   "isnull(cast([CGSTAmt] as decimal(18,2)),0) as CGSTAmt ,isnull(cast([SGSTAmt] as decimal(18,2)),0) as SGSTAmt " +
                                   ",isnull(cast([IGSTAmt] as decimal(18,2)),0) as IGSTAmt ,isnull(cast([NetAmt] as decimal(18,2)),0) as NetAmt "+
                                   "FROM [Purchase] where [Pur_BillNo] = '" + txtSupBillNo.Text.TrimEnd().TrimStart() + "' and [SupName] = '" + cmbSupName.Text.TrimEnd().TrimStart() + "'";
                
                dr = ObjCon.getData(commandT1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dtpBill_Date.Text = dr["Pur_BillDate"].ToString();
                        lblTot_Amt.Text = dr["TotalAmt"].ToString();
                        lblTotalgst.Text = dr["TotalGST"].ToString();
                        lblcgstamt.Text = dr["CGSTAmt"].ToString();
                        lblsgstamt.Text = dr["SGSTAmt"].ToString();
                        lbligstamt.Text = dr["IGSTAmt"].ToString();
                        lblNet_Amt.Text = dr["NetAmt"].ToString();
                    }
                }
                dr.Close();

                dataGridView1_RowsRemoved(dataGridView1, ee);
                int count = 0;

                string commandT2 =  "select [P_Name],[EAC_code],[Groups],[unit1],[Rack],cast([P_QTY] as decimal(18,0)) as P_QTY,"+
                                    "cast([Qty] as decimal(18,0)) as Qty,cast([Rate] as decimal(18,2)) as Rate,"+
                                    "cast([Amount] as decimal(18,2)) as Amount,cast([cgsttax] as decimal(18,2)) as cgsttax,"+
                                    "cast([cgstamt] as decimal(18,2)) as cgstamt,cast([sgsttax] as decimal(18,2)) as sgsttax,"+
                                    "cast([sgstamt] as decimal(18,2)) as sgstamt,cast([igsttax] as decimal(18,2)) as igsttax,"+
                                    "cast([igstamt] as decimal(18,2)) as igstamt,cast([gst_amt] as decimal(18,2)) as gst_amt,"+
                                    "cast([GST_per] as decimal(18,2)) as GST_per,cast([total_amount] as decimal(18,2)) as total_amount,[pk] "+
                                    "from [SubPurchase] where [Pur_BillNo] = '"+txtSupBillNo.Text.TrimEnd().TrimStart()+"'";
                
                dr = ObjCon.getData(commandT2);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[count].Cells["EACcode"].Value = dr["EAC_code"].ToString();
                            dataGridView1.Rows[count].Cells["p_name"].Value = dr["P_Name"].ToString();
                            dataGridView1.Rows[count].Cells["group"].Value = dr["Groups"].ToString();
                            dataGridView1.Rows[count].Cells["unit"].Value = dr["unit1"].ToString();
                            dataGridView1.Rows[count].Cells["rack1"].Value = dr["Rack"].ToString();
                            dataGridView1.Rows[count].Cells["P_qty"].Value = dr["P_QTY"].ToString();
                            dataGridView1.Rows[count].Cells["rate1"].Value = dr["Rate"].ToString();
                            dataGridView1.Rows[count].Cells["amount1"].Value = dr["Amount"].ToString();
                            dataGridView1.Rows[count].Cells["gst_amt"].Value = dr["gst_amt"].ToString();
                            dataGridView1.Rows[count].Cells["cgst_per"].Value = dr["cgsttax"].ToString();
                            dataGridView1.Rows[count].Cells["cgst_amt"].Value = dr["cgstamt"].ToString();
                            dataGridView1.Rows[count].Cells["sgst_per"].Value = dr["sgsttax"].ToString();
                            dataGridView1.Rows[count].Cells["sgst_amt"].Value = dr["sgstamt"].ToString();
                            dataGridView1.Rows[count].Cells["igst_per"].Value = dr["igsttax"].ToString();
                            dataGridView1.Rows[count].Cells["igst_amt"].Value = dr["igstamt"].ToString();
                            dataGridView1.Rows[count].Cells["Tamt"].Value = dr["total_amount"].ToString();
                            dataGridView1.Rows[count].Cells["GST_per"].Value = dr["GST_per"].ToString();
                            dataGridView1.Rows[count].Cells["pro_pk"].Value = dr["pk"].ToString();
                            dataGridView1.Rows[count].Cells["R_qty"].Value = "0";
                            count++;
                     }
                }
                dr.Close();
                try
                {
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        string stock = null;
                        string commandT5 = "exec Stock_Barcodewise '" + dataGridView1.Rows[i].Cells["EACcode"].Value.ToString() + "','" + company + "','" + dataGridView1.Rows[i].Cells["rack1"].Value.ToString() + "','"+cmbwarehouse.Text.TrimEnd().TrimStart()+"'";
                        dr2 = ObjCon.getData(commandT5);
                        if (dr2.HasRows == true)
                        {
                            while (dr2.Read())
                            {
                                stock = dr2[0].ToString();
                            }
                        }
                        dr2.Close();
                        dataGridView1.Rows[i].Cells["qty1"].Value = String.Format("{0:0.00}", Double.Parse(stock));
                        dataGridView1.Rows[i].Cells["st_qty"].Value = String.Format("{0:0.00}", Double.Parse(stock));
                    }
                }
                catch { }
                dataGridView1_RowsRemoved(dataGridView1, ee);
            }
            catch
            {
            }
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
                        string str5 = "select [dbo].[GenDebitNoteNo]('"+dateTimePicker1.Value.ToString("MM/dd/yyyy")+"','"+company+"')";
                        dr = ObjCon.getData(str5);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            { 
                                txtdebitno.Text = dr[0].ToString();
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
                                //Purchase_Return_Save
                                command = new SqlCommand("Purchase_Return_Save", connection, transaction);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@debitNoteNo",txtdebitno.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@debitnoteDate",dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@Pur_BillNo",txtSupBillNo.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Pur_BillDate",dtpBill_Date.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@SupName",cmbSupName.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@SupId",sup_id);
                                command.Parameters.AddWithValue("@Mobile",txtMob.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@sup_state",cmbState.Text);
                                command.Parameters.AddWithValue("@sup_gst_no",txtgstno.Text);
                                command.Parameters.AddWithValue("@sup_email",txtemail.Text);
                                command.Parameters.AddWithValue("@PaymentMode","");
                                command.Parameters.AddWithValue("@Warehouse",cmbwarehouse.Text);
                                command.Parameters.AddWithValue("@TotalAmt",lblTot_Amt.Text);
                                command.Parameters.AddWithValue("@TotalGST",lblTotalgst.Text);
                                command.Parameters.AddWithValue("@CGSTAmt",lblcgstamt.Text);
                                command.Parameters.AddWithValue("@SGSTAmt",lblsgstamt.Text);
                                command.Parameters.AddWithValue("@IGSTAmt",lbligstamt.Text);
                                command.Parameters.AddWithValue("@NetAmt",lblNet_Amt.Text);
                                command.Parameters.AddWithValue("@EnterBy",username);
                                command.Parameters.AddWithValue("@Company",company);
                                command.Parameters.AddWithValue("@Narration", txtNarration.Text);
                                command.ExecuteNonQuery();

                                //SubPurchaseReturn_save
                                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count - 1); i++)
                                {
                                    command = new SqlCommand("SubPurchaseReturn_save", connection, transaction);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@debitNoteNo",txtdebitno.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Pur_BillNo",txtSupBillNo.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@ItemName", dataGridView1.Rows[i].Cells["p_name"].Value.ToString());
                                    command.Parameters.AddWithValue("@EACcode", dataGridView1.Rows[i].Cells["EACcode"].Value.ToString());
                                    command.Parameters.AddWithValue("@Groups", dataGridView1.Rows[i].Cells["group"].Value.ToString());
                                    command.Parameters.AddWithValue("@unit1", dataGridView1.Rows[i].Cells["unit"].Value.ToString());
                                    if (dataGridView1.Rows[i].Cells["rack1"].Value.ToString() == "" || dataGridView1.Rows[i].Cells["rack1"].Value.ToString() == null)
                                    {
                                        command.Parameters.AddWithValue("@rack", "");
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("@rack", dataGridView1.Rows[i].Cells["rack1"].Value.ToString());
                                    }
                                    command.Parameters.AddWithValue("@P_QTY", dataGridView1.Rows[i].Cells["P_qty"].Value.ToString());
                                    command.Parameters.AddWithValue("@Qty", dataGridView1.Rows[i].Cells["st_qty"].Value.ToString());
                                    command.Parameters.AddWithValue("@R_QTY", dataGridView1.Rows[i].Cells["R_qty"].Value.ToString());
                                    command.Parameters.AddWithValue("@Rate", dataGridView1.Rows[i].Cells["rate1"].Value.ToString());
                                    command.Parameters.AddWithValue("@Amt", dataGridView1.Rows[i].Cells["amount1"].Value.ToString());
                                    command.Parameters.AddWithValue("@cgsttax", dataGridView1.Rows[i].Cells["cgst_per"].Value.ToString());
                                    command.Parameters.AddWithValue("@cgstamt", dataGridView1.Rows[i].Cells["cgst_amt"].Value.ToString());
                                    command.Parameters.AddWithValue("@sgsttax", dataGridView1.Rows[i].Cells["sgst_per"].Value.ToString());
                                    command.Parameters.AddWithValue("@sgstamt", dataGridView1.Rows[i].Cells["sgst_amt"].Value.ToString());
                                    command.Parameters.AddWithValue("@igsttax", dataGridView1.Rows[i].Cells["igst_per"].Value.ToString());
                                    command.Parameters.AddWithValue("@igstamt", dataGridView1.Rows[i].Cells["igst_amt"].Value.ToString());
                                    command.Parameters.AddWithValue("@gst_amt", dataGridView1.Rows[i].Cells["gst_amt"].Value.ToString());
                                    command.Parameters.AddWithValue("@GST_per", dataGridView1.Rows[i].Cells["GST_per"].Value.ToString());
                                    command.Parameters.AddWithValue("@total_amount", dataGridView1.Rows[i].Cells["Tamt"].Value.ToString());
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
                    catch
                    { }
                }
            }
        }

        public void Clear()
        {
            txtSupBillNo.Text = "";
            cmbState.Text = "";
            cmbSupName.Text = "";
            txtemail.Text = "";
            txtMob.Text = "";
            txtgstno.Text = "";
            txtNarration.Text = "";
            txttotqty.Text = "";
            lblcgstamt.Text = "0";
            lblsgstamt.Text = "0";
            lbligstamt.Text = "0";
            lblTot_Amt.Text = "0";
            lblNet_Amt.Text = "0";
            lblTotalgst.Text = "0";
            textBox3.Text = "0";
            dataGridView1.Rows.Clear();
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
                        string str5 = "select [dbo].[GenDebitNoteNo]('" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "','" + company + "')";
                        dr = ObjCon.getData(str5);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtdebitno.Text = dr[0].ToString();
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
                                //Purchase_Return_Save
                                command = new SqlCommand("Purchase_Return_Save", connection, transaction);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@debitNoteNo", txtdebitno.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@debitnoteDate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@Pur_BillNo", txtSupBillNo.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Pur_BillDate", dtpBill_Date.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@SupName", cmbSupName.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@SupId", sup_id);
                                command.Parameters.AddWithValue("@Mobile", txtMob.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@sup_state", cmbState.Text);
                                command.Parameters.AddWithValue("@sup_gst_no", txtgstno.Text);
                                command.Parameters.AddWithValue("@sup_email", txtemail.Text);
                                command.Parameters.AddWithValue("@PaymentMode", "");
                                command.Parameters.AddWithValue("@Warehouse", cmbwarehouse.Text);
                                command.Parameters.AddWithValue("@TotalAmt", lblTot_Amt.Text);
                                command.Parameters.AddWithValue("@TotalGST", lblTotalgst.Text);
                                command.Parameters.AddWithValue("@CGSTAmt", lblcgstamt.Text);
                                command.Parameters.AddWithValue("@SGSTAmt", lblsgstamt.Text);
                                command.Parameters.AddWithValue("@IGSTAmt", lbligstamt.Text);
                                command.Parameters.AddWithValue("@NetAmt", lblNet_Amt.Text);
                                command.Parameters.AddWithValue("@EnterBy", username);
                                command.Parameters.AddWithValue("@Company", company);
                                command.Parameters.AddWithValue("@Narration", txtNarration.Text);
                                command.ExecuteNonQuery();

                                //SubPurchaseReturn_save
                                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count - 1); i++)
                                {
                                    command = new SqlCommand("SubPurchaseReturn_save", connection, transaction);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@debitNoteNo", txtdebitno.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Pur_BillNo", txtSupBillNo.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@ItemName", dataGridView1.Rows[i].Cells["p_name"].Value.ToString());
                                    command.Parameters.AddWithValue("@EACcode", dataGridView1.Rows[i].Cells["EACcode"].Value.ToString());
                                    command.Parameters.AddWithValue("@Groups", dataGridView1.Rows[i].Cells["group"].Value.ToString());
                                    command.Parameters.AddWithValue("@unit1", dataGridView1.Rows[i].Cells["unit"].Value.ToString());
                                    if (dataGridView1.Rows[i].Cells["rack1"].Value.ToString() == "" || dataGridView1.Rows[i].Cells["rack1"].Value.ToString() == null)
                                    {
                                        command.Parameters.AddWithValue("@rack", "");
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("@rack", dataGridView1.Rows[i].Cells["rack1"].Value.ToString());
                                    }
                                    command.Parameters.AddWithValue("@P_QTY", dataGridView1.Rows[i].Cells["P_qty"].Value.ToString());
                                    command.Parameters.AddWithValue("@Qty", dataGridView1.Rows[i].Cells["st_qty"].Value.ToString());
                                    command.Parameters.AddWithValue("@R_QTY", dataGridView1.Rows[i].Cells["R_qty"].Value.ToString());
                                    command.Parameters.AddWithValue("@Rate", dataGridView1.Rows[i].Cells["rate1"].Value.ToString());
                                    command.Parameters.AddWithValue("@Amt", dataGridView1.Rows[i].Cells["amount1"].Value.ToString());
                                    command.Parameters.AddWithValue("@cgsttax", dataGridView1.Rows[i].Cells["cgst_per"].Value.ToString());
                                    command.Parameters.AddWithValue("@cgstamt", dataGridView1.Rows[i].Cells["cgst_amt"].Value.ToString());
                                    command.Parameters.AddWithValue("@sgsttax", dataGridView1.Rows[i].Cells["sgst_per"].Value.ToString());
                                    command.Parameters.AddWithValue("@sgstamt", dataGridView1.Rows[i].Cells["sgst_amt"].Value.ToString());
                                    command.Parameters.AddWithValue("@igsttax", dataGridView1.Rows[i].Cells["igst_per"].Value.ToString());
                                    command.Parameters.AddWithValue("@igstamt", dataGridView1.Rows[i].Cells["igst_amt"].Value.ToString());
                                    command.Parameters.AddWithValue("@gst_amt", dataGridView1.Rows[i].Cells["gst_amt"].Value.ToString());
                                    command.Parameters.AddWithValue("@GST_per", dataGridView1.Rows[i].Cells["GST_per"].Value.ToString());
                                    command.Parameters.AddWithValue("@total_amount", dataGridView1.Rows[i].Cells["Tamt"].Value.ToString());
                                    command.Parameters.AddWithValue("@Company", company);
                                    command.Parameters.AddWithValue("@EnterBy", username);
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
                    catch
                    { }
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
            try
            {
                txtSupBillNo.Items.Clear();
                cmbwarehouse.Items.Clear();
                dataGridView1.Rows.Clear();
                string str5 = "select [Pur_BillNo],[SupId],[Mobile],[sup_state],[sup_gst_no],[sup_email],[Warehouse] FROM [Purchase] where [SupName] = '" + cmbSupName.Text.TrimEnd().TrimStart() + "'";
                dr = ObjCon.getData(str5);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtSupBillNo.Items.Add(dr["Pur_BillNo"].ToString());
                        sup_id = Convert.ToInt32(dr["SupId"].ToString());
                        txtMob.Text = dr["Mobile"].ToString();
                        txtemail.Text = dr["sup_email"].ToString();
                        cmbState.Text = dr["sup_state"].ToString();
                        txtgstno.Text = dr["sup_gst_no"].ToString();
                        cmbwarehouse.Text = dr["Warehouse"].ToString();
                    }
                }
                dr.Close();
            }
            catch { }
        }
    }
}
