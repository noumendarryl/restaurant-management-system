using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.Common.Move;

namespace AppRestaurant.Model.DiningRoom.Actors
{
    public class HotelMaster
    {
        private List<LineChief> rankChiefs;

        public List<LineChief> RankChiefs
        {
            get => rankChiefs;
            set => rankChiefs = value;
        }

        public HotelMaster() : base()
        {
            this.rankChiefs = new List<LineChief>();
            this.rankChiefs.Add(new LineChief());
        }

    }
}
