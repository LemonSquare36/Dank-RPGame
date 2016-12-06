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
using System.IO;
using System.Collections;
using System.Diagnostics;

// Loads the Content for the various GameStates and allows the switching between GameStates
namespace RPGame
{
    class GameState : Global
    {
        Dictionary<string, int> screens = new Dictionary<string, int>();

        KeyboardState mPreviousKeyboardState;
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphicsManager;
        Camera camera = new Camera();
        Vector3 screenScale = Vector3.Zero;

        #region Declaring the Areas and Menus
        Screen CurrentScreen;

        Area_1 TriangleLand;
        TutorialZone Tutorial;

        MainMenu mainMenu;
        OptionsMenu Options;
        PuaseScreen Puase;
        FileSelectScreen fileSelect;
        #endregion


        public void Initialize()
        {
            ErrorFileReset();

            #region Creating the areas
            TriangleLand = new Area_1();
            Tutorial = new TutorialZone();
            #endregion
            #region Creating the Menus
            mainMenu = new MainMenu();
            Options = new OptionsMenu();
            Puase = new PuaseScreen();
            fileSelect = new FileSelectScreen();
            #endregion

            if (CurrentScreen != null)
            {
                CurrentScreen.Initialize();
            }
            else
            {
                CurrentScreen = mainMenu;
                CurrentScreen.Initialize();
            }
        }

        //Loads the Content for The GameStates
        public void LoadContent(SpriteBatch spriteBatchMain, GraphicsDevice graphicsDeviceMain, GraphicsDeviceManager graphicsManagerMain)
        {
            spriteBatch = spriteBatchMain;
            graphicsDevice = graphicsDeviceMain;
            graphicsManager = graphicsManagerMain;

            mainMenu.ChangeScreen += HandleScreenChanged;

            CurrentScreen.LoadContent(spriteBatch);
        }

        //The update function for changing the GameStates and for using functions of the current GameStates
        public void Update(GameTime gameTime)
        {
            KeyboardState CurrentKeyBoardState = Keyboard.GetState();
            mPreviousKeyboardState = CurrentKeyBoardState;

            Draw(spriteBatch);
            CurrentScreen.Update(camera, graphicsManager);

        }
        //Draws the images and textures we use
        public void Draw(SpriteBatch spriteBatch)
        {
            var viewMatrix = camera.Transform(graphicsDevice);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, viewMatrix * Matrix.CreateScale(1));
            CurrentScreen.Draw();
            spriteBatch.End();
        }

        //The Event that Changes the Screens
        public void HandleScreenChanged(object sender, EventArgs eventArgs)
        {
            bool Load = true;
            //Next Screen is Based off the buttons Name
            switch (CurrentScreen.getNextScreen())
            {
                case "Play":
                    CurrentScreen = TriangleLand;
                    break;

                case "Option":
                   CurrentScreen = Options;
                    break;
                default:
                    Load = false;
                    break;
                        
            }
            if (Load)
            LoadContent(spriteBatch, graphicsDevice, graphicsManager);
        }
    }
}
