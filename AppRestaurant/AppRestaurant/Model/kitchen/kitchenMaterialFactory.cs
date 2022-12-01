using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class kitchenMaterialFactory
    {
		private static string spritePath = "/Resources/kitchenMaterials/";

		private static string imageCookingFire = "cookingFire.png";
		private static string imageOven = "oven.png";
		private static string imagePan = "pan.png";
		private static string imageBlender = "blender.png";
		private static string imageKnife = "knife.png";

		private static Sprite cookingFire = new Sprite(spritePath, imageCookingFire);
		private static Sprite oven = new Sprite(spritePath, imageOven);
		private static Sprite pan = new Sprite(spritePath, imagePan);
		private static Sprite blender = new Sprite(spritePath, imageBlender);
		private static Sprite knife = new Sprite(spritePath, imageKnife);

		private static kitchenMaterial COOKING_FIRE;
		private static kitchenMaterial PAN;
		private static kitchenMaterial BLENDER;
		private static kitchenMaterial OVEN;
		private static kitchenMaterial KNIFE;

		/*
		 * Creates a cooking fire
		 * @return The {@link cooking fire} {@link kitchenMaterial}.
		 */
		public static kitchenMaterial createCookingFire()
		{
			//cookingFire.loadImage();
			COOKING_FIRE = new kitchenMaterial("cooking fire", 5, cookingFire);
			COOKING_FIRE.washable = false;
			return COOKING_FIRE;
		}

		/*
		 * Creates an oven
		 * @return The {@link oven} {@link kitchenMaterial}.
		 */
		public static kitchenMaterial createOven()
		{
			//oven.loadImage();
			OVEN = new kitchenMaterial("oven", 1, oven);
			OVEN.washable = false;
			return OVEN;
		}

		/*
		 * Creates a pan
		 * @return The {@link pan} {@link kitchenMaterial}.
		 */
		public static kitchenMaterial createPan()
		{
			//pan.loadImage();
			PAN = new kitchenMaterial("pan", 10, pan);
			return PAN;
		}

		/*
		 * Creates a blender
		 * @return The {@link blender} {@link kitchenMaterial}.
		 */
		public static kitchenMaterial createBlender()
		{
			//blender.loadImage();
			BLENDER = new kitchenMaterial("blender", 1, blender);
			return BLENDER;
		}

		/*
		 * Creates a knife
		 * @return The {@link knife} {@link kitchenMaterial}.
		 */
		public static kitchenMaterial createKnife()
		{
			//knife.loadImage();
			KNIFE = new kitchenMaterial("kitchen knife", 5, knife);
			return KNIFE;
		}
	}
}
