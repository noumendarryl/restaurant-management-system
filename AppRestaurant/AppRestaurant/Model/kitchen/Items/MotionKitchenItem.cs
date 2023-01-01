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

		/* 
		* Instantiates a new MotionKitchenItem.
		*/
		public MotionKitchenItem()
		{
			speed = 1;
		}

		/*
		 * Moves the element up
		 */
		public virtual void moveUp()
		{
			PosY -= speed;
		}

		/*
		 * Moves the element down
		 */
		public virtual void moveDown()
		{
			PosY += speed;
		}

		/*
		 * Moves the element to the left
		 */
		public virtual void moveLeft()
		{
			PosX -= speed;
		}

		/*
		 * Moves the element to the right
		 */
		public virtual void moveRight()
		{
			PosX += speed;
		}

		/*
		 * Moves the element to the right, up diagonal
		 */
		public virtual void moveRightUp()
		{
			PosX += speed;
			PosY -= speed;
		}

		/*
		 * Moves the element to the right, down diagonal
		 */
		public virtual void moveRightDown()
		{
			PosX += speed;
			PosY += speed;
		}

		/*
		 * Moves the element to the left, up diagonal.
		 */
		public virtual void moveLeftUp()
		{
			PosX -= speed;
			PosY -= speed;
		}

		/*
		 * Moves the element to the left, down diagonal.
		 */
		public virtual void moveLeftDown()
		{
			PosX -= speed;
			PosY += speed;
		}
	}
}
