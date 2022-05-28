using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GEST_JAWDA
{
    public partial class ONE_PRINT : Form
    {
        Clients client = new Clients();

        ADO ado = new ADO();
     
        public ONE_PRINT()
        {
            InitializeComponent(); 

          
        }

        private void ONE_PRINT_Load(object sender, EventArgs e)
        {
        

         
            ado.DT.Clear();
            ado.DA = new SqlDataAdapter("select * from CLIENTS where cin='" + client.@cin + "' ", ado.cn);
            ado.DA.Fill(ado.DT);

            CrystalReport2 repport = new CrystalReport2();
            repport.SetDataSource(ado.DT);
            crystalReportViewer2.ReportSource = repport;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clients cl = new Clients();
            cl.Show();
        }

       
    }
}
