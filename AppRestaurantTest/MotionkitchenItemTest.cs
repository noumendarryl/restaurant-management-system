using AppRestaurant.Model.Kitchen.Actors;
using AppRestaurant.Model.Kitchen.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppRestaurantTest
{
    [TestClass]
    public class MotionKitchenItemTest
    {
        [TestMethod]
        public void moveLeftTest()
        {
            Chef chef = new Chef();
            int PastX = chef.PosX;

            chef.moveLeft();
            Assert.AreEqual<int>(chef.PosX, PastX - MotionKitchenItem.speed);
        }

        public void moveRightTest()
        {
            Chef chef = new Chef();
            int PastX = chef.PosX; 
    
            chef.moveRight();
            Assert.AreEqual<int>(chef.PosX, PastX + MotionKitchenItem.speed);
        }
        public void moveDownTest()
        {
            Chef chef = new Chef();
            int PastY = chef.PosY;
    
            chef.moveDown();
            Assert.AreEqual<int>(chef.PosY, PastY + MotionKitchenItem.speed);
        }
        public void moveUpTest()
        {
            Chef chef = new Chef();
            int PastY = chef.PosY;
    
            chef.moveUp();
            Assert.AreEqual<int>(chef.PosY, PastY - MotionKitchenItem.speed);
        }
    }
}
