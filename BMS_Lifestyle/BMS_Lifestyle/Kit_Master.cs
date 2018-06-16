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
    public partial class Kit_Master : Form
    {
        string username;
        string company;
        int affect;
        public string image;
        bool chkimg = false;
        ConnectDb ObjCon = new ConnectDb();
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        string itemname, barcodeno1, skucode1;
        string connectionString;
        AutoCompleteStringCollection EACCode = new AutoCompleteStringCollection();

        public Kit_Master(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        
        private void Kit_Master_Load(object sender, EventArgs e)
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

                string str115 = "select [EAC_Code] FROM [Kit_Master]";
                dr = ObjCon.getData(str115);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EACCode.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
                txtbarcodeno.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtbarcodeno.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtbarcodeno.AutoCompleteCustomSource = EACCode;

                //txtbarcodeno.Focus();
                this.ActiveControl = txtbarcodeno;
            }
            catch { }
            
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

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            chkimg = false;
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

        private void txtmrpvalue_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Validation()
        {
            
            try
            {
                string str5 = "select [Kit_Name] FROM [Kit_Master] where [Kit_Name] = '" + txtItemName.Text + "'";
                dr = ObjCon.getData(str5);
                if (dr.HasRows)
                {
                    MessageBox.Show("Kit Name already exists.");
                    this.ActiveControl = txtItemName;
                    return false;
                }
                dr.Close();

                string str115 = "select [EAC_Code] FROM [Kit_Master] where [EAC_Code] = '" + txtbarcodeno.Text + "' ";
                dr = ObjCon.getData(str115);
                if (dr.HasRows)
                {
                    MessageBox.Show("EAC Code already exists for Kit");
                    this.ActiveControl = txtbarcodeno;
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

                string str112 = "select [SKU_Code] FROM [Kit_Master] where [SKU_Code] = '" + txtskucode.Text + "'";
                dr = ObjCon.getData(str112);
                if (dr.HasRows)
                {
                    MessageBox.Show("SKU Code already exists.");
                    this.ActiveControl = txtskucode;
                    return false;
                }
                dr.Close();

            }
            catch { }

            
            if (txtItemName.Text == "")
            {
                MessageBox.Show("Kit Name cannot be empty.");
                this.ActiveControl = txtbarcodeno;
                return false;
            }
           
            if (cmbTax.Text == "")
            {
                MessageBox.Show("TaxName cannot be empty");
                this.ActiveControl = txtbarcodeno;
                return false;
            }

            if (cmbGroup.Text == "")
            {
                MessageBox.Show("Group cannot be empty");
                this.ActiveControl = cmbGroup;
                return false;
            }

            if (cmbsize.Text == "")
            {
                MessageBox.Show("Size cannot be empty");
                this.ActiveControl = cmbsize;
                return false;
            }

            if (txthsncode.Text == "")
            {
                MessageBox.Show("HSNCode cannot be empty");
                this.ActiveControl = txthsncode;
                return false;
            }
            
            if (cmbUnit.Text == "")
            {
                MessageBox.Show("Unit cannot be empty");
                this.ActiveControl = cmbUnit;
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
                                command = new SqlCommand("KitMaster_Insert", connection, transaction);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Kit_Name", txtItemName.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kit_Group", cmbGroup.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kit_Unit", cmbUnit.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kit_HSN_code", txthsncode.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kitRateDate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@EnterBy", username);
                                command.Parameters.AddWithValue("@kit_Tax_Name", cmbTax.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@cgst_tax", txtCgst.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@igst_tax", txtIgst.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@sgst_tax", txtSgst.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@company",company);
                                command.Parameters.AddWithValue("@EAC_Code", txtbarcodeno.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kit_MRP_value", txtmrpvalue.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kit_Size", cmbsize.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@SKU_Code", txtskucode.Text.TrimStart().TrimEnd());
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

                            //string cmp1 = null;
                            //    try
                            //    {
                            //        //Print Barcode------------------------------------------------------------------
                            //        cmp1 = "I8,A,001" + System.Environment.NewLine +
                            //                "" + System.Environment.NewLine +
                            //                "" + System.Environment.NewLine +
                            //                "" + System.Environment.NewLine +
                            //                "Q120,024" + System.Environment.NewLine +
                            //                "q831" + System.Environment.NewLine +
                            //                "rN" + System.Environment.NewLine +
                            //                "S5" + System.Environment.NewLine +
                            //                "D10" + System.Environment.NewLine +
                            //                "ZT" + System.Environment.NewLine +
                            //                "JF" + System.Environment.NewLine +
                            //                "O" + System.Environment.NewLine +
                            //                "R263,0" + System.Environment.NewLine +
                            //                "f100" + System.Environment.NewLine +
                            //                "N" + System.Environment.NewLine +

                            //                "A252,79,2,2,1,1,N,\"Weight:\"" + System.Environment.NewLine +
                            //                "A252,100,2,2,1,1,N,\"" + txtItemName.Text + "\"" + System.Environment.NewLine +
                            //                "A165,79,2,2,1,1,N,\"" + txtshippingwt.Text + "\"" + System.Environment.NewLine +
                            //                "B252,60,2,1,2,6,30,B,\"" + txtbarcodeno.Text + "\"" + System.Environment.NewLine +
                            //                "P" + 1 + System.Environment.NewLine;
                            //    }
                            //    catch
                            //    { }
                            //    string directory = Directory.GetCurrentDirectory();
                            //    FileStream fs2 = new FileStream(directory + "\\Dtm.prn", FileMode.Create, FileAccess.ReadWrite);

                            //    StreamWriter sw2 = new StreamWriter(fs2);

                            //    sw2.Write(cmp1);

                            //    sw2.Flush();
                            //    sw2.Close();
                            //    fs2.Close();

                            //    System.Diagnostics.Process.Start(directory + "\\dms.bat");
                            //    Thread.Sleep(500);
                    }
                }
            }
            catch { }
        }

        public void Clear()
        {
            txtItemName.Text = "";
            cmbGroup.Text = "";
            cmbUnit.Text = "";
            txthsncode.Text = "";
            dateTimePicker1.Text = "";
            cmbTax.Text = "";
            cmbsize.Text = "";
            txtCgst.Text = "0";
            txtIgst.Text = "0";
            txtSgst.Text = "0";
            txtbarcodeno.Text = "";
            txtmrpvalue.Text = "0";
            txtshippingwt.Text = "0";
            pictureBox1.Image = null;
            txtskucode.Text = "";
            dateTimePicker1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
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

 
    }
}
