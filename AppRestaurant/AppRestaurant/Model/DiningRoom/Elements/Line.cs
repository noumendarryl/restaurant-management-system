using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppRestaurant.Model.DiningRoom.Actors;

namespace AppRestaurant.Model.DiningRoom.Elements
{
    public class Line
    {
        private List<Table> tables;
        //private LineChief lineChief;

        public List<Table> Tables { get => tables; set => tables = value; }
        //public LineChief LineChief { get => lineChief; set => lineChief = value; }
        public Line(int nbTables, int nbSeats)
        {
            for (int i = 0; i < nbTables; i++)
            {
                tables.Add(new Table(nbSeats));
            }
            //    this.tables = new List<Table>();
        }
    }
}
