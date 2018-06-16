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
    public partial class USerMaster_Edit : Form
    {
        string username, company, connectionString;
        int affect;
        public string image;
        bool chkimg = false;
        ConnectDb ObjCon = new ConnectDb();
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        string user, pass,userid;
        AutoCompleteStringCollection username1 = new AutoCompleteStringCollection();

        public USerMaster_Edit(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void USerMaster_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                string strfill = "select distinct [User_Name] FROM [User_Master] where [company] = '"+company+"'";
                dr = ObjCon.getData(strfill);
                if (dr.HasRows)
                { 
                    while(dr.Read())
                    {
                        username1.Add(dr[0].ToString());
                    }                
                }
                dr.Close();
                txtusername.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtusername.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtusername.AutoCompleteCustomSource = username1;
            }
            catch
            { }
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strfill2 = "SELECT [Password],[Role],[Gender],[email_id],[Mobile_no],[Resi_address],[Birth_date],[date_of_join],[User_img],[User_ID] FROM [User_Master] where [User_Name] = '" + txtusername.Text + "' and [company] = '" + company + "'";
                dr = ObjCon.getData(strfill2);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtpassword.Text = dr["Password"].ToString();
                        txtnewpass.Text = dr["Password"].ToString();
                        cmbrole.Text = dr["Role"].ToString();
                        cmbgender.Text = dr["Gender"].ToString();
                        txtemail.Text = dr["email_id"].ToString();
                        txtmob.Text = dr["Mobile_no"].ToString();
                        txtadd.Text = dr["Resi_address"].ToString();
                        dtpbirthdate.Text = dr["Birth_date"].ToString();
                        dtpjoiningdate.Text = dr["date_of_join"].ToString();
                        userid = dr["User_ID"].ToString();
                        if (dr["User_img"] != DBNull.Value)
                            {
                                byte[] imageData = (byte[])dr["User_img"];


                                //Initialize image variable
                                Image newImage;
                                //Read image data into a memory stream
                                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                                {
                                    ms.Write(imageData, 0, imageData.Length);

                                    //Set image variable value using memory stream.


                                    newImage = Image.FromStream(ms, false);
                                    pictureBox1.Image = newImage;
                                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                    
                                }
                            }
                            else
                            {
                            }
                    }
                }
                dr.Close();
            }
            catch { 
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            chkimg = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
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
                string strfill1 = "select [Password] FROM [User_Master] where [Password] ! = '"+txtnewpass.Text+"'";
                dr = ObjCon.getData(strfill1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pass = dr[0].ToString();
                    }
                }
                dr.Close();
            }
            catch { }

            if (txtnewpass.Text == pass)
            {
                MessageBox.Show("Password already exixts for another user.");
                return false;
            }
            if (cmbrole.Text == "")
            {
                MessageBox.Show("Select Role for user.");
                return false;
            }
            return true;

        }
        public void Clear()
        {
            txtadd.Text = "";
            txtnewpass.Text = "";
            txtmob.Text = "";
            txtemail.Text = "";
            txtusername.Text = "";
            txtpassword.Text = "";
            cmbgender.Text = "";
            cmbrole.Text = "";
            pictureBox1.Image = null;
            dtpbirthdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            dtpjoiningdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
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
                                    
                                    command.CommandText = "User_edit";
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@User_ID", userid);
                                    command.Parameters.AddWithValue("@User_Name", txtusername.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Password", txtnewpass.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@old_password", txtpassword.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Role", cmbrole.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Gender", cmbgender.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@email_id", txtemail.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Mobile_no", txtmob.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Resi_address", txtadd.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@Birth_date", dtpbirthdate.Value.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@date_of_join", dtpbirthdate.Value.ToString("MM/dd/yyyy"));
                                    command.Parameters.AddWithValue("@company", company);
                                    command.Parameters.AddWithValue("@Modify_by", username);
                                    
                                    if(chkimg == true)
                                    {
                                        byte[] imageData = ReadFile(image);
                                        command.Parameters.AddWithValue("@User_img", (object)imageData);
                                    }
                                    else if (chkimg == false)
                                    {
                                        SqlParameter imageParameter = new SqlParameter("@User_img", SqlDbType.Image);
                                        imageParameter.Value = DBNull.Value;
                                        command.Parameters.Add(imageParameter);
                                    }
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
    }
}
