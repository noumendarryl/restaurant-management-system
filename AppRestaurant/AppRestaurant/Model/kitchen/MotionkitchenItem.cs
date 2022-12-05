using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.kitchen;

namespace AppRestaurant.Model.kitchen
{
	public class MotionkitchenItem : kitchenItem
	{
		public static int speed { get; set; }

		/* Instantiates a new MotionkitchenItem.
		* @param map
		* 
		* 	The map.
		* @param position
		* 	The position if the MotionkitchenItem.
		*/
		public MotionkitchenItem()
		{
			speed = 1;
		}

		/*
		 * Moves the element up
		 */
		public void moveUp()
		{
			this.posY -= speed;
		}

		/*
		 * Moves the element down
		 */
		public void moveDown()
		{
			this.posY += speed;
		}

		/*
		 * Moves the element to the left
		 */
		public void moveLeft()
		{
			this.posX -= speed;
		}

		/*
		 * Moves the element to the right
		 */
		public void moveRight()
		{
			this.posX += speed;
		}

		/*
		 * Moves the element to the right, up diagonal
		 */
		public void moveRightUp()
		{
			this.posX += speed;
			this.posY -= speed;
		}

		/*
		 * Moves the element to the right, down diagonal
		 */
		public void moveRightDown()
		{
			this.posX += speed;
			this.posY += speed;
		}

		/*
		 * Moves the element to the left, up diagonal.
		 */
		public void moveLeftUp()
		{
			this.posX -= speed;
			this.posY -= speed;
		}

		/*
		 * Moves the element to the left, down diagonal.
		 */
		public void moveLeftDown()
		{
			this.posX -= speed;
			this.posY += speed;
		}
	}
}
