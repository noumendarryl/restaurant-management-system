using RestaurantG5.Model.Salle.Role;

namespace RestaurantG5.Model.Salle.Components
{
    public class SalleModel
    {
        private HotelMaster hotelMaster;
        private static Commis commis;

        static SalleModel()
        {
            commis = new Commis();
        }

        public SalleModel()
        {
            hotelMaster = new HotelMaster();
        }

        public HotelMaster HotelMaster { get => hotelMaster; set => hotelMaster = value; }
        public static Commis Commis { get => commis; set => commis = value; }
    }
}
