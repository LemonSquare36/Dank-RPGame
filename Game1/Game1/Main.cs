using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace RPGame
{
    public class Main : Game
    {
        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Global global = new Global();

        //Allows other classes to load code from content manager - Convient
        private static ContentManager content;
        public static ContentManager GameContent
        {
            get { return content; }
            set { content = value; }
        }
        //
        private static GameWindow window;
        public static GameWindow gameWindow
        {
            get { return window; }
            set { window = value; }
        }

        GameState theGameState;
        CrashHandler CrashHandle;

        //Constructor
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            window = Window;
            IsMouseVisible = true;

            //The class that basically runs the game
            theGameState = new GameState();
            //Class that runs the crashhandling code
            CrashHandle = new CrashHandler();
        }
        //Utilizes the crash manager and Initializes GameState
        protected override void Initialize()
        {
            CrashHandle.CrashCheck();
            CrashHandle.CrashFileMake();
            global.ErrorFileReset();


            theGameState.Initialize();
            base.Initialize();
        }

        //Loads Everything
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            theGameState.LoadContent(spriteBatch, GraphicsDevice, graphics);
            
        }
        //Updates the Game
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            theGameState.Update(gameTime);
            base.Update(gameTime);
            theGameState.getGameTime(gameTime);
        }

        //Draws the Game
        protected override void Draw(GameTime gameTime)
        {          
            theGameState.Draw(spriteBatch);
            base.Draw(gameTime);
        }
        protected override void OnExiting(object sender, EventArgs e)
        {
            CrashHandle.CrashFileRemove();
        }
        
    }
}
