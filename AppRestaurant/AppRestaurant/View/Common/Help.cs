using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppRestaurant.View.Common
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void customizeDesign()
        {
            panel4.Visible = false;
            panel6.Visible = false;
            panel8.Visible = false;
            panel10.Visible = false;
            panel12.Visible = false;
        }

        private void hideSubPanel()
        {
            if (panel4.Visible == true)
                panel4.Visible = false;
            if (panel6.Visible == true)
                panel6.Visible = false;
            if (panel8.Visible == true)
                panel8.Visible = false;
            if (panel10.Visible == true)
                panel10.Visible = false;
            if (panel12.Visible == true)
                panel12.Visible = false;
        }

        private void showSubPanel(Panel panel)
        {
            if (panel.Visible == false)
            {
                hideSubPanel();
                panel.Visible = true;
            }
            else
                panel.Visible = false;
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            showSubPanel(panel4);
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            showSubPanel(panel6);
        }

        private void siticoneButton3_Click(object sender, EventArgs e)
        {
            showSubPanel(panel8);
        }

        private void siticoneButton4_Click(object sender, EventArgs e)
        {
            showSubPanel(panel10);
        }

        private void siticoneButton5_Click(object sender, EventArgs e)
        {
            showSubPanel(panel12);
        }
    }
}
