using AppRestaurant.Model.Kitchen.Factory;
using AppRestaurant.Model.Kitchen.Materials;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppRestaurantTest
{
    [TestClass]
    public class KitchenMaterialFactoryTest
    {
        [TestMethod]
        public void CreateCookingFireTest()
        {
            Material cookingFire = KitchenMaterialFactory.createCookingFire();
            Assert.AreEqual<String>(cookingFire.name, "cooking fire");
        }

        public void CreatePanTest()
        {
            Material fridge = KitchenMaterialFactory.createFridge();
            Assert.AreEqual<String>(fridge.name, "Fridge");
    }

        public void CreateOvenTest()
        {
            Material oven = KitchenMaterialFactory.createOven();
            Assert.AreEqual<String>(oven.name, "oven");
         }

        public void CreateBlenderTest()
        {
            Material blender = KitchenMaterialFactory.createBlender();
            Assert.AreEqual<String>(blender.name, "blender");
    
    }
        public void CreateKitchenKnifeTest()
        {
            Material kitchenKnife = KitchenMaterialFactory.createKnife(2);
            Assert.AreEqual<String>(kitchenKnife.name, "kitchen knife");
        }
    }
}
