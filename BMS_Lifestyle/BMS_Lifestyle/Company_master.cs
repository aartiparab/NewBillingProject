using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Configuration;

namespace BMS_Lifestyle
{
    public partial class Company_master : Form
    {
        public string image;
        SqlDataReader dr = null;
        int affect;
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectDb objCon = new ConnectDb();
        string save, commandT, commandT1, commandT2, save1;
        string username;
        string company;
        string state1;
        string statevalue, assestype, compid;
        AutoCompleteStringCollection state = new AutoCompleteStringCollection();
        AutoCompleteStringCollection juri = new AutoCompleteStringCollection();
        AutoCompleteStringCollection pincode1 = new AutoCompleteStringCollection();
        
        string status;
        public string Id1;
        public Company_master(string username, string company)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;

        }

        private void Company_master_Load(object sender, EventArgs e)
        {
            try
            {
                string strfill2 = "select [sate_name] FROM [State_Code_Master] order by [State_Code] asc";
                dr = objCon.getData(strfill2);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        state.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
                txtState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtState.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtState.AutoCompleteCustomSource = state;

                string strfill3 = "select [code] FROM [State_Code_Master] order by [State_Code] asc";
                dr = objCon.getData(strfill3);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        state.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
                txtState.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtState.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtState.AutoCompleteCustomSource = state;

                string strfill4 = "select distinct [city_name] FROM [pincode_city_master] order by [city_name] asc";
                dr = objCon.getData(strfill4);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        juri.Add(dr[0].ToString().ToUpper());
                    }
                }
                dr.Close();
                juris.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                juris.AutoCompleteSource = AutoCompleteSource.CustomSource;
                juris.AutoCompleteCustomSource = juri;
                city.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                city.AutoCompleteSource = AutoCompleteSource.CustomSource;
                city.AutoCompleteCustomSource = juri;


