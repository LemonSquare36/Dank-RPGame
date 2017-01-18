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
    class OptionsMenu : MenuManager
    {
        Button Sound, Fullscreen, Back;
        Texture2D Sounds, SoundChecked, Fullscreens, FullscreenChecked, Backs, BackHover;

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void LoadContent(SpriteBatch spirteBatchMain)
        {
            spriteBatch = spirteBatchMain;

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
            Fullscreen= new Button(new Vector2(200, 200), 400, 100, Fullscreens, FullscreenChecked, "Fullscreen");
            Back = new Button(new Vector2(200, 325), 400, 100, Backs, BackHover, "Back");
            #endregion

            //Important or the event doesnt work
            Sound.ButtonClicked += ButtonClicked;
            Fullscreen.ButtonClicked += ButtonClicked;
            Back.ButtonClicked += ButtonClicked;

        }
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            //Get current mouse state
            mouse = Mouse.GetState();

            //Update the buttons
            Sound.Update(mouse);
            Fullscreen.Update(mouse);
            Back.Update(mouse);
        }
        public override void Draw()
        {
            Sound.Draw(spriteBatch);
            Fullscreen.Draw(spriteBatch);
            Back.Draw(spriteBatch);
        }
    }
}
