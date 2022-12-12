using AppRestaurant.Model;
using AppRestaurant.Model.kitchen;
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
            Sprite spriteTest = chef.getSprite();
            Assert.IsNotNull(spriteTest);
        }

        public void setSpriteTest()
        {
            Chef chef = new Chef();

            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDir = System.IO.Path.GetDirectoryName(exePath);
            DirectoryInfo binDir = System.IO.Directory.GetParent(exeDir);
            binDir = System.IO.Directory.GetParent(binDir.FullName);


            string spritePath = binDir.FullName + "\\Resources\\Chef\\";

            Sprite front = new Sprite(spritePath, "front.png");
            chef.setSprite(front);

            Assert.IsNotNull(chef.getSprite());
        }
    }
}
