﻿using System;
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
        Button Test, Option, Tutorials;
        Texture2D Play, PlayHover, Options, OptionsHover, Background, Tutorial, TutorialHover;

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
            Tutorial = Main.GameContent.Load<Texture2D>("buttons/tutorial");
            TutorialHover = Main.GameContent.Load<Texture2D>("buttons/tutorial_hover");

            Background = Main.GameContent.Load<Texture2D>("Sprites/main_Menu_Background");
            #endregion

            #region Button Load
            Test = new Button(new Vector2(200, 75), 400, 100, Play, PlayHover, "Play");
            Option = new Button(new Vector2(200, 200), 400, 100, Options, OptionsHover, "Option");
            Tutorials = new Button(new Vector2(200, 325), 400, 100, Tutorial, TutorialHover, "Tutorial");
            #endregion

            //Important or the event doesnt work
            Test.ButtonClicked += ButtonClicked;
            Option.ButtonClicked += ButtonClicked;
            Tutorials.ButtonClicked += ButtonClicked;
        }
        //Updates the Buttons and other things for the menu
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager, GraphicsDevice graphicsDevice)
        {
            //Get current mouse state
            mouse = Mouse.GetState();
            Vector2 worldPosition;
            worldPosition.X = mouse.X / (float)(Main.gameWindow.ClientBounds.Width / 800.0);
            worldPosition.Y = mouse.Y / (float)(Main.gameWindow.ClientBounds.Height / 480.0);

            //Update the buttons
            Test.Update(mouse,worldPosition);
            Option.Update(mouse,worldPosition);
            Tutorials.Update(mouse,worldPosition);
        }
        //Draws the Buttons
        public override void Draw()
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), null, null);
            Test.Draw(spriteBatch);
            Option.Draw(spriteBatch);
            Tutorials.Draw(spriteBatch);
        }
        //Used for edge detection
        public override void ButtonReset()
        {
            Test.ButtonReset();
            Option.ButtonReset();
            Tutorials.ButtonReset();

        }

    }
}
