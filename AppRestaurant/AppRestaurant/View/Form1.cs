using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public KitchenModel model { get; set; }

        public Form1(KitchenModel model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int width = (model.kitchen.map.GetUpperBound(0) - 1) * KitchenView.FRAME_SIZE;
            int height = (model.kitchen.map.GetUpperBound(1) - 1) * KitchenView.FRAME_SIZE;
            Size = new Size(width, height);
        }

        private void kitchenForm_Paint(object sender, PaintEventArgs e)
        {

            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDir = System.IO.Path.GetDirectoryName(exePath);
            DirectoryInfo binDir = System.IO.Directory.GetParent(exeDir);
            binDir = System.IO.Directory.GetParent(binDir.FullName);


            string spritePath = binDir.FullName + "\\Resources\\kitchentile.png";

            for (int i = 0; i < model.kitchen.map.GetUpperBound(0); i++)
            { 
                for (int j = 0; j < model.kitchen.map.GetUpperBound(1); j++)
                {
                    e.Graphics.DrawImage(Image.FromFile(spritePath), i * KitchenView.FRAME_SIZE, j * KitchenView.FRAME_SIZE);
                }
            }

            foreach (Chef chef in model.chefs)
            {
                e.Graphics.DrawImage(chef.getSprite().getImage(), chef.posX * KitchenView.FRAME_SIZE, chef.posY * KitchenView.FRAME_SIZE);
            }

            foreach (DeputyChef deputyChef in model.deputyChefs)
            {
                e.Graphics.DrawImage(deputyChef.getSprite().getImage(), deputyChef.posX * KitchenView.FRAME_SIZE, deputyChef.posY * KitchenView.FRAME_SIZE);
            }

            foreach (KitchenClerk kitchenClerk in model.kitchenClerks)
            {
                e.Graphics.DrawImage(kitchenClerk.getSprite().getImage(), kitchenClerk.posX * KitchenView.FRAME_SIZE, kitchenClerk.posY * KitchenView.FRAME_SIZE);
            }

            foreach (Diver diver in model.divers)
            {
                e.Graphics.DrawImage(diver.getSprite().getImage(), diver.posX * KitchenView.FRAME_SIZE, diver.posY * KitchenView.FRAME_SIZE);
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
