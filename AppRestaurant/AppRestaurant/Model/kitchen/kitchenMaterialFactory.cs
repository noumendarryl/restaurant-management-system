using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
	public class kitchenMaterialFactory
	{
		private static kitchenMaterial COOKING_FIRE;
		private static kitchenMaterial FRIDGE;
		private static kitchenMaterial BLENDER;
		private static kitchenMaterial OVEN;
		private static kitchenMaterial KNIFE;


		private static string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
		private static string exeDir = System.IO.Path.GetDirectoryName(exePath);
		private static DirectoryInfo binDir = System.IO.Directory.GetParent(System.IO.Directory.GetParent(exeDir).FullName);

		//private static string spritePath = "C:\\Users\\NOUMEN DARRYL\\Documents\\prog-sys-obj\\AppRestaurant\\AppRestaurant\\Resources\\kitchenMaterials\\";

		private static string spritePath = binDir.FullName + "\\Resources\\kitchenMaterials\\";

		private static string imageCookingFire = "cookingFire.png";
		private static string imageOven = "oven.png";
		private static string imageFridge = "fridge.png";
		private static string imageBlender = "blender.png";
		private static string imageKnife = "knife.png";

		private static Sprite cookingFire = new Sprite(spritePath, imageCookingFire);
		private static Sprite oven = new Sprite(spritePath, imageOven);
		private static Sprite fridge = new Sprite(spritePath, imageFridge);
		private static Sprite blender = new Sprite(spritePath, imageBlender);
		private static Sprite knife = new Sprite(spritePath, imageKnife);

		/*
		 * Creates a cooking fire
		 * @return The {@link cooking fire} {@link kitchenMaterial}.
		 */
		public static kitchenMaterial createCookingFire()
		{
			COOKING_FIRE = new kitchenMaterial("cooking fire", 5, cookingFire);
			cookingFire.loadImage();
			COOKING_FIRE.setSprite(cookingFire);
			COOKING_FIRE.washable = false;
			return COOKING_FIRE;
		}

		/*
		 * Creates an oven
		 * @return The {@link oven} {@link kitchenMaterial}.
		 */
		public static kitchenMaterial createOven()
		{
			OVEN = new kitchenMaterial("oven", 5, oven);
			oven.loadImage();
			OVEN.setSprite(oven);
			OVEN.washable = false;
			return OVEN;
		}

		/*
		 * Creates a fridge
		 * @return The {@link fridge} {@link kitchenMaterial}.
		 */
		public static kitchenMaterial createFridge()
		{
			FRIDGE = new kitchenMaterial("fridge", 1, fridge);
			fridge.loadImage();
			FRIDGE.setSprite(fridge);
			return FRIDGE;
		}

		/*
		 * Creates a blender
		 * @return The {@link blender} {@link kitchenMaterial}.
		 */
		public static kitchenMaterial createBlender()
		{
			BLENDER = new kitchenMaterial("blender", 1, blender);
			//blender.loadImage();
			//BLENDER.setSprite(blender);
			return BLENDER;
		}

		/*
		 * Creates a knife
		 * @return The {@link knife} {@link kitchenMaterial}.
		 */
		public static kitchenMaterial createKnife(int quantity)
		{
			KNIFE = new kitchenMaterial("kitchen knife", quantity, knife);
			//knife.loadImage();
			//KNIFE.setSprite(cookingFire);
			return KNIFE;
		}
	}
}
