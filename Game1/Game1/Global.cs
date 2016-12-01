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
using System.Collections;

namespace RPGame
{
    // Global is my class that lets code go between Menus and Areas
    public class Global
    {


        //The Game States get defined here
        public enum GameStates { Menu, Playing }

        public GameStates gameState;


        static string UserFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        static string errorPathFolder = Path.Combine(UserFolder, "Source/Repos/Dank-RPGame/Game1/Errors");
        string errorPath = Path.Combine(errorPathFolder, "errors.txt");
        protected string filePathFolder = Path.Combine(UserFolder, "Source/Repos/Dank-RPGame/Game1/Game1/Shapes");

        protected static Hashtable shapeVerts = new Hashtable();

        protected SpriteFont font;

        //Deletes the file So the errors dont build up.
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
                if (ex.InnerException != null)
                {
                    logMessage = Convert.ToString(ex.InnerException);
                    errorWrite.WriteLine("InnerException:\r\n{0}");
                }
                errorWrite.Close();
            }
        }

        /*! This Polygon based code is used by polygons in the Areas and Buttons in the Menus. !*/

        //Creates the Shapes of Polygon Clas
        protected Polygons CreateShape(string shapeName)
        {

            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            Polygons myPolygon = new Polygons(NewList);
            return myPolygon;
        }
        /// <summary>
        /// </summary>
        /// <param name="AreaVsMenu"></param>
        /// Set to 2 if its for the Menu Classes and set to anything else for the Area Classes
        /// <returns></returns>
        protected void RetrieveShapes(int AreaVsMenu)
        {
            string filePath = "";
            if (AreaVsMenu != 2)
            {
                filePath = Path.Combine(filePathFolder, "shapeList.txt");
            }
            else
            {
                filePath = Path.Combine(filePathFolder, "MenusList.txt");
            }
            StreamReader shapeConfig = new StreamReader(filePath);
            string line;
            string key = null;
            List<Vector2> verticies = new List<Vector2>();
            while ((line = shapeConfig.ReadLine()) != null)
            {
                try
                {
                    string[] VertCords = (line.Split(','));
                    float xVert = (float)Convert.ToDouble(VertCords[0]);
                    float yVert = (float)Convert.ToDouble(VertCords[1]);
                    Vector2 myVector2 = new Vector2(xVert, yVert);
                    verticies.Add(myVector2);

                }
                catch
                {
                    if (key != null)
                    {
                        shapeVerts[key] = verticies;
                        verticies = new List<Vector2>();
                    }
                    key = line;
                }
            }
        }

        protected bool Collision(Polygons Shape, Polygons Shape2)
        {
            bool collision = true;
            
            // Y is for the verticies one higher than i; I named it Y since it rhymes with i;
            int Y = 2;
            // Z is the same as Y but for Shape2; Named that since it is after Y;
            int Z = 2;

            for (int i = 1; i < Shape.getNumVerticies(); i++)
            {
                if (Y == Shape.getNumVerticies())
                {
                    Y = 1;
                }
                if (!Shape.Projection(Shape2, Shape.NormalVector(i, Y)))
                {
                    collision = false;
                }
                Y++;
            }

            for (int i = 1; i < Shape2.getNumVerticies(); i++)
            {
                if (Z == Shape2.getNumVerticies())
                {
                    Z = 1;
                }
                if (!Shape2.Projection(Shape, Shape2.NormalVector(i, Z)))
                {
                    collision = false;
                }
                Z++;
            }
            return collision;
        }
    }
}

