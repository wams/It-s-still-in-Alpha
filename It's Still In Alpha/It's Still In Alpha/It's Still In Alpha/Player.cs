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
        public List<GhostShip> ghostShips = new List<GhostShip>();
        public static List<Player> all_player_ships = new List<Player>();

        public Player(Game1 gameRef)
            : base(gameRef)
        {
            shipImageName = "player_ship";
            all_player_ships.Add(this);
        }

        public override bool TileCollision(List<List<Tile>> tiles)
        {
            return base.TileCollision(tiles);
        }

        public override bool Collision(Ship ship)
        {
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

        #region Input Functions
        private void set_direction_from_input(Facing dir)
        {
            if ((int)Direction % 180 != (int)dir % 180)
            {
                StopMoving = false;
                GhostShip ghost = new GhostShip(GameRef, this);
                ghost.Direction = Direction;
                ghost.Position = Position;
                ghost.SourceSize = SourceSize;
                ghost.LoadContent();
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

        public new static void draw_all_ships(SpriteBatch spriteBatch)
        {
            draw_ships(spriteBatch, all_player_ships);
        }

        public new static void update_all_ships(GameTime gameTime)
        {
            update_ships(gameTime, all_player_ships);
        }


        public new static void tile_collision_all_ships(List<List<Tile>> tiles)
        {
            tile_collision_ships(tiles, all_player_ships);
        }
    }
}
