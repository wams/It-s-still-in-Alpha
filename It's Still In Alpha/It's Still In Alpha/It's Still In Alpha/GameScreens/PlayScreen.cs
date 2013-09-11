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

using It_s_Still_In_Alpha.GameStates;

namespace It_s_Still_In_Alpha.GameScreens
{
    public class PlayScreen : BaseGameState
    {
        #region Map Region

        List<List<Tile>> tiles;
        const int gridSize = 96;
        #endregion

        #region Variables Region

        Ship playerShip;
        List<Ship> Ships;

        #endregion

        #region Constructor Region

        public PlayScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            tiles = new List<List<Tile>>();

            for (int i = 0; i <= GameRef.screenRectangle.Width; i += gridSize)
            {
                List<Tile> tileRow = new List<Tile>();
                for (int j = 0; j <= GameRef.screenRectangle.Height; j += gridSize)
                {
                    
                    Tile newTile = new Tile(new Rectangle(i, j, gridSize, gridSize), new Rectangle(i, j, gridSize, gridSize), GameRef.Content);
                    newTile.Type = 0;

                    if (i == 0 || j == 0 || i >= GameRef.screenRectangle.Width - gridSize || j >= GameRef.screenRectangle.Height - gridSize)
                    {
                        newTile.Type = 1;
                    }
                    //add tile image and type here
                    tileRow.Add(newTile);
                }
                tiles.Add(tileRow);
            }

            playerShip = new Player(GameRef);
            playerShip.Position = new Vector2(5, 3);
            playerShip.SourceSize = gridSize;



            Ships = new List<Ship>();
            Ships.Add(playerShip);

            map_keyboard_input();
        }

        #endregion

        #region XNA Functions

        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;

            foreach (List<Tile> tileRow in tiles)
            {
                foreach (Tile tile in tileRow)
                {
                    switch (tile.Type)
                    {
                        case 0:
                            tile.LoadContent("empty_space");
                            break;
                        case 1:
                            tile.LoadContent("wall_1x1");
                            break;
                        default:
                            break;
                    }
                }
            }

            foreach (Ship ship in Ships)
            {
                if (ship is Player)
                {
                    ship.LoadContent("player_ship");
                }
                else
                {
                    ship.LoadContent("ghost_ship");
                }
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyReleased(Keys.Escape))
            {
                //switch to the title screen
               
            }

            process_input(gameTime);

            foreach (List<Tile> tileRow in tiles)
            {
                foreach (Tile tile in tileRow)
                {
                    tile.Update( gameTime );
                }
            }

            foreach (Ship ship in Ships)
            {
                if (ship is Player)
                {
                    ship.Update(gameTime);
                }
                else
                {
                    ship.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();

            GameRef.GraphicsDevice.Clear(Color.Black);

            foreach (List<Tile> tileRow in tiles)
            {
                foreach (Tile tile in tileRow)
                {
                    tile.Draw(GameRef.spriteBatch);
                }
            }

            foreach (Ship ship in Ships)
            {
                if (ship is Player)
                {
                    ship.Draw(GameRef.spriteBatch);
                }
                else
                {
                    ship.Draw(GameRef.spriteBatch);
                }
            }

            base.Draw(gameTime);

            GameRef.spriteBatch.End();
        }
        #endregion

        #region Input Mappings

        delegate void fpointer(GameTime time);
        Dictionary<Keys, Delegate> input_mappings = new Dictionary<Keys, Delegate>();

        private void map_keyboard_input()
        {
            #region mapping wasd
            fpointer D = new fpointer(playerShip.set_facing_right);
            fpointer W = new fpointer(playerShip.set_facing_up);
            fpointer A = new fpointer(playerShip.set_facing_left);
            fpointer S = new fpointer(playerShip.set_facing_down);
            input_mappings[Keys.D] = D;
            input_mappings[Keys.W] = W;
            input_mappings[Keys.A] = A;
            input_mappings[Keys.S] = S;
            #endregion

            #region mapping change to title screen
            fpointer Escape = new fpointer(back_to_title_screen);
            input_mappings[Keys.Escape] = Escape;
            #endregion
        }

        private void map_gamepad_input()
        {

        }

        private void process_input(GameTime gameTime)
        {
            foreach (Keys key in input_mappings.Keys)
            {
                if (InputHandler.KeyReleased(key))
                {
                    input_mappings[key].DynamicInvoke(gameTime);
                }
            }
        }
        #endregion

        #region Input Functions
        private void back_to_title_screen(GameTime gameTime)
        {
            StateManager.PushState(GameRef.titleScreen);
        }
        #endregion
    }
}
