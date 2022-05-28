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
    public partial class Clients : Form
    {
        public static string ussef;

        public string @cin
        {

            get { return ussef; }
          
        }

        int position = 0;
        int t = 0;
        
        ADO ado = new ADO();
      


        public Clients()
        {
            InitializeComponent();
            DGV();
           
        }

        public void DGVPOSITION()
        {

            if (position > dataGridView1.Rows.Count - 1 )
            { position = 0; return; }
            else if (position < 0) { position = dataGridView1.Rows.Count - 1; return; }
            else
            {
                txt_nom.Text = dataGridView1.Rows[position].Cells[0].Value.ToString();
                txt_prenom.Text = dataGridView1.Rows[position].Cells[1].Value.ToString();
                txt_cin.Text = dataGridView1.Rows[position].Cells[2].Value.ToString();
                dtp1.Text = dataGridView1.Rows[position].Cells[3].Value.ToString();
                dtp2.Text = dataGridView1.Rows[position].Cells[4].Value.ToString();
                //txt_password.Text = dataGridView1.Rows[position].Cells[5].Value.ToString();
              
            }
        }

        public void SELECT_P() { 
            dataGridView1.ClearSelection();
            dataGridView1.Rows[position].Selected = true;
        
        }

        public void DGV()
        {
            
            ado.ds1.Clear();  //nom,prenom,cin,[date inscri],[date fin]      
            ado.DA = new SqlDataAdapter("select nom,prenom,cin,[date inscri],[date fin]  from CLIENTS ", ado.cn);
            ado.DA.Fill(ado.ds1,"tab1");
       
            dataGridView1.DataSource =ado.ds1.Tables["tab1"];
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            ado.connecter();
            dtp1.Text = DateTime.Now.ToShortDateString();
        }

        private void Clients_Leave(object sender, EventArgs e)
        {
            ado.ds1.WriteXml("XMLFile1.xml");
        
            ado.donnecter();

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime @DATEFIN = dtp1.Value.AddMonths(Convert.ToInt16(comboBox1.Text));
           dtp2.Text = @DATEFIN.ToShortDateString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t++;

            if (t == 1) { this.BackColor = Color.PapayaWhip; }
            if (t == 2) { this.BackColor = Color.Silver; }
            if (t == 3) { this.BackColor = Color.WhiteSmoke; }
            if (t == 4) { this.BackColor = Color.Thistle; }
            if (t == 5) { this.BackColor = Color.Lavender; }
            if (t == 6) { this.BackColor = Color.Khaki; }
            if (t == 7) { this.BackColor = Color.LawnGreen; }
            if (t == 8) { this.BackColor = Color.LightSeaGreen; }
            if (t == 9) { this.BackColor = Color.Violet; }
            if (t == 10) { this.BackColor = Color.PaleGoldenrod; t = 1; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            position = dataGridView1.Rows.Count - 1; DGVPOSITION(); SELECT_P();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            position = 0; DGVPOSITION(); SELECT_P();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            position -=1 ; DGVPOSITION(); SELECT_P();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            position += 1; DGVPOSITION(); SELECT_P();
        }
        public void VIDER() { txt_nom.Clear(); txt_prenom.Clear(); txt_cin.Clear(); dtp1.ResetText(); dtp2.ResetText(); txt_password.Clear(); }
      
        private void aperçuavantimpressionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            VIDER();

        }

        private void quitterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voullez vous Vraiment Quitter", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) this.Close();

        }

        private void aDDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ado.tran = ado.cn.BeginTransaction();

                SqlCommand cmd = new SqlCommand(@"insert into CLIENTS values(@a,@b,@c,@d,@e,@f)", ado.cn ,ado.tran);
                cmd.Parameters.AddWithValue("@a",txt_nom.Text.ToUpper());
                cmd.Parameters.AddWithValue("@b", txt_prenom.Text);
                cmd.Parameters.AddWithValue("@c", txt_cin.Text);
                cmd.Parameters.AddWithValue("@d", dtp1.Text);
                cmd.Parameters.AddWithValue("@e", dtp2.Text);
                cmd.Parameters.AddWithValue("@f", txt_password.Text);

                if (txt_nom.Text == "" || txt_prenom.Text == ""  || dtp1.Text == "" || dtp2.Text == "" || txt_password.Text == "") { MessageBox.Show(" VERIFIEZ SVP LES ZONES DU TEXTES EST VIDES !! ","ERREUR",MessageBoxButtons.OK,MessageBoxIcon.Hand); }
                else
                {
                    cmd.ExecuteNonQuery();
                    DGV(); ARRCHIVE_XML_ADD(); VIDER();
                    MessageBox.Show(" ADDED successfully ", "ADD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //ado.tran.Commit();
                }
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message, "ERREUR in ADD"); 
                //ado.tran.Rollback(); 
            }
           
          

          
        }
      
        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            ado.DT.Clear();
           ado.DA = new SqlDataAdapter("select * from CLIENTS  where cin like '%" + txt_search.Text + "%' or nom like '%" + txt_search.Text + "%'  ", ado.cn);
           ado. DA.Fill(ado.DT);

           if (txt_search.Text != "") { dataGridView1.DataSource = ado.DT; }
           else { dataGridView1.DataSource = ado.DT; }
           
        }



        public void ARRCHIVE_XML_ADD()
        {

            
            ado.ds_xml.Clear();
            ado.ds_xml.ReadXml("ARRCHIVE.xml");

            DataRow d= ado.ds_xml.Tables[0].NewRow();
            d[0] = txt_nom.Text.ToUpper();
            d[1] = txt_prenom.Text;
            d[2] = txt_cin.Text;
            d[3] = dtp1.Text;
            d[4] = dtp2.Text;
            d[5] = txt_password.Text;

            ado.ds_xml.Tables["tab_xml"].Rows.Add(d);


            ado.ds_xml.WriteXml(@"ARRCHIVE.xml");
        }


        public void ARRCHIVE_XML_UPDATE()
        {
            ado.ds_xml.Clear();
            ado.ds_xml.ReadXml("ARRCHIVE.xml");

            //ado.ds_xml.Tables.Add("tab_xml");

            for (int i = 0; i < ado.ds_xml.Tables[0].Rows.Count ; i++)
            {
                DataRow r = ado.ds_xml.Tables[0].Rows[i];
                if (r["cin"].ToString() == txt_cin.Text)
                {

                    r[0] = txt_nom.Text.ToUpper(); r[1] = txt_prenom.Text; r["password"] = txt_password.Text;

                     ado.ds_xml.WriteXml("ARRCHIVE.xml");

            MessageBox.Show(" Arrchive Updated !! ", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break; 
                }
            }
          




        }


        private void personnaliserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

        }

        private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
            ado.ds1.WriteXml("XMLFile1.xml");
            MessageBox.Show(" Saved Saccessfully ");
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ado.ds1.Clear();
            ado.ds1.ReadXml(@"XMLFile1.xml");
            dataGridView1.DataSource = ado.ds1.Tables["tab1"];


         
            
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            this.Hide();
            DataXML DATA = new DataXML();
            DATA.Show();
       
        }
        
        private void dataGridView1_Click_1(object sender, EventArgs e)
        {
          
            txt_nom.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txt_prenom.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txt_cin.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dtp1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dtp2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            //txt_password.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
           ussef = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           
        }
    

        private void uPDATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ado.tran = ado.cn.BeginTransaction();

                SqlCommand cmd = new SqlCommand("Update CLIENTS  set nom=@a, prenom=@b,password=@c where cin=@d ", ado.cn, ado.tran);
                cmd.Parameters.AddWithValue("@a", txt_nom.Text);
                cmd.Parameters.AddWithValue("@b", txt_prenom.Text);
                cmd.Parameters.AddWithValue("@c", txt_password.Text);
                cmd.Parameters.AddWithValue("@d", txt_cin.Text);

                cmd.ExecuteNonQuery();
                ado.tran.Commit();
                DGV();
                MessageBox.Show(" UPDATEED successfully !! ", "UPDATE", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (SqlException ex) { MessageBox.Show(ex.Message, "ERREUR in UPDATE"); ado.tran.Rollback(); }
            finally {     ARRCHIVE_XML_UPDATE(); VIDER(); }
        }

        private void dELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //ado.tran = ado.cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand("delete from CLIENTS  where cin=@a ", ado.cn);
                cmd.Parameters.AddWithValue("@a", txt_cin.Text);
                

                cmd.ExecuteNonQuery();
                MessageBox.Show(" DELETED successfully !! ", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
              DGV(); VIDER(); 
                //ado.tran.Commit();

            }
            catch (SqlException ex) { MessageBox.Show(ex.Message, "ERREUR in DELETE");
                //ado.tran.Rollback();
            }
         
        }
       
        private void paiementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Paiement P = new Paiement();
            P.Show();

        }

        private void Clients_MouseHover(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void groupBox1_MouseHover(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void dataGridView1_MouseEnter(object sender, EventArgs e)
        {
             timer1.Stop();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
             if (ussef == null)

            { MessageBox.Show("vous devez selectionner un ligne dans la grille !!" ,"ERREUR",MessageBoxButtons.AbortRetryIgnore,MessageBoxIcon.Hand); }
           
            else
            {
                this.Hide();
                ONE_PRINT print = new ONE_PRINT();
                print.Show();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void imprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            PRINT_ALL all = new PRINT_ALL();
            all.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            position -= 1; DGVPOSITION(); SELECT_P();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            position += 1; DGVPOSITION(); SELECT_P();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            position = 0; DGVPOSITION(); SELECT_P();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            position = dataGridView1.Rows.Count - 1; DGVPOSITION(); SELECT_P();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void retourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Authentif A = new Authentif();
            A.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voullez vous Vraiment Quitter", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) this.Close();

        }
    }
}
