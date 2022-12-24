using AppRestaurant.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Controller.Kitchen.Observer;
using AppRestaurant.Model.Kitchen.DAO;
using AppRestaurant.Model.Kitchen.Actors;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.Kitchen.Materials;
using AppRestaurant.Model.Kitchen.Factory;

namespace AppRestaurant.Model.Kitchen
{
	public class KitchenModel
	{
		private List<IObserver> observers;

		/*
		* The kitchen.
		*/
		public Kitchen kitchen { get; set; }

		/*
		* The time scale.
		*/
		public int TIME_SCALE;

		/*
		* The dao.
		*/
		public DAOEntity<Entity> dao { get; set; }

		/*
		* The chef
		*/
		public int chefNumber;
		public Chef[] chefs { get; set; }

		/*
		* The deputyChef
		*/
		public int deputyChefNumber;
		public DeputyChef[] deputyChefs { get; set; }

		/*
		* The kitchenClerk
		*/
		public int kitchenClerkNumber;
		public KitchenClerk[] kitchenClerks { get; set; }

		/*
		* The diver
		*/
		public int diverNumber;
		public Diver[] divers { get; set; }

		/*
		* The recipes
		*/
		public Recipe[] recipes { get; set; }

		/*
		* The cooking fire 
		*/
		public int cookingFireNumber;
		public KitchenMaterial cookingFire { get; set; }

		/*
		* The oven 
		*/
		public int ovenNumber;
		public KitchenMaterial oven { get; set; }

		/*
		* The blender
		*/
		public int blenderNumber;
		public KitchenMaterial blender { get; set; }

		/*
		* The fridge
		*/
		public int fridgeNumber;
		public KitchenMaterial fridge { get; set; }

		/*
		* The knife
		*/
		public int knifeNumber;
		public KitchenMaterial knife { get; set; }

		//public Random random = new Random();

		public KitchenModel()
		{
			kitchen = new Kitchen();
			observers = new List<IObserver>();

			cookingFire = KitchenMaterialFactory.createCookingFire();
			oven = KitchenMaterialFactory.createOven();
			blender = KitchenMaterialFactory.createBlender();
			fridge = KitchenMaterialFactory.createFridge();

			setMaterialConfig(cookingFireNumber, ovenNumber, blenderNumber, fridgeNumber);

			recipes = new Recipe[1]
			{
				new Recipe
				(
					"Feuilleté au crabe",

					new Ingredient[4]
					{
						new Ingredient("pâte feuilletée", 5),
						new Ingredient("oeuf", 10),
						new Ingredient("sel", 3),
						new Ingredient("poivre", 6)
					},

					new RecipeStep[3]
					{
						new RecipeStep("Préchauffer le four à 230°", 5, oven),
						new RecipeStep("Mélanger crabe, citron, chapelure, herbes", 1, cookingFire),
						new RecipeStep("Lier le tout avec un oeuf", 1, fridge)
						//new RecipeStep("Préchauffer le four à 230°", 5, oven),
						//new RecipeStep("Mélanger crabe, citron, chapelure, herbes", 1, blender),
						//new RecipeStep("Lier le tout avec un oeuf", 1, knife)
					}
				),
			};
			setEmployeeConfig(chefNumber, deputyChefNumber, kitchenClerkNumber, diverNumber);
		}

		public void setEmployeeConfig(int chefNumber, int deputyChefNumber, int kitchenClerkNumber, int diverNumber)
		{
			chefs = new Chef[chefNumber];
			deputyChefs = new DeputyChef[deputyChefNumber];
			kitchenClerks = new KitchenClerk[kitchenClerkNumber];
			divers = new Diver[diverNumber];

			for (int i = 0; i < chefs.Length; i++)
			{
				chefs[i] = new Chef();
				chefs[i].PosY = i;
			}

			for (int i = 0; i < deputyChefs.Length; i++)
			{
				deputyChefs[i] = new DeputyChef();
				deputyChefs[i].PosX = 5;
				deputyChefs[i].PosY = i;
			}

			for (int i = 0; i < kitchenClerks.Length; i++)
			{
				kitchenClerks[i] = new KitchenClerk();
				kitchenClerks[i].PosX = i;
				kitchenClerks[i].PosY = i + 1;
			}

			for (int i = 0; i < divers.Length; i++)
			{
				divers[i] = new Diver();
				divers[i].PosX = i + 1;
				divers[i].PosY = i + 2;
			}
		}

		public void setMaterialConfig(int cookingFireNumber,  int ovenNumber, int blenderNumber, int fridgeNumber)
        {
			cookingFire.quantity = cookingFireNumber;
			oven.quantity = ovenNumber;
			blender.quantity = blenderNumber;
			fridge.quantity = fridgeNumber;

			for (int i = 0; i < cookingFireNumber; i++)
			{
				kitchen.map[i+12, 2] = cookingFire;
			}

			for (int i = 0; i < ovenNumber; i++)
			{
				kitchen.map[i, 4] = oven;
			}

			//for (int i = 0; i < blenderNumber; i++)
			//{
			//	blender = kitchenMaterialFactory.createBlender(blenderNumber);
			//	kitchen.map[i, 1] =blender;
			//}

			for (int i = 0; i < fridgeNumber; i++)
			{
				kitchen.map[i+16, 5] = fridge;
			}
		}

		public void AddObserver(IObserver observer)
		{
			observers.Add(observer);
		}

		/*
		* Notify when there is a change at some particulars regions of code
		*/
		public void NotifyWhenMoved(int oldX, int oldY, int newX, int newY)
		{
			foreach (IObserver observer in observers)
			{
				observer.UpdateWithMoves(oldX, oldY, newX, newY);
			}
		}

		public void NotifyMaterialAvailaibility(string name)
        {
			foreach (IObserver observer in observers)
			{
				//observer.UpdateMaterial(name);
			}
		}

		public void NotifyFreeEmployee(string name)
		{
			foreach (IObserver observer in observers)
			{
				//observer.UpdateFreeMobilekitchenItem(name);
			}
		}

		public void NotifyBusyEmployee(string name)
		{
			foreach (IObserver observer in observers)
			{
				//observer.UpdateBusyMobilekitchenItem(name);
			}
		}

		public void NotifyEventLog(string str)
		{
			foreach (IObserver observer in observers)
			{
				observer.UpdateEventLog(str);
			}
		}
	}
}
