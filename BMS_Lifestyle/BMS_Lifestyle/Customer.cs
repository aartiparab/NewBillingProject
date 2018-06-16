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
using System.Text.RegularExpressions;

namespace BMS_Lifestyle
{
    public partial class Customer : Form
    {
        string username;
        string company, connectionString;
        int affect;
        ConnectDb ObjCon = new ConnectDb();
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        AutoCompleteStringCollection Party = new AutoCompleteStringCollection();
        AutoCompleteStringCollection Areas = new AutoCompleteStringCollection();
        string cust;
        int flag = 0;
        public Customer(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            fillArea();  
        }
        public void fillArea()
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
            cmbState.SelectedIndex = 26;
        }
        

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool txtValidation()
        {
            string cmd1 = "select CustName from Customer where CustName='" + txtCust_Name.Text + "'";
            dr = ObjCon.getData(cmd1);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cust = dr[0].ToString();
                }   
             }
            dr.Close();

            string cmd2 = "select [CustName],[Gst_No] FROM [Customer] where [CustName] = '"+txtCust_Name.Text+"' and [Gst_No] = '"+txtgstno.Text+"'";
            dr = ObjCon.getData(cmd2);
            if (dr.HasRows)
            {
                flag = 1;
            }
            dr.Close();

            if (flag == 1)
            {
                MessageBox.Show("GST No. with same customer already exists");
                this.ActiveControl = txtgstno;
                return false;
            }

            if (txtCust_Name.Text == "")
            {
                MessageBox.Show("Enter customer name");
                this.ActiveControl = txtCust_Name;
                return false;
            }
            if (cust == txtCust_Name.Text)
            {
              MessageBox.Show("Customer Name already Exists");
              this.ActiveControl = txtCust_Name;
              return false;
            }
            if (txtMobile.Text == "")
            {
                MessageBox.Show("Enter Mobile No.");
                this.ActiveControl = txtMobile;
                return false;
            }
            if (cmbState.Text == "")
            {
                MessageBox.Show("Select State");
                this.ActiveControl = cmbState;
                return false;
            }
            if (txtDisplayName.Text == "")
            {
                MessageBox.Show("Enter Display Name");
                this.ActiveControl = txtDisplayName;
                return false;
            }
               
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtValidation() == true)
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
                                command.CommandText = "Customer_Save";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@CustName",txtCust_Name.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@CustDisplayName", txtDisplayName.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@PersonName",txtContactPer.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@CustAddress",txtAdd.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Mobile",txtMobile.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@PhoneNo",txtPhoneNo.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@EmailId",txtEmail.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Cust_TANno",txtTAN_no.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Cust_CINno",txtCIN_no.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Company",company);
                                command.Parameters.AddWithValue("@AadharNo",txtAadharNo.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Gst_No",txtgstno.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@State",cmbState.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@PAN_no",txtpanno.Text.TrimEnd().TrimStart());
                                command.Parameters.AddWithValue("@Enter_by", username);
                                command.ExecuteNonQuery();
                                
                                // Attempt to commit the transaction.
                                transaction.Commit();
                                MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Clear();
                            }
                            catch (Exception ex)
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
            }
        }
        public void Clear()
        {
            txtContactPer.Text = "";
            txtCust_Name.Text = "";
            txtAdd.Text = "";
            txtMobile.Text = "";
            txtPhoneNo.Text = "";
            txtEmail.Text = "";
            txtTAN_no.Text = "";
            txtCIN_no.Text = "";
            txtgstno.Text = "";
            cmbState.Text = "";
            txtpanno.Text = "";
            txtAadharNo.Text = "";
            txtDisplayName.Text = "";
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

        private void label1_Click(object sender, EventArgs e)
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

                    this.ActiveControl = txtgstno;
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

        
        private void txtCIN_no_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;

            if (txtCIN_no.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"^[A-Z]{1}\d{5}[A-Z]{2}\d{4}[A-Z]{3}\d{6}$");

                if (!mRegxExpression.IsMatch(txtCIN_no.Text.Trim()))
                {

                    MessageBox.Show("Invalid CIN No");
                    this.ActiveControl = txtCIN_no;
                    txtCIN_no.Text = "";
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
                    this.ActiveControl = txtTAN_no;
                    txtTAN_no.Text = "";

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
                    this.ActiveControl = txtpanno;
                    txtpanno.Text = "";
                }
            }
        }

        private void txtCust_Name_KeyUp(object sender, KeyEventArgs e)
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

        private void txtContactPer_KeyUp(object sender, KeyEventArgs e)
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

        private void txtArea_KeyUp(object sender, KeyEventArgs e)
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

        private void txtPhoneNo_KeyUp(object sender, KeyEventArgs e)
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
    }
}
