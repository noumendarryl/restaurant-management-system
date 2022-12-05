using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.Common;
using AppRestaurant.Model;
using AppRestaurant.Model.DiningRoom.Move;

namespace AppRestaurant.Model.Common
{
    public class MenuCard : Equipment, IPosition
    {
        private DateTime date;
        private Menu menu;
        private int posX;
        private int posY;

        public MenuCard(Menu menu, DateTime date)
        {
            this.menu = menu;
            this.date = date;
            this.State = EquipmentState.Available;
        }
        public MenuCard(Menu menu)
        {
            this.menu = menu;
            this.State = EquipmentState.Available;
        }
        public int PosX
        {
            get
            {
                return posX;
            }
            set
            {
                posX = value >= 0 ? value : 0;
            }
        }

        public int PosY
        {
            get
            {
                return posY;
            }
            set
            {
                posY = value >= 0 ? value : 0;
            }
        }
        public Menu Menu { get; set; }
        public DateTime Date { get => date; set => date = value; }
    }

}
