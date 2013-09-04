using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using It_s_Still_In_Alpha.GameStates;

namespace It_s_Still_In_Alpha.GameScreens
{
    public abstract partial class BaseGameState : GameState
    {
        #region Variables Region

        protected Game1 GameRef;

        protected PlayerIndex playerIndexInControl;

        #endregion

        #region Constructor Region

        public BaseGameState(Game game, GameStateManager manager)
            : base(game, manager)
        {
            GameRef = (Game1)game;

            playerIndexInControl = PlayerIndex.One;
        }

        #endregion

        #region XNA Functions

        protected override void LoadContent()
        {
            ContentManager Content = Game.Content;

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        #endregion
    }
}
