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
    public partial class Size_Modify : Form
    {
        string username;
        string company, connectionString;
        SqlDataReader dr = null;
        int affect;
        ConnectDb objCon = new ConnectDb();
        SqlConnection con = null;
        string save;
        SqlCommand cmd = null;
        string sizepk;

        public Size_Modify(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Size_Modify_Load(object sender, EventArgs e)
        {
            string str2 = "SELECT [size_name] FROM [Item_size_Master] order by [size_pk] asc";
            dr = objCon.getData(str2);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cmbName.Items.Add(dr[0].ToString().ToUpper());
                }
            }
            dr.Close();
            cmbName.SelectedIndex = -1;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strfill = "select [size_pk], [size_name] from [Item_size_Master] where [size_name] = '" + cmbName.Text + "'";
                dr = objCon.getData(strfill);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        sizepk = dr["size_pk"].ToString();
                        txtnew.Text = dr["size_name"].ToString();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbName.Text != "")
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
                                command.CommandText = "Itemsize_Edit";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@sizepk ", sizepk);
                                command.Parameters.AddWithValue("@sizename", txtnew.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@old_name", cmbName.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Company", company);
                                command.Parameters.AddWithValue("@Modifyby", username);
                                command.ExecuteNonQuery();

                                // Attempt to commit the transaction.
                                transaction.Commit();
                                MessageBox.Show("Updated Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtnew.Clear();
                               // txtold.Clear();
                                
                            }
                            catch (Exception ex)
                            {
                                // Attempt to roll back the transaction.
                                try
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Update was unsuccessful...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtnew.Clear();
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
                    this.ActiveControl = cmbName;
                }
            }
            catch (Exception e1)
            { }
        }
    }
}

//            try
//            {
//                if (cmbName.Text != "")
//                {
//                string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();

//                if (ans == "Yes")
//                {
                    
                        
//                        using (SqlConnection connection = new SqlConnection(connectionString))
//                        {
//                            connection.Open();

//                            SqlCommand command = connection.CreateCommand();
//                            SqlTransaction transaction;

//                            // Start a local transaction.
//                            transaction = connection.BeginTransaction("SampleTransaction");

//                            // Must assign both transaction object and connection
//                            // to Command object for a pending local transaction
//                            command.Connection = connection;
//                            command.Transaction = transaction;
//                            try
//                            {
//                                command.CommandText = "Customer_Edit";
//                                command.CommandType = CommandType.StoredProcedure;
//                        string save = "UPDATE [Item_size_Master] SET [size_name] = '" + txtGroup.Text.Trim() + "',[Modify_by]= '" + username + "' ,[Modify_date]= '" + System.DateTime.Now.ToString("MM/dd/yyyy") + "',[Company] = '" + company + "' where [size_name] = '" + comboBox1.Text + "'";
//                        affect = objCon.affect(save);
//                        MessageBox.Show("Updated Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                    else
//                    {
//                        MessageBox.Show("Size Name cannnot be blank");
//                    }
//                    txtGroup.Text = "";
//                    comboBox1.Text = "";
//                }
//            }
//            catch
//            { }
//        }
//    }
//}
