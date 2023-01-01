using System.Collections.Generic;
using System.IO;
using AppRestaurant.Model.Common;
using AppRestaurant.Model.Kitchen.Items;

namespace AppRestaurant.Model.Kitchen.Actors
{
    public class Chef : MotionKitchenItem
    {
        private static string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        private static string exeDir = Path.GetDirectoryName(exePath);
        private static DirectoryInfo binDir = Directory.GetParent(Directory.GetParent(exeDir).FullName);
       
        private static string spritePath = binDir.FullName + "\\Resources\\Chef\\";

        private static string imageWaiting = "left.gif";
        private static string imageWorking = "front.gif";

        public Sprite waiting = new Sprite(spritePath, imageWaiting);
        public Sprite working = new Sprite(spritePath, imageWorking);

        private List<DeputyChef> deputyChefs;
        public List<DeputyChef> DeputyChefs
        {
            get => deputyChefs;
            set => deputyChefs = value;
        }

        public Chef()
        {
            PosX = 0;
            PosY = 0;

            waiting.loadImage();
            working.loadImage();

            setSprite(waiting);
        }

        public override void moveLeft()
        {
            setSprite(waiting);
            base.moveLeft();
        }
    }
}
