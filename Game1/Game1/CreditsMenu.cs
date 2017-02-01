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
    class CreditsMenu : MenuManager
    {
        Button Mains;
        Texture2D MainMenu, MainMenuHover, Background;

        public override void Initialize()
        {
            base.Initialize();
        }
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
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            //Get current mouse state
            mouse = Mouse.GetState();

            //Update the buttons
            Mains.Update(mouse);
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
