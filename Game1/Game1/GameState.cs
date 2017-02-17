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

// Holds the Screens and most of the game
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
        Color prevColor;
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
        HighscoreScreen scorescreen;
        #endregion

        HighScores highscores;
        public GameState()
         {
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
            scorescreen = new HighscoreScreen();
            #endregion

            mainMenu.ChangeScreen += HandleScreenChanged;
            Options.ChangeScreen += HandleScreenChanged;
            Credits.ChangeScreen += HandleScreenChanged;
            scorescreen.ChangeScreen += HandleScreenChanged;
            habitation.changeScreen += PlayerChangeScreen;
            Tutorial.changeScreen += PlayerChangeScreen;
        }

        /// <summary>
        /// Called each time a screen is changed. Initializes them
        /// </summary>
        public void Initialize()
        {
            ErrorFileReset();

            if (game == gameState.Loading && CurrentScreen.getisarea() == true)
            {
                loadingInterval = 10;
                prevColor = color;
                color = Color.LightSeaGreen;
            }
            else { loadingInterval = 1; }

            if (CurrentScreen != null)
            {
                CurrentScreen.Initialize();
            }
            else
            {
                CurrentScreen = Credits;
                CurrentScreen.Initialize();
            }
            highscores = new HighScores();
        }

        //Loads the Content for The screens
        public void LoadContent(SpriteBatch spriteBatchMain, GraphicsDevice graphicsDeviceMain, GraphicsDeviceManager graphicsManagerMain)
        {
            spriteBatch = spriteBatchMain;
            graphicsDevice = graphicsDeviceMain;
            graphicsManager = graphicsManagerMain;


            CurrentScreen.LoadContent(spriteBatch);

        }

        //The update function for changing the screen and for using functions of the current screens
        public void Update(GameTime gameTime)
        {
            KeyboardState CurrentKeyBoardState = Keyboard.GetState();
            mPreviousKeyboardState = CurrentKeyBoardState;
            //Update if its playing
            if (game == gameState.Playing)
            {
                CurrentScreen.Update(camera, graphicsManager,graphicsDevice);
            }
            //Update if its puased
            else if (game == gameState.Puased)
            {

            }
            //get that game time
            CurrentScreen.getGameTimePrime(time);

        }
        //Draws the images and textures we use
        public void Draw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(color);
            //Draws the game when its playing
            if (game == gameState.Playing)
            {
                var viewMatrix = camera.Transform(graphicsDevice);

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, viewMatrix * Matrix.CreateScale(1));
                CurrentScreen.Draw();
                spriteBatch.End();
                
            }

            //Runs if the game is loading
            if (game == gameState.Loading)
            {
                if (font == null)
                {
                    font = Main.GameContent.Load<SpriteFont>("myFont");
                }
                //Loading screen time
                loadingInterval--;
                //Draws loading screen items
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Loading", new Vector2(600, 400), Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
                spriteBatch.End();
            }
            //Changes it from loading to playing
            if (loadingInterval == 0)
            {
                if (game == gameState.Loading)
                    color = prevColor;
                    game = gameState.Playing;
            }
        }

        //The Event that Changes the Screens
        public void HandleScreenChanged(object sender, EventArgs eventArgs)
        {
            bool Load = true;
            //Next Screen is Based off the buttons Name (not garenteed to even load a new screen)
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
                case "OptionsFullscreen":
                    Load = false;
                    camera.ChangeScreenSize(graphicsManager);
                    break; 

                default:
                    Load = false;
                    break;
            }
            //Resets the button on the screen
            CurrentScreen.ButtonReset();
            //Loads if a new screen is activated
            if (Load)
            {
                game = gameState.Loading;
                Initialize();
                LoadContent(spriteBatch, graphicsDevice, graphicsManager);
            }
        }

        public void PlayerChangeScreen(object sender, EventArgs eventArgs)
        {
            CurrentScreen = scorescreen;
            color = Color.Blue;
            camera.posReset();
            Initialize();
            LoadContent(spriteBatch, graphicsDevice, graphicsManager);
        }
        //Gets the gametime
        public void getGameTime(GameTime gameTime)
        {
            time = gameTime;
        }
    }
}
