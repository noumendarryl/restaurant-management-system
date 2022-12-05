using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model;
using AppRestaurant.Model.kitchen;
using AppRestaurant.View;

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
            int width = (model.kitchen.map.GetUpperBound(0) - 1) * kitchenView.FRAME_SIZE;
            int height = (model.kitchen.map.GetUpperBound(1) - 1) * kitchenView.FRAME_SIZE;
            Size = new Size(width, height);
        }

        private void kitchenForm_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < model.kitchen.map.GetUpperBound(0); i++)
            { 
                for (int j = 0; j < model.kitchen.map.GetUpperBound(1); j++)
                {
                    e.Graphics.DrawImage(Image.FromFile("C:\\Users\\NOUMEN DARRYL\\Documents\\prog-sys-obj\\AppRestaurant\\AppRestaurant\\Resources\\kitchentile.png"), i * kitchenView.FRAME_SIZE, j * kitchenView.FRAME_SIZE);
                }
            }

            foreach (Chef chef in model.chefs)
            {
                e.Graphics.DrawImage(chef.getSprite().getImage(), chef.posX * kitchenView.FRAME_SIZE, chef.posY * kitchenView.FRAME_SIZE);
            }

            foreach (DeputyChef deputyChef in model.deputyChefs)
            {
                e.Graphics.DrawImage(deputyChef.getSprite().getImage(), deputyChef.posX * kitchenView.FRAME_SIZE, deputyChef.posY * kitchenView.FRAME_SIZE);
            }

            foreach (kitchenClerk kitchenClerk in model.kitchenClerks)
            {
                e.Graphics.DrawImage(kitchenClerk.getSprite().getImage(), kitchenClerk.posX * kitchenView.FRAME_SIZE, kitchenClerk.posY * kitchenView.FRAME_SIZE);
            }

            foreach (Diver diver in model.divers)
            {
                e.Graphics.DrawImage(diver.getSprite().getImage(), diver.posX * kitchenView.FRAME_SIZE, diver.posY * kitchenView.FRAME_SIZE);
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
