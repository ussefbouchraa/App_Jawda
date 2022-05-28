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
    public partial class Paiement : Form
    {
        ADO ado = new ADO();
        string @cin;
        string @nom;
        public Paiement()
        {
            InitializeComponent();
        }

        public void DGV()
        {

            ado.DT.Clear();
            ado.DA = new SqlDataAdapter("select * from CLIENTS ", ado.cn);
           ado. DA.Fill(ado.DT);
            dataGridView1.DataSource = ado.DT;

        }


        private void Paiement_Load(object sender, EventArgs e)
        {
            ado.connecter();
            comboBox1.Items.Add("1"); comboBox1.Items.Add("2"); comboBox1.Items.Add("3"); comboBox1.Items.Add("6"); comboBox1.Items.Add("12");
            DGV();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            lb1.BackColor = Color.Red;
            lb1.Text = dataGridView1.CurrentRow.Cells["date fin"].Value.ToString();
            @cin= dataGridView1.CurrentRow.Cells["cin"].Value.ToString();
            @nom = dataGridView1.CurrentRow.Cells["nom"].Value.ToString();

        }

        private void t10_TextChanged(object sender, EventArgs e)
        {
           ado. DT.Clear();
          ado.DA = new SqlDataAdapter("select * from CLIENTS where cin like'" + t10.Text + "%'or nom like '" + t10.Text + "%' ", ado.cn);
            ado.DA.Fill(ado.DT);
            dataGridView1.DataSource = ado.DT;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime @datefin = Convert.ToDateTime(lb1.Text).AddMonths(Convert.ToInt32(comboBox1.Text));
          
            
            try
            {

               ado. cmd = new SqlCommand("update CLIENTS SET [date fin] ='" + @datefin + "'where cin='" + @cin + "' or nom='" +@nom + "'", ado.cn);

               
               ado. cmd.ExecuteNonQuery();
               MessageBox.Show(" PAIEMENT EST Success" + @datefin.ToShortDateString()+ " " , "PAIE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
           

        }

        private void Paiement_Leave(object sender, EventArgs e)
        {
            ado.donnecter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Clients cl = new Clients();
            cl.Show();
        }

      
    }
}
