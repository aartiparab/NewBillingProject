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
    public partial class Bill_Of_Master : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        ConnectDb obj = new ConnectDb();
        AutoComplete objName = new AutoComplete();
        string username, company, Bom_pk;
        int affect,sum, m = 0;
        AutoCompleteStringCollection kitname = new AutoCompleteStringCollection();
        string connectionString;
        bool ispresent = false;
        public Bill_Of_Master(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Bill_Of_Master_Load(object sender, EventArgs e)
        {
            try 
            {
                string strfill1 = "select [Kit_Name] FROM [Kit_Master] where [Kit_Name] not in(select [kit_name] from [BOM_Master])";
                dr = obj.getData(strfill1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtkitname.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
            }
            catch
            {
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRowsRemovedEventArgs ee = null;
            //int rows = Convert.ToInt32(dataGridView1.RowCount)-2;
            //string stock = null,Bcode1;

            //if (dataGridView1.Columns[e.ColumnIndex].Name == "sname")
            //{
            //    SendKeys.Send("{TAB}");
            //}

            //if (e.RowIndex > 0)
            //{
            //    try {
            //        int row = dataGridView1.RowCount - 2;
            //        string[] bcode = new string[row + 1];

            //        for (int i = 0; i <= row; i++)
            //        {
            //            if (dataGridView1.Rows[i].Cells[0].Value != null && e.RowIndex - 1 != i)
            //            {
            //                bcode[i] = dataGridView1.Rows[i].Cells["eaccode"].Value.ToString();
            //            }
            //        }
            //        for (int i = 0; i <= row; i++)
            //        {

            //            if (dataGridView1.Rows[e.RowIndex - 1].Cells["eaccode"].Value.ToString() == bcode[i])
            //            {
            //                dataGridView1.Rows[e.RowIndex - 1].Cells["eaccode"].Value = "";
            //                dataGridView1.Rows[e.RowIndex - 1].Cells["sname"].Value = "";
            //                dataGridView1.Rows[e.RowIndex - 1].Cells["qty1"].Value = "";

            //            }
            //        }
            //        dataGridView1_RowsRemoved(dataGridView1, ee);
            //    }
            //    catch { }  
            //}

            //if (e.ColumnIndex == 0)
            //{
            //    try
            //    {
            //        if (dataGridView1.Rows[e.RowIndex - 1].Cells[1].Value == null || dataGridView1.Rows[e.RowIndex - 1].Cells[1].Value == "")
            //        {
            //            dr.Close();
            //            string commadT1 = "select [S_Name] from [Item] where [EAC_Code] = '" + dataGridView1.Rows[e.RowIndex - 1].Cells["eaccode"].Value.ToString() + "'";
            //            dr = obj.getData(commadT1);
            //            if (dr.HasRows)
            //            {
            //                while (dr.Read())
            //                {
            //                    dataGridView1.Rows[e.RowIndex - 1].Cells[1].Value = dr["S_Name"].ToString();
            //                    dataGridView1.Rows[e.RowIndex - 1].Cells[2].Value = "1";
            //                }
            //            }
            //            dr.Close();
            //            dataGridView1_RowsRemoved(dataGridView1, ee);
            //        }
            //    }
            //    catch { }
            //}
            //dataGridView1_RowsRemoved(dataGridView1, ee);
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            Double QTY1 = 0;
            int rows1 = Convert.ToInt32(dataGridView1.RowCount) - 2;
            for (int i = 0; i <= rows1; i++)
            {
                try
                {
                    if (dataGridView1.Rows[i].Cells["qty1"].Value.ToString() != null || dataGridView1.Rows[i].Cells["qty1"].Value.ToString() != "")
                    {
                        QTY1 += Convert.ToDouble(dataGridView1.Rows[i].Cells["qty1"].Value.ToString());     
                    }
                }
                catch{}
            }
            txtTotalQty.Text = QTY1.ToString();     
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
            //objName.productname.Clear();
            //var txtBox = e.Control as TextBox;
            //colIndex = dataGridView1.CurrentCell.ColumnIndex;
            //rowIndex = dataGridView1.CurrentCell.RowIndex;
            //if (colIndex == dataGridView1.Rows[rowIndex].Cells["eaccode"].ColumnIndex)
            //{
            //    if (e.Control is TextBox)
            //    {
            //        if (txtBox != null)
            //        {
            //            txtBox.CharacterCasing = CharacterCasing.Upper;
            //            txtBox.TextChanged += new EventHandler(Caption_TextChanged);
            //            txtBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //            txtBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //            string commandT = "select distinct [EAC_Code] FROM [Item]";
            //            objName.filltextbox(commandT);
            //            txtBox.AutoCompleteCustomSource = objName.productname;

            //        }
            //    }
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool txtValidation()
        {
            string str6 = "select [kit_name] from [BOM_Master] where [kit_name] = '"+txtkitname.Text.TrimEnd().TrimStart()+"'";
            dr = obj.getData(str6);
            if (dr.HasRows)
            {
                MessageBox.Show("BOM of this Kit already exists");
                this.ActiveControl = txtkitname;
                return false;
            }
            dr.Close();

            if (txtkitname.Text == "")
            {
                MessageBox.Show("kit Name cannot be empty");
                this.ActiveControl = txtkitname;
                return false;
            }

            if (dataGridView1.Rows.Count <= 1)
            {
                MessageBox.Show("Item details cannot be blank");
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

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = connection.CreateCommand();
                            SqlTransaction transaction;

                            // Start a local transaction.
                            transaction = connection.BeginTransaction("SampleTransaction");

                            // Must assign both transaction object and connection
                            // to Command object for a pending local transaction
                            //command.Connection = connection;
                            //command.Transaction = transaction;
                            try
                            {
                                command =new SqlCommand("BOM_Master_Save",connection,transaction);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@kit_name", txtkitname.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kit_Eaccode", txtEacCode.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@BOM_date", dtpBOM.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@total_qty", txtTotalQty.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@EnterBy", username);
                                command.Parameters.AddWithValue("@company", company);
                                command.Parameters.AddWithValue("@DeleteFlag", "No");
                                command.ExecuteNonQuery();

                                for (int i = 0; i <= Convert.ToInt32(dataGridView1.RowCount) - 2; i++)
                                {

                                    command = new SqlCommand("Sub_BOM_Master_Save", connection, transaction);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@kitname", txtkitname.Text.TrimStart().TrimEnd());
                                    command.Parameters.AddWithValue("@kit_Eaccode", txtEacCode.Text.TrimStart().TrimEnd());
                                    command.Parameters.AddWithValue("@ItemName", dataGridView1.Rows[i].Cells["sname"].Value.ToString().TrimStart().TrimEnd());
                                    command.Parameters.AddWithValue("@Qty", dataGridView1.Rows[i].Cells["qty1"].Value.ToString().TrimStart().TrimEnd());
                                    command.Parameters.AddWithValue("@EAC_Code", dataGridView1.Rows[i].Cells["eaccode"].Value.ToString().TrimStart().TrimEnd());
                                    command.Parameters.AddWithValue("@Enterby", username);
                                    command.Parameters.AddWithValue("@company", company);
                                    command.Parameters.AddWithValue("@Deleteflag", "No");
                                    command.ExecuteNonQuery();
                                }


                                // Attempt to commit the transaction.
                                transaction.Commit();
                                MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                            }
                            catch
                            {
                                // Attempt to roll back the transaction.
                                try
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Save was unsuccessful...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Clear();
                                }
                                catch (Exception ex2)
                                {
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        public void Clear()
        {
            txtkitname.Text = "";
            txtEacCode.Text = "";
            txtskucode.Text = "";
            txtTotalQty.Text = "0";
            dataGridView1.Rows.Clear();
        }

        private void txtkitname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strfill1 = "select [EAC_Code] FROM [Kit_Master] where [Kit_Name] = '"+txtkitname.Text+"'";
                dr = obj.getData(strfill1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtEacCode.Text = dr["EAC_Code"].ToString();
                    }
                }
                dr.Close();
            }
            catch
            {
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
                    string i = txt_EacCode.Text.TrimEnd().TrimStart();
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
                                string str556 = "select [EAC_Code] FROM [Kit_Master] where [EAC_Code] = '" + txt_EacCode.Text.TrimEnd().TrimStart() + "'";
                                dr = obj.getData(str556);
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        flagkit = 1;
                                    }
                                }
                                dr.Close();

                                string str558 = "select [EAC_Code] FROM [Item] where [EAC_Code] = '" + txt_EacCode.Text.TrimEnd().TrimStart() + "'";
                                dr = obj.getData(str558);
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        flagbcode = 1;
                                    }
                                }
                                dr.Close();

                                if (flagbcode == 1)
                                {
                                    dr.Close();
                                    string commadT1 = "select [S_Name] from [Item] where [EAC_Code] = '" + txt_EacCode.Text.TrimEnd().TrimStart() + "'";
                                    dr = obj.getData(commadT1);
                                    if (dr.HasRows)
                                    {
                                        while (dr.Read())
                                        {
                                            dataGridView1.Rows.Add();
                                            dataGridView1.Rows[count].Cells[0].Value = txt_EacCode.Text.TrimEnd().TrimStart();
                                            dataGridView1.Rows[count].Cells[1].Value = dr["S_Name"].ToString();
                                            dataGridView1.Rows[count].Cells[2].Value = "1";
                                        }
                                    }
                                    dr.Close();

                                    //string str77 = "exec Stock_Barcodewise '" + txt_EacCode.Text.TrimEnd().TrimStart() + "','" + company + "','All','All'";
                                    //dr = obj.getData(str77);
                                    //if (dr.HasRows)
                                    //{
                                    //    while (dr.Read())
                                    //    {
                                    //        if (Convert.ToInt32(dr[0].ToString()) > 0)
                                    //        {
                                    //            dataGridView1.Rows[count].Cells["stkqty"].Value = dr[0].ToString();
                                    //            dataGridView1.Rows[count].Cells["balqty"].Value = (Convert.ToInt32(dataGridView1.Rows[count].Cells["stkqty"].Value.ToString()) - 1).ToString();
                                    //        }
                                    //        else
                                    //        {
                                    //            MessageBox.Show("Not in Stock....", "OK", MessageBoxButtons.OK);
                                    //            this.ActiveControl = txt_EacCode;
                                    //            txt_EacCode.Text = "";
                                    //            dataGridView1.Rows.RemoveAt(count);
                                    //        }
                                    //    }
                                    //}
                                    //dr.Close();
                                    
                                }
                                if (flagkit == 1)
                                {
                                    MessageBox.Show("This EAC code not belongs to Items");
                                    this.ActiveControl = txt_EacCode;
                                    txt_EacCode.Text = "";
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

                        //int stkqty1 = Convert.ToInt32(dataGridView1.Rows[m].Cells["stkqty"].Value.ToString());

                        //int balqty1 = Convert.ToInt32(dataGridView1.Rows[m].Cells["balqty"].Value.ToString());

                        //if (balqty1 >= 0)
                        //{
                        //    if (qty < stkqty1)
                        //    {
                        //        sum = sum + Convert.ToInt32(dataGridView1.Rows[m].Cells["qty1"].Value.ToString());

                        //        balsku = balsku + Convert.ToInt32(dataGridView1.Rows[m].Cells["balqty"].Value.ToString());

                        //        if (sum < stkqty1)
                        //        {
                                    dataGridView1.Rows[m].Cells["qty1"].Value = (Convert.ToInt32(dataGridView1.Rows[m].Cells["qty1"].Value.ToString()) + 1).ToString();

                            //        dataGridView1.Rows[m].Cells["balqty"].Value = (Convert.ToInt32(dataGridView1.Rows[m].Cells["balqty"].Value.ToString()) - 1).ToString();
                            //    }
                            //    else
                            //    {
                            //        MessageBox.Show("Out of Stock...", "OK", MessageBoxButtons.OK);
                            //    }
                            //    this.ActiveControl = txt_EacCode;
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Out of Stock...", "OK", MessageBoxButtons.OK);
                            //    this.ActiveControl = txt_EacCode;
                            //}
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Quantity is exceeding...", "OK", MessageBoxButtons.OK);
                        //    this.ActiveControl = txt_EacCode;
                        //}
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
    }
}
