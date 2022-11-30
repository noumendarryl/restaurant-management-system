using AppRestaurant.Model.Common;
using AppRestaurant.Model.DiningRoom.Actors;
using System.Collections.Generic;

namespace AppRestaurant.Model.DiningRoom.Elements
{
    public class Square
    {
        private List<Table> tables;
        private List<Waiter> waiters;
        private List<RankChief> rankChiefs;

        public List<Waiter> Waiters { get => waiters; set => waiters = value; }
        public List<RankChief> RankChiefs { get => rankChiefs; set => rankChiefs = value; }
        public List<Table> Tables { get => tables; set => tables = value; }

        public Square()
        {
            this.tables = new List<Table>();
            this.waiters = new List<Waiter>();
            this.rankChiefs = new List<RankChief>();



        //    for (int i = 0; i < Param.WAITER_BY_SQUARE; i++)
        //        this.waiters.Add(new Waiter());
        }
    }
}
