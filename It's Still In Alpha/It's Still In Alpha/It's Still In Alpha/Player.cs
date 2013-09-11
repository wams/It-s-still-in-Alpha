﻿using System;
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
        public Player(Game1 gameRef)
            : base(gameRef)
        {
            Alive = true;
            Direction = Facing.Up;
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

        public override void Update(GameTime gameTime)
        {
            if (Direction == Facing.Down || Direction == Facing.Up)
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

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
