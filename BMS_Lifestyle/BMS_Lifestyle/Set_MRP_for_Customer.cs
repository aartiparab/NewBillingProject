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
    public partial class Set_MRP_for_Customer : Form
    {
        string username;
        string company;
        int affect;
        ConnectDb ObjCon = new ConnectDb();
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
       // AutoCompleteStringCollection itemname = new AutoCompleteStringCollection();
        AutoComplete objAccName = new AutoComplete();
        int flag1 = 0;
        int role_Id;
        public Set_MRP_for_Customer(string username,string company,int role_Id)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.role_Id = role_Id;
        }

        private void Set_MRP_for_Customer_Load(object sender, EventArgs e)
        {
            try
            {
                string commandT = "select distinct [CustName] FROM [Customer]";
                dr = ObjCon.getData(commandT);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr[0].ToString());
                    }
                }
                dr.Close();
            }
            catch { }
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
            //if (colIndex == dataGridView1.Rows[rowIndex].Cells["itemname"].ColumnIndex)
            //{
            //    if (e.Control is TextBox)
            //    {
            //        if (txtBox != null)
            //        {
            //            txtBox.CharacterCasing = CharacterCasing.Upper;
            //            txtBox.TextChanged += new EventHandler(Caption_TextChanged);
            //            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //            txtBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //            string commandT = "select distinct [ItemName] FROM [Item]";
            //            objAccName.filltextbox(commandT);
            //            txtBox.AutoCompleteCustomSource = objAccName.productname;
            //        }
            //    }
            //}
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rows = Convert.ToInt32(dataGridView1.RowCount.ToString()) - 2;
            DataGridViewRowsRemovedEventArgs ee = null;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "skucode")
            {
                SendKeys.Send("{TAB}");
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "itemname")
            {
                SendKeys.Send("{TAB}");
            }

            if (e.ColumnIndex == 0)
            {
                try
                {
                    string commandT1 = "select [ItemName],[SKU_Code],isnull(cast([MRP_value] as decimal(18,2)),0) as MRP_value FROM [Item] where [Barcode_No] = '" + dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() + "'";
                    dr = ObjCon.getData(commandT1);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            dataGridView1.Rows[e.RowIndex - 1].Cells["skucode"].Value = dr["SKU_Code"].ToString();
                            dataGridView1.Rows[e.RowIndex - 1].Cells["itemname"].Value = dr["ItemName"].ToString();
                            dataGridView1.Rows[e.RowIndex - 1].Cells["mrp1"].Value = dr["MRP_value"].ToString();
                            dataGridView1.Rows[e.RowIndex - 1].Cells["pkid"].Value = "";
                        }
                    }
                    dr.Close();
                }
                catch { }
            }

            if (e.RowIndex > 0)
            {
                int row = dataGridView1.RowCount - 1;

                string[] bcode = new string[row + 1];

                for (int i = 0; i <= row; i++)
                {
                    if (dataGridView1.Rows[i].Cells["barcode"].Value != null && e.RowIndex - 1 != i)
                    {
                        bcode[i] = dataGridView1.Rows[i].Cells["barcode"].Value.ToString();
                    }
                }
                for (int i = 0; i <= row; i++)
                {
                    if (dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value.ToString() == bcode[i])
                    {
                        dataGridView1.Rows[e.RowIndex - 1].Cells["barcode"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["itemname"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["skucode"].Value = "";
                        dataGridView1.Rows[e.RowIndex - 1].Cells["mrp1"].Value = "";                        
                    }
                }
            }
            dataGridView1_RowsRemoved(dataGridView1, ee);
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

            int rows = Convert.ToInt32(dataGridView1.Rows.Count.ToString()) - 1;
            lblrows.Text = rows.ToString();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString().ToUpper();
                e.FormattingApplied = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void filldatagrid()
        {
            try
            {
                dr.Close();
                int count1 = dataGridView1.Rows.Count - 1;
                string filldata2 = "select [ItemName],[SKUcode],[Barcode],isnull(cast([MRP] as decimal(18,2)),0) as MRP,[id] FROM [MRP_for_Customer] where [CustomerName] = '" + comboBox1.Text + "' and [company] = '"+company+"'";
                dr = ObjCon.getData(filldata2);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[count1].Cells["itemname"].Value = dr["ItemName"].ToString();
                        dataGridView1.Rows[count1].Cells["skucode"].Value = dr["SKUcode"].ToString();
                        dataGridView1.Rows[count1].Cells["barcode"].Value = dr["Barcode"].ToString();
                        dataGridView1.Rows[count1].Cells["mrp1"].Value = dr["MRP"].ToString();
                        dataGridView1.Rows[count1].Cells["pkid"].Value = dr["id"].ToString();
                        count1++;
                    }
                }
               // dr.Close();

            }
            catch
            { }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            lblrows.Text = "0";
            string filldata = "select [CustomerName] FROM [MRP_for_Customer] where [CustomerName] = '" + comboBox1.Text + "' and [company] = '"+company+"'";
            dr = ObjCon.getData(filldata);
            if (dr.HasRows)
            {
                if (role_Id == 2 || role_Id == 1)
                {
                    button3.Enabled = true;
                    button1.Enabled = false;
                }
                else if (role_Id == 3)
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                }
                else { MessageBox.Show(" You does not have Permission to update data Please Contact Administrator!!! "); }
                filldatagrid();
            }
            else
            {
                if (role_Id == 2)
                {
                    button3.Enabled = false;
                    button1.Enabled = false;
                }
                else if (role_Id == 3 || role_Id == 1)
                {
                    button3.Enabled = false;
                    button1.Enabled = true;
                }
                else { MessageBox.Show(" You does not have Permission to add data Please Contact Administrator!!! "); }
            }
            dr.Close();
        }

        public bool txtValidation()
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Select Customer");
                return false;
            }
            if (dataGridView1.RowCount < 1)
            {
                MessageBox.Show("Item Details cannot be blank");
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtValidation() == true)
            {
                string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();

                if (ans == "Yes")
                {
                    try
                    {
                        int rows1 = Convert.ToInt32(dataGridView1.RowCount.ToString()) - 2;
                        for (int i = 0; i <= rows1; i++)
                        {
                            string itemname1, barcode1, skucode1, mrp2;
                            try
                            {
                                itemname1 = dataGridView1.Rows[i].Cells["itemname"].Value.ToString();
                            }
                            catch
                            {
                                itemname1 = "";
                            }
                            try
                            {
                                barcode1 = dataGridView1.Rows[i].Cells["barcode"].Value.ToString();
                            }
                            catch
                            {
                                barcode1 = "";
                            }
                            try
                            {
                                skucode1 = dataGridView1.Rows[i].Cells["skucode"].Value.ToString();
                            }
                            catch
                            {
                                skucode1 = "";
                            }
                            try
                            {
                                mrp2 = dataGridView1.Rows[i].Cells["mrp1"].Value.ToString();
                            }
                            catch
                            {
                                mrp2 = "";
                            }

                            string save1 = "INSERT INTO [MRP_for_Customer]([CustomerName],[ItemName],[SKUcode],[Barcode],[MRP],[EnterBy],[EnterDate],[company])VALUES" +
                                            "('" + comboBox1.Text + "','" + itemname1 + "','" + skucode1 + "','" + barcode1 + "','" + mrp2 + "','" + username + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "','"+company+"')";
                            affect = ObjCon.affect(save1);
                        }
                        MessageBox.Show("Data Saved Successfully");

                        comboBox1.Text = "";
                        dataGridView1.Rows.Clear();
                        lblrows.Text = "";

                        }
                        catch { }
                    }
            }
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
                        int rows1 = Convert.ToInt32(dataGridView1.RowCount.ToString()) - 2;
                        for (int i = 0; i <= rows1; i++)
                        {
                            string itemname1, barcode1, skucode1, mrp2,pkid1;
                            try
                            {
                                itemname1 = dataGridView1.Rows[i].Cells["itemname"].Value.ToString();
                            }
                            catch
                            {
                                itemname1 = "";
                            }
                            try
                            {
                                barcode1 = dataGridView1.Rows[i].Cells["barcode"].Value.ToString();
                            }
                            catch
                            {
                                barcode1 = "";
                            }
                            try
                            {
                                skucode1 = dataGridView1.Rows[i].Cells["skucode"].Value.ToString();
                            }
                            catch
                            {
                                skucode1 = "";
                            }
                            try
                            {
                                mrp2 = dataGridView1.Rows[i].Cells["mrp1"].Value.ToString();
                            }
                            catch
                            {
                                mrp2 = "";
                            }
                            try
                            {
                                pkid1 = dataGridView1.Rows[i].Cells["pkid"].Value.ToString();
                            }
                            catch
                            {
                                pkid1 = "";
                            }
                            if (pkid1 == "")
                            {
                                string save1 = "INSERT INTO [MRP_for_Customer]([CustomerName],[ItemName],[SKUcode],[Barcode],[MRP],[EnterBy],[EnterDate],[company])VALUES" +
                                                "('" + comboBox1.Text + "','" + itemname1 + "','" + skucode1 + "','" + barcode1 + "','" + mrp2 + "','" + username + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "','"+company+"')";
                                affect = ObjCon.affect(save1);
                            }
                            else if (pkid1 != "")
                            {
                                string update1 = "UPDATE [MRP_for_Customer] SET [ItemName] = '" + itemname1 + "',[SKUcode] = '" + skucode1 + "',[Barcode] = '" + barcode1 + "',[MRP] = '" + mrp2 + "',[ModifyBy] = '" + username + "' "+
                                                 ",[ModifyDate] = '" + System.DateTime.Now.ToString("MM/dd/yyyy") + "' WHERE [CustomerName] = '" + comboBox1.Text + "' and id = '"+pkid1+"' and [company] = '"+company+"'";
                                affect = ObjCon.affect(update1);
                            }
                        }
                        MessageBox.Show("Data Saved Successfully");

                        comboBox1.Text = "";
                        dataGridView1.Rows.Clear();
                        lblrows.Text = "";
                    }
                    catch { }             
                }
            }
        }
    }
}
