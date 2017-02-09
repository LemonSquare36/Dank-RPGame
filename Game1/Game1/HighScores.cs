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
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace RPGame
{

    class HighScores : Global
    {
        DirectoryInfo Highscores = new DirectoryInfo(Path.Combine((Main.GameContent.RootDirectory), "HighScores"));
        StreamReader readScores;
        StreamWriter createHighscores = new StreamWriter(Path.Combine((Main.GameContent.RootDirectory), "HighScores/highscores.txt"));
        string line;
        //Constructor - Creates the Highscores Filepath
        public HighScores()
        {
            if (!Highscores.Exists)
            {
                Highscores.Create();
            }
            createHighscores.Close();
            readScores = new StreamReader(Path.Combine((Main.GameContent.RootDirectory), "HighScores/highscores.txt"));
            readScores.Close();
        }
        public void writeScore()
        {
            while ((line = readScores.ReadLine()) != null)
            {

            }
        }

    }
}
