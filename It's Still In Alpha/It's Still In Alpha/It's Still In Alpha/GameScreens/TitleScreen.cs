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
            GameRef.font = Content.Load<SpriteFont>("Fonts/myFont");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyPressed(Keys.T))
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
            GameRef.spriteBatch.GraphicsDevice.Clear(Color.Red);
            DrawText();
            base.Draw(gameTime);
            
            GameRef.spriteBatch.End();
        }
        
        public void DrawText()
        {
            GameRef.spriteBatch.DrawString(GameRef.font, 
                "Resume", 
                new Vector2(GameRef.screenRectangle.Center.X, GameRef.screenRectangle.Center.Y), 
                Color.Black);
            GameRef.spriteBatch.DrawString(GameRef.font,
                "Start Game",
                new Vector2(GameRef.screenRectangle.Center.X, GameRef.screenRectangle.Center.Y - 100),
                Color.Black);

        }
        #endregion

    }
}
