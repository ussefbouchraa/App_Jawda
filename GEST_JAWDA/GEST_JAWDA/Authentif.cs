using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GEST_JAWDA
{
    public partial class Authentif : Form
    {
        ADO ado = new ADO();
        DateTime @datefin;
        int cp=0;
        public static string @nom;

        public Authentif()
        {
            InitializeComponent();
            comboBox1.Items.Add("Client");
            comboBox1.Items.Add("Admin");
        }

        public string GETNOM
        {
            get { return @nom; }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { txt_password.UseSystemPasswordChar = false; }
            else { txt_password.UseSystemPasswordChar = true; }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Authentif_Load(object sender, EventArgs e)
        {
            ado.connecter();
        }

        private void Authentif_Leave(object sender, EventArgs e)
        {
            ado.donnecter();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (txt_nom.Text == "" || comboBox1.Text == "" || txt_password.Text == "") { MessageBox.Show(" VERIFIEZ SVP LES ZONES DU TEXTES EST VIDES !! ", "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Hand); }

            else if (comboBox1.Text == "Admin")  
             {

                ado.cmd = new SqlCommand(" select * from ADMINS where nom='" + txt_nom.Text.ToUpper() + "' and password='" + txt_password.Text + "'", ado.cn);
                ado.dr = ado.cmd.ExecuteReader();
                ado.DT.Load(ado.dr);

                if (ado.DT.Rows.Count > 0) //if (ado.dr.HasRows)
                {
                    this.Hide();
                    Clients client = new Clients();
                    cp = 0;
                    client.Show();
                    ado.dr.Close();
                }
                else { MessageBox.Show(" VOTRE NOM ou PASSWORD INCORRECT VERIFIER SVP !!","Verifier !!",MessageBoxButtons.OK,MessageBoxIcon.Error); txt_password.Clear(); txt_nom.Clear(); cp++; checkBox1.Checked = false; lb1.Text = cp.ToString(); ado.DT.Clear(); }
                if (cp >= 3) { panel2.Enabled = false; button3.Hide(); }
            }


            else  if (comboBox1.Text == "Client")
            {


                ado.DA = new SqlDataAdapter(" select * from CLIENTS where nom='" + txt_nom.Text.ToUpper() + "' and password='" + txt_password.Text + "'", ado.cn);
                ado.DA.Fill(ado.DT);

                if (ado.DT.Rows.Count != 0)
                {
                    DateTime date = Convert.ToDateTime(ado.DT.Rows[0]["date fin"]);
                    string format = date.ToString("MM-dd-yyyy");
                    @datefin = Convert.ToDateTime(format);





                    if (DateTime.Now >= @datefin) { ado.DT.Clear(); @nom = txt_nom.Text.ToUpper(); this.Hide(); ERREUR E = new ERREUR(); E.ShowDialog(); }
                    else { @nom = txt_nom.Text.ToUpper(); this.Hide(); Validation V = new Validation(); V.ShowDialog(); comboBox1.Text = ""; txt_nom.Text = ""; txt_password.Clear(); comboBox1.Focus(); lb1.Text = ""; ado.DT.Clear(); }
                    cp = 0;
                }
                else
                {

                    MessageBox.Show(" VOTRE NOM ou PASSWORD INCORRECT VERIFIER SVP !!", "Verifier !!", MessageBoxButtons.OK, MessageBoxIcon.Error); txt_password.Clear(); txt_nom.Clear(); cp++; lb1.Text = cp.ToString(); ado.DT.Clear();
                }

                  if (cp >= 3) { panel2.Enabled = false; button3.Hide(); }
             }


               
              




           



        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voullez Vous Vraiment Quitter !! ", "Fermer", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) this.Close();

        }
    }
}
