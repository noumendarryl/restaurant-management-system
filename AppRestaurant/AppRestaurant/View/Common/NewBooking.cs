using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppRestaurant.Model.DB;

namespace AppRestaurant.View.Common
{
    public partial class NewBooking : Form
    {
        public DBAccess da;

        public NewBooking()
        {
            //da = new DBAccess();
            InitializeComponent();
        }

        // Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void iconbutton6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
