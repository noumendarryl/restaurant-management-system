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
		private Sprite sprite;

		/*
		* Gets sprite value
		*/
		public Sprite Sprite
		{
			get
			{
				return sprite;
			}
			set
			{
				sprite = value;
			}
		}
	}
}
