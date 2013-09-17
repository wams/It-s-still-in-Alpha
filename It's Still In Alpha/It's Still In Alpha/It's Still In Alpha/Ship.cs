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

        protected bool StopMoving = false;

        Texture2D shipImage;
        Dictionary<string, List<Rectangle>> Frames;
        string currentAnimation;
        int frameIndex;

        protected Game1 GameRef;

        Vector2 position;
        Vector2 index;

        int sourceSize;

        public Vector2 Index
        {
            get { return index; }
            set { index = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = new Vector2( value.X, value.Y ); }
        }

        public int SourceSize
        {
            get { return sourceSize; }
            set { sourceSize = value; }
        }

        double speed = 2.0;
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

        List<Ship> ghostShips = new List<Ship>();

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
        public virtual bool TileCollision(List<List<Tile>> tiles)
        {
            switch (Direction)
            {
                case Ship.Facing.Up:
                    if (Index.Y > 0)
                    {
                        if (tiles[(int)Index.X][(int)Index.Y - 1].Type != 0)
                        {
                            StopMoving = true;
                        }
                        else
                        {
                            StopMoving = false;
                        }
                    }
                    break;
                case Ship.Facing.Down:
                    if (Index.Y < tiles[0].Count - 1)
                    {
                        if (tiles[(int)Index.X][(int)Index.Y + 1].Type != 0)
                        {
                            StopMoving = true;
                        }
                        else
                        {
                            StopMoving = false;
                        }
                    }
                    break;
                case Ship.Facing.Left:
                    if (Index.X > 0)
                    {
                        if (tiles[(int)Index.X - 1][(int)Index.Y].Type != 0)
                        {
                            StopMoving = true;
                        }
                        else
                        {
                            StopMoving = false;
                        }
                    }
                    break;
                case Ship.Facing.Right:
                    if (Index.X < tiles.Count - 1)
                    {
                        if (tiles[(int)Index.X + 1][(int)Index.Y].Type != 0)
                        {
                            StopMoving = true;
                        }
                        else
                        {
                            StopMoving = false;
                        }
                    }
                    break;
            }
            return StopMoving;
        }
        public virtual bool Collision(Ship ship) { return false; }

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
            if (!StopMoving)
            {
                double distance = speed * (gameTime.ElapsedGameTime.Milliseconds / 1000.0);
                double change_in_x = distance * Util.cos(Util.degreesToRadians((double)direction));
                double change_in_y = -distance * Util.sin(Util.degreesToRadians((double)direction));
                position.X += (float)change_in_x;
                position.Y += (float)change_in_y;

                index = new Vector2((int)Position.X, (int)Position.Y);
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shipImage, new Rectangle((int)Position.X * SourceSize + shipImage.Bounds.Center.X, (int)Position.Y * SourceSize + shipImage.Bounds.Center.Y, SourceSize, SourceSize), null, Color.White, 
                MathHelper.ToRadians((int)Direction - 90), new Vector2(shipImage.Bounds.Center.X, shipImage.Bounds.Center.Y), SpriteEffects.None, 0);
        }

        #endregion
    }
}
