using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class DeputyChef : MotionkitchenItem
    {
        private static string spritePath = "C:\\Users\\NOUMEN DARRYL\\Documents\\prog-sys-obj\\AppRestaurant\\AppRestaurant\\Resources\\DeputyChef\\";

        private static string imageFront = "front.png";
        private static string imageBack = "back.png";
        private static string imageLeft = "left.png";
        private static string imageRight = "right.png";
        private static string imageWorking = "working.png";

        public Sprite front = new Sprite(spritePath, imageFront);
        public Sprite back = new Sprite(spritePath, imageBack);
        public Sprite left = new Sprite(spritePath, imageLeft);
        public Sprite right = new Sprite(spritePath, imageRight);
        public Sprite working = new Sprite(spritePath, imageWorking);

        public DeputyChef()
        {
            this.posX = 12;
            this.posY = 4;

            //front.loadImage();
            //back.loadImage();
            //left.loadImage();
            //right.loadImage();
            //working.loadImage();

            //this.setSprite(front);
        }
    }
}
