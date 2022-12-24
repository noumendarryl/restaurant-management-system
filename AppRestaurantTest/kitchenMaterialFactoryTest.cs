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
            KitchenMaterial cookingFire = KitchenMaterialFactory.createCookingFire();
            Assert.AreEqual<String>(cookingFire.name, "cooking fire");
        }

        public void CreatePanTest()
        {
            KitchenMaterial fridge = KitchenMaterialFactory.createFridge();
            Assert.AreEqual<String>(fridge.name, "Fridge");
    }

        public void CreateOvenTest()
        {
            KitchenMaterial oven = KitchenMaterialFactory.createOven();
            Assert.AreEqual<String>(oven.name, "oven");
         }

        public void CreateBlenderTest()
        {
            KitchenMaterial blender = KitchenMaterialFactory.createBlender();
            Assert.AreEqual<String>(blender.name, "blender");
    
    }
        public void CreateKitchenKnifeTest()
        {
            KitchenMaterial kitchenKnife = KitchenMaterialFactory.createKnife(2);
            Assert.AreEqual<String>(kitchenKnife.name, "kitchen knife");
        }
    }
}
