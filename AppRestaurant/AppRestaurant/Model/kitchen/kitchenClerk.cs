using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class kitchenClerk : MotionkitchenItem
    {
        private static string spritePath = "Resources/kitchenClerk/";

        private static string imageFront = "front.png";
        private static string imageBack = "back.png";
        private static string imageLeft = "left.png";
        private static string imageRight = "right.png";
        private static string imageWorking = "working.png";

        private Sprite front = new Sprite(spritePath, imageFront);
        private Sprite back = new Sprite(spritePath, imageBack);
        private Sprite left = new Sprite(spritePath, imageLeft);
        private Sprite right = new Sprite(spritePath, imageRight);
        private Sprite working = new Sprite(spritePath, imageWorking);

        public kitchenClerk() 
        {
            this.posX = 50;
            this.posY = 30;

            //front.loadImage();
            //back.loadImage();
            //left.loadImage();
            //right.loadImage();
            //working.loadImage();

            this.setSprite(front);
        }
    }
}
