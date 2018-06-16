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
    public partial class Sales_Return : Form
    {
        string username, company, connectionString, PONo1;
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectDb ObjCon = new ConnectDb();
        SqlDataReader dr = null;
        int affect, flag1 = 0;
        string purchaseOrderPK, Sup_id;
        AutoComplete objAccName = new AutoComplete();
        string company_state, companyname, commandT;
        int cust_id, mp_id,sum, m = 0;
        string T_BalQTY;
        bool ispresent = false;
        public Sales_Return(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Sales_Return_Load(object sender, EventArgs e)
        {
            try
            {
                string str1 = "select distinct [CustName] FROM [Sales]";
                dr = ObjCon.getData(str1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                            cmbCustName.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();

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

                string commandT1 = "select [State_Code] from [State_Code_Master] order by [State_Code] asc";
                dr = ObjCon.getData(commandT1);
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        cmbstate.Items.Add(dr[0].ToString().ToUpper().ToUpper());
                    }

                }
                dr.Close();
                cmbstate.SelectedIndex = 26;

                string str9 = "select [Name] FROM [WareHouse_Master] order by [GID] asc";
                dr = ObjCon.getData(str9);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbwarehouse.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();

                string str10 = "select [Name] from [Salesman_Master]";
                dr = ObjCon.getData(str10);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbMarketPlace.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();

                this.ActiveControl = cmbCustName;
            }
            catch { }
        }

        private void cmbCustName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustName.Text != "")
            {
                try
                {
                    string str2 = "select [Mobile],[EmailId],[Gst_No],[State],[CustId] FROM [Customer] where [CustName] = '"+cmbCustName.Text.TrimEnd().TrimStart()+"'";
                    dr = ObjCon.getData(str2);
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            txtmob.Text = dr[0].ToString();
                            txtemail.Text = dr[1].ToString();
                            cmbstate.Text = dr[3].ToString();
                            txtgstno.Text = dr[2].ToString();
                            cust_id = Convert.ToInt32(dr[4].ToString());
                        }
                    }
                    dr.Close();
                }
                catch
                { }
            }
        }

        private void txt_EacCode_Leave(object sender, EventArgs e)
        {
            if (txt_EacCode.Text != "")
            {
                if (cmbwarehouse.Text != "")
                {
                    int flgchek = 0;
                    string strcheckbcode = "select distinct ss.[EAC_Code] from [SubSales] ss left join [Sales] s on ss.[sub_fk] = s.[SalesBillPk] "+
                                           "where s.[CustName] = '"+cmbCustName.Text.TrimEnd().TrimStart()+"' and ss.[EAC_Code] = '"+txt_EacCode.Text.TrimEnd().TrimStart()+"' and ss.[deleteFlag] = 'No'";
                    dr = ObjCon.getData(strcheckbcode);
                    if (dr.HasRows)
                    {
                        flgchek = 1;
                    }
                    dr.Close();

                    if (flgchek == 1)
                    {
                        try
                        {
                            DataGridViewRowsRemovedEventArgs ee = null;
                            sum = 0;
                            double balsku = 0;
                            string i = txt_EacCode.Text;
                            ispresent = false;

                            for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                            {
                                if (i == dataGridView1.Rows[j].Cells[0].Value.ToString())
                                {
                                    ispresent = true;
                                    m = j;
                                }
                            }

                            if (ispresent == false)
                            {
                                if (txt_EacCode.Text != "")
                                {
                                    int count = dataGridView1.RowCount - 1;
                                    int flagkit = 0, flagbcode = 0;
                                    try
                                    {
                                        //get data from subsales 
                                        string str66 = "select distinct [EAC_Code],[SName],[Group1],[unit1] from [SubSales] where [EAC_Code] = '"+txt_EacCode.Text.TrimEnd().TrimStart()+"' and [deleteFlag] = 'No'";
                                        dr = ObjCon.getData(str66);
                                        if (dr.HasRows)
                                        {
                                            while (dr.Read())
                                            {
                                                dataGridView1.Rows.Add();
                                                dataGridView1.Rows[count].Cells["barcode1"].Value = txt_EacCode.Text.TrimEnd().TrimStart();
                                                dataGridView1.Rows[count].Cells["itemname"].Value = dr["SName"].ToString();
                                                dataGridView1.Rows[count].Cells["group"].Value = dr["Group1"].ToString();
                                                dataGridView1.Rows[count].Cells["unit"].Value = dr["unit1"].ToString();
                                                dataGridView1.Rows[count].Cells["qty1"].Value = "1";
                                                //dataGridView1.Rows[count].Cells["type"].Value = "Item";
                                            }
                                        }
                                        dr.Close();

                                        //check total sale qty of customer for item
                                        string str77 = "select isnull(cast(SUM(ss.Qty) as decimal(18,0)),0) as Qty from [SubSales] ss left join Sales s on ss.[sub_fk] = s.SalesBillPk " +
                                                        "where s.CustName = '"+cmbCustName.Text.TrimEnd().TrimStart()+"' and ss.EAC_Code = '"+txt_EacCode.Text.TrimEnd().TrimStart()+"' and ss.[deleteFlag] = 'No'";
                                        dr = ObjCon.getData(str77);
                                        if (dr.HasRows)
                                        {
                                            while (dr.Read())
                                            {
                                                dataGridView1.Rows[count].Cells["stkqty"].Value = dr[0].ToString();
                                                dataGridView1.Rows[count].Cells["balqty"].Value = (Convert.ToInt32(dataGridView1.Rows[count].Cells["stkqty"].Value.ToString()) - 1).ToString(); 
                                            }
                                        }
                                        dr.Close(); 
                                        
                                    }
                                    catch { }

                                    this.ActiveControl = txt_EacCode;
                                    txt_EacCode.Text = "";
                                    dataGridView1_RowsRemoved(dataGridView1, ee);
                                }
                            }
                            else
                            {
                                int qty = Convert.ToInt32(dataGridView1.Rows[m].Cells["qty1"].Value.ToString());

                                int stkqty1 = Convert.ToInt32(dataGridView1.Rows[m].Cells["stkqty"].Value.ToString());

                                int balqty1 = Convert.ToInt32(dataGridView1.Rows[m].Cells["balqty"].Value.ToString());

                                if (balqty1 >= 0)
                                {
                                    if (qty < stkqty1)
                                    {
                                        sum = sum + Convert.ToInt32(dataGridView1.Rows[m].Cells["qty1"].Value.ToString());

                                        balsku = balsku + Convert.ToInt32(dataGridView1.Rows[m].Cells["balqty"].Value.ToString());

                                        if (sum < stkqty1)
                                        {
                                            dataGridView1.Rows[m].Cells["qty1"].Value = (Convert.ToInt32(dataGridView1.Rows[m].Cells["qty1"].Value.ToString()) + 1).ToString();

                                            dataGridView1.Rows[m].Cells["stkqty"].Value = (Convert.ToInt32(dataGridView1.Rows[m].Cells["stkqty"].Value.ToString()) + 1).ToString();
                                        }
                                        else
                                        {
                                            MessageBox.Show("The Qauntity is Exceeding...", "OK", MessageBoxButtons.OK);
                                        }
                                        this.ActiveControl = txt_EacCode;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Out of Sale...", "OK", MessageBoxButtons.OK);
                                       this.ActiveControl = txt_EacCode;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("The Qauntity is Exceeding...", "OK", MessageBoxButtons.OK);
                                    this.ActiveControl = txt_EacCode;
                                }
                                dataGridView1_RowsRemoved(dataGridView1, ee);
                                this.ActiveControl = txt_EacCode;
                                txt_EacCode.Text = "";
                            }
                        }
                        catch
                        {
                            dr.Close();
                        }
                    }
                    else if (flgchek == 0)
                    {
                        txt_EacCode.Text = "";
                        MessageBox.Show("This Item is not sold to this customer");
                        this.ActiveControl = txt_EacCode;
                    }
                    else { }
                }
                else 
                {
                    txt_EacCode.Text = "";
                    MessageBox.Show("select Warehouse");
                    this.ActiveControl = cmbwarehouse;
                }
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int rows = dataGridView1.RowCount - 1;
            double QTY1 = 0;
            for (int i = 0; i < rows; i++)
            {
                if (dataGridView1.Rows[i].Cells["qty1"].Value != "" || dataGridView1.Rows[i].Cells["qty1"].Value != null)
                {
                    QTY1 += double.Parse(dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                }
            }
            textBox2.Text = rows.ToString();
            txttotqty.Text = QTY1.ToString();
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

            //if (colIndex == dataGridView1.Rows[rowIndex].Cells["rack"].ColumnIndex)
            //{
            //    if (e.Control is TextBox)
            //    {
            //        if (txtBox != null)
            //        {

            //            txtBox.CharacterCasing = CharacterCasing.Upper;
            //            txtBox.TextChanged += new EventHandler(Caption_TextChanged);
            //            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //            txtBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //            string commandT = "select distinct [Rack] FROM [SubPurchase] s left join Purchase p on s.[Pur_Fk] = p.[Pur_BillPk] where s.[EAC_Code] = '" + dataGridView1.Rows[rowIndex].Cells["barcode1"].Value.ToString() + "' and p.Warehouse = '" + cmbwarehouse.Text.TrimEnd().TrimStart() + "'";
            //            objAccName.filltextbox(commandT);
            //            txtBox.AutoCompleteCustomSource = objAccName.productname;
            //        }
            //    }
            //}
        }

        void txtBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rows= dataGridView1.RowCount -1;
            for(int i = 0 ; i < rows ;i++)
            {
                if (dataGridView1.Rows[i].Cells["rack"].Value != null || dataGridView1.Rows[i].Cells["rack"].Value != "")
                {
                    try
                    {
                        DataGridViewRowsRemovedEventArgs ee = null;

                        string str77 = "exec Stock_Barcodewise '" + dataGridView1.Rows[i].Cells["barcode1"].Value.ToString() + "','" + company + "','" + dataGridView1.Rows[i].Cells["rack"].Value.ToString() + "','" + cmbwarehouse.Text.TrimEnd().TrimStart() + "'";
                        dr = ObjCon.getData(str77);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                if (Convert.ToInt32(dr[0].ToString()) > 0)
                                {
                                    dataGridView1.Rows[i].Cells["stkqty"].Value = dr[0].ToString();
                                    dataGridView1.Rows[i].Cells["balqty"].Value = (Convert.ToInt32(dataGridView1.Rows[i].Cells["stkqty"].Value.ToString()) - 1).ToString();
                                }
                                else
                                {
                                    MessageBox.Show("Not in Stock....", "OK", MessageBoxButtons.OK);
                                    this.ActiveControl = txt_EacCode;
                                    txt_EacCode.Text = "";
                                }
                            }
                        }
                        dr.Close();
                        dataGridView1_RowsRemoved(dataGridView1, ee);
                    }
                    catch { }
                    }   
            }
        }

        public bool txtValidation()
        {
            if (cmbCustName.Text == "")
            {
                MessageBox.Show("Select Customer Name.");
                this.ActiveControl = cmbCustName;
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
            if (cmbMarketPlace.Text == "")
            {
                MessageBox.Show("Select market Place.");
                this.ActiveControl = cmbMarketPlace;
                return false;
            }
            try
            {
                int rows = dataGridView1.Rows.Count - 1;
                for (int j = 0; j < rows; j++)
                {
                    if (dataGridView1.Rows[j].Cells["rack"].Value == "" || dataGridView1.Rows[j].Cells["rack"].Value == null)
                    {
                        MessageBox.Show("Select rack in " + j + " row.");
                        return false;
                    }
                }
            }
            catch { }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtValidation() == true)
            {
                string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();
                if (ans == "Yes")
                {
                    try
                    {
                        string CommandT1 = "select [dbo].[GenCreditNoteNo]('" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "','" + company + "')";
                        dr = ObjCon.getData(CommandT1);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtChallanNo.Text = dr[0].ToString();
                            }
                        }
                        dr.Close();
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = connection.CreateCommand();
                            SqlTransaction transaction;
                            transaction = connection.BeginTransaction("SampleTransaction");

                            try
                            {
                                //Sales_Return_Save
                                command = new SqlCommand("Sales_Return_Save", connection, transaction);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@CreditNoteNo", txtChallanNo.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@CreditDate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@CustName", cmbCustName.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@CustID", cust_id);
                                command.Parameters.AddWithValue("@Mobile", txtmob.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Email", txtemail.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@cust_state", cmbstate.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@cust_GST_No", txtgstno.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Warehouse", cmbwarehouse.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@narration", txtNarration.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@TotalQty", txttotqty.Text);
                                command.Parameters.AddWithValue("@Company", company);
                                command.Parameters.AddWithValue("@EnterBy", username);
                                command.Parameters.AddWithValue("@Salesman", txtmob.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@saleman_id", mp_id);
                                command.ExecuteNonQuery();

                                //SubSales_Return_save
                                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count - 1); i++)
                                {
                                    command = new SqlCommand("SubSales_Return_save", connection, transaction);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@CreditNoteNo", txtChallanNo.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@SName", dataGridView1.Rows[i].Cells["itemname"].Value.ToString());
                                    command.Parameters.AddWithValue("@EAC_Code", dataGridView1.Rows[i].Cells["barcode1"].Value.ToString());
                                    command.Parameters.AddWithValue("@Group1", dataGridView1.Rows[i].Cells["group"].Value.ToString());
                                    command.Parameters.AddWithValue("@unit1", dataGridView1.Rows[i].Cells["unit"].Value.ToString());
                                    if (dataGridView1.Rows[i].Cells["rack"].Value == "" || dataGridView1.Rows[i].Cells["rack"].Value == null)
                                    {
                                        command.Parameters.AddWithValue("@rack", "");
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("@rack", dataGridView1.Rows[i].Cells["rack"].Value.ToString());
                                    }
                                    command.Parameters.AddWithValue("@Qty", dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                                    //command.Parameters.AddWithValue("@Instock", dataGridView1.Rows[i].Cells["stkqty"].Value.ToString());
                                    //command.Parameters.AddWithValue("@BalQty", dataGridView1.Rows[i].Cells["balqty"].Value.ToString());
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
                    catch { }
                }
            }
        }
        public void Clear()
        {
            cmbMarketPlace.Text = "";
            cmbwarehouse.Text = "";
            cmbCustName.Text = "";
            txtmob.Text = "";
            txtemail.Text = "";
            txtgstno.Text = "";
            cmbstate.Text = "";
            txtNarration.Text = "";
            textBox2.Text = "0";
            txttotqty.Text = "0";
            txt_EacCode.Text = "";
            dataGridView1.Rows.Clear();
        }

        private void cmbMarketPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string st56 = "select [ID] from [Salesman_Master] where [Name] = '"+cmbMarketPlace.Text.TrimEnd().TrimStart()+"'";
                dr = ObjCon.getData(st56);
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        mp_id = Convert.ToInt32(dr[0].ToString());
                    }
                }
                dr.Close();
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtValidation() == true)
            {
                string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();
                if (ans == "Yes")
                {
                    try
                    {
                        string CommandT1 = "select dbo.[GenSalesBillNo] ('" + dateTimePicker1.Value.ToString("MM/dd/yyyy") + "','" + company + "')";
                        dr = ObjCon.getData(CommandT1);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtChallanNo.Text = dr[0].ToString();
                            }
                        }
                        dr.Close();
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = connection.CreateCommand();
                            SqlTransaction transaction;
                            transaction = connection.BeginTransaction("SampleTransaction");

                            try
                            {
                                //Sales_Return_Save
                                command = new SqlCommand("Sales_Return_Save", connection, transaction);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@CreditNoteNo", txtChallanNo.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@CreditDate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@CustName", cmbCustName.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@CustID", cust_id);
                                command.Parameters.AddWithValue("@Mobile", txtmob.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Email", txtemail.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@cust_state", cmbstate.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@cust_GST_No", txtgstno.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Warehouse", cmbwarehouse.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@narration", txtNarration.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@TotalQty", txttotqty.Text);
                                command.Parameters.AddWithValue("@Company", company);
                                command.Parameters.AddWithValue("@EnterBy", username);
                                command.Parameters.AddWithValue("@Salesman", txtmob.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@saleman_id", mp_id);
                                command.ExecuteNonQuery();

                                //SubSales_Return_save
                                for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count - 1); i++)
                                {
                                    command = new SqlCommand("SubSales_Return_save", connection, transaction);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@CreditNoteNo", txtChallanNo.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@SName", dataGridView1.Rows[i].Cells["itemname"].Value.ToString());
                                    command.Parameters.AddWithValue("@EAC_Code", dataGridView1.Rows[i].Cells["barcode1"].Value.ToString());
                                    command.Parameters.AddWithValue("@Group1", dataGridView1.Rows[i].Cells["group"].Value.ToString());
                                    command.Parameters.AddWithValue("@unit1", dataGridView1.Rows[i].Cells["unit"].Value.ToString());
                                    if (dataGridView1.Rows[i].Cells["rack"].Value == "" || dataGridView1.Rows[i].Cells["rack"].Value == null)
                                    {
                                        command.Parameters.AddWithValue("@rack", "");
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("@rack", dataGridView1.Rows[i].Cells["rack"].Value.ToString());
                                    }
                                    command.Parameters.AddWithValue("@Qty", dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                                    //command.Parameters.AddWithValue("@Instock", dataGridView1.Rows[i].Cells["stkqty"].Value.ToString());
                                    //command.Parameters.AddWithValue("@BalQty", dataGridView1.Rows[i].Cells["balqty"].Value.ToString());
                                    command.Parameters.AddWithValue("@Company", company);
                                    command.Parameters.AddWithValue("@EnterBy", username);
                                    command.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                challanRpt1.SetDatabaseLogon("sa", "preet@1234", "server", "BMSLifestyle");
                                challanRpt1.SetParameterValue("@challanNo", txtChallanNo.Text.TrimEnd().TrimStart());
                                challanRpt1.PrintToPrinter(1,true,1,50);

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

        private void cmbwarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbwarehouse.Text != "")
            {
                try
                {
                    rack.Items.Clear();

                    string strrack = "select distinct [RackName] from [Rack_Master] where [WareHouse_Name] = '" + cmbwarehouse.Text.TrimEnd().TrimStart() + "'";
                    dr = ObjCon.getData(strrack);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            rack.Items.Add(dr[0].ToString());
                        }
                    }
                    dr.Close();
                }
                catch
                { }
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRowsRemovedEventArgs ee = null;

            //if (e.ColumnIndex == 5)
            //{
            //    if (dataGridView1.Rows[e.RowIndex].Cells["rack"].Value != null || dataGridView1.Rows[e.RowIndex].Cells["rack"].Value != "")
            //    {
            //        try
            //        {
            //            if (dataGridView1.Rows[e.RowIndex].Cells["type"].Value == "Item")
            //            {
            //                string str77 = "exec Stock_Barcodewise '" + dataGridView1.Rows[e.RowIndex].Cells["barcode1"].Value.ToString() + "','" + company + "','" + dataGridView1.Rows[e.RowIndex].Cells["rack"].Value.ToString() + "','" + cmbwarehouse.Text.TrimEnd().TrimStart() + "'";
            //                dr = ObjCon.getData(str77);
            //                if (dr.HasRows)
            //                {
            //                    while (dr.Read())
            //                    {
                                    
            //                            dataGridView1.Rows[e.RowIndex].Cells["stkqty"].Value = dr[0].ToString();
            //                            //dataGridView1.Rows[e.RowIndex].Cells["balqty"].Value = (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["stkqty"].Value.ToString()) - 1).ToString();
                                    
            //                    }
            //                }
            //                dr.Close();
            //            }
            //            else if (dataGridView1.Rows[e.RowIndex].Cells["type"].Value == "kit")
            //            {
            //                string str77 = "exec Stock_KITwise '" + txt_EacCode.Text.TrimEnd().TrimStart() + "','" + company + "','" + dataGridView1.Rows[e.RowIndex].Cells["rack"].Value.ToString() + "','" + cmbwarehouse.Text.TrimEnd().TrimStart() + "'";
            //                dr = ObjCon.getData(str77);
            //                if (dr.HasRows)
            //                {
            //                    while (dr.Read())
            //                    {
                                    
            //                            dataGridView1.Rows[e.RowIndex].Cells["stkqty"].Value = dr[0].ToString();
            //                            //dataGridView1.Rows[e.RowIndex].Cells["balqty"].Value = (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["stkqty"].Value.ToString()) - 1).ToString();
                                    
            //                    }
            //                }
            //                dr.Close();
            //            }
            //            dataGridView1_RowsRemoved(dataGridView1, ee);
            //        }
            //        catch { }
            //    }
            //}
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        private void txt_EacCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
