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
        public int simulationTimeScale { get; set; }
        public int chefNumber { get; set; }
        public int deputyChefNumber { get; set; }
        public int kitchenClerkNumber { get; set; }
        public int diverNumber { get; set; }
        public int cookingFireNumber { get; set; }
        public int fridgeNumber { get; set; }
        public int blenderNumber { get; set; }
        public int ovenNumber { get; set; }

        public Setting(Simulation simulationForm)
        {
            simulation = simulationForm;
            InitializeComponent();
        }

        private void confirm(object sender, EventArgs e)
        {
            simulationTimeScale = Convert.ToInt32(textBox1.Text);

            chefNumber = Convert.ToInt32(textBox2.Text);
            deputyChefNumber = Convert.ToInt32(textBox4.Text);
            kitchenClerkNumber = Convert.ToInt32(textBox3.Text);
            diverNumber = Convert.ToInt32(textBox5.Text);

            blenderNumber = Convert.ToInt32(textBox6.Text);
            ovenNumber = Convert.ToInt32(textBox7.Text);
            cookingFireNumber = Convert.ToInt32(textBox8.Text);
            fridgeNumber = Convert.ToInt32(textBox9.Text);

            simulation.model.setEmployeeConfig(chefNumber, deputyChefNumber, kitchenClerkNumber, diverNumber);
            simulation.model.setMaterialConfig(cookingFireNumber, ovenNumber, blenderNumber, fridgeNumber);
            simulation.model.TIME_SCALE = simulationTimeScale * 1000;
            Close();
        }
    }
}
