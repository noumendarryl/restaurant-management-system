using AppRestaurant.View.Common;
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
    public partial class Setting : Form
    {
        private Simulation simulation;
        private Monitoring monitoring;
        public int simulationTimeScale { get; set; }
        public int simulationTotalTime { get; set; }
        public int chefNumber { get; set; }
        public int deputyChefNumber { get; set; }
        public int kitchenClerkNumber { get; set; }
        public int diverNumber { get; set; }
        public int cookingFireNumber { get; set; }
        public int fridgeNumber { get; set; }
        public int blenderNumber { get; set; }
        public int ovenNumber { get; set; }

        // Initialize the setting form
        public Setting(Simulation simulation, Monitoring monitoring)
        {
            this.simulation = simulation;
            this.monitoring = monitoring;
            InitializeComponent();
        }

        // Confirm the application parameters entered by the user
        private void Confirm(object sender, EventArgs e)
        {
            // General Paramters
            simulationTimeScale = Convert.ToInt32(siticoneNumericUpDown3.Value);
            simulationTotalTime = Convert.ToInt32(siticoneNumericUpDown2.Value);

            // Kitchen Staff Parameters
            chefNumber = Convert.ToInt32(siticoneNumericUpDown20.Value);
            deputyChefNumber = Convert.ToInt32(siticoneNumericUpDown9.Value);
            kitchenClerkNumber = Convert.ToInt32(siticoneNumericUpDown11.Value);
            diverNumber = Convert.ToInt32(siticoneNumericUpDown16.Value);

            // KitchenWare Parameters
            blenderNumber = Convert.ToInt32(siticoneNumericUpDown19.Value);
            ovenNumber = Convert.ToInt32(siticoneNumericUpDown15.Value);
            cookingFireNumber = Convert.ToInt32(siticoneNumericUpDown10.Value);
            fridgeNumber = Convert.ToInt32(siticoneNumericUpDown18.Value);

            simulation.model.TIME_SCALE = simulationTimeScale * 1000;
            simulation.totalSeconds = simulationTotalTime;
            simulation.displayTime(simulationTotalTime);
            simulation.siticoneTrackBar1.Maximum = simulationTotalTime;
            simulation.timer.Interval = 1000;
            simulation.timer.Elapsed += simulation.onTimeEvent;
            simulation.model.setEmployeeConfig(chefNumber, deputyChefNumber, kitchenClerkNumber, diverNumber);
            simulation.model.setMaterialConfig(cookingFireNumber, ovenNumber, blenderNumber, fridgeNumber);
            monitoring.setRestaurantStaff(chefNumber, deputyChefNumber, kitchenClerkNumber, diverNumber);
        }

        // Set black as the fore color of the confirm button when mouse hovers
        private void siticoneButton1_MouseEnter(object sender, EventArgs e)
        {
            siticoneButton1.ForeColor = Color.Black;
        }

        // Set black as the fore color of the confirm button when mouse leaves
        private void siticoneButton1_MouseLeave(object sender, EventArgs e)
        {
            siticoneButton1.ForeColor = Color.FromArgb(253, 138, 114);
        }
    }
}
