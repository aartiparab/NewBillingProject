using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace BMS_Lifestyle
{

    public partial class SupplierEdit : Form
    {
        int affect;
        string username, connectionString;
        string company;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        ConnectDb ObjCon = new ConnectDb();
        string sup_id;
        public SupplierEdit(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void SupplierEdit_Load(object sender, EventArgs e)
        {
            try
            {
                string strfill2 = "select [State_Code] FROM [State_Code_Master] order by [State_Code] asc";
                dr = ObjCon.getData(strfill2);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbState.Items.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();

                string strfill3 = "select [SupName] FROM [Supplier] order by [SupId] asc";
                dr = ObjCon.getData(strfill3);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        comboBox2.Items.Add(dr[0].ToString());
                    }
                }
                dr.Close();
            }
            catch { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text != "")
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
                                command.CommandText = "Supplier_Edit";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@SupId",sup_id);
                                command.Parameters.AddWithValue("@SupName",NewName.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@SupOldName",comboBox2.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Contact_person",txtconPer.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Display_Name",txtDisplayName.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@SupAddress",txtAdd.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Mobile",txtMobile.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@PhoneNo",txtPhoneNo.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@EmailId",txtEmail.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Sup_TANno",txtTAN_no.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Sup_CINno",txtCIN_no.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@GST_No",txtgstno.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@state",cmbState.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@PAN_no",txtpanno.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Aadhar_No", txtAadharNo.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Modify_by",username);
                                command.Parameters.AddWithValue("@Company",company);
                                command.ExecuteNonQuery();

                                // Attempt to commit the transaction.
                                transaction.Commit();
                                MessageBox.Show("Updated Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                            }
                            catch (Exception ex)
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
                    MessageBox.Show("Select Customer Name.");
                    this.ActiveControl = comboBox2;
                }
            }
            catch (Exception e1)
            {
            }
        }

        public void Clear()
        {
            comboBox2.Text = "";
            txtAdd.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtCIN_no.Text = "";
            txtTAN_no.Text = "";
            txtconPer.Text = "";
            txtgstno.Text = "";
            cmbState.Text = "";
            txtpanno.Text = "";
            NewName.Text = "";
            txtDisplayName.Text = "";
            txtAadharNo.Text = "";
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string CommantT = "select [SupId],[SupName],[Contact_person],[Display_Name],[SupAddress],[Mobile],[PhoneNo],[EmailId],[Sup_TANno],[Sup_CINno]"+
                                  ",[GST_No],[state],[PAN_no],[Aadhar_No] from [Supplier] where [SupName] = '" + comboBox2.Text + "'";
                dr = ObjCon.getData(CommantT);
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        sup_id = dr["SupId"].ToString();
                        NewName.Text = dr["SupName"].ToString();
                        txtconPer.Text = dr["Contact_person"].ToString();
                        txtDisplayName.Text = dr["Display_Name"].ToString();
                        txtAdd.Text = dr["SupAddress"].ToString();
                        txtMobile.Text = dr["Mobile"].ToString();
                        txtPhoneNo.Text = dr["PhoneNo"].ToString();
                        txtEmail.Text = dr["EmailId"].ToString();
                        txtTAN_no.Text = dr["Sup_TANno"].ToString();
                        txtCIN_no.Text = dr["Sup_CINno"].ToString();
                        txtgstno.Text = dr["GST_No"].ToString();
                        cmbState.Text = dr["state"].ToString();
                        txtpanno.Text = dr["PAN_no"].ToString();
                        txtAadharNo.Text = dr["Aadhar_No"].ToString();
                    }
                }
                dr.Close();
                if (cmbState.Text == "")
                {
                    cmbState.SelectedIndex = 26;
                }
            }
            catch
            {
            }
          
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtVatTinNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtgstno_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;

            if (txtgstno.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"\d{2}[A-Z]{5}\d{4}[A-Z]{1}[0-9A-Z]{1}[A-Z]{1}[0-9A-Z]{1}$");

                if (!mRegxExpression.IsMatch(txtgstno.Text.Trim()))
                {

                    MessageBox.Show("Invalid GST No");

                    txtgstno.Focus();
                    txtgstno.Text = "";
                    txtpanno.Text = "";
                    cmbState.Text = "";
                }
                else
                {
                    string gstin = txtgstno.Text;
                    string code = gstin.Substring(0, 2).ToString();
                    string panno = (gstin.Substring(2, 10));
                    txtpanno.Text = panno;

                    string sel = "Select [State_Code] from [State_Code_Master] where [State_Code] like '" + code + "%'";
                    dr = ObjCon.getData(sel);
                    if (dr.HasRows == true)
                    {
                        while (dr.Read())
                        {
                            cmbState.Text = dr["State_Code"].ToString().ToUpper();

                        }
                    }
                    dr.Close();
                }

            }
        }

        private void txtpanno_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;

            if (txtpanno.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"^[A-Z]{5}\d{4}[A-Z]{1}$");

                if (!mRegxExpression.IsMatch(txtpanno.Text.Trim()))
                {

                    MessageBox.Show("Invalid PAN No");

                    txtpanno.Focus();
                    txtpanno.Text = "";
                }

            }
        }

        private void txtTAN_no_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;

            if (txtTAN_no.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"^[A-Z]{4}\d{5}[A-Z]{1}$");

                if (!mRegxExpression.IsMatch(txtTAN_no.Text.Trim()))
                {

                    MessageBox.Show("Invalid TAN No");
                    txtTAN_no.Focus();
                    txtTAN_no.Text = "";
                }
            }
        }

        private void txtCIN_no_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;

            if (txtCIN_no.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"^[A-Z]{1}\d{5}[A-Z]{2}\d{4}[A-Z]{3}\d{6}$");

                if (!mRegxExpression.IsMatch(txtCIN_no.Text.Trim()))
                {

                    MessageBox.Show("Invalid CIN No");
                    txtCIN_no.Focus();
                    txtCIN_no.Text = "";
                }
            }
        }

        private void comboBox2_KeyUp(object sender, KeyEventArgs e)
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

        private void txtsupcode_KeyUp(object sender, KeyEventArgs e)
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

        private void txtAdd_KeyUp(object sender, KeyEventArgs e)
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

        private void txtMobile_KeyUp(object sender, KeyEventArgs e)
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

        private void txtEmail_KeyUp(object sender, KeyEventArgs e)
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

        private void txtgstno_KeyUp(object sender, KeyEventArgs e)
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

        private void txtpanno_KeyUp(object sender, KeyEventArgs e)
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

        private void cmbState_KeyUp(object sender, KeyEventArgs e)
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

        private void txtTAN_no_KeyUp(object sender, KeyEventArgs e)
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

        private void txtCIN_no_KeyUp(object sender, KeyEventArgs e)
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

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void txtAadharNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13 || e.KeyChar == 32)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void txtAadharNo_KeyUp(object sender, KeyEventArgs e)
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

        private void txtAadharNo_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;

            if (txtAadharNo.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"^\d{4}\s\d{4}\s\d{4}$");

                if (!mRegxExpression.IsMatch(txtAadharNo.Text.Trim()))
                {
                    MessageBox.Show("Invalid Aadhar No.");
                    this.ActiveControl = txtAadharNo;
                    txtAadharNo.Text = "";
                }
            }
        }
    }
}
