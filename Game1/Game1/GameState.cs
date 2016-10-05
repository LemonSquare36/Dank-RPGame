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
    class GameState
    {    

        KeyboardState mPreviousKeyboardState;
        SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;

        Camera camera = new Camera();

        Area_1 TriangleLand = new Area_1();
        //The Game States get defined here
        public enum GameStates { Menu, Playing }

        private GameStates gameState;
        event EventHandler GameStateChanged;

        public GameStates Gamestate
        {
            get { return gameState; }
            set
            {
                gameState = value;
                OnGameStateChanged();
            }
        }

        //Loads the Content for The GameStates
        public void LoadContent(SpriteBatch spriteBatchMain, GraphicsDevice graphicsDeviceMain)
        {
            spriteBatch = spriteBatchMain;
            graphicsDevice = graphicsDeviceMain;
            switch (gameState)
            {
                case GameStates.Playing:
                    TriangleLand.LoadContent(spriteBatch);
                    break;

                case GameStates.Menu:
                    
                    break;
            }

        }


        //The update function for changing the GameStates and for using functions of the current GameStates
        public void Update(GameTime gameTime)
            {
            KeyboardState CurrentKeyBoardState = Keyboard.GetState();
            mPreviousKeyboardState = CurrentKeyBoardState;
            ChangeGameState(CurrentKeyBoardState);

            switch (gameState)
            {
                case GameStates.Playing:
                    Draw(spriteBatch);
                    TriangleLand.Update();
                    break;

                case GameStates.Menu:

                    break;
            }
        }
        //Draws the images and textures we use
        public void Draw(SpriteBatch spriteBatch)
        {

            switch (gameState)
            {
                case GameStates.Playing:
                    TriangleLand.Draw();
                    break;

                case GameStates.Menu:
                    break;
            }

        }
        //Change the GameState with a button click
        private void ChangeGameState(KeyboardState CurrentKeyBoardState)
        {
            if (CurrentKeyBoardState.IsKeyDown(Keys.Z) == true)
            {
                if (gameState == GameStates.Menu)
                {
                    gameState = GameStates.Playing;
                    LoadContent(spriteBatch, graphicsDevice);
                }

                else if (gameState == GameStates.Playing)
                {
                    gameState = GameStates.Menu;;
                }
            }
        }
        //Prevents errors in GameStates
    private void OnGameStateChanged()
        {
            GameStateChanged?.Invoke(this, EventArgs.Empty);
        }


    }
}
