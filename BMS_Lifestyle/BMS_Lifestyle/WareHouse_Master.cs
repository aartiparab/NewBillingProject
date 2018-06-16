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
    public partial class WareHouse_Master : Form
    {
        string username;
        string company;
        SqlDataReader dr = null;
        int affect;
        ConnectDb objCon = new ConnectDb();
        SqlConnection con = null;
        string save;
        SqlCommand cmd = null;
        string connectionString;
        AutoCompleteStringCollection warehuse = new AutoCompleteStringCollection();
        public WareHouse_Master(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void WareHouse_Master_Load(object sender, EventArgs e)
        {
            string str5 = "select [Name] from [WareHouse_Master] ";
            dr = objCon.getData(str5);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    warehuse.Add(dr[0].ToString());
                }
            }
            dr.Close();
            txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtName.AutoCompleteCustomSource = warehuse;
        }

        public bool txtValidation()
        {
            int flag = 0;
            string str5 = "select [Name] from [WareHouse_Master] where [Name] = '"+txtName.Text+"'";
            dr = objCon.getData(str5);
            if (dr.HasRows)
            {
                flag = 1;
            }
            dr.Close();

            if (flag == 1)
            {
                MessageBox.Show("Name already exists");
                this.ActiveControl = txtName;
                return false;
            }

            if (txtName.Text == "")
            {
                MessageBox.Show("Enter Name");
                this.ActiveControl = txtName;
                return false;
            }
            if (txtAddress.Text == "")
            {
                MessageBox.Show("Enter Address");
                this.ActiveControl = txtAddress;
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
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

                            // Start a local transaction.
                            transaction = connection.BeginTransaction("SampleTransaction");

                            // Must assign both transaction object and connection
                            // to Command object for a pending local transaction
                            command.Connection = connection;
                            command.Transaction = transaction;
                            try
                            {
                                command.CommandText = "insert into [WareHouse_Master] ([Name],[Address],[EnterBy],[Enter_date]) values ('" + txtName.Text.TrimEnd().TrimStart() + "','" + txtAddress.Text.TrimEnd().TrimStart() + "','" + username + "',getdate())";
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
            catch
            { }
        }

        public void Clear()
        {
            txtName.Text = "";
            txtAddress.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
