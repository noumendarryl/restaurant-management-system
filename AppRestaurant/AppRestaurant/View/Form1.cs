using AppRestaurant.Model.kitchen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppRestaurant
{
    public partial class Form1 : Form
    {
        public kitchenModel model { get; set; }

        public Form1(kitchenModel model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void kitchenForm_Paint(object sender, PaintEventArgs e)
        {
           foreach (Chef chef in model.chefs)
            {
                //e.Graphics.DrawImage(chef.getSprite(), chef.posX * kitchenV)
            }
        }
    }
}
