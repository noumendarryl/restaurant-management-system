using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class Chef : MotionkitchenItem
    {
        //private static string spritePath = "C:\\Users\\NOUMEN DARRYL\\Documents\\prog-sys-obj\\AppRestaurant\\AppRestaurant\\Resources\\Chef\\";

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
