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
        public override void Initialize()
        {
            base.Initialize();
        }
        //Loads the BUttons and Textures
        public override void LoadContent(SpriteBatch spirteBatchMain)
        {
            spriteBatch = spirteBatchMain;
            base.LoadContent(spriteBatch);
        }
        //Updates the Highscores
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            highscores.ChangeScores();
        }
        //Draw
        public override void Draw()
        {
            highscores.ScorestoScreen(spriteBatch);
        }
        //Used for edge detection
        public override void ButtonReset()
        {

        }

    }
}
