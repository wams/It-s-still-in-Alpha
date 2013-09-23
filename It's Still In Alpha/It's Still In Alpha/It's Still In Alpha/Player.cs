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
        public List<Tile> pickedUpTiles = new List<Tile>();
        public static List<Player> all_player_ships = new List<Player>();
        public int score;
        public bool has_turned = false;
        Facing old_dir;
        Vector2 old_pos;

        public Player(Game1 gameRef)
            : base(gameRef)
        {
            score = 0;
            shipImageName = "player_ship";
            all_player_ships.Add(this);
        }

        public override void dispose()
        {
            ghostShips = null;
        }

        public override bool TileCollision(List<List<Tile>> tiles)
        {
            return base.TileCollision(tiles);
        }

        public Tile current_tile(List<List<Tile>> tiles)
        {
            return tiles[(int)Position.X][(int)Position.Y];
        }

        public bool update_score(List<List<Tile>> tiles)
        {
            Tile t = current_tile(tiles);
            if ( !t.visited )
            {
                t.visited = true;
                score += t.Score;
                pickedUpTiles.Add(t);
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
            GameRef.spriteBatch.DrawString(GameRef.titleFont,
                score.ToString(),
                new Vector2(GameRef.screenRectangle.Center.X + 650, GameRef.screenRectangle.Center.Y + 400),
                Color.Yellow);
            base.Draw(spriteBatch);
        }

        private void addGhostShip()
        {
            GhostShip ghost = new GhostShip( GameRef, this );
            ghost.Direction = old_dir;
            ghost.Position = old_pos;
            ghost.SourceSize = SourceSize;
            ghost.LoadContent();
            ghost.Index = new Vector2((int)old_pos.X, (int)old_pos.Y);
            ghostShips.Add(ghost);
        }

        #region Input Functions
        private void set_direction_from_input(Facing dir)
        {
            if ((int)Direction % 180 != (int)dir % 180 && !has_turned )
            {
                StopMoving = false;
                has_turned = true;
                old_dir = Direction;
                old_pos = Position;
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

        public void check_if_turned()
        {
            if (has_moved)
            {
                if (has_turned)
                {
                    addGhostShip();
                }
                has_turned = false;
            }
        }

        public static void check_for_turned_players()
        {
            foreach (Player player in all_player_ships)
            {
                player.check_if_turned();
            }
        }
    }
}
