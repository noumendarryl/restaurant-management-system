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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void siticoneButton1_MouseEnter(object sender, EventArgs e)
        {
            siticoneButton1.ForeColor = Color.Black;
        }

        private void siticoneButton1_MouseLeave(object sender, EventArgs e)
        {
            siticoneButton1.ForeColor = Color.FromArgb(255, 152, 0);
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                siticoneHtmlLabel4.Visible = true;
            }
            if (textBox2.Text == "")
            {
                siticoneHtmlLabel5.Visible = true;
            }
        }
    }
}
