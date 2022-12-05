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
    public partial class AppRestaurant : Form
    {
        public kitchenModel model { get; set; }
        public Simulation simulationFrame { get; set; }
        public AppRestaurant(kitchenModel model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void startClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AppRestaurant_Load(object sender, EventArgs e)
        {
            label.Parent = pictureBox;
            label.BackColor = Color.Transparent;
        }

        private void label_Click(object sender, EventArgs e)
        {

        }
    }
}
