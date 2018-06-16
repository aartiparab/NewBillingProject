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
    public partial class Tax_Master : Form
    {
        int affect;
        ConnectDb objCon = new ConnectDb();
        string save;
        string username, company, connectionString;
        SqlDataReader dr = null;
        public AutoCompleteStringCollection tax = new AutoCompleteStringCollection();
        string Command;

        public Tax_Master(string username, string company, string connectionString)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void Tax_Master_Load(object sender, EventArgs e)
        {
            string str5 = "select [Tax_Name] FROM [Tax_Master]";
            dr = objCon.getData(str5);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    tax.Add(dr[0].ToString());
                }
            }
            dr.Close();
            txtName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtName.AutoCompleteCustomSource = tax;
        }
        public bool txtValidation()
        {
            int flag = 0;
            string str5 = "select [Tax_Name] FROM [Tax_Master] where [Tax_Name] = '"+txtName.Text+"'";
            dr = objCon.getData(str5);
            if (dr.HasRows)
            {
                flag = 1;
            }
            dr.Close();

            if (flag == 1)
            {
                MessageBox.Show("Tax Name already exists");
                this.ActiveControl = txtName;
                return false;
            }
            if (txtName.Text == "")
            {
                MessageBox.Show("Enter Tax Name.");
                this.ActiveControl = txtName;
                return false;
            }
            if (txtPers.Text == "")
            {
                MessageBox.Show("Enter Tax %.");
                this.ActiveControl = txtPers;
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
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
                                command.CommandText = "INSERT INTO [Tax_Master]([Tax_Name],[Tax_per],[SGST],[CGST],[IGST],[Enterby],[Enterdate],[company])"+
                                                      "VALUES('"+ txtName.Text.TrimEnd().TrimStart() +"','"+ txtPers.Text.TrimEnd().TrimStart() +"','"+ txtsgst.Text +"',"+
                                                      "'"+ txtcgst.Text +"','"+ txtigst.Text +"','"+ username +"',getDate(),'"+ company +"')";
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
                }
            }
            catch
            { }
        }
        public void Clear()
        {
            txtcgst.Text = "0";
            txtigst.Text = "0";
            txtsgst.Text = "0";
            txtPers.Text = "0";
            txtName.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPers_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double gst = Convert.ToDouble(txtPers.Text);
                txtsgst.Text = (gst / 2).ToString();
                txtcgst.Text = (gst / 2).ToString();
                txtigst.Text = txtPers.Text;
            }
            catch
            { }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
