using AppRestaurant.Model.Common;
using AppRestaurant.Model.Kitchen.Actors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AppRestaurantTest
{
    [TestClass]
    public class SpriteTest
    {
        [TestMethod]
        public void getSpriteTest()
        {
            Chef chef = new Chef();
            Sprite spriteTest = chef.Sprite;
            Assert.IsNotNull(spriteTest);
        }

        public void setSpriteTest()
        {
            Chef chef = new Chef();

            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDir = Path.GetDirectoryName(exePath);
            DirectoryInfo binDir = Directory.GetParent(exeDir);
            binDir = Directory.GetParent(binDir.FullName);


            string spritePath = binDir.FullName + "\\Resources\\Chef\\";

            Sprite front = new Sprite(spritePath, "front.png");
            chef.Sprite = front;

            Assert.IsNotNull(chef.Sprite);
        }
    }
}
