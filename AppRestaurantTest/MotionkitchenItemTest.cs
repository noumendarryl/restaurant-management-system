using AppRestaurant.Model.kitchen;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppRestaurantTest
{
    [TestClass]
    public class MotionkitchenItemTest
    {
        [TestMethod]
        public void moveLeftTest()
        {
            Chef chef = new Chef();
            int PastX = chef.posX;

            chef.moveLeft();
            Assert.AreEqual<int>(chef.posX, PastX - MotionkitchenItem.speed);
        }

        public void moveRightTest()
        {
            Chef chef = new Chef();
            int PastX = chef.posX; 
    
            chef.moveRight();
            Assert.AreEqual<int>(chef.posX, PastX + MotionkitchenItem.speed);
        }
        public void moveDownTest()
        {
            Chef chef = new Chef();
            int PastY = chef.posY;
    
            chef.moveDown();
            Assert.AreEqual<int>(chef.posY, PastY + MotionkitchenItem.speed);
        }
        public void moveUpTest()
        {
            Chef chef = new Chef();
            int PastY = chef.posY;
    
            chef.moveUp();
            Assert.AreEqual<int>(chef.posY, PastY - MotionkitchenItem.speed);
        }
    }
}
