using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace BMS_Lifestyle
{
    class AutoComplete
    {
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataReader dr;

        public AutoCompleteStringCollection productname = new AutoCompleteStringCollection();

        public AutoComplete()
        {

            string conString = "Data Source=SERVER;Initial Catalog=BMSLifestyle;User ID=sa;Password=preet@1234";
            conn = new SqlConnection(conString);
            conn.Open();
        }

        public void filltextbox(string strfill)
        {
            cmd = new SqlCommand(strfill, conn);
            dr = cmd.ExecuteReader();
            if (dr.HasRows == true)
            {
                while (dr.Read())
                {
                    productname.Add(dr[0].ToString());
                }
            }
            dr.Close();
        }
    }
}
