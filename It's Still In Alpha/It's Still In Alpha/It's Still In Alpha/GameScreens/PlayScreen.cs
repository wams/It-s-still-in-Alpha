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

        Player playerShip;

        #endregion

        #region Timer Region

        int moveTime = 0;
        //at 60fps, 6 is 1 move every 10th of a second
        int maxMoveTime = 0;

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
            playerShip.Index = new Vector2(5, 3);
            playerShip.Position = playerShip.Index;
            playerShip.SourceSize = gridSize;

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

            playerShip.LoadContent("player_ship");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            process_input(gameTime);

            foreach (List<Tile> tileRow in tiles)
            {
                foreach (Tile tile in tileRow)
                {
                    tile.Update(gameTime);
                }
            }

            if (moveTime > maxMoveTime)
            {
                Vector2 Index = playerShip.Index;
                switch (playerShip.Direction)
                {
                    case Ship.Facing.Up:
                        if (Index.Y > 0)
                        {
                            if (tiles[(int)Index.X][(int)Index.Y - 1].Type != 0)
                            {
                                playerShip.StopMoving = true;
                            }
                            else
                            {
                                playerShip.StopMoving = false;
                            }
                        }
                        break;
                    case Ship.Facing.Down:
                        if (Index.Y < tiles[0].Count - 1)
                        {
                            if (tiles[(int)Index.X][(int)Index.Y + 1].Type != 0)
                            {
                                playerShip.StopMoving = true;
                            }
                            else
                            {
                                playerShip.StopMoving = false;
                            }
                        }
                        break;
                    case Ship.Facing.Left:
                        if (Index.X > 0)
                        {
                            if (tiles[(int)Index.X - 1][(int)Index.Y].Type != 0)
                            {
                                playerShip.StopMoving = true;
                            }
                            else
                            {
                                playerShip.StopMoving = false;
                            }
                        }
                        break;
                    case Ship.Facing.Right:
                        if (Index.X < tiles.Count - 1)
                        {
                            if (tiles[(int)Index.X + 1][(int)Index.Y].Type != 0)
                            {
                                playerShip.StopMoving = true;
                            }
                            else
                            {
                                playerShip.StopMoving = false;
                            }
                        }
                        break;
                }
            }
            else
            {
                moveTime++;
            }

            playerShip.Update(gameTime, playerShip.StopMoving);
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

            playerShip.Draw(GameRef.spriteBatch);

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
