using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace It_s_Still_In_Alpha
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {
        #region Variable Region

        static KeyboardState keyboardState;
        static KeyboardState prevKeyboardState;

        static MouseState mouseState;
        static MouseState prevMouseState;

        static GamePadState[] gamePadStates;
        static GamePadState[] prevGamePadStates;

        #endregion

        #region Property Region

        public static KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }

        public static KeyboardState PrevKeyboardState
        {
            get { return prevKeyboardState; }
        }

        public static MouseState MouseState
        {
            get { return mouseState; }
        }

        public static MouseState PrevMouseState
        {
            get { return prevMouseState; }
        }

        public static GamePadState[] GamepadState
        {
            get { return gamePadStates; }
        }

        public static GamePadState[] PrevGamepadState
        {
            get { return prevGamePadStates; }
        }

        #endregion

        #region Constructor Region

        public InputHandler(Game game)
            : base(game)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            gamePadStates = new GamePadState[Enum.GetValues(typeof(PlayerIndex)).Length];

            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                gamePadStates[(int)index] = GamePad.GetState(index);
            }
        }

        #endregion

        #region XNA Region

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            prevGamePadStates = (GamePadState[])gamePadStates.Clone();
            foreach (PlayerIndex index in Enum.GetValues(typeof(PlayerIndex)))
            {
                gamePadStates[(int)index] = GamePad.GetState(index);
            }

            base.Update(gameTime);
        }

        #endregion

        #region My Functions Region

        public static void Reset()
        {
            prevKeyboardState = keyboardState;
            prevMouseState = mouseState;
        }

        #region Keyboard Functions

        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) &&
                prevKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) &&
                prevKeyboardState.IsKeyUp(key);
        }

        public static bool KeyHeld(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        #endregion

        #region Mouse Functions

        //Need to do mouse functions

        #endregion

        #region Gamepad Functions

        public static bool ButtonReleased(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonUp(button) &&
                prevGamePadStates[(int)index].IsButtonDown(button);
        }

        public static bool ButtonPressed(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonDown(button) &&
                prevGamePadStates[(int)index].IsButtonUp(button);
        }

        public static bool ButtonHeld(Buttons button, PlayerIndex index)
        {
            return gamePadStates[(int)index].IsButtonDown(button);
        }

        #endregion

        #endregion
    }
}
