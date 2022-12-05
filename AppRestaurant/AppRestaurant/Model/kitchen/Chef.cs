using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class Chef : MotionkitchenItem
    {
        private static string spritePath = "C:\\Users\\NOUMEN DARRYL\\Documents\\prog-sys-obj\\AppRestaurant\\AppRestaurant\\Resources\\Chef\\";

        private static string imageFront = "front.gif";
        private static string imageBack = "back.gif";
        private static string imageLeft = "left.gif";
        private static string imageRight = "right.gif";
        private static string imageStop = "stop.png";

        public Sprite front = new Sprite(spritePath, imageFront);
        public Sprite back = new Sprite(spritePath, imageBack);
        public Sprite left = new Sprite(spritePath, imageLeft);
        public Sprite right = new Sprite(spritePath, imageRight);
        public Sprite stop = new Sprite(spritePath, imageStop);

        public Chef()
        {
            posX = 0;
            posY = 0;

            front.loadImage();
            //back.loadImage();
            //left.loadImage();
            right.loadImage();
            //stop.loadImage();

            setSprite(front);
        }
    }
}
