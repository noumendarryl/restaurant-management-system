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
using AppRestaurant.Model.Kitchen;
using AppRestaurant.Controller.Kitchen;
using AppRestaurant.Model.Kitchen.Materials;
using AppRestaurant.Model.Kitchen.Actors;
using AppRestaurant.View.Kitchen;
using AppRestaurant.Controller.DiningRoom;

namespace AppRestaurant
{
    public partial class Simulation : Form
    {
        public KitchenModel model { get; set; }
        public BookingForm bookingForm { get; set; }
        public StreamWriter writer { get; set; }
        public Setting setting { get; set; }
        public System.Timers.Timer timer { get; set; }
        public string applicationPath { get; set; }
        public string saveFilePath { get; set; }
        public int simulationTimeScale { get; set; }
        public int chefNumber { get; set; }
        public int deputyChefNumber { get; set; }
        public int kitchenClerkNumber { get; set; }
        public int diverNumber { get; set; }
        public int cookingFireNumber { get; set; }
        public int fridgeNumber { get; set; }
        public int blenderNumber { get; set; }
        public int ovenNumber { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }

        [Obsolete]
        public Simulation(KitchenModel model)
        {
            this.model = model;
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += onTimeElapsed;
            InitializeComponent();
        }

        private void onTimeElapsed(Object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                second += 1;
                if (second == 60)
                {
                    second = 0;
                    minute += 1;
                }
                if (minute == 60)
                {
                    minute = 0;
                    hour += 1;
                }
                label8.Text = string.Format("{0}:{1}:{2}", hour.ToString().PadLeft(2, '0'), minute.ToString().PadLeft(2, '0'), second.ToString().PadLeft(2, '0'));
            }));
        }

        private void book(object sender, EventArgs e)
        {
            bookingForm = new BookingForm();
            bookingForm.Show();
        }

        private void load(object sender, EventArgs e)
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

                    blenderNumber = Convert.ToInt32(lines.ElementAt(5));
                    Console.WriteLine(blenderNumber);
                    ovenNumber = Convert.ToInt32(lines.ElementAt(6));
                    Console.WriteLine(ovenNumber);
                    cookingFireNumber = Convert.ToInt32(lines.ElementAt(7));
                    Console.WriteLine(cookingFireNumber);
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

        private void save(object sender, EventArgs e)
        {
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            } 
            else
            {
                // The directory that your program is installed in
                applicationPath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);
                saveFilePath = Path.Combine(applicationPath, "AppRestaurantConfigs.txt");
                writer = new StreamWriter(saveFilePath, false);

                writer.WriteLine(KitchenView.setting.simulationTimeScale.ToString());

                writer.WriteLine(KitchenView.setting.chefNumber.ToString());
                writer.WriteLine(KitchenView.setting.deputyChefNumber.ToString());
                writer.WriteLine(KitchenView.setting.kitchenClerkNumber.ToString());
                writer.WriteLine(KitchenView.setting.diverNumber.ToString());
           
                writer.WriteLine(KitchenView.setting.blenderNumber.ToString());
                writer.WriteLine(KitchenView.setting.ovenNumber.ToString());
                writer.WriteLine(KitchenView.setting.cookingFireNumber.ToString());
                writer.WriteLine(KitchenView.setting.fridgeNumber.ToString());
            
                writer.Close();
            }
        }

        private void settings(object sender, EventArgs e)
        {
            setting = new Setting(KitchenView.simulationForm);
            setting.Show();
        }

        [Obsolete]
        private void start(object sender, EventArgs e)
        {
            DiningRoomController.Run();
            KitchenController.Start();
            timer.Start();
            button6.Visible = false;
            button8.Visible = true;
        }

        [Obsolete]
        private void pause(object sender, EventArgs e)
        {
            KitchenController.Suspend();
            timer.Stop();
            button8.Visible = false;
            button12.Visible = true;
        }

        [Obsolete]
        private void resume(object sender, EventArgs e)
        {
            KitchenController.Resume();
            timer.Start();
            button12.Visible = false;
            button8.Visible = true;
        }

        private void speedUp(object sender, EventArgs e)
        {
            second += 10;
        }

        private void slowDown(object sender, EventArgs e)
        {
            second -= 10;
        }

        private void clientMonitoring(object sender, EventArgs e)
        {
            panel6.Visible = false;
        }

        private void postMonitoring(object sender, EventArgs e)
        {
            panel6.Visible = true;
        }

        private void objectMonitoring(object sender, EventArgs e)
        {
            panel6.Visible = false;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDir = Path.GetDirectoryName(exePath);
            DirectoryInfo binDir = Directory.GetParent(exeDir);
            binDir = Directory.GetParent(binDir.FullName);


            string spritePath = binDir.FullName + "\\Resources\\Diningroomtile.png";

            for (int i = 0; i < model.kitchen.map.GetUpperBound(0); i++)
            {
                for (int j = 0; j < model.kitchen.map.GetUpperBound(1); j++)
                { 
                   e.Graphics.DrawImage(Image.FromFile(spritePath), i * KitchenView.FRAME_SIZE, j * KitchenView.FRAME_SIZE);
                }
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDir = Path.GetDirectoryName(exePath);
            DirectoryInfo binDir = Directory.GetParent(exeDir);
            binDir = Directory.GetParent(binDir.FullName);

            string spritePath = binDir.FullName + "\\Resources\\Kitchentile.png";

            for (int i = 0; i < model.kitchen.map.GetUpperBound(0); i++)
            {
                for (int j = 0; j < model.kitchen.map.GetUpperBound(1); j++)
                {
                    e.Graphics.DrawImage(Image.FromFile(spritePath), i * KitchenView.FRAME_SIZE, j * KitchenView.FRAME_SIZE);
                }
            }

            for (int i = 0; i < model.kitchen.map.GetUpperBound(0); i++)
            {
               for (int j = 0; j < model.kitchen.map.GetUpperBound(1); j++)
               {
                   KitchenMaterial material = model.kitchen.map[i, j];

                   if (material != null)
                   {
                        e.Graphics.DrawImage(material.getSprite().getImage(), i * KitchenView.FRAME_SIZE, j * KitchenView.FRAME_SIZE);
                   }
               }
            }

            foreach (Chef chef in model.chefs)
            {
                e.Graphics.DrawImage(chef.getSprite().getImage(), chef.PosX * KitchenView.FRAME_SIZE, chef.PosY * KitchenView.FRAME_SIZE);
            }

            foreach (DeputyChef deputyChef in model.deputyChefs)
            {
                e.Graphics.DrawImage(deputyChef.getSprite().getImage(), deputyChef.PosX * KitchenView.FRAME_SIZE, deputyChef.PosY * KitchenView.FRAME_SIZE);
            }

            foreach (KitchenClerk kitchenClerk in model.kitchenClerks)
            {
                e.Graphics.DrawImage(kitchenClerk.getSprite().getImage(), kitchenClerk.PosX * KitchenView.FRAME_SIZE, kitchenClerk.PosY * KitchenView.FRAME_SIZE);
            }

            foreach (Diver diver in model.divers)
            {
                e.Graphics.DrawImage(diver.getSprite().getImage(), diver.PosX * KitchenView.FRAME_SIZE, diver.PosY * KitchenView.FRAME_SIZE);
            }
        }
    }
}
