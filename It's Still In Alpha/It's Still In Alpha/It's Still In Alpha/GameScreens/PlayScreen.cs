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

        #region Timer Region

        int moveTime = 0;
        int enemyMoveTime = 0;
        //at 60fps, 6 is 1 move every 10th of a second
        int maxMoveTime = 6;

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
                StateManager.PushState(GameRef.titleScreen);
            }
            foreach (List<Tile> tileRow in tiles)
            {
                foreach (Tile tile in tileRow)
                {
                    tile.Update(gameTime);
                }
            }

            foreach (Ship ship in Ships)
            {
                Vector2 Position = ship.Position;
                if (ship is Player)
                {
                    ship.Update(gameTime);


                    if (moveTime > maxMoveTime)
                    {
                        switch (ship.Direction)
                        {
                            case Ship.Facing.Up:
                                if (Position.Y > 0)
                                {
                                    if (tiles[(int)Position.X][(int)Position.Y - 1].Type == 0)
                                    {
                                        ship.Position = new Vector2(Position.X, Position.Y - 1);
                                    }
                                }
                                break;
                            case Ship.Facing.Down:
                                if (Position.Y < tiles[0].Count - 1)
                                {
                                    if (tiles[(int)Position.X][(int)Position.Y + 1].Type == 0)
                                    {
                                        ship.Position = new Vector2(Position.X, Position.Y + 1);
                                    }
                                }
                                break;
                            case Ship.Facing.Left:
                                if (Position.X > 0)
                                {
                                    if (tiles[(int)Position.X - 1][(int)Position.Y].Type == 0)
                                    {
                                        ship.Position = new Vector2(Position.X - 1, Position.Y);
                                    }
                                }
                                break;
                            case Ship.Facing.Right:
                                if (Position.X < tiles.Count - 1)
                                {
                                    if (tiles[(int)Position.X + 1][(int)Position.Y].Type == 0)
                                    {
                                        ship.Position = new Vector2(Position.X + 1, Position.Y);
                                    }
                                }
                                break;
                        }
                        moveTime = 0;
                    }
                    else
                    {
                        moveTime++;
                    }
                }
                else
                {
                    ship.Update(gameTime);

                    if (moveTime > maxMoveTime)
                    {
                        switch (ship.Direction)
                        {
                            case Ship.Facing.Up:
                                if (Position.Y > 0)
                                {
                                    if (tiles[(int)Position.X][(int)Position.Y - 1].Type == 0)
                                    {
                                        ship.Position = new Vector2(Position.X, Position.Y - 1);
                                    }
                                    else
                                    {
                                        ship.Direction = Ship.Facing.Down;
                                    }
                                }
                                break;
                            case Ship.Facing.Down:
                                if (Position.Y < tiles[0].Count - 1)
                                {
                                    if (tiles[(int)Position.X][(int)Position.Y + 1].Type == 0)
                                    {
                                        ship.Position = new Vector2(Position.X, Position.Y + 1);
                                    }
                                    else
                                    {
                                        ship.Direction = Ship.Facing.Up;
                                    }
                                }
                                break;
                            case Ship.Facing.Left:
                                if (Position.X > 0)
                                {
                                    if (tiles[(int)Position.X - 1][(int)Position.Y].Type == 0)
                                    {
                                        ship.Position = new Vector2(Position.X - 1, Position.Y);
                                    }
                                    else
                                    {
                                        ship.Direction = Ship.Facing.Right;
                                    }
                                }
                                break;
                            case Ship.Facing.Right:
                                if (Position.X < tiles.Count - 1)
                                {
                                    if (tiles[(int)Position.X + 1][(int)Position.Y].Type == 0)
                                    {
                                        ship.Position = new Vector2(Position.X + 1, Position.Y);
                                    }
                                    else
                                    {
                                        ship.Direction = Ship.Facing.Left;
                                    }
                                }
                                break;
                        }
                        enemyMoveTime = 0;
                    }
                    else
                    {
                        enemyMoveTime++;
                    }
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

    }
}
