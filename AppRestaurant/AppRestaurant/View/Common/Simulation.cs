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
    public partial class Simulation : Form
    {
        public Simulation()
        {
            InitializeComponent();
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            iconButton3.Visible = false;
            iconButton4.Visible = true;
            siticoneTrackBar1.Value++;
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            iconButton4.Visible = false;
            iconButton7.Visible = true;
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            iconButton4.Visible = true;
            iconButton7.Visible = false;
        }
    }
}
