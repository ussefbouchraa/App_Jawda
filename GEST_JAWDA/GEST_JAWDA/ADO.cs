using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace GEST_JAWDA
{
    class ADO
    {
       
       // public SqlConnection cn = new SqlConnection(@" workstation id=BASEJAWDA.mssql.somee.com;packet size=4096;user id=Q321_SQLLogin_1;pwd=egy1eeg9s8;data source=BASEJAWDA.mssql.somee.com;persist security info=False;initial catalog=BASEJAWDA ");  

        public SqlCommand cmd = new SqlCommand();
        public SqlDataAdapter DA = new SqlDataAdapter();
        public SqlDataReader dr;
        public DataSet ds_xml = new DataSet();
        public DataSet ds1 = new DataSet();
        public DataTable DT= new DataTable();
        public SqlCommandBuilder SCB = new SqlCommandBuilder();
        public SqlTransaction tran;


        public void connecter()
        {
            if (cn.State == ConnectionState.Closed || cn.State == ConnectionState.Broken) { cn.Open(); }

        }


        public void donnecter()
        {
            if (cn.State == ConnectionState.Open || cn.State == ConnectionState.Broken) { cn.Close(); }

        }


    }
}
