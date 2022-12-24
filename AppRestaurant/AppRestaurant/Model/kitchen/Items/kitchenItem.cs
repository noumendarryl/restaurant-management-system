using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.Common.Move;

namespace AppRestaurant.Model.Kitchen.Items
{
    public class KitchenItem : Position
    {
		public Sprite sprite;

		/*
		* The sprite
		*/
		public Sprite getSprite()
		{
			return sprite;
		}

		/*
		* The sprite
		*/
		public void setSprite(Sprite value)
		{
			sprite = value;
		}
	}
}
