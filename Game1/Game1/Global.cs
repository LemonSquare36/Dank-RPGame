using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Global
    {
        static string UserFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        static string errorPathFolder = Path.Combine(UserFolder, "Source/Repos/Dank-RPGame/Game1/Errors");
        string errorPath = Path.Combine(errorPathFolder, "errors.txt");

        public void ErrorHandling(string logMessage)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(errorPathFolder));

            var errorWrite = new StreamWriter(errorPath, true);

            errorWrite.Write(logMessage);
            errorWrite.Close();
        }
    }
}
