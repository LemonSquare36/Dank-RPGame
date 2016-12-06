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
        Button Sound, Fullscreen;
        Texture2D Sounds, SoundChecked, Fullscreens, FullscreenChecked;

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
            #endregion

            #region Button Load
            Sound = new Button(new Vector2(200, 75), 400, 100, Sounds, SoundChecked, "Sound");
            #endregion

            //Important or the event doesnt work
            Sound.ButtonClicked += ButtonClicked;

        }
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            //Get current mouse state
            mouse = Mouse.GetState();

            //Update the buttons
            Sound.Update(mouse);
        }
        public override void Draw()
        {
            Sound.Draw(spriteBatch);
        }
    }
}
