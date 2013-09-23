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

using It_s_Still_In_Alpha.GameStates;
using It_s_Still_In_Alpha.GameScreens;

// E_Layer
public enum E_Layer
{
    UI = 0,

    // etc ...

    Count,
};

namespace It_s_Still_In_Alpha
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager   graphics;
        public SpriteBatch      spriteBatch;

        public GameStateManager stateManager;
        public TitleScreen      titleScreen;
        public PlayScreen       playScreen;

        // ------------XUI-------------------
        private UiLayer         UiLayer;

        public bool             IsRunningSlowly;
        // ------------END-------------------

        #region Screen Properties

        const int screenWidth = 96 * 20;
        const int screenHeight = 96 * 12;

        bool fullscreen = false;

        public readonly Rectangle screenRectangle;

        public SpriteFont titleFont;
        public SpriteFont subtitleFont;

        #endregion

        public Game1() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            graphics.IsFullScreen = fullscreen;

            screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            Content.RootDirectory = "Content";

            Components.Add(new InputHandler(this));

            stateManager = new GameStateManager(this);
            Components.Add(stateManager);

            titleScreen = new TitleScreen(this, stateManager);
            playScreen = new PlayScreen(this, stateManager);

            stateManager.ChangeState(titleScreen);
            // ------------XUI-------------------
//            graphics = new GraphicsDeviceManager(this);

//            graphics.PreferredBackBufferWidth = screenWidth;
//            graphics.PreferredBackBufferHeight = screenHeight;
//            graphics.IsFullScreen = true; 
//            graphics.PreferMultiSampling = true;

//#if PROFILE
//        IsFixedTimeStep = false;
//        Graphics.SynchronizeWithVerticalRetrace = false;
//#endif

//            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // ------------XUI-------------------
            //// TODO: Add your initialization logic here
            //_G.Game = this;

            //// add core components
            //Components.Add(new GamerServicesComponent(this));

            //// add layers
            //UiLayer = new UiLayer();
            //_G.UI = UiLayer;

            //// add other components
            //_G.GameInput = new GameInput((int)E_GameButton.Count, (int)E_GameAxis.Count);
            //GameControls.Setup(); // initialise mappings
		
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
             //Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // ------------XUI-------------------
//            Guide.NotificationPosition = NotificationPosition.BottomRight;

//            // startup ui
//            UiLayer.Startup(Content);

//            // setup debug menu
//#if !RELEASE
//            _UI.SetupDebugMenu(null);
//#endif

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            //_UI.Shutdown();
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
            // ------------XUI-------------------
//            IsRunningSlowly = gameTime.IsRunningSlowly;

//            float frameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

//            // update input
//            _G.GameInput.Update(frameTime);

//#if !RELEASE
//            Input input = _G.GameInput.GetInput(0);

//            if (input.ButtonJustPressed((int)E_UiButton.Quit))
//                this.Exit();
//#endif

//#if !RELEASE
//            // update debug menu
//            _UI.DebugMenuActive = _UI.DebugMenu.Update(frameTime);
//#endif

//            // TODO - other stuff here ...

//            // update ui
//            UiLayer.Update(frameTime);

//            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
            // ------------XUI-------------------
//            GraphicsDevice.Clear(Color.Black);

//            float frameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

//            // render ui 
//            UiLayer.Render(frameTime);
//#if !RELEASE 
//            // render debug menu
//            _UI.DebugMenu.Render();
//#endif 
//            base.Draw(gameTime);
        }
    }
}
