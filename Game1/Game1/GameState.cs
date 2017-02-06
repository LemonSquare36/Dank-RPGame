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

        public enum gameState { Playing, Loading, Puased }

        gameState game = new gameState();

        KeyboardState mPreviousKeyboardState;
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        GraphicsDeviceManager graphicsManager;
        Camera camera = new Camera();
        Vector3 screenScale = Vector3.Zero;
        Color color = Color.Blue;
        GameTime time;

        int loadingInterval;

        #region Declaring the Areas and Menus
        Screen CurrentScreen;

        Area_1 TriangleLand;
        TutorialZone Tutorial;
        Habitation habitation;

        MainMenu mainMenu;
        OptionsMenu Options;
        PuaseScreen Puase;
        CreditsMenu Credits;
        FileSelectScreen fileSelect;
        #endregion


        public void Initialize()
        {
            ErrorFileReset();

            if (game == gameState.Loading && CurrentScreen.getisarea() == true)
            {
                loadingInterval = 10;
            }
            else { loadingInterval = 1; }

            #region Creating the areas
            TriangleLand = new Area_1(true);
            Tutorial = new TutorialZone(true);
            habitation = new Habitation(true);
            #endregion
            #region Creating the Menus
            mainMenu = new MainMenu();
            Options = new OptionsMenu();
            Puase = new PuaseScreen();
            fileSelect = new FileSelectScreen();
            Credits = new CreditsMenu();

            #endregion

            if (CurrentScreen != null)
            {
                CurrentScreen.Initialize();
            }
            else
            {
                CurrentScreen = Credits;
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
            Options.ChangeScreen += HandleScreenChanged;
            Credits.ChangeScreen += HandleScreenChanged;

            CurrentScreen.LoadContent(spriteBatch);

        }

        //The update function for changing the GameStates and for using functions of the current GameStates
        public void Update(GameTime gameTime)
        {
            KeyboardState CurrentKeyBoardState = Keyboard.GetState();
            mPreviousKeyboardState = CurrentKeyBoardState;

            if (game == gameState.Playing)
            {
                CurrentScreen.Update(camera, graphicsManager);
            }
            else if (game == gameState.Puased)
            {

            }

            CurrentScreen.getGameTimePrime(time);

        }
        //Draws the images and textures we use
        public void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(color);

            if (game == gameState.Playing)
            {
                var viewMatrix = camera.Transform(graphicsDevice);

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, viewMatrix * Matrix.CreateScale(1));
                CurrentScreen.Draw();
                spriteBatch.End();
            }


            if (game == gameState.Loading)
            {
                if (font == null)
                {
                    font = Main.GameContent.Load<SpriteFont>("myFont");
                }
                loadingInterval--;
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Loading", new Vector2(600, 400), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                spriteBatch.End();
            }

            if (loadingInterval == 0)
            {
                if (game == gameState.Loading)
                    game = gameState.Playing;
            }
        }

        //The Event that Changes the Screens
        public void HandleScreenChanged(object sender, EventArgs eventArgs)
        {
            bool Load = true;
            //Next Screen is Based off the buttons Name
            switch (CurrentScreen.getNextScreen())
            {
                case "Play":
                    CurrentScreen = habitation;
                    color = Color.LightSlateGray;

                    break;

                case "Option":
                    CurrentScreen = Options;
                    break;

                case "Credit":
                    CurrentScreen = Credits;
                    break;

                case "Back":
                    CurrentScreen = mainMenu;
                    break;

                case "Mains":
                    CurrentScreen = mainMenu;
                    break;

                case "Tutorial":
                    CurrentScreen = Tutorial;
                    break;
                default:
                    Load = false;
                    break;
            }

            CurrentScreen.ButtonReset();
            if (Load)
            {
                game = gameState.Loading;
                Initialize();
                LoadContent(spriteBatch, graphicsDevice, graphicsManager);
            }
        }

        public void getGameTime(GameTime gameTime)
        {
            time = gameTime;
        }
    }
}
