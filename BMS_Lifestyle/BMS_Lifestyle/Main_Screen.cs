using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;

namespace BMS_Lifestyle
{
    public partial class Main_Screen : Form
    {

        string username, right;
        int role_Id;
        string company;
        SqlDataReader dr = null;
        ConnectDb objCon = new ConnectDb();
        string commandT;
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = null;
        string connectionString;
        public Main_Screen(string username, int role_Id, string company, string connectionString)
        {
            InitializeComponent();
             this.Text = "Main Screen * Welcome " + username;
            this.username = username;
            this.role_Id = role_Id;
            this.company = company;
            this.connectionString = connectionString;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void logOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
             this.Close();
             System.Diagnostics.Process.GetCurrentProcess().Kill();
             //Login_screen obj = new Login_screen();
             //obj.Show();

        }

        private void Main_Screen_Load(object sender, EventArgs e)
        {
            commandT = "select Company_Name from Company_Master where company_Id = '" + company + "'";
            dr = objCon.getData(commandT);

            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    try
                    {
                        lblcname.Text = dr[0].ToString();
                        
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            dr.Close();
            toolStripStatusLabel.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void taxMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (role_Id == 3 || role_Id ==  1)
                {
                    Tax_Master obj = new Tax_Master(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    taxMasterToolStripMenuItem.Visible = false;
                }
            }
            catch { }
        }

        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (role_Id == 2 || role_Id == 1)
                {
                    TaxMasterEdit obj = new TaxMasterEdit(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem.Visible = false;
                }
            }
            catch { }        
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if(role_Id == 3 || role_Id == 1)
                {
                    Customer obj = new Customer(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }
                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    addToolStripMenuItem1.Visible = false;
                }
            }
            catch { }
        }

        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 3 || role_Id == 1)
                {
                    Supplier obj = new Supplier(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    addToolStripMenuItem2.Visible = false;
                }
            }
            catch { }
        }

        private void modifyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (role_Id == 2 || role_Id == 1)
                {
                    CustomerEdit obj = new CustomerEdit(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem1.Visible = false;
                }
            }
            catch { }
            
        }

        private void modifyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 2 || role_Id == 1)
                {
                    SupplierEdit obj = new SupplierEdit(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem2.Visible = false;
                }
            }
            catch { }
            
        }

        private void purchaseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void purchaseEntryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (role_Id == 3 || role_Id == 1)
            //    {
            //        Purchase obj = new Purchase(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
            //        purchaseEntryToolStripMenuItem1.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void salesEntryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                if (role_Id == 3 || role_Id == 1)
                {
                    OutwardEntry obj = new OutwardEntry(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }
                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    salesEntryToolStripMenuItem1.Visible = false;
                }
            }
            catch { }
            
        }

        private void addToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 3 || role_Id == 1)
                {
                    Item_Master obj = new Item_Master(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }
                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    addToolStripMenuItem3.Visible = false;
                }
            }
            catch { }
           
        }

        private void modifyToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (role_Id == 2 || role_Id == 1)
                {
                    ItemMaster_Edit obj = new ItemMaster_Edit(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem3.Visible = false;
                }
            }
            catch { }
            
        }

        private void addToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (role_Id == 3 || role_Id == 1)
                {
                    Item_GroupMaster obj = new Item_GroupMaster(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    addToolStripMenuItem4.Visible = false;
                }
            }
            catch { } 
        }

        private void addToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {

                if (role_Id == 3 || role_Id == 1)
                {
                    Unit_master obj = new Unit_master(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    addToolStripMenuItem5.Visible = false;
                }
            }
            catch { } 
        }

        private void modifyToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (role_Id ==2 || role_Id ==1)
                {
                    ItemGroup_modify obj = new ItemGroup_modify(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem4.Visible = false;
                }
            }
            catch { }
            
        }

        private void modifyToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (role_Id ==2 || role_Id ==1)
                {
                    Unit_Modify obj = new Unit_Modify(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem5.Visible = false;
                }
            }
            catch { }
            
        }

        private void addToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (role_Id == 3 || role_Id == 1)
                {
                    MarketPlace_Master obj = new MarketPlace_Master(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!!");
                    addToolStripMenuItem6.Visible = false;
                }
            }
            catch { }
            
        }

        private void modifyToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (role_Id == 2 || role_Id == 1)
                {
                    MarketPlace_Edit obj = new MarketPlace_Edit(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem6.Visible = false;
                }
            }
            catch { }
             
        }

        private void addToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (role_Id == 3 ||role_Id ==1)
                {
                    Company_master obj = new Company_master(username, company);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    addToolStripMenuItem7.Visible = false;
                }
            }
            catch { } 
        }

        private void modifyToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (role_Id == 2 ||role_Id == 1)
                {
                    Company_Edit obj = new Company_Edit(username, company);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem7.Visible = false;
                }
            }
            catch { }
            
        }

        private void purchaseEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
                
            //    if (role_Id == 2 || role_Id == 1)
            //    {
            //        Purchase_Edit obj = new Purchase_Edit(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        purchaseEditToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void salesEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
                
            //    if (role_Id ==2 ||role_Id == 1)
            //    {
            //        Sales_Edit obj = new Sales_Edit(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        salesEditToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void purchaseReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //try
            //{
                
            //    if (role_Id == 2 || role_Id == 3 || role_Id == 1)
            //    {
            //        Purchase_ReportScreen obj = new Purchase_ReportScreen(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        purchaseReportToolStripMenuItem1.Visible = false;
            //    }
            //}
            //catch { }
            
            
        }

        private void billPrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (role_Id == 2 || role_Id == 3 || role_Id == 1)
                {
                    Sale_Bill_PrintingScreen obj = new Sale_Bill_PrintingScreen(username, company);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    billPrintToolStripMenuItem.Visible = false;
                }
            }
            catch { }
            
        }

        private void purchaseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saleReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 2 || role_Id == 3 || role_Id == 1)
            //    {
            //        Sale_report_screen obj = new Sale_report_screen(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        saleReportToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void purchaseReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (role_Id == 2 || role_Id == 3 || role_Id == 1)
                {
                    Purchase_ReturrnScreen obj = new Purchase_ReturrnScreen(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    purchaseReturnToolStripMenuItem.Visible = false;
                }
            }
            catch { } 
        }

        private void saleReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (role_Id == 3 || role_Id == 1)
                {
                    Sales_Return obj = new Sales_Return(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    salesEntryToolStripMenuItem1.Visible = false;
                }
            }
            catch { }
            
        }

        private void barcodeWiseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void addToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            try
            {

                if (role_Id == 3 || role_Id == 1)
                {
                    UserMaster_Screen obj = new UserMaster_Screen(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    addToolStripMenuItem8.Visible = false;
                }
            }
            catch { }
            
            
        }

        private void modifyToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            try
            {

                if (role_Id == 2 || role_Id == 1)
                {
                    USerMaster_Edit obj = new USerMaster_Edit(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem8.Visible = false;
                }
            }
            catch { }
            
        }

        private void addToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 3 || role_Id == 1)
            //    {
            //        Purchase_add obj = new Purchase_add(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        addToolStripMenuItem9.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void addToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 3 || role_Id == 1)
            //    {
            //        Measurement_Entry obj = new Measurement_Entry(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }
            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        addToolStripMenuItem10.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void addToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 3 || role_Id == 1)
            //    {
            //        Tailor_master obj = new Tailor_master(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        addToolStripMenuItem11.Visible = false;
            //    }
            //}
            //catch { }
            
            
            
            
        }

        private void modifyToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 2 || role_Id == 1)
            //    {
            //        Tailor_Modify obj = new Tailor_Modify(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        modifyToolStripMenuItem9.Visible = false;
            //    }
            //}
            //catch { }
            
            
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
                
            //    if (role_Id == 2 || role_Id == 1)
            //    {
            //        Tailor_Purchase_Edit obj = new Tailor_Purchase_Edit(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        editToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void assignOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 3 || role_Id == 1)
            //    {
            //        Order_to_Tailor obj = new Order_to_Tailor(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        assignOrderToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void customerDeliveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if ( role_Id == 3 || role_Id == 1)
            //    {
            //        Customer_DeliveryScreen obj = new Customer_DeliveryScreen(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        customerDeliveryToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void billPrintToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 2 || role_Id == 3 || role_Id == 1)
            //    {
            //        Measurement_bill_Printing obj = new Measurement_bill_Printing(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        billPrintToolStripMenuItem1.Visible = false;
            //    }
            //}
            //catch { }
        }

        private void extraBarcodePrintingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (role_Id == 2 || role_Id == 3 || role_Id == 1)
                {

                    Single_Barcode_Printing obj = new Single_Barcode_Printing(username, company);
                    obj.MdiParent = this;
                    obj.Show();

                    //Extra_Barcode_Printing obj = new Extra_Barcode_Printing(username, company);
                    //obj.MdiParent = this;
                    //obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    extraBarcodePrintingToolStripMenuItem.Visible = false;
                }
            }
            catch { }
            
        }

        private void salesmanReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void itemWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 2 || role_Id == 3 || role_Id == 1)
            //    {
            //        ItemWise_Stock_Reports obj = new ItemWise_Stock_Reports(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        itemWiseToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void purchaseReturnReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 2 || role_Id == 3 || role_Id == 1)
            //    {
            //        PurchaseReturn_report_Screen obj = new PurchaseReturn_report_Screen(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        purchaseReturnReportToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void userRightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 1)
            //    {
            //        UserRights obj = new UserRights(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        userRightsToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
            
        }

        private void salesmanReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 2 || role_Id == 3 || role_Id == 1)
            //    {
            //        Saleman_Report_Screen obj = new Saleman_Report_Screen(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        salesmanReportToolStripMenuItem1.Visible = false;
            //    }
            //}
            //catch { }
        }

        private void gSTR1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (role_Id == 2 || role_Id == 3 || role_Id == 1)
            //    {
            //        GSTR_1 obj = new GSTR_1(company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        gSTR1ToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
        }

        private void sMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SMS_Integration obj = new SMS_Integration(username,company);
            //obj.MdiParent = this;
            //obj.Show();
        }

        private void dailyCollectionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{

            //    if (role_Id == 2 || role_Id == 3 || role_Id == 1)
            //    {
            //        DailyCollectionRpt obj = new DailyCollectionRpt(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        dailyCollectionReportToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
        }

        private void addToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            try
            {
                if ( role_Id == 3 || role_Id == 1)
                {
                    Size_Master obj = new Size_Master(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    addToolStripMenuItem12.Visible = false;
                }
            }
            catch { }
        }

        private void modifyToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            try
            {
                if ( role_Id == 2 || role_Id == 1)
                {
                    Size_Modify obj = new Size_Modify(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem10.Visible = false;
                }
            }
            catch { }
        }

        private void addToolStripMenuItem13_Click(object sender, EventArgs e)
        {
            //Weight_Master obj = new Weight_Master(username, company);
            //obj.MdiParent = this;
            //obj.Show();        
        }

        private void modifyToolStripMenuItem11_Click(object sender, EventArgs e)
        {

        }

        private void multipleSMSSendingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setMRPForCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 3 || role_Id == 1 || role_Id == 2)
                {
                    Set_MRP_for_Customer obj = new Set_MRP_for_Customer(username, company,role_Id);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    setMRPForCustomerToolStripMenuItem.Visible = false;
                }
            }
            catch { }
        }

        private void saleDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (role_Id == 1 || role_Id == 2)
            //    {
            //        Sale_deleteScreen obj = new Sale_deleteScreen(username, company);
            //        obj.MdiParent = this;
            //        obj.Show();
            //    }

            //    else
            //    {
            //        MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
            //        saleDeleteToolStripMenuItem.Visible = false;
            //    }
            //}
            //catch { }
        }

        private void addToolStripMenuItem9_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    Kit_Master obj = new Kit_Master(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    addToolStripMenuItem9.Visible = false;
                }
            }
            catch { }
        }

        private void modifyToolStripMenuItem9_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 2)
                {
                    Kit_Modify obj = new Kit_Modify(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    modifyToolStripMenuItem9.Visible = false;
                }
            }
            catch { }
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    Bill_Of_Master obj = new Bill_Of_Master(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void addToolStripMenuItem10_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    WareHouse_Master obj = new WareHouse_Master(username, company,connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void modifyToolStripMenuItem11_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    WareHouse_Master_Edit obj = new WareHouse_Master_Edit(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    Purchase_Order obj = new Purchase_Order(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void addToolStripMenuItem11_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    Rack_Master obj = new Rack_Master(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void modifyToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    Rack_Master_Edit obj = new Rack_Master_Edit(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void pendingPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    Pending_Purchase_order obj = new Pending_Purchase_order(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }
                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void incompletePurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    IncompletePurchaseOrder obj = new IncompletePurchaseOrder(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void kitProductionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    Kit_Production_Process obj = new Kit_Production_Process(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void salableProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    Saleable_Item_Screen obj = new Saleable_Item_Screen(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void damagedProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (role_Id == 1 || role_Id == 3)
                {
                    Damaged_Item_Screen obj = new Damaged_Item_Screen(username, company, connectionString);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show("You does not have Permission to view this Screen Please Contact Administrator!!! ");
                    toolStripMenuItem14.Visible = false;
                }
            }
            catch { }
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (role_Id == 2 || role_Id == 3 || role_Id == 1)
                {
                    BrcodeWise_Stock_Report obj = new BrcodeWise_Stock_Report(username, company);
                    obj.MdiParent = this;
                    obj.Show();
                }

                else
                {
                    MessageBox.Show(" You does not have  Permission to view this Screen Please Contact Administrator!!! ");
                    barcodeWiseReportToolStripMenuItem.Visible = false;
                }
            }
            catch { }
        }     
    }
}