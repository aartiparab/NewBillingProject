using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace BMS_Lifestyle
{
    public partial class Size_Master : Form
    {
        string username;
        string company, connectionString;
        SqlDataReader dr = null;
        int affect;
        ConnectDb ObjCon = new ConnectDb();
        SqlConnection con = null;
        string save;
        int flag = 0;
        AutoCompleteStringCollection sizename = new AutoCompleteStringCollection();

        public Size_Master(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Size_Master_Load(object sender, EventArgs e)
        {
            string cmd2 = "select [size_name] from [Item_size_Master]";
            dr = ObjCon.getData(cmd2);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    sizename.Add(dr[0].ToString());
                }
            }
            dr.Close();
            txtSize.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSize.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSize.AutoCompleteCustomSource = sizename;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool txtValidation()
        {
            string cmd2 = "select [size_name] from [Item_size_Master] where  [size_name] = '" + txtSize.Text + "'";
            dr = ObjCon.getData(cmd2);
            if (dr.HasRows)
            {
                flag = 1;
            }
            dr.Close();

            if (flag == 1)
            {
                MessageBox.Show("Size Name already exists");
                this.ActiveControl = txtSize;
                return false;
            }

            if (txtSize.Text == "")
            {
                MessageBox.Show("Enter Size Name");
                this.ActiveControl = txtSize;
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
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
                            command.CommandText = "INSERT INTO [Item_size_Master]([size_name],[Company],[Enterby],[EnterDate]) VALUES('" + txtSize.Text.TrimEnd().TrimStart() + "','" + company + "','" + username + "',getDate())";
                            command.ExecuteNonQuery();
                           
                            //Attemt to commit the transaction.
                            transaction.Commit();
                            MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSize.Clear();
                        }
                        catch (Exception ex)
                        {
                            // Attempt to roll back the transaction.
                            try
                            {
                                transaction.Rollback();
                                MessageBox.Show("Save was unsuccessful...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtSize.Clear();
                            }
                            catch (Exception ex2)
                            {
                            }
                        }
                    }

                }
            }
        }
    }
}
                   
