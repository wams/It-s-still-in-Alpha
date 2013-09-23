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
    public class WinScreen : BaseGameState
    {
        private int score;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public WinScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
            score = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyPressed(Keys.Enter))
            {
                //change this to main menu or whatever wally used.
                StateManager.ChangeState(GameRef.titleScreen);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();

            GameRef.GraphicsDevice.Clear(Color.Black);

            GameRef.spriteBatch.DrawString(GameRef.titleFont, "Congratulations\nYou got a score of\n" + score, 
                new Vector2(GameRef.screenRectangle.Center.X - 350, GameRef.screenRectangle.Center.Y - 250), Color.White);

            GameRef.spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
