using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGame
{
    class Sound : Global
    {
        SoundEffect Dash;
        SoundEffectInstance DashInstance;
        
         protected void LoadContent()
        {
            Dash = Main.GameContent.Load<SoundEffect>("Dash");
            DashInstance = DashInstance.CreateInstance();
            DashInstance.Volume = 1f;
            DashInstance.IsLooped = false;
        }
    }
}
