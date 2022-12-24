using System.IO;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.Kitchen.Items;

namespace AppRestaurant.Model.Kitchen.Actors
{
    public class Chef : MotionKitchenItem
    {
        private static string imageFront = "front.gif";
        private static string imageBack = "back.gif";
        private static string imageLeft = "left.gif";
        private static string imageRight = "right.gif";
        private static string imageStop = "stop.png";

        public Sprite front;
        public Sprite back;
        public Sprite left;
        public Sprite right;
        public Sprite stop;

        public Chef()
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDir = System.IO.Path.GetDirectoryName(exePath);
            DirectoryInfo binDir = System.IO.Directory.GetParent(exeDir);
            binDir = System.IO.Directory.GetParent(binDir.FullName);

            string spritePath = binDir.FullName + "\\Resources\\Chef\\";

            front = new Sprite(spritePath, imageFront);
            back = new Sprite(spritePath, imageBack);
            left = new Sprite(spritePath, imageLeft);
            right = new Sprite(spritePath, imageRight);
            stop = new Sprite(spritePath, imageStop);

            PosX = 0;
            PosY = 0;

            front.loadImage();
            //back.loadImage();
            //left.loadImage();
            right.loadImage();
            //stop.loadImage();

            setSprite(front);
        }
    }
}
