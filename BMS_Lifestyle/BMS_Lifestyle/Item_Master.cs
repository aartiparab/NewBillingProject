using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace BMS_Lifestyle
{
    public partial class Item_Master : Form
    {
        string username;
        string company, connectionString;
        int affect;
        public string image;
        bool chkimg = false;
        ConnectDb ObjCon = new ConnectDb();
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        string itemname,barcodeno1,skucode1;
        AutoCompleteStringCollection eaccode1 = new AutoCompleteStringCollection();
        public Item_Master(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Item_Master_Load(object sender, EventArgs e)
        {
            try
            {
                string str1 = "SELECT [unit_name] FROM [Unit_Master] order by [unit_pk] asc";
                dr = ObjCon.getData(str1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbUnit.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();

                string str2 = "SELECT [group_name] FROM [Item_Group_Master] order by [grp_pk] asc";
                dr = ObjCon.getData(str2);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbGroup.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();

                string str3 = "SELECT [Tax_Name] FROM [Tax_Master] order by [Pk] asc";
                dr = ObjCon.getData(str3);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbTax.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();

                string str4 = "SELECT [size_name] FROM [Item_size_Master] order by [size_pk] asc";
                dr = ObjCon.getData(str4);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbsize.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();

                string str111 = "select [EAC_Code] FROM [Item]";
                dr = ObjCon.getData(str111);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        eaccode1.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
                txtbarcodeno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtbarcodeno.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtbarcodeno.AutoCompleteCustomSource = eaccode1;

                this.ActiveControl = txtbarcodeno;
            }
            catch { }
        }

        private void cmbTax_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string str4 = "SELECT cast([SGST] as decimal(18,2)) as SGST,cast([CGST] as decimal(18,2)) as CGST,cast([IGST] as decimal(18,2)) as IGST FROM [Tax_Master] where [Tax_Name] = '" + cmbTax.Text + "'";
                dr = ObjCon.getData(str4);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtCgst.Text = dr[0].ToString();
                        txtSgst.Text = dr[1].ToString();
                        txtIgst.Text = dr[2].ToString();
                    }
                }
                dr.Close();
            }
            catch { }
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 46)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void txthsncode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 46)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            chkimg = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult dlgRes = dlg.ShowDialog();
            if (dlgRes != DialogResult.Cancel)
            {
                pictureBox1.ImageLocation = dlg.FileName;
                image = dlg.FileName;
                chkimg = true;
            }
        }

        byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        public bool Validation()
        {
            try
            {
                string str5 = "select [P_Name] FROM [Item] where [P_Name] = '" + txtP_Name.Text + "'";
                dr = ObjCon.getData(str5);

                if (dr.HasRows)
                {
                    MessageBox.Show("Purchase Name already exists");
                    this.ActiveControl = txtP_Name;
                    return false;
                }
                dr.Close();

                string str6 = "select [S_Name] FROM [Item] where [S_Name] = '" + txtS_Name.Text + "'";
                dr = ObjCon.getData(str6);

                if (dr.HasRows)
                {
                    MessageBox.Show("Sale Name already exists");
                    this.ActiveControl = txtS_Name;
                    return false;
                }
                dr.Close();

                string str111 = "select [EAC_Code] FROM [Item] where [EAC_Code] = '" + txtbarcodeno.Text + "'";
                dr = ObjCon.getData(str111);

                if (dr.HasRows)
                {
                    MessageBox.Show("EAC Code already exists for Item");
                    this.ActiveControl = txtbarcodeno;
                    return false;
                }
                dr.Close();

                string str113 = "select [EAC_Code] FROM [Kit_Master] where [EAC_Code] = '" + txtbarcodeno.Text + "'";
                dr = ObjCon.getData(str113);

                if (dr.HasRows)
                {
                    MessageBox.Show("EAC Code already exists for Kit");
                    this.ActiveControl = txtbarcodeno;
                    return false;
                }
                dr.Close();

                string str112 = "select [SKU_Code] FROM [Item] where [SKU_Code] = '" + txtskucode.Text + "'";
                dr = ObjCon.getData(str112);
                if (dr.HasRows)
                {
                    MessageBox.Show("SKU Code already exists");
                    this.ActiveControl = txtskucode;
                    return false;
                }
                dr.Close();
            }
            catch { }
                if (txtP_Name.Text == "")
                {
                    MessageBox.Show("Enter Purchase Name.");
                    this.ActiveControl = txtP_Name;
                    return false;
                }
                if (txtS_Name.Text == "")
                {
                    MessageBox.Show("Enter Sale Name.");
                    this.ActiveControl = txtS_Name;
                    return false;
                }
                if (txtbarcodeno.Text == "")
                {
                    MessageBox.Show("Enter EAC Code.");
                    this.ActiveControl = txtbarcodeno;
                    return false;
                }
                if (txthsncode.Text == "")
                {
                    MessageBox.Show("Enter HSN Code.");
                    this.ActiveControl = txthsncode;
                    return false;
                }
                if (cmbTax.Text == "")
                {
                    MessageBox.Show("Select Tax Name.");
                    this.ActiveControl = cmbTax;
                    return false;
                }

            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation() == true)
                {
                    string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();

                    if (ans == "Yes")
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
                            command.Connection = connection;
                            command.Transaction = transaction;
                            try
                            {
                                command.CommandText = "Item_Save";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@P_Name",txtP_Name.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@S_Name",txtS_Name.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@EAC_Code",txtbarcodeno.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@SKU_Code",txtskucode.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Item_size",cmbsize.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Item_Group",cmbGroup.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Item_Unit",cmbUnit.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@HSN_code",txthsncode.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@MRP_value",txtmrpvalue.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@ItemRateDate",dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@MinStockqty",txtminStkQty.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Tax_Name",cmbTax.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@cgst_tax",txtCgst.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@igst_tax",txtIgst.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@sgst_tax",txtSgst.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@company",company);
                                command.Parameters.AddWithValue("@EnterBy",username);
                                command.ExecuteNonQuery();

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
                }
            }
            catch { }
        }

        public void Clear()
        {
            txtP_Name.Text = "";
            txtS_Name.Text = "";
            cmbGroup.Text = "";
            cmbUnit.Text = "";
            txthsncode.Text = "";
            cmbTax.Text = "";
            cmbsize.Text = "";
            txtCgst.Text = "0";
            txtIgst.Text = "0";
            txtSgst.Text = "0";
            txtbarcodeno.Text = "";
            txtmrpvalue.Text = "0";
            pictureBox1.Image = null;
            txtskucode.Text = "";
            dateTimePicker1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void txtmrpvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 46)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void txtshippingwt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 46)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //if (checkBox1.Checked == true)
            //{
            //    txtbarcodeno.ReadOnly = true;
            //    string strfillb = "select max(CAST(substring([Barcode_No],3,LEN([Barcode_No])-1) AS INT)) as Barcode1 FROM [Item] where [Barcode_No] like 'NN%'";
            //    dr = ObjCon.getData(strfillb);
            //    if (dr.HasRows)
            //    {
            //        while (dr.Read())
            //        {
            //            try
            //            {
            //                txtbarcodeno.Text = "NN" + (Convert.ToInt32(dr[0].ToString()) + 1);
            //            }
            //            catch
            //            {
            //                txtbarcodeno.Text = "NN100";
            //            }
            //        }
            //    }
            //    dr.Close();
            //    if (txtbarcodeno.Text == "0")
            //    {
            //        txtbarcodeno.Text = "NN100";
            //    }

            //}
            //else 
            //{
            //    txtbarcodeno.Text = "";
            //    if (txtbarcodeno.ReadOnly == true)
            //    {
            //        txtbarcodeno.ReadOnly = false;
            //    }
            //}
        }

    }
}
