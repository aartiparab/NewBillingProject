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

namespace BMS_Lifestyle
{
    public partial class Kit_Modify : Form
    {
        string username,connectionString;
        string company;
        int affect;
        public string image;
        bool chkimg = false;
        ConnectDb ObjCon = new ConnectDb();
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        AutoCompleteStringCollection EACCode = new AutoCompleteStringCollection();
        string oldKitName;
        public Kit_Modify(string username, string company,string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Kit_Modify_Load(object sender, EventArgs e)
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

                string str6 = "select [EAC_Code] FROM [Kit_Master] order by [KITCode] asc";
                dr = ObjCon.getData(str6);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtbarcodeno.Items.Add(dr[0].ToString().ToUpper());
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


        private void txtshippingwt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 46)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
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

            if (txtItemName.Text == "")
            {
                MessageBox.Show("Kit Name cannot be empty");
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
                    string ans = (MessageBox.Show("Do you want to Continue Update", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();

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
                                command = new SqlCommand("KitMaster_Update", connection, transaction);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Kit_Name", txtItemName.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@old_name", oldKitName.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kit_Group", cmbGroup.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kit_Unit", cmbUnit.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kit_HSN_code", txthsncode.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kitRateDate", dateTimePicker1.Value.ToString("MM/dd/yyyy"));
                                command.Parameters.AddWithValue("@kit_Tax_Name", cmbTax.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@cgst_tax", txtCgst.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@igst_tax", txtIgst.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@sgst_tax", txtSgst.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@ModifyBy", username);
                                command.Parameters.AddWithValue("@kit_MRP_value", txtmrpvalue.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@kit_Size", cmbsize.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@SKU_Code", txtskucode.Text.TrimStart().TrimEnd());
                                command.Parameters.AddWithValue("@EAC_Code", txtbarcodeno.Text.TrimStart().TrimEnd());
                                command.ExecuteNonQuery();

                                // Attempt to commit the transaction.
                                transaction.Commit();
                                MessageBox.Show("Update Successfully...", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                            }
                            catch
                            {
                                // Attempt to roll back the transaction.
                                try
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Update was unsuccessful...", "Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            txtItemName.Text = "";
            cmbGroup.Text = "";
            cmbUnit.Text = "";
            txthsncode.Text = "";
            dateTimePicker1.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            cmbTax.Text = "";
            txtCgst.Text = "0";
            txtIgst.Text = "0";
            txtSgst.Text = "0";
            txtbarcodeno.Text = "";
            cmbsize.Text = "";
            txtmrpvalue.Text = "0";
            pictureBox1.Image = null;
            txtskucode.Text = "";
        }

        private void txtbarcodeno_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string str7 = "SELECT [Kit_Name],[kit_Group],[kit_Unit],[kit_HSN_code],[kitRateDate],[kit_Tax_Name],cast([cgst_tax] as decimal(18,2)) as cgst_tax,"+
                              "cast([igst_tax] as decimal(18,2)) as igst_tax,cast([sgst_tax] as decimal(18,2)) as sgst_tax, cast([kit_MRP_value] as decimal(18,2)) as kit_MRP_value,"+
                              "[kit_size],[SKU_Code] FROM [Kit_Master] where [EAC_Code] = '" + txtbarcodeno.Text + "'";
                dr = ObjCon.getData(str7);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtItemName.Text = dr["Kit_Name"].ToString();
                        oldKitName = dr["Kit_Name"].ToString();
                        cmbGroup.Text = dr["kit_Group"].ToString();
                        cmbUnit.Text = dr["kit_Unit"].ToString();
                        txthsncode.Text = dr["kit_HSN_code"].ToString();
                        dateTimePicker1.Text = dr["kitRateDate"].ToString();
                        cmbTax.Text = dr["kit_Tax_Name"].ToString();
                        txtCgst.Text = dr["cgst_tax"].ToString();
                        txtIgst.Text = dr["igst_tax"].ToString();
                        txtSgst.Text = dr["sgst_tax"].ToString();
                        txtmrpvalue.Text = dr["kit_MRP_value"].ToString();
                        cmbsize.Text = dr["kit_size"].ToString();
                        txtskucode.Text = dr["SKU_Code"].ToString();
                        
                        //pictureBox1.Image = null;
                        //if (dr["ImageData"] != DBNull.Value)
                        //{
                        //    byte[] imageData = (byte[])dr["ImageData"];


                        //    //Initialize image variable
                        //    Image newImage;
                        //    //Read image data into a memory stream
                        //    using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                        //    {
                        //        ms.Write(imageData, 0, imageData.Length);

                        //        //Set image variable value using memory stream.
                        //        newImage = Image.FromStream(ms, false);
                        //        pictureBox1.Image = newImage;
                        //        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        //    }
                        //}
                        //else
                        //{ }
                    }
                }
                dr.Close();
            }
            catch { }
        }

     }
}
