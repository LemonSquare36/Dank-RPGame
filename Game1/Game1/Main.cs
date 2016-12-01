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

        private static ContentManager content;
        public static ContentManager GameContent
        {
            get { return content; }
            set { content = value; }
        }

        GameState theGameState = new GameState();
        

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            CrashHandle.CrashCheck();
            CrashHandle.CrashFileMake();

            theGameState.Initialize();
            base.Initialize();
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            theGameState.LoadContent(spriteBatch, GraphicsDevice, graphics);
        }

        protected override void UnloadContent()
        {
            //Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                CrashHandle.CrashFileRemove();
                Exit();
            }

            theGameState.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);           
            theGameState.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
