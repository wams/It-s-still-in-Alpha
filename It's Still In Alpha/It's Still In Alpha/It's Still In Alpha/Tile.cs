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
        static ContentManager Content;

        private Rectangle sourceRect;
        private Rectangle destRect;

        private int type;

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

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private Texture2D tileImage;

        public Tile(Rectangle source, Rectangle dest, ContentManager content)
        {
            SourceRect = source;
            DestRect = dest;

            Content = content;
        }

        //we wont need to pass the graphics device when we add image support
        //instead we would use a string which is the path to the image
        public void LoadContent(string image)
        {
            //you would load the tile image here
            tileImage = Content.Load<Texture2D>("Tiles/" + image);
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
