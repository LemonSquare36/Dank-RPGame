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

    class Areas : Screen
    {
      

        public override void LoadContent(SpriteBatch spriteBatchMain)
        {
            font = Main.GameContent.Load<SpriteFont>("myFont");
            font1 = Main.GameContent.Load<SpriteFont>("font1");
        }
    }
}
