﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.Kitchen.Materials;

namespace AppRestaurant.Model.Kitchen.Factory
{
	public class KitchenMaterialFactory
	{
		private static KitchenMaterial COOKING_FIRE;
		private static KitchenMaterial FRIDGE;
		private static KitchenMaterial BLENDER;
		private static KitchenMaterial OVEN;
		private static KitchenMaterial KNIFE;

		private static string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
		private static string exeDir = Path.GetDirectoryName(exePath);
		private static DirectoryInfo binDir = Directory.GetParent(Directory.GetParent(exeDir).FullName);

		private static string spritePath = binDir.FullName + "\\Resources\\KitchenMaterials\\";

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
		 * @return The {@link cooking fire} {@link KitchenMaterial}.
		 */
		public static KitchenMaterial createCookingFire()
		{
			COOKING_FIRE = new KitchenMaterial("cooking fire", 5, cookingFire);
			cookingFire.loadImage();
			COOKING_FIRE.Sprite = cookingFire;
			COOKING_FIRE.washable = false;
			return COOKING_FIRE;
		}

		/*
		 * Creates an oven
		 * @return The {@link oven} {@link KitchenMaterial}.
		 */
		public static KitchenMaterial createOven()
		{
			OVEN = new KitchenMaterial("oven", 5, oven);
			oven.loadImage();
			OVEN.Sprite = oven;
			OVEN.washable = false;
			return OVEN;
		}

		/*
		 * Creates a fridge
		 * @return The {@link fridge} {@link KitchenMaterial}.
		 */
		public static KitchenMaterial createFridge()
		{
			FRIDGE = new KitchenMaterial("fridge", 1, fridge);
			fridge.loadImage();
			FRIDGE.Sprite = fridge;
			return FRIDGE;
		}

		/*
		 * Creates a blender
		 * @return The {@link blender} {@link KitchenMaterial}.
		 */
		public static KitchenMaterial createBlender()
		{
			BLENDER = new KitchenMaterial("blender", 1, blender);
			//blender.loadImage();
			//BLENDER.Sprite = blender;
			return BLENDER;
		}

		/*
		 * Creates a knife
		 * @return The {@link knife} {@link KitchenMaterial}.
		 */
		public static KitchenMaterial createKnife(int quantity)
		{
			KNIFE = new KitchenMaterial("kitchen knife", quantity, knife);
			//knife.loadImage();
			//KNIFE.Sprite = cookingFire;
			return KNIFE;
		}
	}
}
