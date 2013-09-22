using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
        List<List<char>> tileType;

        const int gridSize = 96;
        #endregion

        #region Variables Region

        Player playerShip;
        int counter;

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

            tileType = ReadFromFile("Level 1");

            for (int i = 0; i < GameRef.screenRectangle.Width/gridSize; i ++)
            {
                List<Tile> tileRow = new List<Tile>();
                for (int j = 0; j < GameRef.screenRectangle.Height/gridSize; j ++)
                {
                    
                    Tile newTile = new Tile(new Rectangle(i*gridSize, j*gridSize, gridSize, gridSize), new Rectangle(i*gridSize, j*gridSize, gridSize, gridSize), GameRef.Content);
                    newTile.Type = tileType[j][i];

                    /*if (tileType[j][i] != 0)
                    {
                        newTile.Type = 1;
                    }*/

                    //add tile image and type here
                    tileRow.Add(newTile);
                }
                tiles.Add(tileRow);
            }
            Sounds.playGameSong();
            score_tiles();

            playerShip = new Player(GameRef);
            playerShip.Index = new Vector2(5, 3);
            playerShip.Position = playerShip.Index;
            playerShip.SourceSize = gridSize;

            map_keyboard_input();
        }

        #endregion

        #region My Functions

        List<List<char>> ReadFromFile(string levelName)
        {
            List<List<char>> tileType = new List<List<char>>();

            StreamReader levelFile = new StreamReader("Content/Maps/" + levelName + ".txt");

            string line;
            while ((line = levelFile.ReadLine()) != null)
            {
                List<char> row = new List<char>();

                string[] splitLine = line.Split(' ');

                for (int i = 0; i < splitLine.Length; i++)
                {
                    row.Add(Convert.ToChar(splitLine[i]));
                }

                tileType.Add(row);
            }

            return tileType;
        }

        public void LoadLevel(string levelName)
        {
            tiles = new List<List<Tile>>();

            tileType = ReadFromFile(levelName);

            for (int i = 0; i < GameRef.screenRectangle.Width / gridSize; i++)
            {
                List<Tile> tileRow = new List<Tile>();
                for (int j = 0; j < GameRef.screenRectangle.Height / gridSize; j++)
                {

                    Tile newTile = new Tile(new Rectangle(i * gridSize, j * gridSize, gridSize, gridSize), new Rectangle(i * gridSize, j * gridSize, gridSize, gridSize), GameRef.Content);
                    newTile.Type = tileType[j][i];

                    /*if (tileType[j][i] != 0)
                    {
                        newTile.Type = 1;
                    }*/

                    //add tile image and type here
                    tileRow.Add(newTile);
                }
                tiles.Add(tileRow);
            }

            playerShip.score = 10;
            playerShip.Index = new Vector2(5, 3);
            playerShip.Position = playerShip.Index;

            LoadContent();
            score_tiles();
        }

        public void checkWin()
        {
            if (counter == 0)
            {
                //you have won the game exit
            }
        }

        private void score_tiles()
        {
            for (int x = 0; x < tiles.Count; x++)
            {
                for (int y = 0; y < tiles[0].Count; y++)
                {
                    int count = num_of_surrounding_empty_tiles(x, y);
                    if (count > 0)
                        tiles[x][y].Score *= (int)Math.Pow(10, count);
                }
            }
        }

        private bool is_tile_not_empty(int x, int y)
        {
            if (x >= 0 && x < tiles.Count && y >= 0 && y < tiles[0].Count)
            {
                return tiles[x][y].Type != 'a';
            }
            return false;
        }

        private int num_of_surrounding_empty_tiles(int x, int y)
        {
            int count = 0;
            for (double a = 0; a < Math.PI*2; a += Math.PI / 2)
            {
                if ( is_tile_not_empty( x+(int)Math.Cos(a), y+(int)Math.Sin(a) ) )
                {
                    count++;
                }
            }
            return count;
        }

        private void updateScores()
        {
            foreach (Player player in Player.all_player_ships)
            {
                if (player.update_score(tiles))
                {
                    counter--;
                }
            }
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
                        case 'a':
                            counter++;
                            tile.LoadContent("empty_space");
                            tile.SourceRect = new Rectangle(0, 0, tile.tileImage.Bounds.Width, tile.tileImage.Bounds.Width);
                            break;
                        case 'b':
                            tile.LoadContent("wall_1x1");
                            tile.SourceRect = new Rectangle(0, 0, tile.tileImage.Bounds.Width, tile.tileImage.Bounds.Width);
                            break;
                        case 'c':
                            tile.LoadContent("wall_2x2");
                            tile.SourceRect = new Rectangle(0, 0, tile.tileImage.Bounds.Width/2, tile.tileImage.Bounds.Height/2);
                            break;
                        case 'd':
                            tile.LoadContent("wall_2x2");
                            tile.SourceRect = new Rectangle(tile.tileImage.Bounds.Width/2, 0, tile.tileImage.Bounds.Width/2, tile.tileImage.Bounds.Height/2);
                            break;
                        case 'e':
                            tile.LoadContent("wall_2x2");
                            tile.SourceRect = new Rectangle(0, tile.tileImage.Bounds.Height/2, tile.tileImage.Bounds.Width/2, tile.tileImage.Bounds.Height/2);
                            break;
                        case 'f':
                            tile.LoadContent("wall_2x2");
                            tile.SourceRect = new Rectangle(tile.tileImage.Bounds.Width/2, tile.tileImage.Bounds.Height/2, tile.tileImage.Bounds.Width/2, tile.tileImage.Bounds.Height/2);
                            break;
                        case 'g':
                            tile.LoadContent("wall_2x1_astroidBelt1");
                            tile.SourceRect = new Rectangle(0, 0, tile.tileImage.Bounds.Width/2, tile.tileImage.Bounds.Height);
                            break;
                        case 'h':
                            tile.LoadContent("wall_2x1_astroidBelt1");
                            tile.SourceRect = new Rectangle(tile.tileImage.Bounds.Width/2, 0, tile.tileImage.Bounds.Width/2, tile.tileImage.Bounds.Height);
                            break;
                        default:
                            break;
                    }
                }
            }

            playerShip.LoadContent();

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

            Ship.update_all_ships(gameTime);
            Player.check_for_turned_players();
            Ship.tile_collision_all_ships(tiles);
            Ship.collision_all_ships();
            
            updateScores();
            
            checkWin();
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

            Ship.draw_all_ships(GameRef.spriteBatch);

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
            Ship.reset();
            StateManager.PushState(GameRef.titleScreen);
        }
        #endregion
    }
}
