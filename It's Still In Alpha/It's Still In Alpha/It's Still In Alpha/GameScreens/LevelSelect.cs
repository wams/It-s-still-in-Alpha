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
    public class LevelSelect : BaseGameState
    {
        public string levelSelected;
        public int currentSelection;

        List<string> allLevels = new List<string>() {"Level 1", "Level 2"};

        public LevelSelect(Game game, GameStateManager manager)
            : base(game, manager)
        {
            currentSelection = 0;
            levelSelected = allLevels[currentSelection];
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyPressed(Keys.Right) || InputHandler.KeyPressed(Keys.D))
            {
                currentSelection++;
                if (currentSelection >= allLevels.Count)
                {
                    currentSelection = 0;
                }

                levelSelected = allLevels[currentSelection];
            }
            if (InputHandler.KeyPressed(Keys.Left) || InputHandler.KeyPressed(Keys.A))
            {
                currentSelection--;
                if (currentSelection < 0)
                {
                    currentSelection = allLevels.Count-1;
                }

                levelSelected = allLevels[currentSelection];
            }

            if (InputHandler.KeyPressed(Keys.Enter))
            {
                GameRef.playScreen.LoadLevel(levelSelected);
                StateManager.ChangeState(GameRef.playScreen);
            }

            if (InputHandler.KeyPressed(Keys.Escape))
            {
                StateManager.ChangeState(GameRef.titleScreen);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            GameRef.GraphicsDevice.Clear(Color.Black);

            GameRef.spriteBatch.DrawString(GameRef.subtitleFont, levelSelected, 
                new Vector2(GameRef.screenRectangle.Center.X-25, GameRef.screenRectangle.Center.Y), Color.White);

            GameRef.spriteBatch.DrawString(GameRef.subtitleFont, "Press Enter to play selected level\n(left and right changes selection)",
                new Vector2(GameRef.screenRectangle.Center.X-175, GameRef.screenRectangle.Center.Y+50), Color.White);

            GameRef.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
