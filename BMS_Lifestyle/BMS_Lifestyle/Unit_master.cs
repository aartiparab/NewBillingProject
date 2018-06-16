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
    public partial class Unit_master : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        int affect;
        public string image;
        string username, company, connectionString, unitname;
        ConnectDb objCon = new ConnectDb();
        AutoCompleteStringCollection unitname1 = new AutoCompleteStringCollection();
        public Unit_master(string username,string company,string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Unit_master_Load(object sender, EventArgs e)
        {
            string str = "SELECT [unit_name] FROM [Unit_Master]";
            dr = objCon.getData(str);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    unitname1.Add(dr[0].ToString());
                }
            }
            dr.Close();
            txtunit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtunit.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtunit.AutoCompleteCustomSource = unitname1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool Validation()
        {
            try
            {
                string str = "SELECT [unit_name] FROM [Unit_Master] where [unit_name] = '" + txtunit.Text + "'";
                dr = objCon.getData(str);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    { 
                    unitname = dr[0].ToString();
                    }
                }
                dr.Close();
            }
            catch { }
            if (txtunit.Text == unitname)
            {
                MessageBox.Show("Unit already exists");
                return false;
            }
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtunit.Text != "")
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
                            command.CommandText = "INSERT INTO [Unit_Master]([unit_name],[company],[Enter_by],[Enter_date])VALUES('"+txtunit.Text.TrimEnd().TrimStart()+"','"+company+"','"+username+"',getDate())";
                            command.ExecuteNonQuery();

                            // Attempt to commit the transaction.
                            transaction.Commit();
                            MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtunit.Text = "";
                        }
                        catch
                        {
                            // Attempt to roll back the transaction.
                            try
                            {
                                transaction.Rollback();
                                MessageBox.Show("Save was unsuccessful...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtunit.Text = "";
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
                    MessageBox.Show("Unit cannot be empty");
                    this.ActiveControl = txtunit;
                }
                
            }
            catch
            { }
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
