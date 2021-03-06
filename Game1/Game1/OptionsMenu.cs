﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGame
{
    class OptionsMenu : MenuManager
    {
        Button Sound, Fullscreen, Back;
        Texture2D Sounds, SoundChecked, Fullscreens, FullscreenChecked, Backs, BackHover, Background;

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
            Sounds = Main.GameContent.Load<Texture2D>("buttons/sound");
            SoundChecked = Main.GameContent.Load<Texture2D>("buttons/sound_checked");
            Fullscreens = Main.GameContent.Load<Texture2D>("buttons/fullscreen");
            FullscreenChecked = Main.GameContent.Load<Texture2D>("buttons/fullscreen_checked");
            Backs = Main.GameContent.Load<Texture2D>("buttons/back");
            BackHover = Main.GameContent.Load<Texture2D>("buttons/back_hover");
            #endregion

            #region Button Load
            Sound = new Button(new Vector2(200, 75), 400, 100, Sounds, SoundChecked, "Sound");
            Fullscreen= new Button(new Vector2(200, 200), 400, 100, Fullscreens, FullscreenChecked, "OptionsFullscreen");
            Back = new Button(new Vector2(200, 325), 400, 100, Backs, BackHover, "Back");
            #endregion

            //Important or the event doesnt work
            Sound.ButtonClicked += ButtonClicked;
            Fullscreen.ButtonClicked += ButtonClicked;
            Back.ButtonClicked += ButtonClicked;


        }
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager,GraphicsDevice graphicsDevice)
        {
            //Get current mouse state
            Vector2 worldPosition = MousePos();

            Sound.Update(mouse,worldPosition);
            Fullscreen.Update(mouse,worldPosition);
            Back.Update(mouse,worldPosition);
        }
        //Draws the buttons
        public override void Draw()
        {
            spriteBatch.Draw(Background, new Vector2(0, 0), null, null);

            Sound.Draw(spriteBatch);
            Fullscreen.Draw(spriteBatch);
            Back.Draw(spriteBatch);

            
        }
        //Used for edge detection
        public override void ButtonReset()
        {
            Sound.ButtonReset();
            Fullscreen.ButtonReset();
            Back.ButtonReset();

        }
    }
}
