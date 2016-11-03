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
using System.Runtime.CompilerServices;

namespace RPGame
{
    public class Global
    {
        static string UserFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        static string errorPathFolder = Path.Combine(UserFolder, "Source/Repos/Dank-RPGame/Game1/Errors");
        string errorPath = Path.Combine(errorPathFolder, "errors.txt");

        public void ErrorFileReset()
        {
            if (File.Exists(errorPath))
            File.Delete(errorPath);
        }

        public string getErrorPath()
        {
            return errorPathFolder;
        }

        public void createErrorFolder()
        {
            DirectoryInfo Errors = new DirectoryInfo(errorPathFolder);

            if (!Errors.Exists)
            {
                Errors.Create();
            }
        }
        public void ErrorHandling(string logMessage, string classname, [CallerLineNumber] int LineNumber = 0)
        {
            string line;
            bool write = true;

            createErrorFolder();

            try
            {
                var errorReader = new StreamReader(errorPath);
                while (true)
                {
                    line = errorReader.ReadLine();
                    if (line == classname)
                    {
                        line = errorReader.ReadLine();
                        if (line == "Line " + LineNumber)
                        {
                            write = false;
                            break;
                        }
                    }
                    if (errorReader.EndOfStream)
                    {
                        break;
                    }
                }
                errorReader.Close();
            }
            catch { }

            if (write)
            {
                var errorWrite = new StreamWriter(errorPath, true);
                errorWrite.WriteLine("{0}\r\nLine {2}\r\nError:\r\n{1}\r\n", classname, logMessage, LineNumber);
                errorWrite.Close();
            }

        }
    }
}

