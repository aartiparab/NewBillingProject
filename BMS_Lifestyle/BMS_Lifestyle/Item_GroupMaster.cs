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
    public partial class Item_GroupMaster : Form
    {
        string username;
        string company;
        SqlDataReader dr = null;
        int affect;
        ConnectDb objCon = new ConnectDb();
        SqlConnection con = null;
        string connectionString;
        string save ;
        AutoCompleteStringCollection groupname = new AutoCompleteStringCollection();

        public Item_GroupMaster(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Validation()
        {
            if (txtGroup.Text == "")
            {
                MessageBox.Show("Please Enter Group Name!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = txtGroup;
                return false;  
            }

            string strfill = "SELECT [group_name] FROM [Item_Group_Master] where [group_name] = '" + txtGroup.Text + "'";
            dr = objCon.getData(strfill);
            if (dr.HasRows)
            {
                MessageBox.Show("Item Group already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.ActiveControl = txtGroup;
                return false;
            }
            dr.Close();
            return true;
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (Validation() == true)
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
                                command.Connection = connection;
                                command.Transaction = transaction;
                                try
                                {
                                    command.CommandText = "INSERT INTO [Item_Group_Master]([group_name],[Company],[Enterby],[EnterDate]) VALUES('" + txtGroup.Text.TrimStart().TrimEnd() + "','" + company + "','" + username + "',getdate())";
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
                        catch
                        {
                        }
                }
                dr.Close();
             }
            else
            {
               txtGroup.Focus();
            }
        }

        public void Clear()
        {
            txtGroup.Text = "";
        }

        private void txtGroup_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                this.SelectNextControl((Control)sender, false, true, true, true);
            }
            if (e.KeyCode == Keys.Down)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return))
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void General_Group_Load(object sender, EventArgs e)
        {
            string strfill = "SELECT [group_name] FROM [Item_Group_Master]";
            dr = objCon.getData(strfill);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    groupname.Add(dr[0].ToString());
                }
            }
            dr.Close();
            txtGroup.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtGroup.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtGroup.AutoCompleteCustomSource = groupname;
        }

    }
}
