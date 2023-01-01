using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.Kitchen.Items;

namespace AppRestaurant.Model.Kitchen.Actors
{
    public class KitchenClerk : MotionKitchenItem
    {
        private static string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        private static string exeDir = Path.GetDirectoryName(exePath);
        private static DirectoryInfo binDir = Directory.GetParent(Directory.GetParent(exeDir).FullName);

        private static string spritePath = binDir.FullName + "\\Resources\\KitchenClerk\\";

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

        public KitchenClerk()
        {
            PosX = 50;
            PosY = 30;

            front.loadImage();

            setSprite(front);
        }

        public override void moveUp()
        {
            setSprite(front);
            base.moveUp();
        }

        public override void moveDown()
        {
            setSprite(back);
            base.moveDown();
        }

        public override void moveLeft()
        {
            setSprite(left);
            base.moveLeft();
        }

        public override void moveRight()
        {
            setSprite(right);
            base.moveRight();
        }
    }
}
