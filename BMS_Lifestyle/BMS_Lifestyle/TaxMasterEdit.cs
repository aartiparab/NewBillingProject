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
    public partial class TaxMasterEdit : Form
    {
        string username;
        string company, connectionString;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        ConnectDb objCon = new ConnectDb();
        int affect;
        string taxid;
        public TaxMasterEdit(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
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
                                command.CommandText = "Tax_Edit";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@Pk", taxid);
                                command.Parameters.AddWithValue("@Tax_Name",txtnewName.Text);
                                command.Parameters.AddWithValue("@old_name",comboBox1.Text);
                                command.Parameters.AddWithValue("@Tax_per",txtVatPer.Text);
                                command.Parameters.AddWithValue("@SGST",txtsgsttax.Text);
                                command.Parameters.AddWithValue("@CGST",txtcgsttax.Text);
                                command.Parameters.AddWithValue("@IGST",txtigsttax.Text);
                                command.Parameters.AddWithValue("@Modify_by",username);
                                command.Parameters.AddWithValue("@company",company);
                                command.ExecuteNonQuery();

                                // Attempt to commit the transaction.
                                transaction.Commit();
                                MessageBox.Show("Updated Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                            }
                            catch
                            {
                                // Attempt to roll back the transaction.
                                try
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Update was unsuccessful...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Clear();
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
                    MessageBox.Show("Select Tax Name");
                    this.ActiveControl = comboBox1;
                }
            }
            catch
            { }
        }

        public void Clear()
        {
            txtcgsttax.Text = "0";
            txtigsttax.Text = "0";
            txtsgsttax.Text = "0";
            txtnewName.Text = "";
            comboBox1.Text = "";
            txtVatPer.Text = "0";
        }
        private void txtVatPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 46)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void TaxMasterEdit_Load(object sender, EventArgs e)
        {
            try
            {
                string Command = "select [Tax_Name] from Tax_Master order by [Pk] asc ";
                dr = objCon.getData(Command);

                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        comboBox1.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
            }
            catch { }
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

        private void txtVatPer_KeyUp(object sender, KeyEventArgs e)
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

        private void txtcgsttax_KeyUp(object sender, KeyEventArgs e)
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

        private void txtsgsttax_KeyUp(object sender, KeyEventArgs e)
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

        private void txtigsttax_KeyUp(object sender, KeyEventArgs e)
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

        private void txtVatPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gst = Convert.ToDouble(txtVatPer.Text);
                txtsgsttax.Text = (gst / 2).ToString();
                txtcgsttax.Text = (gst / 2).ToString();
                txtigsttax.Text = txtVatPer.Text;
            }
            catch
            { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string str3 = "SELECT cast([Tax_per] AS decimal(18,2)) as Tax_per,cast([SGST] AS decimal(18,2)) as SGST,cast([CGST] AS decimal(18,2)) as CGST " +
                ",cast([IGST] AS decimal(18,2)) as IGST,[Tax_Name],[Pk] FROM [Tax_Master] where [Tax_Name] = '" + comboBox1.Text + "' and [company] = '" + company + "'";
                dr = objCon.getData(str3);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtVatPer.Text = dr["Tax_per"].ToString();
                        txtcgsttax.Text = dr["CGST"].ToString();
                        txtsgsttax.Text = dr["SGST"].ToString();
                        txtigsttax.Text = dr["IGST"].ToString();
                        txtnewName.Text = dr["Tax_Name"].ToString();
                        taxid = dr["Pk"].ToString();
                    }
                }
                dr.Close();
            }
            catch { }
        }

        
    }
}
