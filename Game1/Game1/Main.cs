using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace RPGame
{
    public class Main : Game
    {
        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        CrashHandler CrashHandle = new CrashHandler();

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

        //Constructor
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            window = Window;
            IsMouseVisible = true;
            //Window.IsBorderless = true;

            //The class that basically runs the game
            theGameState = new GameState();
        }
        //Utilizes the crash manager (Cancer) and Initializes GameState
        protected override void Initialize()
        {
           // CrashHandle.CrashCheck();
            CrashHandle.CrashFileMake();

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
        //NOt used but create when the prject was made. Kept in case a use apeared
        protected override void UnloadContent()
        {
            //Unload any non ContentManager content here
        }
        //Updates the Game
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                CrashHandle.CrashFileRemove();
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
    }
}
