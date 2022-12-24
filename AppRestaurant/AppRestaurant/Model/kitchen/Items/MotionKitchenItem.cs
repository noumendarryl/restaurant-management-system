using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Kitchen;

namespace AppRestaurant.Model.Kitchen.Items
{
	public class MotionKitchenItem : KitchenItem
	{
		public static int speed { get; set; }

		/* Instantiates a new MotionKitchenItem.
		* @param map
		* 
		* 	The map.
		* @param position
		* 	The position if the MotionKitchenItem.
		*/
		public MotionKitchenItem()
		{
			speed = 1;
		}

		/*
		 * Moves the element up
		 */
		public void moveUp()
		{
			PosY -= speed;
		}

		/*
		 * Moves the element down
		 */
		public void moveDown()
		{
			PosY += speed;
		}

		/*
		 * Moves the element to the left
		 */
		public void moveLeft()
		{
			PosX -= speed;
		}

		/*
		 * Moves the element to the right
		 */
		public void moveRight()
		{
			PosX += speed;
		}

		/*
		 * Moves the element to the right, up diagonal
		 */
		public void moveRightUp()
		{
			PosX += speed;
			PosY -= speed;
		}

		/*
		 * Moves the element to the right, down diagonal
		 */
		public void moveRightDown()
		{
			PosX += speed;
			PosY += speed;
		}

		/*
		 * Moves the element to the left, up diagonal.
		 */
		public void moveLeftUp()
		{
			PosX -= speed;
			PosY -= speed;
		}

		/*
		 * Moves the element to the left, down diagonal.
		 */
		public void moveLeftDown()
		{
			PosX -= speed;
			PosY += speed;
		}
	}
}
