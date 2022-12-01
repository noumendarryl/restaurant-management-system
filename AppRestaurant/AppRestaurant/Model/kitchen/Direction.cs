using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public enum Direction
    {
		/*
		* To the up
		*/
		Up,
		/*
		* To the down
		*/
		Down,
		/*
		* To the left
		*/
		Left,
		/*
		* To the right
		*/
		Right,
		/*
		* To the right, up diagonal.
		*/
		RightUp,
		/*
		* To the right, down diagonal.
		*/
		RightDown,
		/*
		* To the left, down diagonal.
		*/
		LeftDown,
		/*
		* To the left, up diagonal.
		*/
		LeftUp
	}
}
