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
    public partial class Login_screen : Form
    {
        static string login;
        static string userID;
        static string password;
        static string tempPass;
        static string role;
        int role_Id;
        string loginCommand;
        DateTime fileDate;
        string directory = null;
        TimeSpan diff;
        DateTime chkDate;
        ConnectDb obj = new ConnectDb();
        SqlDataReader dr = null;
        string username;
        string company,name;
        string commandT = null;
        AutoCompleteStringCollection temp = new AutoCompleteStringCollection();
        string connectionString = "Data Source=SERVER;Initial Catalog=BMSLifestyle;User ID=sa;Password=preet@1234";
        public Login_screen()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            login = txtLogin.Text.ToString();
            password = txtPass.Text.ToString();

            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please Enter Company Name!", "Error");
                comboBox1.Focus();
            }
            else
            {

                commandT = "select [Company_ID], [Company_Name] from [Company_Master] where [Company_Name] = '"+comboBox1.Text+"'";
                String name = null;

                dr = obj.getData(commandT);

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        company = dr["company_Id"].ToString();
                        name = dr["Company_Name"].ToString();
                    }
                    dr.Close();
                    userLogin();

                }
                else
                {
                    MessageBox.Show("Invalid Company Name!");
                    comboBox1.Text = "";
                    comboBox1.Focus();
                    dr.Close();
                }


            }
            txtLogin.Clear();
            txtPass.Clear();
            comboBox1.Clear();
        }

        public void userLogin()
        {
            try
            {
                loginCommand = "select User_Name, Password, Role from User_Master where User_Name = '" + login + "'";

                obj = new ConnectDb();
                dr = obj.getData(loginCommand);

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        userID = dr["User_Name"].ToString();
                        tempPass = dr["Password"].ToString();
                        role = dr["Role"].ToString();

                        tempPass.Trim();
                        password.Trim();

                        if (password == tempPass)
                        {
                            if (role == "Administrator")
                            {
                                role_Id = 1;
                            }
                            else if(role == "Manager")
                            {
                                role_Id = 2;
                            }
                            else if (role == "Executive")
                            {
                                role_Id = 3;
                            }
                            MessageBox.Show("Welcome " + userID + " to Billing software of " + comboBox1.Text + "", "Login", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            this.Hide();
                            Main_Screen md = new Main_Screen(txtLogin.Text, role_Id, company, connectionString);
                            md.ShowDialog();

                        }

                        else
                        {
                            MessageBox.Show("Invalid Password", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Invalid User name", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("The following error occured:\n" + e.Message.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dr.Close();
            }
        }

        private void Login_screen_Load(object sender, EventArgs e)
        {
            try
            {
                txtLogin.Focus();
                commandT = "select [Company_Name] from [Company_Master] order by Company_Name desc ";
                dr = obj.getData(commandT);
                while (dr.Read())
                {
                    temp.Add(dr[0].ToString());
                }
                dr.Close();
                comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                comboBox1.AutoCompleteCustomSource = temp;

                DateValidator dv = new DateValidator();
                int syscmd = dv.dateValid();

                if (syscmd == 1)
                {
                    chkDate = DateTime.Now;
                    directory = Directory.GetCurrentDirectory();

                    fileDate = Convert.ToDateTime("25/12/2018");

                    diff = new TimeSpan();

                    diff = chkDate - fileDate;

                    double d = diff.TotalDays;

                    if (d > 0)
                    {
                        //MessageBox.Show("System", "Windows Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Diagnostics.Process.GetCurrentProcess().Kill();
                    }
                }
                else
                {
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
               
            }
            catch
            { }
        }

        private void Login_screen_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}