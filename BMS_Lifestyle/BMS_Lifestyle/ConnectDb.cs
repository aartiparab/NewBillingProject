using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace BMS_Lifestyle
{
    class ConnectDb
    {
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlCommand cmd1;
        public SqlDataReader dr;
        public SqlTransaction ts;
        public SqlDataReader dr1;

        int refNo = 0;
        string conString = "Data Source=SERVER;Initial Catalog=BMSLifestyle;User ID=sa;Password=preet@1234";
        //string conString ="Data Source=server;Initial Catalog=Fashion_HUB;User ID=sa;Password=preet@1234";
      
        public ConnectDb()
        {
            con = new SqlConnection(conString);

            cmd = new SqlCommand();
             con.Open();
        }

        public SqlDataReader getData(string command)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd = con.CreateCommand();

                dr = null;
                cmd.CommandText = command;
                cmd.Transaction = this.ts;

                dr = cmd.ExecuteReader();

                return dr;
            }

            catch (SqlException se)
            {
                MessageBox.Show("Database Error 1" + se.Message);
                //con.Close();
                return null;
            }
        }



        public SqlDataReader getData1(string command)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                cmd1 = con.CreateCommand();

                dr1 = null;
                cmd1.CommandText = command;
                cmd1.Transaction = this.ts;

                dr1 = cmd1.ExecuteReader();

                return dr1;
            }

            catch (SqlException se)
            {
                MessageBox.Show("Database Error 1" + se.Message);
                //con.Close();
                return null;
            }
        }


        public int affect(string command)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd = con.CreateCommand();

                cmd.CommandText = command;

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected;
            }

            catch (SqlException se)
            {
                MessageBox.Show("Database Error 2" + se.Message);
                //con.Close();
                return 0;
            }
        }

        public int getRefNo(string query)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                dr = getData(query);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        refNo = Convert.ToInt32(dr["Ref_no"]);
                    }
                }

                return refNo;
            }

            catch (SqlException se)
            {
                MessageBox.Show("Database Error 3" + se.Message);
                //con.Close();
                //con.Dispose();
                return 0;
            }
        }


        public SqlConnection getConObj()
        {
            return con;
        }

        public void setSqlTran(SqlTransaction ts)
        {
            this.ts = ts;
        }

        public void releaseResources()
        {
            cmd.Dispose();
            con.Close();
        }

    }
}
