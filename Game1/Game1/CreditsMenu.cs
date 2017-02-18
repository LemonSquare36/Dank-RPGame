using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGame
{
    class CreditsMenu : MenuManager
    {
        Button Mains;
        Texture2D MainMenu, MainMenuHover, Background;

        public override void Initialize()
        {
            base.Initialize();
        }
        //Loads the BUttons and Textures
        public override void LoadContent(SpriteBatch spirteBatchMain)
        {
            spriteBatch = spirteBatchMain;
            Background = Main.GameContent.Load<Texture2D>("Sprites/credits_Background");

            //Load textures before buttons
            #region Texture Load
            MainMenu = Main.GameContent.Load<Texture2D>("buttons/mainmenu");
            MainMenuHover = Main.GameContent.Load<Texture2D>("buttons/mainmenu_hover");
            #endregion

            #region Button Load
            Mains = new Button(new Vector2(200, 325), 400, 100, MainMenu, MainMenuHover, "Mains");
            #endregion

            //Important or the event doesnt work
            Mains.ButtonClicked += ButtonClicked;

            base.LoadContent(spriteBatch);

        }
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager, GraphicsDevice graphicsDevice)
        {
            //Get current mouse state
            Vector2 worldPosition = MousePos();

            //Update the buttons
            Mains.Update(mouse,worldPosition);
        }
        //Draws the buttons
        public override void Draw()
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), null, null);
            Mains.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Made By: Josh Glover, Isaac Allen, Michael Maher", new Vector2(150, 200), Color.Black);
            spriteBatch.DrawString(font, "Studio:wSilentwStudios", new Vector2(250, 100), Color.Black);
            spriteBatch.DrawString(font, "Cognitive thought media", new Vector2(250, 0), Color.Black);
        }
        //Used for edge detection
        public override void ButtonReset()
        {
            Mains.ButtonReset();

        }
    }
}
