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
             Right = 0, Up = 90, Left = 180, Down = 270 
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

        double speed = 1.0;
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
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

        public virtual void Update(GameTime gameTime) 
        {
            double distance = speed * (gameTime.ElapsedGameTime.Milliseconds / 1000.0);
            double change_in_x = distance * Util.cos(Util.degreesToRadians((double)direction));
            double change_in_y = - distance * Util.sin(Util.degreesToRadians((double)direction));
            position.X += (float)change_in_x;
            position.Y += (float)change_in_y;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shipImage, new Rectangle((int)Position.X * SourceSize, (int)Position.Y * SourceSize, SourceSize, SourceSize), Color.White);
        }

        #endregion

        #region Input Functions
        public void set_facing_right( GameTime time ) 
        {
            direction = Facing.Right;
        }

        public void set_facing_up(GameTime time)
        {
            direction = Facing.Up;
        }

        public void set_facing_left( GameTime time )
        {
            direction = Facing.Left;
        }

        public void set_facing_down(GameTime time)
        {
            direction = Facing.Down;
        }
        #endregion

    }
}
