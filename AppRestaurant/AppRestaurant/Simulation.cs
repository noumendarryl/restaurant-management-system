using AppRestaurant.Model.kitchen;
using AppRestaurant.View;
using AppRestaurant.Controller;
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

namespace AppRestaurant
{
    public partial class Simulation : Form
    {
        public kitchenModel model { get; set; }
        public BookingForm bookingForm { get; set; }
        public string applicationPath { get; set; }
        public string saveFilePath { get; set; }
        public StreamWriter writer { get; set; }
        public Setting setting { get; set; }
        public int simulationTimeScale { get; set; }
        public int chefNumber { get; set; }
        public int deputyChefNumber { get; set; }
        public int kitchenClerkNumber { get; set; }
        public int diverNumber { get; set; }
        public int cookingFireNumber { get; set; }
        public int fridgeNumber { get; set; }
        public int blenderNumber { get; set; }
        public int ovenNumber { get; set; }

        [Obsolete]
        public Simulation(kitchenModel model)
        {
            this.model = model;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.bookingForm = new BookingForm();
            this.bookingForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string directory = Directory.GetCurrentDirectory();
            string filePath = directory + "\\AppRestaurantConfigs.txt";
            saveFilePath = filePath;
            if (File.Exists(saveFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(saveFilePath);

                    simulationTimeScale = Convert.ToInt32(lines.ElementAt(0));
                    Console.WriteLine(simulationTimeScale);
                    chefNumber = Convert.ToInt32(lines.ElementAt(1));
                    Console.WriteLine(chefNumber);
                    deputyChefNumber = Convert.ToInt32(lines.ElementAt(2));
                    Console.WriteLine(deputyChefNumber);
                    kitchenClerkNumber = Convert.ToInt32(lines.ElementAt(3));
                    Console.WriteLine(kitchenClerkNumber);
                    diverNumber = Convert.ToInt32(lines.ElementAt(4));
                    Console.WriteLine(diverNumber);

                    cookingFireNumber = Convert.ToInt32(lines.ElementAt(5));
                    Console.WriteLine(cookingFireNumber);
                    ovenNumber = Convert.ToInt32(lines.ElementAt(6));
                    Console.WriteLine(ovenNumber);
                    blenderNumber = Convert.ToInt32(lines.ElementAt(7));
                    Console.WriteLine(blenderNumber);
                    fridgeNumber = Convert.ToInt32(lines.ElementAt(8));
                    Console.WriteLine(fridgeNumber);

                    model.setEmployeeConfig(chefNumber, deputyChefNumber, kitchenClerkNumber, diverNumber);
                    model.setMaterialConfig(cookingFireNumber, ovenNumber, blenderNumber, fridgeNumber);
                } catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            } 
            else
            {
                // the directory that your program is installed in
                applicationPath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
                saveFilePath = Path.Combine(applicationPath, "AppRestaurantConfigs.txt");
                writer = new StreamWriter(saveFilePath, false);

                writer.WriteLine(kitchenView.setting.simulationTimeScale.ToString());
                writer.WriteLine(kitchenView.setting.chefNumber.ToString());
                writer.WriteLine(kitchenView.setting.deputyChefNumber.ToString());
                writer.WriteLine(kitchenView.setting.kitchenClerkNumber.ToString());
                writer.WriteLine(kitchenView.setting.diverNumber.ToString());
           
                writer.WriteLine(kitchenView.setting.cookingFireNumber.ToString());
                writer.WriteLine(kitchenView.setting.ovenNumber.ToString());
                writer.WriteLine(kitchenView.setting.blenderNumber.ToString());
                writer.WriteLine(kitchenView.setting.fridgeNumber.ToString());
            
                writer.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            setting = new Setting(kitchenView.simulationForm);
            setting.Show();
        }

        [Obsolete]
        private void button6_Click(object sender, EventArgs e)
        {
            kitchenController.Start();
            button6.Visible = false;
            button8.Visible = true;
        }

        [Obsolete]
        private void button8_Click(object sender, EventArgs e)
        {
            kitchenController.Suspend();
            button8.Visible = false;
            button12.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
        }

        [Obsolete]
        private void button12_Click(object sender, EventArgs e)
        {
            kitchenController.Resume();
            button12.Visible = false;
            button8.Visible = true;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDir = System.IO.Path.GetDirectoryName(exePath);
            DirectoryInfo binDir = System.IO.Directory.GetParent(exeDir);
            binDir = System.IO.Directory.GetParent(binDir.FullName);


            string spritePath = binDir.FullName + "\\Resources\\dinningroomtile.png";

            for (int i = 0; i < model.kitchen.map.GetUpperBound(0); i++)
            {
                for (int j = 0; j < model.kitchen.map.GetUpperBound(1); j++)
                { 
                   e.Graphics.DrawImage(Image.FromFile(spritePath), i * kitchenView.FRAME_SIZE, j * kitchenView.FRAME_SIZE);
                }
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
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
                    e.Graphics.DrawImage(Image.FromFile(spritePath), i * kitchenView.FRAME_SIZE, j * kitchenView.FRAME_SIZE);
                }
            }

            for (int i = 0; i < model.kitchen.map.GetUpperBound(0); i++)
            {
               for (int j = 0; j < model.kitchen.map.GetUpperBound(1); j++)
               {
                   kitchenMaterial material = model.kitchen.map[i, j];

                   if (material != null)
                   {
                        e.Graphics.DrawImage(material.getSprite().getImage(), i * kitchenView.FRAME_SIZE, j * kitchenView.FRAME_SIZE);
                   }
               }
            }

            foreach (Chef chef in model.chefs)
            {
                e.Graphics.DrawImage(chef.getSprite().getImage(), chef.posX * kitchenView.FRAME_SIZE, chef.posY * kitchenView.FRAME_SIZE);
            }

            foreach (DeputyChef deputyChef in model.deputyChefs)
            {
                //e.Graphics.DrawImage(deputyChef.getSprite().getImage(), deputyChef.posX * kitchenView.FRAME_SIZE, deputyChef.posY * kitchenView.FRAME_SIZE);
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

    }
}
