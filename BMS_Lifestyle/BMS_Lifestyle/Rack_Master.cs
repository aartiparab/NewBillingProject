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
    public partial class Rack_Master : Form
    {
        string username;
        string company;
        SqlDataReader dr = null;
        int affect;
        ConnectDb objCon = new ConnectDb();
        SqlConnection con = null;
        string connectionString;
        string save;
        string GID;
        public Rack_Master(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Rack_Master_Load(object sender, EventArgs e)
        {
            string query = "select [Name] from [WareHouse_Master]";
            dr = objCon.getData(query);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CmbWarehouse.Items.Add(dr["Name"].ToString().ToUpper());
                }
            }
            dr.Close();
        }

        public bool Validation()
        {
            if (CmbWarehouse.Text == "")
            {
                MessageBox.Show("Enter WareHouse Name");
                this.ActiveControl = CmbWarehouse;
                return false;
            }
            if (dataGridView1.RowCount <= 1 )
            {
                MessageBox.Show("Enter RackName");
                return false;
            }
            try
            {
                int rowss = dataGridView1.RowCount - 1;
                for (int i = 0; i <= rowss; i++)
                {
                    string rack11 = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    string strrack = "select [RackName] from [Rack_Master] where [WareHouse_Name] = '" + CmbWarehouse.Text.TrimEnd().TrimStart() + "' and [RackName] = '" + rack11 + "'";
                    dr = objCon.getData(strrack);
                    if (dr.HasRows)
                    {
                        MessageBox.Show("Rack " + rack11 + "belongs to this warehouse already exixts.");
                        return false;
                    }
                }
            }
            catch { }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
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
                            
                            try
                            {
                                for(int i = 0 ; i < Convert.ToInt32(dataGridView1.Rows.Count -1) ; i++)
                                {
                                   //command.Connection = connection;
                                   // command.Transaction = transaction;
                                    command = new SqlCommand("Rack_Save",connection,transaction);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@WH_ID", GID);
                                    command.Parameters.AddWithValue("@WareHouse_Name",CmbWarehouse.Text.TrimEnd().TrimStart());
                                    command.Parameters.AddWithValue("@RackName",dataGridView1.Rows[i].Cells[0].Value.ToString());
                                    command.Parameters.AddWithValue("@EnterBy",username);
                                    command.Parameters.AddWithValue("@Company",company);
                                    command.ExecuteNonQuery();
                                }
                                
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
                                    MessageBox.Show("Save was unsuccessful...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CmbWarehouse.Focus();
            }
        }

        public void Clear()
        {
            CmbWarehouse.Text = "";
            dataGridView1.Rows.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                string query = "select [GID] from [WareHouse_Master] where [Name] = '"+CmbWarehouse.Text+"'";
                dr = objCon.getData(query);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        GID = dr["GID"].ToString();
                    }
                }
                dr.Close();
            }
            catch { }
            
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                try
                {
                    int row = dataGridView1.RowCount - 2;
                    string[] bcode = new string[row + 1];

                    for (int i = 0; i <= row; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value != null && e.RowIndex - 1 != i)
                        {
                            bcode[i] = dataGridView1.Rows[i].Cells["rackname"].Value.ToString();
                        }
                    }
                    for (int i = 0; i <= row; i++)
                    {

                        if (dataGridView1.Rows[e.RowIndex - 1].Cells["rackname"].Value.ToString() == bcode[i])
                        {
                            dataGridView1.Rows[e.RowIndex - 1].Cells["rackname"].Value = "";
                        }
                    }
                }
                catch { }  
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = e.Value.ToString().ToUpper();
                e.FormattingApplied = true;
            }
        }
    }
}
