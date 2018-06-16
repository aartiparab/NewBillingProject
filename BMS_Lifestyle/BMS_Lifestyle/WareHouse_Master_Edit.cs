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
    public partial class WareHouse_Master_Edit : Form
    {
        string username;
        string company;
        SqlDataReader dr = null;
        int affect;
        ConnectDb objCon = new ConnectDb();
        SqlConnection con = null;
        string save;
        SqlCommand cmd = null;
        int GID;
        AutoCompleteStringCollection name = new AutoCompleteStringCollection();
        AutoCompleteStringCollection Name1 = new AutoCompleteStringCollection();
        string connectionString;
        public WareHouse_Master_Edit(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void WareHouse_Master_Load(object sender, EventArgs e)
        {
            string query = "select [Name] from [WareHouse_Master] order by [GID] asc";
            dr = objCon.getData(query);
            if (dr.HasRows)
            {
                while (dr.Read())
                { 
                    txtName.Items.Add(dr["Name"].ToString().ToUpper());
                }
            }
            dr.Close();
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text != "")
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
                                command.CommandText = "WareHouse_Update";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@GID", GID));
                                command.Parameters.Add(new SqlParameter("@Name", txtName.Text.TrimEnd().TrimStart()));
                                command.Parameters.Add(new SqlParameter("@Old_Name", txtNewName.Text.TrimEnd().TrimStart()));
                                command.Parameters.Add(new SqlParameter("@Address", txtAddress.Text.TrimEnd().TrimStart()));
                                command.Parameters.Add(new SqlParameter("@EnterBy", username));
                                command.Parameters.Add(new SqlParameter("@Modify_By", username));
                                command.Parameters.Add(new SqlParameter("@Company", company));
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
                                    MessageBox.Show("Save was unsuccessful...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Clear();
                                }
                                catch (Exception ex2)
                                {
                                }
                            }
                        }

                    }

                    txtName.Text = "";
                    txtAddress.Text = "";
                    txtNewName.Text = "";
                }
                else 
                {
                    MessageBox.Show("Select Name");
                    this.ActiveControl = txtName;
                }

            }
            catch
            { }
        }

        public void Clear()
        {
            txtName.Text = "";
            txtAddress.Text = "";
            txtNewName.Text = "";
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select [Name],[GID],[Address] from [WareHouse_Master] where [Name] = '" + txtName.Text + "'";
            dr = objCon.getData(query);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtNewName.Text = dr["Name"].ToString();
                    GID = Convert.ToInt32(dr["GID"].ToString());
                    txtAddress.Text = dr["Address"].ToString();
                }
            }
            dr.Close();
        }
    }
}
