using System;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Runtime.CompilerServices;

namespace RPGame
{
    // Global is my class that lets code go between Menus and Areas
    public class Global
    {
        //gets the folders for the project
        static string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        static string errorPathFolder = Path.Combine(AppDataFolder, "RPGame/Errors");
        string errorPath = Path.Combine(AppDataFolder, "RPGame/Errors/errors.txt");


        //Holds the fonts
        protected SpriteFont font;
        protected SpriteFont font1;


        /// <summary>
        /// Deletes the file So the errors dont build up.
        /// </summary>
        public void ErrorFileReset()
        {
            if (File.Exists(errorPath))
            {
                File.Delete(errorPath);
            }
            File.Create(errorPath);
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
            if (!File.Exists(errorPath))
            {
                var myfile = File.Create(errorPath);
                myfile.Close();
            }
        }

        //Write the errors to the Error.txt file
        public void ErrorHandling(string logMessage, string classname, Exception ex, [CallerLineNumber] int LineNumber = 0)
        {

            string line;
            bool write = true;

            createErrorFolder();

            //Checks to see if the error was already there
            StreamReader errorReader = new StreamReader(errorPath);
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

            //Writes the Error to the File
            if (write)
            {
                StreamWriter errorWrite = new StreamWriter(errorPath);
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

