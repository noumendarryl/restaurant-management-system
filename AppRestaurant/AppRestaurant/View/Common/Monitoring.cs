using Siticone.Desktop.UI.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AppRestaurant.View.Common
{
    public partial class Monitoring : Form
    {
        float angle = 0;
        float sweep = 0;
        Dictionary<Color, int> pieCharts;
        int total = 0;

        public Monitoring()
        {
            InitializeComponent();
            customizeDesign();
            pieCharts = new Dictionary<Color, int>();
            pieCharts.Add(Color.FromArgb(172, 126, 241), 26);
            pieCharts.Add(Color.FromArgb(63, 81, 181), 5);
            pieCharts.Add(Color.FromArgb(1, 135, 144), 26);
            pieCharts.Add(Color.FromArgb(24, 161, 251), 48);
            pieCharts.Add(Color.FromArgb(253, 138, 114), 35);
            pieCharts.Add(Color.FromArgb(255, 152, 0), 48);
            pieCharts.Add(Color.FromArgb(234, 75, 51), 3);
            pieCharts.Add(Color.FromArgb(249, 88, 155), 53);
            pieCharts.Add(Color.FromArgb(95, 77, 221), 14);
            pieCharts.Add(Color.FromArgb(249, 118, 176), 80);
            total += pieCharts.Values.Sum();
        }

        private void siticonePanel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(38, 38, 82));
            Rectangle rc = new Rectangle(0, 0, siticonePanel1.Width - 1, siticonePanel1.Height - 1);

            foreach (KeyValuePair<Color, int> piechart in pieCharts)
            {
                sweep = 360f * piechart.Value / total;
                e.Graphics.FillPie(new SolidBrush(piechart.Key), rc, angle, sweep);
                angle += sweep;
            }
        }

        private void siticonePanel2_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, siticonePanel2.Width - 1, siticonePanel2.Height - 1);
            e.Graphics.FillPie(new SolidBrush(Color.FromArgb(38, 38, 82)), rect, 0, 180);
        }

        private void customizeDesign()
        {
            panel20.Visible = false;
            panel22.Visible = false;
            panel24.Visible = false;
            panel26.Visible = false;
            panel28.Visible = false;
            panel30.Visible = false;
            panel32.Visible = false;
            panel34.Visible = false;
        }

        private void hideSubPanel()
        {
            if (panel20.Visible == true)
            {
                panel20.Visible = false;
                siticoneButton6.Visible = false;
                siticoneButton9.Visible = true;
            }
            if (panel22.Visible == true)
            {
                panel22.Visible = false;
                siticoneButton10.Visible = false;
                siticoneButton1.Visible = true;
            }
            if (panel24.Visible == true)
            {
                panel24.Visible = false;
                siticoneButton11.Visible = false;
                siticoneButton2.Visible = true;
            }
            if (panel26.Visible == true)
            {
                panel26.Visible = false;
                siticoneButton12.Visible = false;
                siticoneButton5.Visible = true;
            }
            if (panel28.Visible == true)
            {
                panel28.Visible = false;
                siticoneButton13.Visible = false;
                siticoneButton4.Visible = true;
            }
            if (panel30.Visible == true)
            {
                panel30.Visible = false;
                siticoneButton14.Visible = false;
                siticoneButton3.Visible = true;
            }
            if (panel32.Visible == true)
            {
                panel32.Visible = false;
                siticoneButton15.Visible = false;
                siticoneButton8.Visible = true;
            }
            if (panel34.Visible == true)
            {
                panel34.Visible = false;
                siticoneButton7.Visible = false;
                siticoneButton16.Visible = true;
            }
        }

        private void showSubPanel(Panel panel)
        {
            if (panel.Visible == false)
            {
                hideSubPanel();
                panel.Visible = true;
            }
            else
                panel.Visible = false;
        }

        private void ShowAndHide(SiticoneButton btn1, SiticoneButton btn2)
        {
            btn1.Visible = false;
            btn2.Visible = true;
        }

        private void siticoneButton9_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton9, siticoneButton6);
            showSubPanel(panel20);
        }

        private void siticoneButton6_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton6, siticoneButton9);
            hideSubPanel();
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton1, siticoneButton10);
            showSubPanel(panel22);
        }

        private void siticoneButton10_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton10, siticoneButton1);
            hideSubPanel();
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton2, siticoneButton11);
            showSubPanel(panel24);
        }

        private void siticoneButton11_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton11, siticoneButton2);
            showSubPanel(panel24);
        }

        private void siticoneButton5_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton5, siticoneButton12);
            showSubPanel(panel26);
        }

        private void siticoneButton12_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton12, siticoneButton5);
            showSubPanel(panel26);
        }

        private void siticoneButton4_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton4, siticoneButton13);
            showSubPanel(panel28);
        }

        private void siticoneButton13_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton13, siticoneButton4);
            showSubPanel(panel28);
        }

        private void siticoneButton3_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton3, siticoneButton14);
            showSubPanel(panel30);
        }

        private void siticoneButton14_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton14, siticoneButton3);
            showSubPanel(panel30);
        }

        private void siticoneButton8_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton8, siticoneButton15);
            showSubPanel(panel32);
        }

        private void siticoneButton15_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton15, siticoneButton8);
            showSubPanel(panel32);
        }

        private void siticoneButton7_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton7, siticoneButton16);
            showSubPanel(panel34);
        }

        private void siticoneButton16_Click(object sender, EventArgs e)
        {
            ShowAndHide(siticoneButton16, siticoneButton7);
            showSubPanel(panel34);
        }

        public void setRestaurantStaff(int chefNumber, int deputyChefNumber, int kitchenClerkNumber, int diverNumber)
        {
            if (chefNumber < 10)
            {
                siticoneHtmlLabel31.Text = "0" + chefNumber.ToString();
                siticoneHtmlLabel77.Text = "0" + chefNumber.ToString();
            }
            else
            {
                siticoneHtmlLabel31.Text = chefNumber.ToString();
                siticoneHtmlLabel77.Text = chefNumber.ToString();
            }

            if (deputyChefNumber < 10)
            {
                siticoneHtmlLabel18.Text = "0" + deputyChefNumber.ToString();
                siticoneHtmlLabel48.Text = "0" + deputyChefNumber.ToString();
            }
            else
            {
                siticoneHtmlLabel18.Text = deputyChefNumber.ToString();
                siticoneHtmlLabel48.Text = deputyChefNumber.ToString();
            }

            if (kitchenClerkNumber < 10)
            {
                siticoneHtmlLabel94.Text = "0" + kitchenClerkNumber.ToString();
                siticoneHtmlLabel85.Text = "0" + kitchenClerkNumber.ToString();
            }
            else
            {
                siticoneHtmlLabel94.Text = kitchenClerkNumber.ToString();
                siticoneHtmlLabel85.Text = kitchenClerkNumber.ToString();
            }

            if (diverNumber < 10)
            {
                siticoneHtmlLabel92.Text = "0" + diverNumber.ToString();
                siticoneHtmlLabel89.Text = "0" + diverNumber.ToString();
            }
            else
            {
                siticoneHtmlLabel92.Text = diverNumber.ToString();
                siticoneHtmlLabel89.Text = diverNumber.ToString();
            }
        }

        public void fillStats(int customerCount, int OrderCount)
        {
            if (customerCount < 10)
            {
                siticoneHtmlLabel9.Text = "00" + customerCount.ToString();
            } 
            else if (10 < customerCount && customerCount < 100) {
                siticoneHtmlLabel9.Text = "0" + customerCount.ToString();
            }
            else
            {
                siticoneHtmlLabel9.Text = customerCount.ToString();
            }

            if (OrderCount < 10)
            {
                siticoneHtmlLabel10.Text = "00" + OrderCount.ToString();
            }
            else if (10 < OrderCount && OrderCount < 100)
            {
                siticoneHtmlLabel10.Text = "0" + OrderCount.ToString();
            }
            else
            {
                siticoneHtmlLabel10.Text = OrderCount.ToString();
            }
        }
    }
}
