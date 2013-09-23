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
            ContentManager Content = GameRef.Content;
            GameRef.titleFont = Content.Load<SpriteFont>("Fonts/titleFont");
            GameRef.subtitleFont = Content.Load<SpriteFont>("Fonts/subtitleFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyPressed(Keys.D1))
            {
                StateManager.ChangeState(GameRef.playScreen);
            }

            if (InputHandler.KeyReleased(Keys.Escape))
            {
                GameRef.Exit();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            GameRef.spriteBatch.GraphicsDevice.Clear(Color.Black);
            DrawText();
            base.Draw(gameTime);

            GameRef.spriteBatch.End();
        }

        public void DrawText()
        {
            GameRef.spriteBatch.DrawString(GameRef.titleFont,
                "Game Title",
                new Vector2(GameRef.screenRectangle.Center.X - 250, GameRef.screenRectangle.Center.Y - 200),
                Color.Black);
            GameRef.spriteBatch.DrawString(GameRef.subtitleFont,
                "Resume (Press 1)",
                new Vector2(GameRef.screenRectangle.Center.X - 100, GameRef.screenRectangle.Center.Y),
                Color.Black);
            GameRef.spriteBatch.DrawString(GameRef.subtitleFont,
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
                Color.Black);
            GameRef.spriteBatch.DrawString(GameRef.subtitleFont,
                "Exit Game (Esc)",
                new Vector2(GameRef.screenRectangle.Center.X - 100, GameRef.screenRectangle.Center.Y + 375),
                Color.Black);
        }

        #endregion

    }
}
