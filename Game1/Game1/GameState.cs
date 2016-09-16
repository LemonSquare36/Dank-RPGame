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
    class GameState
    {
        KeyboardState mPreviousKeyboardState;

        Play GamePlaying;
        Menu menu;

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


        public void LoadContent()
        {
            gameState = GameStates.Menu;
            switch (gameState)
            {
                case GameStates.Playing:

                    break;

                case GameStates.Menu:

                    break;
            }

        }



        public void Update(GameTime gameTime)
            {
            KeyboardState CurrentKeyBoardState = Keyboard.GetState();
            mPreviousKeyboardState = CurrentKeyBoardState;
            ChangeGameState(CurrentKeyBoardState);

            switch (gameState)
            {
                case GameStates.Playing:

                    break;

                case GameStates.Menu:

                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            switch (gameState)
            {
                case GameStates.Playing:
                    break;

                case GameStates.Menu:
                    break;
            }

        }
        private void ChangeGameState(KeyboardState CurrentKeyBoardState)
        {
            if (CurrentKeyBoardState.IsKeyDown(Keys.Z) == true)
            {
                if (gameState == GameStates.Menu)
                {
                    gameState = GameStates.Playing;
                }

                if (gameState == GameStates.Playing)
                {
                    gameState = GameStates.Menu;
                }
            }
        }

    private void OnGameStateChanged()
        {
            GameStateChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
