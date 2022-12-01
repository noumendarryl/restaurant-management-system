using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.DiningRoom.Elements
{
    public class Line
    {
        private List<Table> tables;
        public List<Table> Tables { get => tables; set => tables = value; }

        public Line()
        {
            this.tables = new List<Table>();
        }
    }
}
