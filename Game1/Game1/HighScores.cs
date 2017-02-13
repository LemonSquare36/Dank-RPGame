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
        StreamWriter createHighscores;
        string line;
        int num = 0;
        int score;

        //Constructor - Creates the Highscores Filepath
        public HighScores()
        {
            if (!Highscores.Exists)
            {
                Highscores.Create();
            }
            File.Create(Path.Combine((Main.GameContent.RootDirectory), "HighScores/highscores.txt"));
        }

        public void ChangeScores(int score)
        {
            bool written = false;
            using (StreamReader readStream = new StreamReader(Path.Combine((Main.GameContent.RootDirectory), "HighScores/highscores.txt")))
            {
                using (StreamWriter writeStream = new StreamWriter(Path.Combine((Main.GameContent.RootDirectory), "HighScores/temp.txt")))
                {
                    int maxcount = 9;
                    string line;
                    while ((line = readStream.ReadLine()) != null && maxcount > 0)
                    {

                        maxcount--;

                        if (!written)
                        {
                            if (Convert.ToInt64(line) < score)
                            {
                                writeStream.WriteLine(score);
                                written = true;
                            }
                        }
                        writeStream.WriteLine(line);                       
                    }
                    if (maxcount == 9 && line == null)
                    {
                        writeStream.WriteLine(score);
                    }
                }
            }
            File.Delete(Path.Combine((Main.GameContent.RootDirectory), "HighScores/highscores.txt"));
            File.Move(Path.Combine((Main.GameContent.RootDirectory), "HighScores/temp.txt"), Path.Combine((Main.GameContent.RootDirectory), "HighScores/highscores.txt"));
        }


        public void ScorestoScreen(SpriteBatch spriteBatch)
        {
            if (font == null)
            {
                font = Main.GameContent.Load<SpriteFont>("myFont");
            }

            Vector2 position = new Vector2(20, 20);
            readScores = new StreamReader(Path.Combine((Main.GameContent.RootDirectory), "HighScores/highscores.txt"));

            num = 0;
            while (true)
            {
                line = readScores.ReadLine();

                if (line == null)
                {
                    readScores.Close();
                    break;
                }

                num++;

                spriteBatch.DrawString(font, num + "   " + line, position, Color.Red);
                position.Y += 40;

            }
        }

    }
}
