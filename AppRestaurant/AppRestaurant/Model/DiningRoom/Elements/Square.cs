using AppRestaurant.Model.DiningRoom.Actors;
using System.Collections.Generic;

namespace AppRestaurant.Model.DiningRoom.Elements
{
    public class Square
    {
        private List<Line> lines;
        private List<Waiter> waiters;
        private List<RankChief> rankChiefs;

        public List<Waiter> Waiters { get => waiters; set => waiters = value; }
        public List<RankChief> RankChiefs { get => rankChiefs; set => rankChiefs = value; }
        public List<Line> Lines { get => lines; set => lines = value; }

        public Square()
        {
            this.waiters = new List<Waiter>();
            this.rankChiefs = new List<RankChief>();
            this.lines = new List<Line>();


        //    for (int i = 0; i < Param.WAITER_BY_SQUARE; i++)
        //        this.waiters.Add(new Waiter());
        }
    }
}
