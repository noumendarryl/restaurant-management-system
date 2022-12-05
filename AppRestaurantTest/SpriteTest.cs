using AppRestaurant.Model;
using AppRestaurant.Model.kitchen;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppRestaurantTest
{
    [TestClass]
    public class SpriteTest
    {
        [TestMethod]
        public void getSpriteTest()
        {
            Chef chef = new Chef();
            Sprite spriteTest = chef.getSprite();
            Assert.IsNotNull(spriteTest);
        }

        public void setSpriteTest()
        {
            Chef chef = new Chef();
            Sprite front = new Sprite("C:\\Users\\NOUMEN DARRYL\\Documents\\prog-sys-obj\\AppRestaurant\\AppRestaurant\\Resources\\Chef\\", "front.png");
            chef.setSprite(front);

            Assert.IsNotNull(chef.getSprite());
        }
    }
}
