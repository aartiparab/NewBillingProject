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

namespace BMS_Lifestyle
{
    public partial class Company_Edit : Form
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
        AutoCompleteStringCollection CompanyName = new AutoCompleteStringCollection();
       
        public string Id1;

        public Company_Edit(string username, string company)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
        }

        private void Company_Edit_Load(object sender, EventArgs e)
        {
            try
            {
                string str = "select Company_Name from Company_Master";
                dr = objCon.getData(str);
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        CompanyName.Add(dr[0].ToString());
                    }
                }
                else
                { }
                dr.Close();
                compname.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                compname.AutoCompleteSource = AutoCompleteSource.CustomSource;
                compname.AutoCompleteCustomSource = CompanyName;

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
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                string ans = (MessageBox.Show("Do you want to Continue Save", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Information)).ToString();

                if (ans == "Yes")
                {
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
                                    dr.Close();
                                    save = "update [Company_Master] set [Comp_display_name] = '" + compname.Text + "' , [Company_Address1] = '" + add1.Text + "' , [Company_Address2]='" + add2.Text + "' , " +
                                    "[Company_Address3] = '" + add3.Text + "' , [Bussiness_Line]='" + busline.Text + "' , [Pincode]= '" + pincode.Text + "' , [City] = '" + city.Text + "' , [State] ='" + txtState.Text + "' , " +
                                    "[Country]='" + country.Text + "' , [Phone_No]='" + phoneno.Text + "' , [FaxNo]='" + faxno.Text + "', [E_MailID]='" + email.Text + "' , [DateOfIncorp]='" + datofincorp.Value.ToString("MM/dd/yyyy") + "' , " +
                                    "[OwnerName1]='" + ownname1.Text + "' , [OwnerName2]='" + ownname2.Text + "' , [MobileNo1]='" + ownmob1.Text + "' ,[MobileNo2]='" + ownmob2.Text + "' ,[PANo]='" + panno.Text + "' , " +
                                    "[Jurisdication]='" + juris.Text + "' , [Company_sht_code]='" + txtShortCode.Text + "' , [LogoImage]=@ImageData , [GST_No]='" + gstno + "' , [CINno]='" + cin_no.Text + "' ," +
                                    "[Assese_type]='Yes',[Modify_by] = '"+username+"',[Modify_Date] = '"+System.DateTime.Now.ToString("MM/dd/yyyy")+"' where [Company_Name] = '" + compname.Text + "'";

                                    SqlCommand SqlCom = new SqlCommand(save, con);
                                    byte[] imageData = ReadFile(image);
                                    SqlCom.Parameters.Add(new SqlParameter("@ImageData", (object)imageData));
                                    SqlCom.ExecuteNonQuery();
                                }
                                else
                                {
                                    dr.Close();
                                    save = "update [Company_Master] set [Comp_display_name] = '" + compname.Text + "' , [Company_Address1] = '" + add1.Text + "' , [Company_Address2]='" + add2.Text + "' , " +
                                    "[Company_Address3] = '" + add3.Text + "' , [Bussiness_Line]='" + busline.Text + "' , [Pincode]= '" + pincode.Text + "' , [City] = '" + city.Text + "' , [State] ='" + txtState.Text + "' , " +
                                    "[Country]='" + country.Text + "' , [Phone_No]='" + phoneno.Text + "' , [FaxNo]='" + faxno.Text + "' , [E_MailID]='" + email.Text + "' , [DateOfIncorp]='" + datofincorp.Text + "' , " +
                                    "[OwnerName1]='" + ownname1.Text + "' ,[OwnerName2]='" + ownname2.Text + "' , [MobileNo1]='" + ownmob1.Text + "' , [MobileNo2]='" + ownmob2.Text + "' , [PANo]='" + panno.Text + "' , " +
                                    "[Jurisdication]='" + juris.Text + "' , [Company_sht_code]='" + txtShortCode.Text + "' , [LogoImage]=@ImageData , [CINno]='" + cin_no.Text + "' , [Assese_type]='No' "+
                                    ",[Modify_by] = '" + username + "',[Modify_Date] = '" + System.DateTime.Now.ToString("MM/dd/yyyy") + "' where [Company_Name] = '" + compname.Text + "'";

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

                                string save2 = "update [Bank_details] set [bank_name]='" + txt_bankname.Text + "' , [bank_branch]='" + txtbranch.Text + "',[account_name]='" + txt_ac_name.Text + "' , [account_no]='" + txt_ac_no.Text + "' ,"+
                                "[IFSC_code]='" + txtifsc_code.Text + "' , [MICR_code]='" + txtMicr_code.Text + "',[Modify_by] = '" + username + "',[Modify_Date] ='" + System.DateTime.Now.ToString("MM/dd/yyyy") + "' where [comp_id] = '" + compid + "'";
                                affect = objCon.affect(save2);

                               
                            }
                            else 
                            {
                                if (assestype == "Yes")
                                {
                                    dr.Close();
                                    save = "update [Company_Master] set [Comp_display_name] = '" + compname.Text + "' ,[Company_Address1] = '" + add1.Text + "' , [Company_Address2]='" + add2.Text + "' , " +
                                   "[Company_Address3] = '" + add3.Text + "' ,[Bussiness_Line]='" + busline.Text + "' , [Pincode]= '" + pincode.Text + "' , [City] = '" + city.Text + "' ,[State] ='" + txtState.Text + "' , " +
                                   "[Country]='" + country.Text + "' , [Phone_No]='" + phoneno.Text + "',[FaxNo]='" + faxno.Text + "' , [E_MailID]='" + email.Text + "' , [DateOfIncorp]='" + datofincorp.Text + "' , " +
                                   "[OwnerName1]='" + ownname1.Text + "' , [OwnerName2]='" + ownname2.Text + "' ,[MobileNo1]='" + ownmob1.Text + "' , [MobileNo2]='" + ownmob2.Text + "' , [PANo]='" + panno.Text + "' , " +
                                   "[Jurisdication]='" + juris.Text + "' ,[Company_sht_code]='" + txtShortCode.Text + "',[GST_No]='" + gstno + "' , [CINno]='" + cin_no.Text + "' ,[LogoImage] = null, " +
                                   "[Assese_type]='Yes',[Modify_by] = '" + username + "',[Modify_Date] = '" + System.DateTime.Now.ToString("MM/dd/yyyy") + "' where [Company_Name] = '" + compname.Text + "'";
                                    affect = objCon.affect(save);
                                }
                                else 
                                {
                                    dr.Close();
                                    save = "update [Company_Master] set [Comp_display_name] = '" + compname.Text + "' , [Company_Address1] = '" + add1.Text + "' ,[Company_Address2]='" + add2.Text + "' , " +
                                   "[Company_Address3] = '" + add3.Text + "' , [Bussiness_Line]='" + busline.Text + "' , [Pincode]= '" + pincode.Text + "' , [City] = '" + city.Text + "' , [State] ='" + txtState.Text + "' , " +
                                   "[Country]='" + country.Text + "' ,[Phone_No]='" + phoneno.Text + "' , [FaxNo]='" + faxno.Text + "' ,[E_MailID]='" + email.Text + "' , [DateOfIncorp]='" + datofincorp.Text + "' , " +
                                   "[OwnerName1]='" + ownname1.Text + "' , [OwnerName2]='" + ownname2.Text + "' , [MobileNo1]='" + ownmob1.Text + "' , [MobileNo2]='" + ownmob2.Text + "' , [PANo]='" + panno.Text + "' , " +
                                   "[Jurisdication]='" + juris.Text + "' , [Company_sht_code]='" + txtShortCode.Text + "' , [CINno]='" + cin_no.Text + "' ,[LogoImage] = null,[Assese_type]='No' "+
                                   ",[Modify_by] = '" + username + "',[Modify_Date] = '" + System.DateTime.Now.ToString("MM/dd/yyyy") + "' where [Company_Name] = '" + compname.Text + "'";
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

                                string save2 = "update [Bank_details] set [bank_name]='" + txt_bankname.Text + "' , [bank_branch]='" + txtbranch.Text + "',[account_name]='" + txt_ac_name.Text + "' , [account_no]='" + txt_ac_no.Text + "' , "+
                                "[IFSC_code]='" + txtifsc_code.Text + "' , [MICR_code]='" + txtMicr_code.Text + "',[Modify_by] = '" + username + "',[Modify_Date] ='" + System.DateTime.Now.ToString("MM/dd/yyyy") + "' where [comp_id] = '" + compid + "'";
                                affect = objCon.affect(save2);

                                
                            }
                            MessageBox.Show("Saved Successfully...", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            txtifsc_code.Text = "";
                            txtMicr_code.Text = "";
                            textBox1.Text = "";
                    }
                    catch
                    {
                    }
                    
                }
             }
   

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
            pictureBox1.Image = null;
            chkimg = false;
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void juris_Leave(object sender, EventArgs e)
        {
            city.Text = juris.Text;
            dr.Close();
            string commandT = "select distinct [pincode] FROM [pincode_city_master] where [city_name] = '" + juris.Text + "'";
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

            string commandT2 = "select distinct s.[State_Code] FROM [pincode_city_master] p left join [State_Code_Master] s on lower(p.[state_name]) = s.sate_name where p.[city_name] = '" + juris.Text + "'";
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

        private void compname_TextChanged(object sender, EventArgs e)
        {
            try 
            {
                dr.Close();
                pictureBox1.Image = null;
                string strfill2 = "SELECT cm.[Company_Address1],cm.[Company_Address2],cm.[Company_Address3],cm.[Bussiness_Line],cm.[Pincode],cm.[City],"+
                                "cm.[State],cm.[Country],cm.[Phone_No],cm.[FaxNo],cm.[E_MailID],cm.[DateOfIncorp],cm.[OwnerName1],cm.[OwnerName2],"+
                                "cm.[MobileNo1],cm.[MobileNo2],cm.[PANo],cm.[Jurisdication],cm.[Company_sht_code],cm.[GST_No],cm.[CINno],cm.[Assese_type],"+
                                "b.[bank_name],b.[bank_branch],b.[account_name],b.[account_no],b.[IFSC_code],b.[MICR_code],[LogoImage]"+
                                "FROM [Company_Master] cm left join [Bank_details] b on cm.[Company_ID]= b.[comp_id]  where [Company_Name] = '"+compname.Text+"' ";
                dr = objCon.getData(strfill2);
                if (dr.HasRows)
                {
                    while(dr.Read())
                    {
                        add1.Text = dr["Company_Address1"].ToString();
                        add2.Text = dr["Company_Address2"].ToString();
                        add3.Text = dr["Company_Address3"].ToString();
                        busline.Text = dr["Bussiness_Line"].ToString();
                        pincode.Text = dr["Pincode"].ToString();
                        city.Text = dr["City"].ToString();
                        txtState.Text = dr["State"].ToString();
                        country.Text = dr["Country"].ToString();
                        phoneno.Text = dr["Phone_No"].ToString();
                        faxno.Text = dr["FaxNo"].ToString();
                        email.Text = dr["E_MailID"].ToString();
                        datofincorp.Text = dr["DateOfIncorp"].ToString();
                        ownname1.Text = dr["OwnerName1"].ToString();
                        ownname2.Text = dr["OwnerName2"].ToString();
                        ownmob1.Text = dr["MobileNo1"].ToString();
                        ownmob2.Text = dr["MobileNo2"].ToString();
                        panno.Text = dr["PANo"].ToString();
                        juris.Text = dr["Jurisdication"].ToString();
                        txtShortCode.Text = dr["Company_sht_code"].ToString();
                        cin_no.Text = dr["CINno"].ToString();
                        txt_bankname.Text = dr["bank_name"].ToString();
                        txtbranch.Text = dr["bank_branch"].ToString();
                        txt_ac_name.Text = dr["account_name"].ToString();
                        txt_ac_no.Text = dr["account_no"].ToString();
                        txtifsc_code.Text = dr["IFSC_code"].ToString();
                        txtMicr_code.Text = dr["MICR_code"].ToString();
                        assestype = dr["Assese_type"].ToString();
                        if (assestype == "Yes")
                        {
                            rbtnAssesse.Checked = true;
                            rbtnnonassesse.Checked = false;
                        }
                        else
                        {
                            rbtnnonassesse.Checked = true;
                            rbtnAssesse.Checked = false;
                        }

                        string gst = dr["GST_No"].ToString().Trim();
                        
                         if (gst == "" || gst == null)
                        {
                            txtgst_no.Text = "";
                            textBox1.Text = "";
                        }
                         else if (gst != "" || gst != null)
                        {
                            txtgst_no.Text = gst.Substring(0, 12).Trim();
                            textBox1.Text = gst.Substring(12, 3).Trim();
                        }
                        if (dr["LogoImage"] != DBNull.Value)
                        {
                            byte[] imageData = (byte[])dr["LogoImage"];
                            //Initialize image variable
                            Image newImage;
                            //Read image data into a memory stream
                            using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                            {
                                ms.Write(imageData, 0, imageData.Length);

                                //Set image variable value using memory stream.
                                newImage = Image.FromStream(ms, false);
                                pictureBox1.Image = newImage;
                                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                        }
                        else
                        {
                        }
                    }
                }
                dr.Close();
            }
            catch
            { }
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
    }
}
