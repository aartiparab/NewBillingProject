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
using System.IO;

namespace BMS_Lifestyle
{
    public partial class Unit_Modify : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        int affect;
        public string image;
        string username, company,connectionString;
         string unitpk;
        ConnectDb objCon = new ConnectDb();

        public Unit_Modify(string username,string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
             this.connectionString =connectionString;
        }

        private void Unit_Modify_Load(object sender, EventArgs e)
        {
            try
            {
                string str1 = "SELECT [unit_name] FROM [Unit_Master] order by [unit_pk] asc";
                dr = objCon.getData(str1);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
            }
            catch { }
            comboBox1.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strfill = "SELECT [unit_name],[unit_pk] FROM [Unit_Master] where [unit_name] = '"+comboBox1.Text+"'";
                dr = objCon.getData(strfill);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtunit.Text = dr[0].ToString();
                        unitpk = dr[1].ToString();
                    }
                }
                dr.Close();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text != "")
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
                                command.CommandText = "Unit_Edit";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@unitpk", unitpk);
                                command.Parameters.AddWithValue("@unitename", txtunit.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@oldunitename", comboBox1.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Company", company);
                                command.Parameters.AddWithValue("@Modifyby", username);
                                command.ExecuteNonQuery();

                                // Attempt to commit the transaction.
                                transaction.Commit();
                                MessageBox.Show("Updated Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtunit.Clear();
                               // txtold.Clear();
                                comboBox1.Text = "";
                                
                            }
                            catch (Exception ex)
                            {
                                // Attempt to roll back the transaction.
                                try
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Update was unsuccessful...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtunit.Clear();
                                    //txtold.Clear();
                                }
                                catch (Exception ex2)
                                {
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select Size Name.");
                    this.ActiveControl = comboBox1;
                }
            }
            catch (Exception e1)
            { }
    
        }
    



            //try
            //{
            //    string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();

            //    if (ans == "Yes")
            //    {
            //        if (txtunit.Text != "")
            //        {
            //            dr.Close();
            //            string save = "UPDATE [Unit_Master] SET [unit_name] = '" + txtunit.Text.TrimEnd() + "',[Modify_by]='" + username + "',[Modify_Date]= '"+System.DateTime.Now.ToString("MM/dd/yyyy")+"',[company] = '" + company + "' WHERE [unit_pk] = '" + label3.Text + "'";
            //            affect = objCon.affect(save);
            //            MessageBox.Show("Updated Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            MessageBox.Show("New Unit name cannot be empty");
            //        }
            //    }
            //}
            //catch { }
        

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

        private void txtunit_KeyUp(object sender, KeyEventArgs e)
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
