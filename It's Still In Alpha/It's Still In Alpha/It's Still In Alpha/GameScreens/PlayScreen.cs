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
        const int gridSize = 64;
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
                    Tile newTile = new Tile(new Rectangle(i, j, gridSize, gridSize), new Rectangle(i, j, gridSize, gridSize));
                    tileRow.Add(newTile);
                }
                tiles.Add(tileRow);
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
                    tile.LoadContent(GameRef.GraphicsDevice);
                }
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (List<Tile> tileRow in tiles)
            {
                foreach (Tile tile in tileRow)
                {
                    tile.Update( gameTime );
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();

            foreach (List<Tile> tileRow in tiles)
            {
                foreach (Tile tile in tileRow)
                {
                    tile.Draw(GameRef.spriteBatch);
                }
            }

            base.Draw(gameTime);

            GameRef.spriteBatch.End();
        }
        #endregion

    }
}
