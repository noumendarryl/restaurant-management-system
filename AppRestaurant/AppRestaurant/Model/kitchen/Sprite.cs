using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AppRestaurant.Model
{
    public class Sprite
    {
        private Image image;
        private string imageName;
        private string spritePath;

        public Sprite(string spritePath, string imageName)
        {
			this.setSpritePath(spritePath);
			this.setImageName(imageName);
        }

		public void loadImage()
        {
            try
            {
                this.setImage(Image.FromFile(this.getSpritePath()+this.getImageName()));
            } catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        /*
        * The image
        */
        public Image getImage()
        {
            return image;
        }

        /*
        * The image
        */
        public void setImage(Image value)
        {
            image = value;
        }

        /*
		 * The name of the image
		 */
        public string getImageName()
        {
            return imageName;
        }

        /*
		 * The name of the image
		 */
        public void setImageName(string value)
        {
            imageName = value;
        }

        /*
		 * Path to the sprite
		 */
        public string getSpritePath()
        {
            return spritePath;
        }

        /*
		 * Path to the sprite
		 */
        public void setSpritePath(string value)
        {
            spritePath = value;
        }
    }
}
