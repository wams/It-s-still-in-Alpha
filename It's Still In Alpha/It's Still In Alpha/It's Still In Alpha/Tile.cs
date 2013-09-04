using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace It_s_Still_In_Alpha
{
    class Tile
    {
        private Rectangle sourceRect;
        private Rectangle destRect;

        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        public Rectangle DestRect
        {
            get { return destRect; }
            set { destRect = value; }
        }

        private Texture2D tileImage;

        public Tile(Rectangle source, Rectangle dest)
        {
            SourceRect = source;
            DestRect = dest;
        }

        //we wont need to pass the graphics device when we add image support
        //instead we would use a string which is the path to the image
        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            //you would load the tile image here
            Random colorGenerator = new Random(DateTime.Now.Millisecond);

            tileImage = new Texture2D(graphicsDevice, 1, 1);
            tileImage.SetData(new Color[] { new Color(colorGenerator.Next(255), colorGenerator.Next(255), colorGenerator.Next(255)) });
        }

        public void Update(GameTime gameTime)
        {
            //dont need anything yet
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            //add image support later

            //the color.white below is a way to shift the hue of any texture, leave it white to keep original colors
            spriteBatch.Draw(tileImage, DestRect, Color.White);
        }
    }
}
