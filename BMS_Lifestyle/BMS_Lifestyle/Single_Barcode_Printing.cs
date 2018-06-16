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
using System.Threading;

namespace BMS_Lifestyle
{
    public partial class Single_Barcode_Printing : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        ConnectDb ObjCon = new ConnectDb();
        SqlDataReader dr = null;
        string username;
        string commandT, save;
        int affect;
        string flag1, flag2;
        string company;
        string purchasePK, ItemCode, companyname;
        
        AutoCompleteStringCollection barcode1 = new AutoCompleteStringCollection();

        public Single_Barcode_Printing(string username, string company)
        {
            InitializeComponent();
            this.username = username;
            this.company = company;
        }

        private void Single_Barcode_Printing_Load(object sender, EventArgs e)
        {
            try
            {
                string str7 = "select [EAC_Code] from [Kit_Master] union select [EAC_Code] from [Item]";
                dr = ObjCon.getData(str7);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        barcode1.Add(dr[0].ToString());
                    }
                }
                dr.Close();
                txtEac_code.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtEac_code.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtEac_code.AutoCompleteCustomSource = barcode1;
            }
            catch { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string cmp1 = null;

                try
                {
                    //Print Barcode------------------------------------------------------------------
                    cmp1 = "I8,A,001" + System.Environment.NewLine +
                            "" + System.Environment.NewLine +
                            "" + System.Environment.NewLine +
                            "" + System.Environment.NewLine +
                            "Q120,024" + System.Environment.NewLine +
                            "q831" + System.Environment.NewLine +
                            "rN" + System.Environment.NewLine +
                            "S5" + System.Environment.NewLine +
                            "D10" + System.Environment.NewLine +
                            "ZT" + System.Environment.NewLine +
                            "JF" + System.Environment.NewLine +
                            "O" + System.Environment.NewLine +
                            "R263,0" + System.Environment.NewLine +
                            "f100" + System.Environment.NewLine +
                            "N" + System.Environment.NewLine +

                            "A252,79,2,2,1,1,N,\"Weight:\"" + System.Environment.NewLine +
                            "A252,100,2,2,1,1,N,\"" + txtName.Text + "\"" + System.Environment.NewLine +
                            "A165,79,2,2,1,1,N,\"" + txtgroup.Text + "\"" + System.Environment.NewLine +
                            "B252,60,2,1,2,6,30,B,\"" + txtEac_code.Text + "\"" + System.Environment.NewLine +
                            "P" + 1 + System.Environment.NewLine;
                }
                catch
                { }
                string directory = Directory.GetCurrentDirectory();
                FileStream fs2 = new FileStream(directory + "\\Dtm.prn", FileMode.Create, FileAccess.ReadWrite);

                StreamWriter sw2 = new StreamWriter(fs2);

                sw2.Write(cmp1);

                sw2.Flush();
                sw2.Close();
                fs2.Close();

                System.Diagnostics.Process.Start(directory + "\\dms.bat");
                Thread.Sleep(500);

                MessageBox.Show("Barcode Print Successfully");

                txtEac_code.Text = "";
                txtName.Text = "";
                txtS_Name.Text = "";
                txtgroup.Text = "";
            }
            catch { }
        }

        private void txtEac_code_Leave(object sender, EventArgs e)
        {
            if (txtEac_code.Text != "")
            {
                try
                {
                    string str1 = "select isnull([EAC_Code],'0') as EAC_Code FROM [Item] where [EAC_Code] = '" + txtEac_code.Text.TrimEnd().TrimStart() + "'";
                    dr = ObjCon.getData(str1);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            flag1 = dr[0].ToString();
                        }
                    }
                    dr.Close();

                    string str3 = "select isnull([EAC_Code],'0') as EAC_Code FROM [Kit_Master] where [EAC_Code] = '" + txtEac_code.Text.TrimEnd().TrimStart() + "'";
                    dr = ObjCon.getData(str3);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            flag2 = dr[0].ToString();
                        }
                    }
                    dr.Close();

                    if (flag2 != "0" || flag2 == null)
                    {
                        
                        dr.Close();
                        string str4 = "select [Kit_Name],[kit_Group] FROM [Kit_Master] where [EAC_Code] = '" + txtEac_code.Text.TrimEnd().TrimStart() + "'";
                        dr = ObjCon.getData(str4);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtgroup.Text = dr["kit_Group"].ToString();
                                txtName.Text = dr["Kit_Name"].ToString();
                                lblPname.Text = "Kit Name";
                                lblSname.Visible = false;
                                txtS_Name.Visible = false;
                            }
                        }
                        dr.Close();
                        
                    }
                    else
                    {
                        lblPname.Text = "P Name";
                        lblSname.Visible = true;
                        txtS_Name.Visible = true;
                    }

                    if (flag1 != "0" || flag1 == null || flag1 == "")
                    {
                        
                        dr.Close();
                        string str2 = "select [P_Name],[S_Name],[Item_Group] FROM [Item] where [EAC_Code] = '" + txtEac_code.Text.TrimEnd().TrimStart() + "'";
                        dr = ObjCon.getData(str2);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                txtgroup.Text = dr["Item_Group"].ToString();
                                txtName.Text = dr["P_Name"].ToString();
                                txtS_Name.Text = dr["S_Name"].ToString();
                                lblPname.Text = "P Name";
                                lblSname.Visible = true;
                                txtS_Name.Visible = true;
                            }
                        }
                        dr.Close();
                        
                    }
                    else
                    {
                        lblPname.Text = "Kit Name";
                        lblSname.Visible = false;
                        txtS_Name.Visible = false;
                    }
                }
                catch { }
            }
        }

        private void txtEac_code_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
