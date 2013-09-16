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
    class Player : Ship
    {
        List<Ship> ghostShips = new List<Ship>();

        private Boolean stopMoving;

        public Boolean StopMoving
        {
            get { return stopMoving; }
            set { stopMoving = value; }
        }

        public Player(Game1 gameRef)
            : base(gameRef)
        {
            Alive = true;
            Direction = Facing.Up;
            StopMoving = false;
        }

        public override bool Collision(Ship ship)
        {
            return false;
        }

        public override void LoadContent(string image)
        {
            base.LoadContent(image);
        }

        public override void LoadContent(string image, Dictionary<string, List<Rectangle>> animation, string currentAnimation, int currentFrame)
        {
            base.LoadContent(image, animation, currentAnimation, currentFrame);
        }

        public override void Update(GameTime gameTime, Boolean stopMoving)
        {
            /*if (Direction == Facing.Down || Direction == Facing.Up)
            {
                if (InputHandler.KeyPressed(Keys.Right))
                {
                    Direction = Facing.Right;
                }
                if (InputHandler.KeyPressed(Keys.Left))
                {
                    Direction = Facing.Left;
                }
            }
            if (Direction == Facing.Left || Direction == Facing.Right)
            {
                if (InputHandler.KeyPressed(Keys.Up))
                {
                    Direction = Facing.Up;
                }
                if (InputHandler.KeyPressed(Keys.Down))
                {
                    Direction = Facing.Down;
                }
            }
            */
            base.Update(gameTime, stopMoving);

            foreach (Ship ghost in ghostShips)
            {
                ghost.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            foreach (Ship ghost in ghostShips)
            {
                ghost.Draw(spriteBatch);
            }
        }

        #region Input Functions
        private void set_direction_from_input(Facing dir)
        {
            if ((int)Direction % 180 != (int)dir % 180)
            {
                Ship ghost = new Ship(GameRef);
                ghost.Direction = Direction;
                ghost.Position = Position;
                ghost.SourceSize = SourceSize;
                ghost.LoadContent("player_ship");
                ghostShips.Add(ghost);
                Direction = dir;
            }
        }

        public void set_facing_right(GameTime time)
        {
            set_direction_from_input(Facing.Right);
        }

        public void set_facing_up(GameTime time)
        {
            set_direction_from_input(Facing.Up);
        }

        public void set_facing_left(GameTime time)
        {
            set_direction_from_input(Facing.Left);
        }

        public void set_facing_down(GameTime time)
        {
            set_direction_from_input(Facing.Down);
        }
        #endregion

    }
}
