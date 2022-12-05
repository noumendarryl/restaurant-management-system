using AppRestaurant.Model.kitchen;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AppRestaurantTest
{
    [TestClass]
    public class kitchenMaterialFactoryTest
    {
        [TestMethod]
        public void CreateCookingFireTest()
        {
            kitchenMaterial cookingFire = kitchenMaterialFactory.createCookingFire();
            Assert.AreEqual<String>(cookingFire.name, "cooking fire");
        }

        public void CreatePanTest()
        {
            kitchenMaterial fridge = kitchenMaterialFactory.createFridge();
            Assert.AreEqual<String>(fridge.name, "Fridge");
    }

        public void CreateOvenTest()
        {
            kitchenMaterial oven = kitchenMaterialFactory.createOven();
            Assert.AreEqual<String>(oven.name, "oven");
         }

        public void CreateBlenderTest()
        {
            kitchenMaterial blender = kitchenMaterialFactory.createBlender();
            Assert.AreEqual<String>(blender.name, "blender");
    
    }
        public void CreateKitchenKnifeTest()
        {
            kitchenMaterial kitchenKnife = kitchenMaterialFactory.createKnife();
            Assert.AreEqual<String>(kitchenKnife.name, "kitchen knife");
        }
    }
}
