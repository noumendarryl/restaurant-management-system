using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class kitchenModel
    {
		private List<IObserver> observers;

		/*
		* The kitchen.
		*/
		private kitchen kitchen { get; set; }

		/*
		* The chef
		*/
		public int chefNumber = 1;
		public Chef[] chefs { get; set; }

		/*
		* The deputyChef
		*/
		public int deputyChefNumber = 2;
		public DeputyChef[] deputyChefs { get; set; }

		/*
		* The kitchenClerk
		*/
		public int kitchenClerkNumber = 2;
		public kitchenClerk[] kitchenClerks { get; set; }

		/*
		* The diver
		*/
		public int diverNumber = 1;
		public Diver[] divers { get; set; }

		/*
		* The recipes
		*/
		public Recipe[] recipes { get; set; }

		/*
		* The List of kitchen materials.
		*/
		public kitchenMaterial cookingFire;

		public kitchenMaterial oven;

		public kitchenMaterial blender;

		public kitchenMaterial pan;

		public kitchenMaterial knife;

		public kitchenModel()
        {
			kitchen = new kitchen();
			observers = new List<IObserver>();

			cookingFire = kitchenMaterialFactory.createCookingFire();
			oven = kitchenMaterialFactory.createOven();
			blender = kitchenMaterialFactory.createBlender();
			pan = kitchenMaterialFactory.createPan();
			knife = kitchenMaterialFactory.createKnife();

			recipes = new Recipe[1]
			{
				new Recipe
				(
					"Feuilleté au crabe",

					new Ingredient[4]
					{
						new Ingredient("pâte feuilletée"),
						new Ingredient("oeuf"),
						new Ingredient("sel"),
						new Ingredient("poivre")
					},

					new RecipeStep[3]
					{
						new RecipeStep("Préchauffer le four à 230°", 5, oven),
						new RecipeStep("Mélanger crabe, citron, chapelure, herbes", 1, blender),
						new RecipeStep("Lier le tout avec un oeuf", 1, knife)
					}
				),
			}; 

			chefs = new Chef[chefNumber];
			deputyChefs = new DeputyChef[deputyChefNumber];
			kitchenClerks = new kitchenClerk[kitchenClerkNumber];
			divers = new Diver[diverNumber];

			for (int i = 0; i < chefs.Length; i++)
            {
				chefs[i] = new Chef();
				chefs[i].posY = i;
            }

			for (int i = 0; i < deputyChefs.Length; i++)
            {
				deputyChefs[i] = new DeputyChef();
				deputyChefs[i].posX = 1;
				deputyChefs[i].posY = i + 1;
            }

			for (int i = 0; i < kitchenClerks.Length; i++)
            {
				kitchenClerks[i] = new kitchenClerk();
				kitchenClerks[i].posX = i;
				kitchenClerks[i].posY = i + 3;
            }

			for (int i = 0;  i < divers.Length; i++)
            {
				divers[i] = new Diver();
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
	}
}
