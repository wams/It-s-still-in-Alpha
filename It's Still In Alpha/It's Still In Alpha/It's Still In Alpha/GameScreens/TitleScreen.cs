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
    public class TitleScreen : BaseGameState
    {
        #region Constructor Region

        public TitleScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        #endregion

        #region XNA Functions

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameRef.in_state)
            {
                if (InputHandler.KeyPressed(Keys.D1))
                {
                    StateManager.PushState(GameRef.levelSelect);
                }

                if (InputHandler.KeyPressed(Keys.Escape))
                {
                    Sounds.stop();
                    GameRef.Exit();
                }

                base.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (GameRef.in_state)
            {
                GameRef.spriteBatch.Begin();
                GameRef.spriteBatch.GraphicsDevice.Clear(Color.Green);
                DrawText();
                base.Draw(gameTime);

                GameRef.spriteBatch.End();
            }
        }
        
        public void DrawText()
        {
            GameRef.spriteBatch.DrawString(GameRef.titleFont,
                "It\'s Still in Alpha",
                new Vector2(GameRef.screenRectangle.Center.X - 450, GameRef.screenRectangle.Center.Y - 200),
                Color.Black);
            GameRef.spriteBatch.DrawString(GameRef.subtitleFont, 
                "New Game (Press 1)", 
                new Vector2(GameRef.screenRectangle.Center.X - 100, GameRef.screenRectangle.Center.Y), 
                Color.Black);
            /*GameRef.spriteBatch.DrawString(GameRef.subtitleFont,
                "Start Game (Press 2)",
                new Vector2(GameRef.screenRectangle.Center.X - 100, GameRef.screenRectangle.Center.Y + 75),
                Color.Black);
            GameRef.spriteBatch.DrawString(GameRef.subtitleFont,
                "Save (Press 3)",
                new Vector2(GameRef.screenRectangle.Center.X - 100, GameRef.screenRectangle.Center.Y + 150),
                Color.Black);
            GameRef.spriteBatch.DrawString(GameRef.subtitleFont,
                "Load (Press 4)",
                new Vector2(GameRef.screenRectangle.Center.X - 100, GameRef.screenRectangle.Center.Y + 225),
                Color.Black);*/
            GameRef.spriteBatch.DrawString(GameRef.subtitleFont,
                "Exit Game (Esc)",
                new Vector2(GameRef.screenRectangle.Center.X - 100, GameRef.screenRectangle.Center.Y + 75),
                Color.Black);
        }   

        #endregion

    }
}
