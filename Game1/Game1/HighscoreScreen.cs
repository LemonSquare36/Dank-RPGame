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
    class HighscoreScreen : MenuManager
    {
        HighScores highscores = new HighScores();

        Button goToMain;
        Texture2D MainMenu, MainMenuHover;

        public override void Initialize()
        {
            base.Initialize();
        }
        //Loads the BUttons and Textures
        public override void LoadContent(SpriteBatch spirteBatchMain)
        {
            MainMenu = Main.GameContent.Load<Texture2D>("buttons/mainmenu");
            MainMenuHover = Main.GameContent.Load<Texture2D>("buttons/mainmenu_hover");

            goToMain = new Button(new Vector2(300, 325), 400, 100, MainMenu, MainMenuHover, "Mains");

            spriteBatch = spirteBatchMain;

            goToMain.ButtonClicked += ButtonClicked;

            base.LoadContent(spriteBatch);
        }
        //Updates the Highscores
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager, GraphicsDevice graphicsDevice)
        {
            highscores.ChangeScores();

            //Get current mouse state
            mouse = Mouse.GetState();
            Vector2 worldPosition;
            worldPosition.X = mouse.X / (float)(Main.gameWindow.ClientBounds.Width / 800.0);
            worldPosition.Y = mouse.Y / (float)(Main.gameWindow.ClientBounds.Height / 480.0);

            //Update the buttons
            goToMain.Update(mouse, worldPosition);

        }
        //Draw
        public override void Draw()
        {
            highscores.ScorestoScreen(spriteBatch);
            goToMain.Draw(spriteBatch);
        }
        //Used for edge detection
        public override void ButtonReset()
        {
            goToMain.ButtonReset();
        }

    }
}
