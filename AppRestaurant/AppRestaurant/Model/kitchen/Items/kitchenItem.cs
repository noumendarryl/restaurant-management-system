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
		* Gets sprite value
		*/
		public Sprite getSprite()
		{
			return sprite;
		}

		/*
		* Sets sprite value
		*/
		public void setSprite(Sprite value)
		{
			sprite = value;
		}
	}
}
