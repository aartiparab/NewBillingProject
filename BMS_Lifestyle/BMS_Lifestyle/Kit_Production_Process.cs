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
    public partial class Kit_Production_Process : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectDb ObjCon = new ConnectDb();
        SqlDataReader dr = null;
        string username, company, connectionString;
        int affect, flag1 = 0;
        string purchaseOrderPK, Sup_id;
        AutoComplete objAccName = new AutoComplete();
        string company_state, companyname, commandT;
        int sum =0, OutofFlag =0;
        public Kit_Production_Process( string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Kit_Production_Process_Load(object sender, EventArgs e)
        {
            try
            {
                string str1 = "select distinct [kit_name] from [BOM_Master]";
                dr = ObjCon.getData(str1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbKitName.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
            }
            catch
            { }

            try
            {
                string str1 = "select [Name] FROM [WareHouse_Master] order by [GID] asc";
                dr = ObjCon.getData(str1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbwarehouse.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
            }
            catch
            { }
        }

        private void cmbKitName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKitName.Text != "")
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    txtQty.Text = "0";
                    DataGridViewRowsRemovedEventArgs ee = null;
                    string str2 = "select [EAC_Code],[kit_Group] FROM [Kit_Master] where [Kit_Name] = '" + cmbKitName.Text.TrimEnd().TrimStart() + "'";
                    dr = ObjCon.getData(str2);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            txtEACcode.Text = dr[0].ToString();
                            txtGroup.Text = dr[1].ToString();
                            txtQty.Text = "0";
                        }
                    }
                    dr.Close();

                    int count1 = 0;
                    string str3 = "select [EAC_Code],[ItemName],cast([Qty] as decimal(18,0)) as Qty FROM [Sub_BOM_Master] where [kitname] = '"+cmbKitName.Text.TrimEnd().TrimStart()+"'";
                    dr = ObjCon.getData(str3);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[count1].Cells["EACcode"].Value = dr["EAC_Code"].ToString();
                            dataGridView1.Rows[count1].Cells["ItemName"].Value = dr["ItemName"].ToString();
                            dataGridView1.Rows[count1].Cells["qty1"].Value = dr["Qty"].ToString();
                            dataGridView1.Rows[count1].Cells["stkqty"].Value = "0";
                            count1++;
                        }
                    }
                    dr.Close();
                    dataGridView1_RowsRemoved(dataGridView1, ee);
                }
                catch { }
            }
            else
            {
                MessageBox.Show("Select Kit Name.");
                this.ActiveControl = cmbKitName;
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            int rows = dataGridView1.Rows.Count;
            Double QTY1 = 0;
            for (int i = 0; i < rows; i++)
            {
                if (dataGridView1.Rows[i].Cells["qty1"].Value != null)
                {
                    if (dataGridView1.Rows[i].Cells["qty1"].Value.ToString() != "")
                    {
                        QTY1 += Double.Parse(dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                    }
                }
            }
            lblTotalQty.Text = (QTY1 * Double.Parse(txtQty.Text)).ToString();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text != "")
            {
                try
                {
                    DataGridViewRowsRemovedEventArgs ee = null;
                    // Check stock of item  
                    for (int j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        string stock = null;

                        commandT = "exec Stock_Barcodewise '" + dataGridView1.Rows[j].Cells["EACcode"].Value.ToString() + "','" + company + "','All','"+cmbwarehouse.Text.TrimEnd().TrimStart()+"'";
                        dr = ObjCon.getData(commandT);
                        if (dr.HasRows == true)
                        {
                            while (dr.Read())
                            {
                                stock = dr[0].ToString();
                            }
                        }
                        dr.Close();
                        dataGridView1.Rows[j].Cells["stkqty"].Value = stock;
                        
                        int qty = Convert.ToInt32(dataGridView1.Rows[j].Cells["qty1"].Value.ToString());

                        int stk_qty = Convert.ToInt32(dataGridView1.Rows[j].Cells["stkqty"].Value.ToString());

                        if (stk_qty >= qty)
                        {
                            dataGridView1.Rows[j].Cells["Bal_stk_qty"].Value = (Convert.ToInt32(dataGridView1.Rows[j].Cells["stkqty"].Value.ToString()) - (Convert.ToInt32(dataGridView1.Rows[j].Cells["qty1"].Value.ToString()) * Convert.ToInt32(txtQty.Text))).ToString();
                        }
                        else
                        {
                            dataGridView1.Rows[j].Cells["Bal_stk_qty"].Value = (Convert.ToInt32(dataGridView1.Rows[j].Cells["stkqty"].Value.ToString()) - (Convert.ToInt32(dataGridView1.Rows[j].Cells["qty1"].Value.ToString()) * Convert.ToInt32(txtQty.Text))).ToString();
                            MessageBox.Show(dataGridView1.Rows[j].Cells["EACcode"].Value.ToString()+" is out of stock");
                            OutofFlag = 1;
                        }
                    }
                    dataGridView1_RowsRemoved(dataGridView1,ee);
                }
                catch 
                { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool txtValidation()
        {
            if(OutofFlag == 1)
            {
                MessageBox.Show("Stock Qty cannot be zero or less than zero.");
                this.ActiveControl = txtQty;
                return false;
            }
            if(cmbKitName.Text == "")
            {
                MessageBox.Show("Select Kit Name.");
                this.ActiveControl = cmbKitName;
                return false;
            }
            if (cmbwarehouse.Text == "")
            {
                MessageBox.Show("Select warehouse Name.");
                this.ActiveControl = cmbKitName;
                return false;
            }
            if (cmbrack.Text == "")
            {
                MessageBox.Show("Select rack Name.");
                this.ActiveControl = cmbKitName;
                return false;
            }
            if (dataGridView1.Rows.Count < 1)
            {
                MessageBox.Show("Item details cannot be blank.");
                return false;
            }
            if (lblTotalQty.Text == "0")
            {
                MessageBox.Show("Total Qty cannot be zero.");
                return false;
            }
            try
            {
                int rows = dataGridView1.Rows.Count - 1;
                for (int j = 0; j < rows; j++)
                {
                    if (int.Parse(dataGridView1.Rows[j].Cells["Bal_stk_qty"].Value.ToString()) < 0)
                    {
                        MessageBox.Show("Balance Qty cannot be less than zero.");
                        return false;
                    }
                    if (dataGridView1.Rows[j].Cells["rack"].Value.ToString() == "" || dataGridView1.Rows[j].Cells["rack"].Value.ToString() == null)
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
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = connection.CreateCommand();
                            SqlTransaction transaction;
                            transaction = connection.BeginTransaction("SampleTransaction");

                            try
                            {
                                    //KitProduction_Save
                                    command = new SqlCommand("KitProduction_Save", connection, transaction);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@Kit_Name",cmbKitName.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@KitEAC_Code",txtEACcode.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@KitGroup",txtGroup.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Production_date",dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@KitQty",txtQty.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@ItemQty",lblTotalQty.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Enter_by",username);
                                    command.Parameters.AddWithValue("@company", company);
                                    command.Parameters.AddWithValue("@rack", cmbrack.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@warehouse",cmbwarehouse.Text.TrimEnd().TrimStart());
                                    
                                    command.ExecuteNonQuery();
                                
                                  //SubkitProduction_Save
                                    for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count); i++)
                                    {
                                        command = new SqlCommand("SubkitProduction_Save", connection, transaction);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@Kit_Name", cmbKitName.Text.TrimEnd().TrimStart());
                                        command.Parameters.AddWithValue("@ItemName", dataGridView1.Rows[i].Cells["ItemName"].Value.ToString());
                                        command.Parameters.AddWithValue("@ItemEACcode", dataGridView1.Rows[i].Cells["EACcode"].Value.ToString());
                                        command.Parameters.AddWithValue("@ItemQty", dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                                        command.Parameters.AddWithValue("@StockQty", dataGridView1.Rows[i].Cells["stkqty"].Value.ToString());
                                        command.Parameters.AddWithValue("@BalQty", dataGridView1.Rows[i].Cells["Bal_stk_qty"].Value.ToString());
                                        command.Parameters.AddWithValue("@Enterby", username);
                                        command.Parameters.AddWithValue("@company", company);
                                        command.Parameters.AddWithValue("@rack",dataGridView1.Rows[i].Cells["rack"].Value.ToString());
                                        command.Parameters.AddWithValue("@warehouse", cmbwarehouse.Text.TrimEnd().TrimStart());

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
            cmbKitName.Text = "";
            txtEACcode.Text = "";
            txtGroup.Text = "";
            txtQty.Text = "0";
            dataGridView1.Rows.Clear();
            lblTotalQty.Text = "0";
        }

        private void cmbwarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbwarehouse.Text != "")
            {
                try
                {
                    cmbrack.Items.Clear();

                    string strrack = "select distinct [RackName] from [Rack_Master] where [WareHouse_Name] = '" + cmbwarehouse.Text.TrimEnd().TrimStart() + "'";
                    dr = ObjCon.getData(strrack);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cmbrack.Items.Add(dr[0].ToString().ToUpper());
                            rack.Items.Add(dr[0].ToString());
                        }
                    }
                    dr.Close();
                }
                catch
                { }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString().ToUpper();
                e.FormattingApplied = true;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRowsRemovedEventArgs ee = null;
            string stock = null;

            if (e.ColumnIndex == 3)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["rack"].Value != null)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells["rack"].Value != "")
                    {
                        try
                        {
                            commandT = "exec Stock_Barcodewise '" + dataGridView1.Rows[e.RowIndex].Cells["EACcode"].Value.ToString() + "','" + company + "','" + dataGridView1.Rows[e.RowIndex].Cells["rack"].Value.ToString() + "','" + cmbwarehouse.Text.TrimEnd().TrimStart() + "'";
                            dr = ObjCon.getData(commandT);
                            if (dr.HasRows == true)
                            {
                                while (dr.Read())
                                {
                                    if (Convert.ToInt32(dr[0].ToString()) > 0)
                                    {
                                        stock = dr[0].ToString();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Not in Stock....", "Error", MessageBoxButtons.OK);
                                        dataGridView1.Rows[e.RowIndex].Cells["rack"].Value = "";
                                        this.ActiveControl = txtQty;
                                        txtQty.Text = "";
                                    }
                                }
                            }
                            dr.Close();
                            dataGridView1.Rows[e.RowIndex].Cells["stkqty"].Value = stock;

                            int qty = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["qty1"].Value.ToString());

                            int stk_qty = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["stkqty"].Value.ToString());

                            if (stk_qty >= qty)
                            {
                                dataGridView1.Rows[e.RowIndex].Cells["Bal_stk_qty"].Value = (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["stkqty"].Value.ToString()) - (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["qty1"].Value.ToString()) * Convert.ToInt32(txtQty.Text))).ToString();
                            }
                            else
                            {
                                dataGridView1.Rows[e.RowIndex].Cells["Bal_stk_qty"].Value = (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["stkqty"].Value.ToString()) - (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["qty1"].Value.ToString()) * Convert.ToInt32(txtQty.Text))).ToString();
                                MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells["EACcode"].Value.ToString() + " is out of stock");
                                OutofFlag = 1;
                            }
                            dataGridView1_RowsRemoved(dataGridView1, ee);
                        }
                        catch { }
                    }
                }
            }
            dataGridView1_RowsRemoved(dataGridView1, ee);
        }
    }
}
