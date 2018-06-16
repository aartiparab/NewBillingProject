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
    public partial class ItemGroup_modify : Form
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
        int grppk;

        public ItemGroup_modify(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void ItemGroup_modify_Load(object sender, EventArgs e)
        {
            string str2 = "SELECT distinct [group_name] FROM [Item_Group_Master]";
            dr = objCon.getData(str2);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    comboBox1.Items.Add(dr[0].ToString().ToUpper());
                }
            }
            dr.Close();
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strfill = "select [group_name],[grp_pk] from [Item_Group_Master] where [group_name] = '" + comboBox1.Text + "'";
                dr = objCon.getData(strfill);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtGroup.Text = dr[0].ToString();
                        grppk = Convert.ToInt32(dr[1].ToString());
                    }
                }
                dr.Close();
            }
            catch
            { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Validation()
        {
            if (txtGroup.Text == "")
            {
                MessageBox.Show("Group Name cannnot be blank");
                this.ActiveControl = txtGroup;
                return false;
            }
            return true;
        }
         
        private void btnSave_Click(object sender, EventArgs e)
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
                                command.CommandText = "ItemGroup_Update";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add(new SqlParameter("@grp_pk",grppk));
                                command.Parameters.Add(new SqlParameter("@OldName", comboBox1.Text.TrimStart().TrimEnd()));
                                command.Parameters.Add(new SqlParameter("@GroupName", txtGroup.Text.TrimStart().TrimEnd()));
                                command.Parameters.Add(new SqlParameter("@Enter_by", username.TrimStart().TrimEnd()));
                                command.Parameters.Add(new SqlParameter("@Modify_by", username.TrimStart().TrimEnd()));
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
            catch
            {}
        }

        public void Clear()
        {
            txtGroup.Text = "";
            comboBox1.Text = "";
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
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
    }
}
