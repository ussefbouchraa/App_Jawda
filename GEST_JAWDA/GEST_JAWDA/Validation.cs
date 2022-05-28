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
    public partial class Validation : Form
    {
        Authentif AU = new Authentif();
        
        public Validation()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();AU.Show();
        }

        private void Validation_Load(object sender, EventArgs e)
        {
            label1.Text = " WELCOME (MR/MRS/MISS) : " + AU.GETNOM + " !! ";

        }
    }
}
