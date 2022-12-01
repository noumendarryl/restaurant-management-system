using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using AppRestaurant.Model.kitchen;
using System.Windows.Forms;

namespace AppRestaurant.View
{
    public class kitchenView : IObserver
    {
        private static int FRAME_SIZE = 32;
        public Form1 form { get; set; }

        public kitchenView(kitchenModel  model)
        {
            form = new Form1(model);
        }

        public void UpdateWithMoves(int oldX, int oldY, int newX, int newY)
        {
            form.Invoke((MethodInvoker)delegate
            {
                form.Invalidate(new Rectangle(oldX, oldY, FRAME_SIZE, FRAME_SIZE));
                form.Invalidate(new Rectangle(newX, newY, FRAME_SIZE, FRAME_SIZE));
                form.Update();
            });

            Thread.Sleep(40);
        }
    }
}
