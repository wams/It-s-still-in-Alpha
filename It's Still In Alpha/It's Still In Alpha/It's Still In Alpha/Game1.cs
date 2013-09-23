//-----------------------------------------------
// XUI - Game1.cs
// Copyright (C) Peter Reid. All rights reserved.
//-----------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using It_s_Still_In_Alpha.GameStates;
using It_s_Still_In_Alpha.GameScreens;
using It_s_Still_In_Alpha;


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
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public GameStateManager stateManager;
        public TitleScreen titleScreen;
        public PlayScreen playScreen;
        public LevelSelect levelSelect;
        public bool in_state = false;
        public bool input_off = false;

        #region Screen Properties

        const int screenWidth = 96 * 20;
        const int screenHeight = 96 * 12;
        bool fullscreen = false;//true;

        public readonly Rectangle screenRectangle;

        public SpriteFont titleFont;
        public SpriteFont subtitleFont;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.PreferMultiSampling = true;

            graphics.IsFullScreen = fullscreen;

            screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            Content.RootDirectory = "Content";

            Sounds.loadSounds(Content);

            Components.Add(new InputHandler(this));

            stateManager = new GameStateManager(this);
            Components.Add(stateManager);

            titleScreen = new TitleScreen(this, stateManager);
            playScreen = new PlayScreen(this, stateManager);
            levelSelect = new LevelSelect(this, stateManager);

            stateManager.ChangeState(titleScreen);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
           _G.Game = this;

		    // add core components
		    Components.Add( new GamerServicesComponent( this ) );

		    // add layers
		    UiLayer = new UiLayer( this );
		    _G.UI = UiLayer;

		    // add other components
		    _G.GameInput = new GameInput( (int)E_GameButton.Count, (int)E_GameAxis.Count );
		    GameControls.Setup(); // initialise mappings

		    base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
         
            // Create a new SpriteBatch, which can be used to draw textures.
            Guide.NotificationPosition = NotificationPosition.BottomRight;

		    // startup ui
		    UiLayer.Startup( Content );

		    // setup debug menu
	    #if !RELEASE
		    //_UI.SetupDebugMenu( null );
	    #endif

            titleFont = Content.Load<SpriteFont>("Fonts/titleFont");
            subtitleFont = Content.Load<SpriteFont>("Fonts/subtitleFont");

		    base.LoadContent();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
       	protected override void UnloadContent()
	    {
		    // shutdown ui
		    UiLayer.Shutdown();

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
            if (!in_state)
            {
                IsRunningSlowly = gameTime.IsRunningSlowly;

                float frameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                // update input
                _G.GameInput.Update(frameTime);

#if !RELEASE
                Input input = _G.GameInput.GetInput(0);

                if (input.ButtonJustPressed((int)E_UiButton.Quit)&&!input_off)
                    this.Exit();
#endif

#if !RELEASE
                // update debug menu
                //_UI.DebugMenuActive = _UI.DebugMenu.Update( frameTime );
#endif

                // TODO - other stuff here ...

                // update ui
                UiLayer.Update(frameTime);


                // TODO: Add your update logic here

               
            } base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           GraphicsDevice.Clear( Color.Black );

		    float frameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

		    // TODO - other stuff here ...

		    // render ui
            if(!in_state)
		        UiLayer.Render( frameTime );

	    #if !RELEASE
		    // render debug menu
		    //_UI.DebugMenu.Render();
	    #endif

		    base.Draw( gameTime );
        }

	    public  UiLayer						UiLayer;
	    public  bool						IsRunningSlowly;

        public void controls_in_game()
        {
            Components.Add(new InputHandler(this));
        }

        public void controls_in_menu()
        {
            _G.GameInput = new GameInput((int)E_GameButton.Count, (int)E_GameAxis.Count);
            GameControls.Setup(); // initialise mappings
        }
	//
    };
};
