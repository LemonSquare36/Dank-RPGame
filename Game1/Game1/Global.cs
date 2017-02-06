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
    // Global is my class that lets code go between Menus and Areas
    public class Global
    {
        //gets the folders for the project
        static string UserFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        static string errorPathFolder = Path.Combine(UserFolder, "Source/Repos/Dank-RPGame/Game1/Errors");
        string errorPath = Path.Combine(errorPathFolder, "errors.txt");
        protected string filePathFolder = Path.Combine(UserFolder, "Source/Repos/Dank-RPGame/Game1/Game1/Shapes");


        //Holds the fonts
        protected SpriteFont font;
        protected SpriteFont font1;


        /// <summary>
        /// Deletes the file So the errors dont build up.
        /// </summary>
        public void ErrorFileReset()
        {
            if (File.Exists(errorPath))
                File.Delete(errorPath);
        }

        //access to the errorPath folder for classes unable to derive from global
        public string getErrorPath()
        {
            return errorPathFolder;
        }

        //Creates the folder if it doesnt already exist
        public void createErrorFolder()
        {
            DirectoryInfo Errors = new DirectoryInfo(errorPathFolder);

            if (!Errors.Exists)
            {
                Errors.Create();
            }
        }

        //Write the errors to the Error.txt file
        public void ErrorHandling(string logMessage, string classname, Exception ex, [CallerLineNumber] int LineNumber = 0)
        {

            string line;
            bool write = true;

            createErrorFolder();

            try
            {
                //Checks to see if the error was already there
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
            //Writes the Error to the File
            if (write)
            {
                var errorWrite = new StreamWriter(errorPath, true);
                errorWrite.WriteLine("{0}\r\nLine {2}\r\nError:\r\n{1}\r\n", classname, logMessage, LineNumber);
                if (ex.InnerException != null)
                {
                    logMessage = Convert.ToString(ex.InnerException);
                    errorWrite.WriteLine("InnerException:\r\n{0}");
                }
                errorWrite.Close();
            }
        }

    }
}

