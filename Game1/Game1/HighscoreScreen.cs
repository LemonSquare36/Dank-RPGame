using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


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

            //Get current mouse state
            Vector2 worldPosition = MousePos();

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
