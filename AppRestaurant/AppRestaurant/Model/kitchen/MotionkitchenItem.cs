using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.kitchen;

namespace AppRestaurant.Model.kitchen
{
    public class MotionkitchenItem : Position
	{
        private Sprite sprite;

		public int speed { get; set; }

		/* Instantiates a new MotionkitchenItem.
		* @param map
		* 
		* 	The map.
		* @param position
		* 	The position if the MotionkitchenItem.
		*/
		public MotionkitchenItem()
		{
			this.speed = 1;
		}

		/*
		 * Moves the element Up
		 */
		public void moveUp()
		{
			this.posY -= this.speed;
		}

		/*
		 * Moves the element Down
		 */
		public void moveDown()
		{
			this.posY += this.speed;
		}

		/*
		 * Moves the element to the Left
		 */
		public void moveLeft()
		{
			this.posX -= this.speed;
		}

		/*
		 * Moves the element to the Right
		 */
		public void moveRight()
		{
			this.posX += this.speed;
		}

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
