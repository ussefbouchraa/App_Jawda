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
    public partial class PRINT_ALL : Form
    {
        ADO ado = new ADO();
        public PRINT_ALL()
        {
            InitializeComponent();
        }

        private void Imprimer_Load(object sender, EventArgs e)
        {
            ado.DT.Clear();
            ado.DA = new SqlDataAdapter(" select * from CLIENTS " ,ado.cn);
            ado.DA.Fill(ado.DT);

            CrystalReport1 repport = new CrystalReport1();
            repport.SetDataSource(ado.DT);
            crystalReportViewer1.ReportSource = repport;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clients cl = new Clients();
            cl.Show();
        }
    }
}
