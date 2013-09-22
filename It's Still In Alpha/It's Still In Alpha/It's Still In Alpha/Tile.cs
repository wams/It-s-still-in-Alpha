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
        public bool visited = false;
        private int score = 100;
        public int Score
        {
            get { return score; }
            set
            {
                score = value;
                int PickUpWidth = (int)((DestRect.Width / 5) * (Math.Log((Math.Log(score) / Math.Log(10))) / Math.Log(2)));
                int PickUpHeight = (int)((DestRect.Height / 5) * (Math.Log((Math.Log(score) / Math.Log(10))) / Math.Log(2)));
                int PickUpX = DestRect.Center.X - PickUpWidth / 2;
                int PickUpY = DestRect.Center.Y - PickUpHeight / 2;
                PickUpDestRect = new Rectangle(PickUpX, PickUpY, PickUpWidth, PickUpHeight);
            }
        }
        private char type;

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
        public char Type
        {
            get { return type; }
            set { type = value; }
        }

        public Texture2D tileImage;
        public Texture2D tilePickUpImage;
        public Rectangle PickUpDestRect;
        
        public Tile(Rectangle source, Rectangle dest, ContentManager content)
        {
            SourceRect = source;
            DestRect = dest;
            Content = content;
            make_pickup_rect();
        }

        private void make_pickup_rect()
        {
            int PickUpWidth = (int)((DestRect.Width / 5) * (Math.Log((Math.Log(score) / Math.Log(10))) / Math.Log(2)));
            int PickUpHeight = (int)((DestRect.Height / 5) * (Math.Log((Math.Log(score) / Math.Log(10))) / Math.Log(2)));
            int PickUpX = DestRect.Center.X - PickUpWidth / 2;
            int PickUpY = DestRect.Center.Y - PickUpHeight / 2;
            PickUpDestRect = new Rectangle(PickUpX, PickUpY, PickUpWidth, PickUpHeight);
        }

        //we wont need to pass the graphics device when we add image support
        //instead we would use a string which is the path to the image
        public void LoadContent(string image)
        {
            //you would load the tile image here
            tileImage = Content.Load<Texture2D>("Tiles/" + image);
            tilePickUpImage = Content.Load<Texture2D>("Tiles/red_particle_thing");
            if (image != "empty_space")
                visited = true;
        }

        public void Update(GameTime gameTime)
        {
            //dont need anything yet
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            //add image support later

            //the color.white below is a way to shift the hue of any texture, leave it white to keep original colors
            spriteBatch.Draw(tileImage, DestRect, SourceRect, Color.White);
            if (!visited)
                spriteBatch.Draw(tilePickUpImage, PickUpDestRect, tilePickUpImage.Bounds, Color.White);
        }
    }
}
