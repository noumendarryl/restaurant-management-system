using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Model.kitchen
{
    public class DeputyChef : MotionkitchenItem
    {
        //private static string spritePath = "C:\\Users\\NOUMEN DARRYL\\Documents\\prog-sys-obj\\AppRestaurant\\AppRestaurant\\Resources\\DeputyChef\\";

        private static string imageFront = "front.png";
        private static string imageBack = "back.png";
        private static string imageLeft = "left.png";
        private static string imageRight = "right.png";
        private static string imageWorking = "working.png";

        public Sprite front;
        public Sprite back;
        public Sprite left;
        public Sprite right;
        public Sprite working;

        public DeputyChef()
        {

            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDir = System.IO.Path.GetDirectoryName(exePath);
            DirectoryInfo binDir = System.IO.Directory.GetParent(exeDir);
            binDir = System.IO.Directory.GetParent(binDir.FullName);


            string spritePath = binDir.FullName + "\\Resources\\DeputyChef\\";

            front = new Sprite(spritePath, imageFront);
            back = new Sprite(spritePath, imageBack);
            left = new Sprite(spritePath, imageLeft);
            right = new Sprite(spritePath, imageRight);
            working = new Sprite(spritePath, imageWorking);


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
