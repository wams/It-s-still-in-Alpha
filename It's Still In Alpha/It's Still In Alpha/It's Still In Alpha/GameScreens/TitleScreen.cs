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

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyPressed(Keys.T))
            {
                StateManager.PushState(GameRef.playScreen);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.spriteBatch.Begin();
            GameRef.spriteBatch.GraphicsDevice.Clear(Color.Red);
            base.Draw(gameTime);

            GameRef.spriteBatch.End();
        }
        #endregion

    }
}
