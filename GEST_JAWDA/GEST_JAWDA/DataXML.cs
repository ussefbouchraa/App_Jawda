using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GEST_JAWDA
{
    public partial class DataXML : Form
    {
        ADO ado1 = new ADO();
        int position = 0;

        public DataXML()
        {
            InitializeComponent();
        }
        public void DGVPOSITION()
        {
            if (position > DataGridView1.Rows.Count - 1) { position = 0; return; }
            if (position < 0) { position = DataGridView1.Rows.Count - 1; return; }
        }

        public void SELECT_P()
        {
            DataGridView1.ClearSelection();
            DataGridView1.Rows[position].Selected = true;
        }

        private void DataXML_Load(object sender, EventArgs e)
        {
            txt_search.Focus();
            ado1.ds_xml.Clear();
            ado1.ds_xml.ReadXml("ARRCHIVE.xml");

            DataGridView1.DataSource = ado1.ds_xml.Tables[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            position = DataGridView1.Rows.Count - 1; DGVPOSITION(); SELECT_P();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            position += 1; DGVPOSITION(); SELECT_P();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            position -= 1; DGVPOSITION(); SELECT_P();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            position = 0; DGVPOSITION(); SELECT_P();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {


            if (txt_search.Text == "") { DataGridView1.DataSource = ado1.ds_xml.Tables[0]; }
            else
            {

                for (int i = 0; i <= ado1.ds_xml.Tables[0].Rows.Count - 1; i++)
                {

                    var A = txt_search.Text.ToUpper().Trim();
                    var B = ado1.ds_xml.Tables[0].Rows[i][0].ToString().Trim();
                    var C = ado1.ds_xml.Tables[0].Rows[i]["CIN"].ToString().Trim();

                    if (A == B || A == C)
                    {

                        DataRow r = ado1.ds_xml.Tables[0].Rows[i];
                        // DataGridView1.Rows.Add(DataGridView1.Rows[i]);

                        DataGridView1.DataSource = null;
                        DataGridView1.ColumnCount = 6;

                        DataGridView1.Columns[0].Name = "nom";
                        DataGridView1.Columns[1].Name = "prenom";
                        DataGridView1.Columns[2].Name = "cin";
                        DataGridView1.Columns[3].Name = "date inscri";
                        DataGridView1.Columns[4].Name = "date fin";
                        DataGridView1.Columns[5].Name = "password";

                        DataGridView1.Rows.Add(r[0], r[1], r[2], r[3], r[4], r[5]);

                        break;
                    }

                    else {
                        DataGridView1.Columns.Clear();
                        DataGridView1.DataSource = null;
                        DataGridView1.DataSource = ado1.ds_xml.Tables[0];  
                         }
                        
                        
                        if (txt_search.Text == " ") 
                    {
                        DataGridView1.Columns.Clear();
                        DataGridView1.DataSource = null;
                        DataGridView1.DataSource = ado1.ds_xml.Tables[0];  
                       
                    
                        
                     
                    }


                }
            }

            
           

            
     
        }


        private void button6_Click(object sender, EventArgs e)
        {
             this.Hide();
            Clients cl = new Clients();
            cl.Show();
        }

     
    }
}

        
    

