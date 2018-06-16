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
    public partial class Rack_Master_Edit : Form
    {
        string username;
        string company;
        SqlDataReader dr = null;
        int affect;
        ConnectDb objCon = new ConnectDb();
        SqlConnection con = null;
        string connectionString;
        string save;
        string WH_ID;
        public Rack_Master_Edit(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Rack_Master_Load(object sender, EventArgs e)
        {
            string query = "select distinct [WareHouse_Name] from [Rack_Master]";
            dr = objCon.getData(query);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CmbWarehouse.Items.Add(dr["WareHouse_Name"].ToString().ToUpper());
                }
            }
            dr.Close();
        }

        public bool Validation()
        {
            if (CmbWarehouse.Text == "")
            {
                MessageBox.Show("Select Warehouse");
                this.ActiveControl = CmbWarehouse;
                return false;
            }
            if (dataGridView1.RowCount <= 1)
            {
                MessageBox.Show("Enter Rack Name");
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation() == true)
            {
                string ans = (MessageBox.Show("Do you want to Continue Update", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();

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
                                //command.Connection = connection;
                                //command.Transaction = transaction;
                                
                                for (int i = 0; i <Convert.ToInt32(dataGridView1.RowCount - 1); i++)
                                {
                                   string id1;
                                    try
                                    {
                                        id1 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                    }
                                    catch
                                    {
                                        id1="";
                                    }
                                    if(id1 != "")
                                    {
                                        command = new SqlCommand("RackMaster_Update",connection,transaction);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@WH_ID", WH_ID);
                                        command.Parameters.AddWithValue("@RackPK", dataGridView1.Rows[i].Cells["rack_id"].Value.ToString());
                                        command.Parameters.AddWithValue("@WareHouse_Name", CmbWarehouse.Text.TrimStart().TrimEnd());
                                        command.Parameters.AddWithValue("@RackName", dataGridView1.Rows[i].Cells["rackname"].Value.ToString());
                                        command.Parameters.AddWithValue("Old_Rack_Name", dataGridView1.Rows[i].Cells["old_name1"].Value.ToString());
                                        command.Parameters.AddWithValue("@Modify_By", username);
                                        command.Parameters.AddWithValue("@Company", company);
                                        command.ExecuteNonQuery();                                       
                                    }
                                    else if(id1 == "")
                                    {
                                        command = new SqlCommand("Rack_Save",connection,transaction);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.AddWithValue("@WH_ID", WH_ID);
                                        command.Parameters.AddWithValue("@WareHouse_Name",CmbWarehouse.Text.TrimStart().TrimEnd());
                                        command.Parameters.AddWithValue("@RackName",dataGridView1.Rows[i].Cells["rackname"].Value.ToString());
                                        command.Parameters.AddWithValue("@EnterBy",username);
                                        command.Parameters.AddWithValue("@Company",company);
                                        command.ExecuteNonQuery();
                                    }                                           
                                }
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
            try {
                    dataGridView1.Rows.Clear();
                    int count = 0;
                    string query = "select [WH_ID],[Rack_pk],[RackName] from [Rack_Master] where [WareHouse_Name] = '" + CmbWarehouse.Text + "'";
                    dr = objCon.getData(query);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            WH_ID = dr["WH_ID"].ToString();
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[count].Cells["rack_id"].Value = dr["Rack_pk"].ToString();
                            dataGridView1.Rows[count].Cells["rackname"].Value = dr["RackName"].ToString();
                            dataGridView1.Rows[count].Cells["old_name1"].Value = dr["RackName"].ToString();
                            count++;
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
