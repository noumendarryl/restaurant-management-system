using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen.DAO;
using AppRestaurant.Model.Kitchen.Items;

namespace AppRestaurant.Model.Kitchen.Materials
{
    public enum MaterialState
    {
        Clean,
        InUse,
        Dirty
    }

    public enum MaterialType
    {
        Kitchen,
        DiningRoom,
        Common
    }

    public class Material : MotionlessKitchenItem
    {
        private MaterialState state;
        private MaterialType type;

        public string name { get; set; }
        public int quantity { get; set; }
        public bool washable { get; set; }
        public MaterialState State { get => state; set => state = value; }
        public MaterialType Type { get => type; set => type = value; }

        public Material(string name, int quantity)
        {
            this.name = name;
            this.quantity = quantity;
            state = MaterialState.Clean;
            type = MaterialType.Common;
            washable = true;
        }
    }
}
