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
    public partial class MarketPlace_Edit : Form
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
        string itemname;
        string mid1;
        public MarketPlace_Edit(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Salesman_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                string strfill = "select [Name] from [Salesman_Master] where [DeleteFlag] = 'No' order by [ID] asc";
                dr = ObjCon.getData(strfill);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbName.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
            }
            catch { }
        }

        private void txtmob_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 46)
            { 
                e.Handled = false; 
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string str1 = "SELECT [ID],[Name] FROM [Salesman_Master] where [Name] = '" + cmbName.Text + "'";
                dr = ObjCon.getData(str1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        mid1 = dr["ID"].ToString();
                        txtNewName.Text = dr["Name"].ToString();
                    }
                }
                dr.Close();
            }
            catch { }
        }
        public bool Validation()
        {
    
            if (cmbName.Text == "")
            {
                MessageBox.Show("Name cannot be empty");
                return false;
            }
            
            if (txtmob.Text == "")
            {
                MessageBox.Show("Mobile No. cannot be empty");
                return false;
            }

            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();
                if (ans == "Yes")
                {
                    if (cmbName.Text != "")
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
                                command.CommandText = "SalesMan_Edit";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@ID", mid1);
                                command.Parameters.AddWithValue("@Name", txtNewName.Text);
                                command.Parameters.AddWithValue("@old_Name", cmbName.Text);
                                command.Parameters.AddWithValue("@Company", company);
                                command.Parameters.AddWithValue("@Modify_by", username);
                                command.ExecuteNonQuery();

                                // Attempt to commit the transaction.
                                transaction.Commit();
                                MessageBox.Show("Updated Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                            }
                            catch
                            {
                                // Attempt to roll back the transaction.
                                try
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Update was unsuccessful...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Clear();
                                }
                                catch (Exception ex2)
                                {
                                }
                            }
                        }
                    }
                    else 
                    {
                        MessageBox.Show("Select Market place name");
                        this.ActiveControl = cmbName;
                    }
                }
            }
            catch { }
        }
        public void Clear()
        {
            txtNewName.Text = "";
            cmbName.Text = "";
            txtmob.Text = "";
            txtadd.Text = "";
            dateTimePicker1.Text = "";
            txtemail.Text = "";
            pictureBox1.Image = null;
        }
    }
}
