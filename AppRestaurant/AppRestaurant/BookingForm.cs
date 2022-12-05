using AppRestaurant.Model.DB;
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
    public partial class BookingForm : Form
    {
        public DBActions dBActions { get; set; }
        public string clientName { get; set; }
        public int nbPeople { get; set; }
        public DateTime hour { get; set; }
        public BookingForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clientName = Convert.ToString(textBox1.Text);
            nbPeople = Convert.ToInt32(numericUpDown1.Value);
            hour = Convert.ToDateTime(dateTimePicker1.Value);

            dBActions.setBooking(clientName, nbPeople, hour);
            this.Close();
        }
    }
}
