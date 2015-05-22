using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LizardsGUI
{
    public partial class InitExperiment : Form
    {
        private string PrevNumLizards;
        public int NumLizards;

        public InitExperiment()
        {
            InitializeComponent();
            PrevNumLizards = txtNumLizards.Text;
        }

        private void InitExperiment_FormClosing(object sender, FormClosingEventArgs e)
        {
            NumLizards = int.Parse(txtNumLizards.Text);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtNumLizards_Leave(object sender, EventArgs e)
        {
            try
            {
                txtNumLizards.Text = Math.Max(1, int.Parse(txtNumLizards.Text)).ToString();
            }
            catch (Exception)
            {
                txtNumLizards.Text = PrevNumLizards;
            }
            PrevNumLizards = txtNumLizards.Text;
        }
    }
}
