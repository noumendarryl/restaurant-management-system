using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class kitchenItem : Position
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
