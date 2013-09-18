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
    class GhostShip : Ship
    {
        #region Static Variables

        public static List<GhostShip> all_ghost_ships = new List<GhostShip>();

        #endregion

        #region Variables
        public Player originShip;
        #endregion

        public GhostShip(Game1 gameRef, Player origin )
            : base(gameRef)
        {
            shipImageName = "ghost_ship";
            originShip = origin;
            all_ghost_ships.Add(this);
        }

        public override bool TileCollision(List<List<Tile>> tiles)
        {
            if (base.TileCollision(tiles))
            {
                StopMoving = false;
                Direction = (Facing)((int)(Direction + 180) % 360);
                return true;
            }
            return false;
        }

        public override void LoadContent(string image, Dictionary<string, List<Rectangle>> animation, string currentAnimation, int currentFrame)
        {
            base.LoadContent(image, animation, currentAnimation, currentFrame);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public new static void draw_all_ships(SpriteBatch spriteBatch)
        {
            draw_ships(spriteBatch, all_ghost_ships);
        }

        public new static void update_all_ships(GameTime gameTime)
        {
            update_ships(gameTime, all_ghost_ships);
        }


        public new static void tile_collision_all_ships(List<List<Tile>> tiles)
        {
            tile_collision_ships(tiles, all_ghost_ships);
        }
    }
}
