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
    public partial class Saleable_Item_Screen : Form
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
        int cust_id, mp_id, sum, m = 0;
        string T_BalQTY;
        bool ispresent = false;

        public Saleable_Item_Screen(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Saleable_Item_Screen_Load(object sender, EventArgs e)
        {
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

        private void txt_EacCode_Leave(object sender, EventArgs e)
        {
            if (txt_EacCode.Text != "")
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
                                string str66 = "select [SName], isnull(cast(sum([Qty])as decimal(18,0)),0) as Qty from [SubSales_Return] ss left join [Sales_return] s "+
                                                "on ss.CreditNote_Fk = s.CreditPk where s.Warehouse = '" + cmbwarehouse.Text.TrimEnd().TrimStart() + "' and ss.EAC_Code = '"+txt_EacCode.Text.TrimEnd().TrimStart()+"' group by SName";
                                dr = ObjCon.getData(str66);
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        dataGridView1.Rows.Add();
                                        dataGridView1.Rows[count].Cells["EAC_Code"].Value = txt_EacCode.Text.TrimEnd().TrimStart();
                                        dataGridView1.Rows[count].Cells["s_name"].Value = dr["SName"].ToString();
                                        dataGridView1.Rows[count].Cells["qty1"].Value = "1";
                                        dataGridView1.Rows[count].Cells["R_qty"].Value = dr["Qty"].ToString();
                                    }
                                }
                                dr.Close();

                                string str77 = "select isnull(cast(sum([Qty])as decimal(18,0)),0) as Qty from [Saleable_Damage_Product] where [Warehouse] = '" + cmbwarehouse.Text.TrimEnd().TrimStart() + "' and [EAC_code] = '" + txt_EacCode.Text.TrimEnd().TrimStart() + "'";
                                dr = ObjCon.getData(str77);
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        dataGridView1.Rows[count].Cells["sa_da_qty"].Value = dr[0].ToString();
                                        dataGridView1.Rows[count].Cells["Bal_R_qty"].Value = (Convert.ToInt32(dataGridView1.Rows[count].Cells["R_qty"].Value.ToString()) - Convert.ToInt32(dataGridView1.Rows[count].Cells["sa_da_qty"].Value.ToString())).ToString();
                                        dataGridView1.Rows[count].Cells["balqty"].Value = (Convert.ToInt32(dataGridView1.Rows[count].Cells["Bal_R_qty"].Value.ToString()) - 1).ToString();
                                    }
                                }
                                dr.Close();

                                if (int.Parse(dataGridView1.Rows[count].Cells["Bal_R_qty"].Value.ToString()) <= 0)
                                {
                                    MessageBox.Show("This product out from Return");
                                    txt_EacCode.Text = "";
                                    this.ActiveControl = txt_EacCode;
                                    dataGridView1.Rows.RemoveAt(count);
                                }
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

                        int stkqty1 = Convert.ToInt32(dataGridView1.Rows[m].Cells["Bal_R_qty"].Value.ToString());

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

                                    dataGridView1.Rows[m].Cells["balqty"].Value = (Convert.ToInt32(dataGridView1.Rows[m].Cells["balqty"].Value.ToString()) - 1).ToString();
                                }
                                else
                                {
                                    MessageBox.Show("The Qauntity is Exceeding...", "OK", MessageBoxButtons.OK);
                                }
                                this.ActiveControl = txt_EacCode;
                            }
                            else
                            {
                                MessageBox.Show("The Qauntity is Exceeding...", "OK", MessageBoxButtons.OK);
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

        }

        public bool txtValidation()
        {
            if (cmbwarehouse.Text == "")
            {
                MessageBox.Show("Select Warehouse.");
                this.ActiveControl = cmbwarehouse;
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
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = connection.CreateCommand();
                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("SampleTransaction");

                        try
                        {
                            //Saleable_Damage_Save
                            for (int i = 0; i < Convert.ToInt32(dataGridView1.Rows.Count - 1); i++)
                            {
                                command = new SqlCommand("Saleable_Damage_Save", connection, transaction);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@EAC_code", dataGridView1.Rows[i].Cells["EAC_Code"].Value.ToString());
                                command.Parameters.AddWithValue("@S_Name", dataGridView1.Rows[i].Cells["s_name"].Value.ToString());
                                command.Parameters.AddWithValue("@Warehouse", cmbwarehouse.Text.TrimEnd().TrimStart());
                                if (dataGridView1.Rows[i].Cells["rack"].Value == "" || dataGridView1.Rows[i].Cells["rack"].Value == null)
                                {
                                    command.Parameters.AddWithValue("@rack", "");
                                }
                                else
                                {
                                    command.Parameters.AddWithValue("@rack", dataGridView1.Rows[i].Cells["rack"].Value.ToString());
                                }
                                command.Parameters.AddWithValue("@qty", dataGridView1.Rows[i].Cells["qty1"].Value.ToString());
                                command.Parameters.AddWithValue("@return_qty", dataGridView1.Rows[i].Cells["Bal_R_qty"].Value.ToString());
                                command.Parameters.AddWithValue("@Status1","Saleable");
                                command.Parameters.AddWithValue("@Enter_by", username);
                                command.Parameters.AddWithValue("@Company", company);
                                
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
            }
        }

        public void Clear()
        {
            cmbwarehouse.Text = "";
            txt_EacCode.Text = "";
            dataGridView1.Rows.Clear();
        }
    }
}
