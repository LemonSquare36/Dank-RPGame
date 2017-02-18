using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Media;

namespace RPGame
{

    
    class Sound : Global
    {
        SoundPlayer snd = new SoundPlayer();

        //Lound the sound or music file
        public void LoadSound(string location)
        {
            snd.SoundLocation = Path.Combine(Main.GameContent.RootDirectory, location);
            snd.Load();
        }
        //Play the sound effect
        public void PlaySound()
        {
            snd.Play();
        }
    }
}
