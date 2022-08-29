using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xmas_Grotto_Information
{
    public partial class frmStartup : Form
    {
        bool bexit = false;
        public frmStartup()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Are you sure you want to close Grotto Controller?", "Close Grotto Controller", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                bexit = true;
                this.Close();
            }
        }

        private void frmStartup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bexit == false)
            {
                DialogResult dg = MessageBox.Show("Are you sure you want to close Grotto Controller?", "Close Grotto Controller", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dg == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void frmStartup_Load(object sender, EventArgs e)
        {
            this.Text = "Grotto " + Properties.Settings.Default.giNo.ToString() + " Information";
        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            frmSettings frmSettings = new frmSettings();
            frmSettings.Show();
        }

        private void cmdGrottoController_Click(object sender, EventArgs e)
        {
            frmGrottoInfo frmGrotto = new frmGrottoInfo();
            frmGrotto.Show();
        }
    }
}
