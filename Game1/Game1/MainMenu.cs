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
using System.IO;

namespace RPGame
{
    class MainMenu : MenuManager
    {
        Button Test, Option, Credit;
        Texture2D Play, PlayHover, Options, OptionsHover, Background, Credits, CreditsHover;

        public override void Initialize()
        {
            base.Initialize();
        }
        //Loads content for this class
        public override void LoadContent(SpriteBatch spirteBatchMain)
        {
            spriteBatch = spirteBatchMain;

            //Load textures before buttons
            #region Texture Load
            Play = Main.GameContent.Load<Texture2D>("buttons/Play");
            PlayHover = Main.GameContent.Load<Texture2D>("buttons/Play_Hover");
            Options = Main.GameContent.Load<Texture2D>("buttons/options");
            OptionsHover = Main.GameContent.Load<Texture2D>("buttons/options_hover");
            Credits = Main.GameContent.Load<Texture2D>("buttons/credits");
            CreditsHover = Main.GameContent.Load<Texture2D>("buttons/credits_hover");

            Background = Main.GameContent.Load<Texture2D>("Sprites/TutorialSprites/main_Menu_Background");
            #endregion

            #region Button Load
            Test = new Button(new Vector2(200, 75), 400, 100, Play, PlayHover, "Play");
            Option = new Button(new Vector2(200, 200), 400, 100, Options, OptionsHover, "Option");
            Credit = new Button(new Vector2(200, 325), 400, 100, Credits, CreditsHover, "Credit");
            #endregion

            //Important or the event doesnt work
            Test.ButtonClicked += ButtonClicked;
            Option.ButtonClicked += ButtonClicked;
            Credit.ButtonClicked += ButtonClicked;
        }
        //Updates the Buttons and other things for the menu
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            //Get current mouse state
            mouse = Mouse.GetState();

            //Update the buttons
            Test.Update(mouse);
            Option.Update(mouse);
            Credit.Update(mouse);
        }
        //Draws the Buttons
        public override void Draw()
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), null, null);
            Test.Draw(spriteBatch);
            Option.Draw(spriteBatch);
            Credit.Draw(spriteBatch);
        }
        //Used for edge detection
        public override void ButtonReset()
        {
            Test.ButtonReset();
            Option.ButtonReset();
            Credit.ButtonReset();

        }

    }
}
