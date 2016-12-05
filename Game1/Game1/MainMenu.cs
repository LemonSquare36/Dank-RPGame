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
        Button Test, Option;
        Texture2D Play, PlayHover, Options, OptionsHover;

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
            #endregion

            #region Button Load
            Test = new Button(new Vector2(100, 300), 400, 100, Play, PlayHover, "Play");
            Option = new Button(new Vector2(100, 100), 400, 100, Options, OptionsHover, "Option");
            #endregion

            Test.ButtonClicked += ButtonClicked;
        }
        //Updates the Buttons and other things for the menu
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            //Get current mouse state
            mouse = Mouse.GetState();

            //Update the buttons
            Test.Update(mouse);
            Option.Update(mouse);
        }
        public override void Draw()
        {
            Test.Draw(spriteBatch);
            Option.Draw(spriteBatch);
        }
    }
}