                compname.Focus();
                txtState.Text = "";
                rbtnAssesse.Checked = true;
                rbtnnonassesse.Checked = false;
                rbtnauto.Checked = true;
                rbtnmanual.Checked = false;
                status = "auto";
            }
            catch { }
        }

        public bool Validation()
        {
            int flag = 0;
            if (compname.Text == "")
            {
                MessageBox.Show("Please Enter Company Name!");
                return false;
            }
            commandT = "select * from Company_Master where Company_Name='"+compname.Text+"'";
            dr = objCon.getData(commandT);
            if (dr.HasRows)
            {
                flag = 1;
            }
            dr.Close();

            if (flag == 1)
            {
                MessageBox.Show("Company Name already Exists!");
                return false;
            }
            if (txtShortCode.Text == "")
            {
                MessageBox.Show("Please Enter Company Short Code!");
                return false;
            }
            if (txtState.Text == "")
            {
                MessageBox.Show("Please Enter State!");
                return false;
            }
            commandT = "select * from Company_Master where Company_sht_code='"+txtShortCode.Text+"'";
            dr = objCon.getData(commandT);
            if (dr.HasRows)
            {
                flag = 1;
            }
            dr.Close();

            if (flag == 1)
            {
                MessageBox.Show("Company short code already Exists!");
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Validation() == true)
            {
                string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();

                if (ans == "Yes")
                {
                    //byte[] imageData = ReadFile(image);

                    string conString = "Data Source=.;Initial Catalog=Neemali_Natural;User ID=sa;Password=preet@1234";
                    con = new SqlConnection(conString);
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand();
                    try
                        {
                            string gstno = txtgst_no.Text + textBox1.Text;
                            if (chkimg == true)
                            {
                                if (assestype == "Yes")
                                {
                                    save = "INSERT INTO [Company_Master]([Company_Name],[Comp_display_name],[Company_Address1],[Company_Address2],[Company_Address3],[Bussiness_Line]," +
                                    "[Pincode],[City],[State],[Country],[Phone_No],[FaxNo],[E_MailID],[DateOfIncorp],[OwnerName1],[OwnerName2],[MobileNo1],[MobileNo2],[PANo]," +
                                    "[Jurisdication],[Company_sht_code],[Enter_by],[Enter_date],[LogoImage],[GST_No],[CINno],[Assese_type])VALUES('" + compname.Text + "'," +
                                    "'" + compname.Text + "','" + add1.Text + "','" + add2.Text + "','" + add3.Text + "','" + busline.Text + "','" + pincode.Text + "','" + city.Text + "','" + txtState.Text + "'," +
                                    "'" + country.Text + "','" + phoneno.Text + "','" + faxno.Text + "','" + email.Text + "','" + datofincorp.Value.ToString("MM/dd/yyyy") + "','" + ownname1.Text + "','" + ownname2.Text + "'," +
                                    "'" + ownmob1.Text + "','" + ownmob2.Text + "','" + panno.Text + "','" + juris.Text + "','" + txtShortCode.Text + "','" + username + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "',@ImageData,'" + gstno + "'," +
                                    "'" + cin_no.Text + "','Yes')";

                                    SqlCommand SqlCom = new SqlCommand(save, con);
                                    byte[] imageData = ReadFile(image);
                                    SqlCom.Parameters.Add(new SqlParameter("@ImageData", (object)imageData));
                                    SqlCom.ExecuteNonQuery();
                                }
                                else
                                {
                                    save = "INSERT INTO [Company_Master]([Company_Name],[Comp_display_name],[Company_Address1],[Company_Address2],[Company_Address3],[Bussiness_Line]," +
                                        "[Pincode],[City],[State],[Country],[Phone_No],[FaxNo],[E_MailID],[DateOfIncorp],[OwnerName1],[OwnerName2],[MobileNo1],[MobileNo2],[PANo]," +
                                        "[Jurisdication],[Company_sht_code],[Enter_by],[Enter_date],[LogoImage],[CINno],[Assese_type])VALUES('" + compname.Text + "'," +
                                        "'" + compname.Text + "','" + add1.Text + "','" + add2.Text + "','" + add3.Text + "','" + busline.Text + "','" + pincode.Text + "','" + city.Text + "','" + txtState.Text + "'," +
                                        "'" + country.Text + "','" + phoneno.Text + "','" + faxno.Text + "','" + email.Text + "','" + datofincorp.Value.ToString("MM/dd/yyyy") + "','" + ownname1.Text + "','" + ownname2.Text + "'," +
                                        "'" + ownmob1.Text + "','" + ownmob2.Text + "','" + panno.Text + "','" + juris.Text + "','" + txtShortCode.Text + "','" + username + "','"+System.DateTime.Now.ToString("MM/dd/yyyy")+"',@ImageData," +
                                        "'" + cin_no.Text + "','No')";

                                    SqlCommand SqlCom = new SqlCommand(save, con);
                                    byte[] imageData = ReadFile(image);
                                    SqlCom.Parameters.Add(new SqlParameter("@ImageData", (object)imageData));
                                    SqlCom.ExecuteNonQuery();
                                }

                                dr.Close();
                                string str = "select [Company_ID] from [Company_Master] where [Company_Name] = '"+compname.Text+"'";
                                dr = objCon.getData(str);
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    { 
                                        compid = dr[0].ToString();
                                    }
                                }
                                dr.Close();

                                string save2 = "INSERT INTO [Bank_details]([comp_id],[bank_name],[bank_branch],[account_name],[account_no],[IFSC_code],[MICR_code],[Enter_by],[Enter_date])VALUES('"+compid+"','"+txt_bankname.Text+"','"+txtbranch.Text+"','"+txt_ac_name.Text+"','"+txt_ac_no.Text+"','"+txtifsc_code.Text+"','"+txtMicr_code.Text+"','"+username+"','"+System.DateTime.Now.ToString("MM/dd/yyyy")+"')";
                                affect = objCon.affect(save2);

                                string save3 = "INSERT INTO [parameter_detail]([salebillgenerate],[bill_format],[company],[Enter_by],[Enter_date])VALUES('" + status + "','" + cmbbillformat.Text + "','" + company + "','" + username + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "')";
                                affect = objCon.affect(save3);

                                MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else 
                            {
                                if (assestype == "Yes")
                                {

                                    save = "INSERT INTO [Company_Master]([Company_Name],[Comp_display_name],[Company_Address1],[Company_Address2],[Company_Address3],[Bussiness_Line]," +
                                    "[Pincode],[City],[State],[Country],[Phone_No],[FaxNo],[E_MailID],[DateOfIncorp],[OwnerName1],[OwnerName2],[MobileNo1],[MobileNo2],[PANo]," +
                                    "[Jurisdication],[Company_sht_code],[Enter_by],[Enter_date],[GST_No],[CINno],[Assese_type])VALUES('" + compname.Text + "'," +
                                    "'" + compname.Text + "','" + add1.Text + "','" + add2.Text + "','" + add3.Text + "','" + busline.Text + "','" + pincode.Text + "','" + city.Text + "','" + txtState.Text + "'," +
                                    "'" + country.Text + "','" + phoneno.Text + "','" + faxno.Text + "','" + email.Text + "','" + datofincorp.Text + "','" + ownname1.Text + "','" + ownname2.Text + "'," +
                                    "'" + ownmob1.Text + "','" + ownmob2.Text + "','" + panno.Text + "','" + juris.Text + "','" + txtShortCode.Text + "','" + username + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "'," + gstno + "'," +
                                    "'" + cin_no.Text + "','Yes')";
                                    affect = objCon.affect(save);
                                }
                                else 
                                {
                                    save = "INSERT INTO [Company_Master]([Company_Name],[Comp_display_name],[Company_Address1],[Company_Address2],[Company_Address3],[Bussiness_Line]," +
                                    "[Pincode],[City],[State],[Country],[Phone_No],[FaxNo],[E_MailID],[DateOfIncorp],[OwnerName1],[OwnerName2],[MobileNo1],[MobileNo2],[PANo]," +
                                    "[Jurisdication],[Company_sht_code],[Enter_by],[Enter_date],[CINno],[Assese_type])VALUES('" + compname.Text + "'," +
                                    "'" + compname.Text + "','" + add1.Text + "','" + add2.Text + "','" + add3.Text + "','" + busline.Text + "','" + pincode.Text + "','" + city.Text + "','" + txtState.Text + "'," +
                                    "'" + country.Text + "','" + phoneno.Text + "','" + faxno.Text + "','" + email.Text + "','" + datofincorp.Text + "','" + ownname1.Text + "','" + ownname2.Text + "'," +
                                    "'" + ownmob1.Text + "','" + ownmob2.Text + "','" + panno.Text + "','" + juris.Text + "','" + txtShortCode.Text + "','" + username + "','" + System.DateTime.Now.ToString("MM/dd/yyyy") + "'," +
                                    "'" + cin_no.Text + "','No')";
                                    affect = objCon.affect(save);
                                }

                                dr.Close();
                                string str = "select [Company_ID] from [Company_Master] where [Company_Name] = '" + compname.Text + "'";
                                dr = objCon.getData(str);
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        compid = dr[0].ToString();
                                    }
                                }
                                dr.Close();

                                string save2 = "INSERT INTO [Bank_details]([comp_id],[bank_name],[bank_branch],[account_name],[account_no],[IFSC_code],[MICR_code])VALUES('" + compid + "','" + txt_bankname.Text + "','" + txtbranch.Text + "','" + txt_ac_name.Text + "','" + txt_ac_no.Text + "','" + txtifsc_code.Text + "','" + txtMicr_code.Text + "')";
                                affect = objCon.affect(save2);

                                MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            compname.Text = "";
                            add1.Text = "";
                            add2.Text = "";
                            add3.Text = "";
                            datofincorp.Text = System.DateTime.Now.ToString("dd/MM/yyyy"); 
                            busline.Text = "";
                            ownname1.Text = "";
                            ownname2.Text = "";
                            ownmob1.Text = "";
                            ownmob2.Text = "";
                            pincode.Text = "";
                            city.Text = "";
                            //comboBox1.SelectedIndex = -1;
                            txtState.Text = "";
                            country.Text = "";
                            phoneno.Text = "";
                            faxno.Text = "";
                            email.Text = "";
                            panno.Text = "";
                            txt_bankname.Text = "";
                            txt_ac_no.Text = "";
                            txt_ac_name.Text = "";
                            txtbranch.Text = "";
                            cin_no.Text = "";
                            txtgst_no.Text = "";
                            juris.Text = "";
                            pictureBox1.Image = null;
                            txtShortCode.Text = "";
                            textBox1.Text = "";
                            txtifsc_code.Text = "";
                            txtMicr_code.Text = "";
                            //MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                
                        catch (Exception ex)
                        {
                            MessageBox.Show("The following error occured:\n" + ex.Message.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        con.Close();        
                     }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        bool chkimg = false;
        byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { }
            catch
            { }
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void setax_TextChanged(object sender, EventArgs e)
        {

        }

        private void vatno_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void txtState_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtState_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtState_Leave(object sender, EventArgs e)
        {
            try
            {
                statevalue = txtState.Text;
                string str4 = "select distinct [State_Code] from [State_Code_Master] where [code] like '" + statevalue + "%' or [sate_name] like '" + statevalue + "%'";
                dr = objCon.getData(str4);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtState.Text = dr[0].ToString();
                    }
                }
                dr.Close();

            }
            catch
            { }
        }

        private void txtgst_no_TabStopChanged(object sender, EventArgs e)
        {
            

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8 || e.KeyChar >= 65 && e.KeyChar <= 90) //The  character represents a backspace
            //{
            //    e.Handled = false; //Do not reject the input
            //}
            //else
            //{
            //    e.Handled = true; //Reject the input
            //}
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;

            if (textBox1.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"^\d{1}Z[0-9A-Z]{1}$");

                if (!mRegxExpression.IsMatch(textBox1.Text.Trim()))
                {

                    MessageBox.Show("Invaid GST No");

                    textBox1.Focus();
                    textBox1.Text = "";

                }

            }
            //if (System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), @"^\d{1}Z[0-9A-Z]{1}$"))
            //{
            //    e.Handled = true;

            //}
            //else
            //{
            //    e.Handled = false;
            //}
        }

        private void panno_Leave(object sender, EventArgs e)
        {
            if (txtState.Text != "")
            {
                string statecode = txtState.Text.Substring(0, 2).ToString();
                string pan = panno.Text;
                string gst_no = statecode + pan;
                txtgst_no.Text = gst_no;
               
            }
            else
            {
                MessageBox.Show("Enter State");
            }

            Regex mRegxExpression;

            if (panno.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"^[A-Z]{5}\d{4}[A-Z]{1}$");

                if (!mRegxExpression.IsMatch(panno.Text.Trim()))
                {

                    MessageBox.Show("Invaid PAN No");

                    panno.Focus();
                    panno.Text = "";
                    txtgst_no.Text = "";

                }

            }
        }

        private void rbtnAssesse_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAssesse.Checked == true)
            {
                rbtnnonassesse.Checked = false;
                lblgst.Visible = true;
                txtgst_no.Visible = true;
                textBox1.Visible = true;
                assestype = "Yes";
            }
            else
            {
                rbtnnonassesse.Checked = true;
                lblgst.Visible = false;
                txtgst_no.Visible = false;
                textBox1.Visible = false;
                assestype = "No";
            }
        }

        private void rbtnnonassesse_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnnonassesse.Checked == true)
            {
                rbtnAssesse.Checked = false;
                lblgst.Visible = false;
                txtgst_no.Visible = false;
                textBox1.Visible = false;
                assestype = "No";
            }
            else
            {
                rbtnAssesse.Checked = true;
                lblgst.Visible = true;
                txtgst_no.Visible = true;
                textBox1.Visible = true;
                assestype = "Yes";
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult dlgRes = dlg.ShowDialog();
            if (dlgRes != DialogResult.Cancel)
            {
                pictureBox1.ImageLocation = dlg.FileName;
                image = dlg.FileName;
                chkimg = true;
            }
        }

        private void btnremove_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            chkimg = false;
        }

        private void juris_Leave(object sender, EventArgs e)
        {
            city.Text = juris.Text;
            dr.Close();
            string commandT = "select distinct [pincode] FROM [pincode_city_master] where [city_name] = '"+juris.Text+"'";
            dr = objCon.getData(commandT);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    pincode1.Add(dr[0].ToString());
                }
            }
            dr.Close();
            pincode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            pincode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            pincode.AutoCompleteCustomSource = pincode1;

            string commandT2 = "select distinct s.[State_Code] FROM [pincode_city_master] p left join [State_Code_Master] s on lower(p.[state_name]) = s.sate_name where p.[city_name] = '"+juris.Text+"'";
            dr = objCon.getData(commandT2);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtState.Text = dr[0].ToString();
                }
            }
            dr.Close();
           

        }

        private void panno_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void cin_no_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;

            if (cin_no.Text.Trim() != string.Empty)
            {
                
                mRegxExpression = new Regex(@"^[A-Z]{1}\d{5}[A-Z]{2}\d{4}[A-Z]{3}\d{6}$");

                if (!mRegxExpression.IsMatch(cin_no.Text.Trim()))
                {

                    MessageBox.Show("Invaid CIN No");
                    cin_no.Focus();
                }
            }
        }

        private void email_Leave(object sender, EventArgs e)
        {
            Regex mRegxExpression;

            if (email.Text.Trim() != string.Empty)
            {

                mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!mRegxExpression.IsMatch(email.Text.Trim()))
                {

                    MessageBox.Show("E-mail address format is not correct.");

                    email.Focus();

                }

            }
        }

        private void city_Leave(object sender, EventArgs e)
        {
            juris.Text = city.Text;
            dr.Close();
            string commandT = "select distinct [pincode] FROM [pincode_city_master] where [city_name] = '" + city.Text + "'";
            dr = objCon.getData(commandT);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    pincode1.Add(dr[0].ToString());
                }
            }
            dr.Close();
            pincode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            pincode.AutoCompleteSource = AutoCompleteSource.CustomSource;
            pincode.AutoCompleteCustomSource = pincode1;

            string commandT2 = "select distinct s.[State_Code] FROM [pincode_city_master] p left join [State_Code_Master] s on lower(p.[state_name]) = s.sate_name where p.[city_name] = '" + city.Text + "'";
            dr = objCon.getData(commandT2);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    txtState.Text = dr[0].ToString();
                }
            }
            dr.Close();
        }

        private void rbtnauto_CheckedChanged(object sender, EventArgs e)
        {
            rbtnmanual.Checked = false;
            status = "Auto";
        }

        private void rbtnmanual_CheckedChanged(object sender, EventArgs e)
        {
            rbtnauto.Checked = false;
            status = "Manual";
        }

        private void compname_KeyUp(object sender, KeyEventArgs e)
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

        private void busline_KeyUp(object sender, KeyEventArgs e)
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

        private void datofincorp_KeyUp(object sender, KeyEventArgs e)
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

        private void juris_KeyUp(object sender, KeyEventArgs e)
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

        private void ownname1_KeyUp(object sender, KeyEventArgs e)
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

        private void ownmob1_KeyUp(object sender, KeyEventArgs e)
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

        private void ownname2_KeyUp(object sender, KeyEventArgs e)
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

        private void ownmob2_KeyUp(object sender, KeyEventArgs e)
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

        private void cin_no_KeyUp(object sender, KeyEventArgs e)
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

        private void txtShortCode_KeyUp(object sender, KeyEventArgs e)
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

        private void add1_KeyUp(object sender, KeyEventArgs e)
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

        private void add2_KeyUp(object sender, KeyEventArgs e)
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

        private void add3_KeyUp(object sender, KeyEventArgs e)
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

        private void city_KeyUp(object sender, KeyEventArgs e)
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

        private void pincode_KeyUp(object sender, KeyEventArgs e)
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

        private void country_KeyUp(object sender, KeyEventArgs e)
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

        private void txt_bankname_KeyUp(object sender, KeyEventArgs e)
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

        private void txt_ac_no_KeyUp(object sender, KeyEventArgs e)
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

        private void txt_ac_name_KeyUp(object sender, KeyEventArgs e)
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

        private void txtbranch_KeyUp(object sender, KeyEventArgs e)
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

        private void txtifsc_code_KeyUp(object sender, KeyEventArgs e)
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

        private void txtMicr_code_KeyUp(object sender, KeyEventArgs e)
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

        private void rbtnAssesse_KeyUp(object sender, KeyEventArgs e)
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

        private void rbtnnonassesse_KeyUp(object sender, KeyEventArgs e)
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

        private void txtState_KeyUp(object sender, KeyEventArgs e)
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

        private void panno_KeyUp(object sender, KeyEventArgs e)
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

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
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

        private void phoneno_KeyUp(object sender, KeyEventArgs e)
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

        private void faxno_KeyUp(object sender, KeyEventArgs e)
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

        private void email_KeyUp(object sender, KeyEventArgs e)
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

        private void rbtnauto_KeyUp(object sender, KeyEventArgs e)
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

        private void rbtnmanual_KeyUp(object sender, KeyEventArgs e)
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

        private void cmbbillformat_KeyUp(object sender, KeyEventArgs e)
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

        private void ownmob1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void phoneno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void ownmob2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) == true || e.KeyChar == 8 || e.KeyChar == 13)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        
    }
}
