using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace It_s_Still_In_Alpha
{
    
    class Ship
    {
        public T NumToEnum<T>(int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }

        public enum Facing
        {
            Up = 0, Right = 90, Down = 180, Left = 270
        }

        #region Variables Region

        Texture2D shipImage;
        Dictionary<string, List<Rectangle>> Frames;
        string currentAnimation;
        int frameIndex;

        protected Game1 GameRef;

        Vector2 position;
        int sourceSize;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int SourceSize
        {
            get { return sourceSize; }
            set { sourceSize = value; }
        }

        Facing direction;

        public Facing Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        Boolean alive;

        public Boolean Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        #endregion

        #region Constructor Region

        public Ship(Game1 gameRef)
        {
            GameRef = gameRef;

            direction = Facing.Up;
        }

        #endregion

        #region Virtual Functions

        /*making these virtual/abstract so that we remember to add them to both inherited ship classes*/
        public virtual bool Collision(Ship ship) { return true; }

        public virtual void LoadContent(string image)
        {
            shipImage = GameRef.Content.Load<Texture2D>("Ships/"+image);
        }

        public virtual void LoadContent(string image, Dictionary<string, List<Rectangle>> animation, string currentAnimation, int currentFrame)
        {
            shipImage = GameRef.Content.Load<Texture2D>("Ships/"+ image);

            Frames = animation;
            this.currentAnimation = currentAnimation;
            frameIndex = currentFrame;
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shipImage, new Rectangle((int)Position.X * SourceSize + shipImage.Bounds.Center.X, (int)Position.Y * SourceSize + shipImage.Bounds.Center.Y, SourceSize, SourceSize), null, Color.White, 
                MathHelper.ToRadians((int)Direction), new Vector2(shipImage.Bounds.Center.X, shipImage.Bounds.Center.Y), SpriteEffects.None, 0);
        }

        #endregion

    }
}
