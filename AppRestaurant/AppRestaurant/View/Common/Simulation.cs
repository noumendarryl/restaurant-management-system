using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using AppRestaurant.Controller.DiningRoom;
using AppRestaurant.Controller.Kitchen;
using AppRestaurant.Model.Kitchen;
using AppRestaurant.Model.Kitchen.Actors;
using AppRestaurant.Model.Kitchen.Materials;
using AppRestaurant.View.Kitchen;

namespace AppRestaurant.View.Common
{
    public partial class Simulation : Form
    {
        public KitchenModel model { get; set; }
        public StreamWriter writer { get; set; }
        public System.Timers.Timer timer { get; set; }
        public string applicationPath { get; set; }
        public string saveFilePath { get; set; }
        public int hour { get; set; }
        public int minute { get; set; }
        public int second { get; set; }
        public int currentTotalSeconds { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
        public int totalSeconds { get; set; }

        public Simulation(KitchenModel model)
        {
            this.model = model;
            currentTotalSeconds = 0;
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += onTimeElapsed;
            timer.Elapsed += onTimeEvent;
            InitializeComponent();
        }

        private void displayTime()
        {
            if (totalSeconds > 0)
            {
                minutes = totalSeconds / 60;
                seconds = totalSeconds - (minutes * 60);
            }
            siticoneHtmlLabel3.Text = string.Format("{0}:{1}:{2}", hours.ToString().PadLeft(2, '0'), minutes.ToString().PadLeft(2, '0'), seconds.ToString().PadLeft(2, '0'));
        }

        private void onTimeEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                if (totalSeconds > 0)
                {
                    totalSeconds--;
                    minutes = totalSeconds / 60;
                    seconds = totalSeconds - (minutes * 60);
                }
                siticoneHtmlLabel3.Text = string.Format("{0}:{1}:{2}", hours.ToString().PadLeft(2, '0'), minutes.ToString().PadLeft(2, '0'), seconds.ToString().PadLeft(2, '0'));
            }));
        }

        private void onTimeElapsed(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                currentTotalSeconds++;
                if (currentTotalSeconds > 0)
                {
                    hour = currentTotalSeconds / 3600;
                    minute = currentTotalSeconds / 60;
                    second = currentTotalSeconds - (minute * 60) - (hour * 3600);
                }
                updateSlider();
                siticoneHtmlLabel2.Text = string.Format("{0}:{1}:{2}", hour.ToString().PadLeft(2, '0'), minute.ToString().PadLeft(2, '0'), second.ToString().PadLeft(2, '0'));
            }));
        }

        private void updateSlider()
        {
            if (currentTotalSeconds != totalSeconds)
            {
                siticoneTrackBar1.Value = currentTotalSeconds;
            } else
            {
                siticoneTrackBar1.Value = totalSeconds;
                timer.Stop();
            }
        }

        private void siticoneTrackBar1_ValueChanged(object sender, EventArgs e)
        {
            currentTotalSeconds = siticoneTrackBar1.Value;
        }

        private void Start(object sender, EventArgs e)
        {
            DiningRoomController.Run();
            KitchenController.Start();
            timer.Start();
            iconButton3.Visible = false;
            iconButton4.Visible = true;
            KitchenView.monitoring.fillStats(KitchenController.customerCount, KitchenController.OrderCount);
        }

        [Obsolete]
        private void Pause(object sender, EventArgs e)
        {
            KitchenController.Suspend();
            timer.Stop();
            iconButton4.Visible = false;
            iconButton7.Visible = true;
        }

        [Obsolete]
        private void Resume(object sender, EventArgs e)
        {
            KitchenController.Resume();
            timer.Start();
            iconButton4.Visible = true;
            iconButton7.Visible = false;
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

                    KitchenView.setting.simulationTimeScale = Convert.ToInt32(lines.ElementAt(0));
                    totalSeconds = Convert.ToInt32(lines.ElementAt(1));

                    KitchenView.setting.chefNumber = Convert.ToInt32(lines.ElementAt(2));
                    KitchenView.setting.deputyChefNumber = Convert.ToInt32(lines.ElementAt(3));
                    KitchenView.setting.kitchenClerkNumber = Convert.ToInt32(lines.ElementAt(4));
                    KitchenView.setting.diverNumber = Convert.ToInt32(lines.ElementAt(5));

                    KitchenView.setting.blenderNumber = Convert.ToInt32(lines.ElementAt(6));
                    KitchenView.setting.ovenNumber = Convert.ToInt32(lines.ElementAt(7));
                    KitchenView.setting.cookingFireNumber = Convert.ToInt32(lines.ElementAt(8));
                    KitchenView.setting.fridgeNumber = Convert.ToInt32(lines.ElementAt(9));

                    model.TIME_SCALE = KitchenView.setting.simulationTimeScale * 1000;
                    displayTime();
                    siticoneTrackBar1.Maximum = totalSeconds;
                    model.setEmployeeConfig(KitchenView.setting.chefNumber, KitchenView.setting.deputyChefNumber, KitchenView.setting.kitchenClerkNumber, KitchenView.setting.diverNumber);
                    model.setMaterialConfig(KitchenView.setting.cookingFireNumber, KitchenView.setting.ovenNumber, KitchenView.setting.blenderNumber, KitchenView.setting.fridgeNumber);
                    KitchenView.monitoring.setRestaurantStaff(KitchenView.setting.chefNumber, KitchenView.setting.deputyChefNumber, KitchenView.setting.kitchenClerkNumber, KitchenView.setting.diverNumber);
                    Refresh();
                }
                catch (Exception err)
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
                writer.WriteLine(KitchenView.setting.simulationTotalTime.ToString());

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

        private void panel4_Paint(object sender, PaintEventArgs e)
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
                    Material material = model.kitchen.map[i, j];

                    if (material != null)
                    {
                        e.Graphics.DrawImage(material.Sprite.getImage(), i * KitchenView.FRAME_SIZE, j * KitchenView.FRAME_SIZE);
                    }
                }
            }

            foreach (Chef chef in model.chefs)
            {
                e.Graphics.DrawImage(chef.Sprite.getImage(), chef.PosX * KitchenView.FRAME_SIZE, chef.PosY * KitchenView.FRAME_SIZE);
            }

            foreach (DeputyChef deputyChef in model.deputyChefs)
            {
                e.Graphics.DrawImage(deputyChef.Sprite.getImage(), deputyChef.PosX * KitchenView.FRAME_SIZE, deputyChef.PosY * KitchenView.FRAME_SIZE);
            }

            foreach (KitchenClerk kitchenClerk in model.kitchenClerks)
            {
                e.Graphics.DrawImage(kitchenClerk.Sprite.getImage(), kitchenClerk.PosX * KitchenView.FRAME_SIZE, kitchenClerk.PosY * KitchenView.FRAME_SIZE);
            }

            foreach (Diver diver in model.divers)
            {
                e.Graphics.DrawImage(diver.Sprite.getImage(), diver.PosX * KitchenView.FRAME_SIZE, diver.PosY * KitchenView.FRAME_SIZE);
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDir = Path.GetDirectoryName(exePath);
            DirectoryInfo binDir = Directory.GetParent(exeDir);
            binDir = Directory.GetParent(binDir.FullName);


            string spritePath = binDir.FullName + "\\Resources\\Dinningroomtile.png";

            for (int i = 0; i < model.kitchen.map.GetUpperBound(0); i++)
            {
                for (int j = 0; j < model.kitchen.map.GetUpperBound(1); j++)
                {
                    e.Graphics.DrawImage(Image.FromFile(spritePath), i * KitchenView.FRAME_SIZE, j * KitchenView.FRAME_SIZE);
                }
            }
        }
    }
}
