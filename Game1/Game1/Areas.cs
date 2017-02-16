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
using System.Reflection;
using System.Collections;

namespace RPGame
{

    class Areas : Screen
    {
        /*! This Polygon based code is used by polygons in the Areas and Buttons in the Menus. !*/

        protected Character Player;

        public Areas(bool isArea)
        {
            isarea = isArea;
        }

        //Hashtable for storing the verticies
        protected static Hashtable shapeVerts = new Hashtable();

        //Creates the Shapes of Polygon Class
        protected Polygons CreateShape(string shapeName)
        {

            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            Polygons myPolygon = new Polygons(NewList);
            return myPolygon;
        }
        //Creates the Character like CreateShape
        protected Character CreateChar(string shapeName)
        {
            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            Character myChar = new Character(NewList);
            return myChar;
        }

        protected CrawlerAlien CreateCrawler(string shapeName)
        {
            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            CrawlerAlien myAlien = new CrawlerAlien(NewList);
            return myAlien;
        }

        protected CrawlerAlien CreateCrawlerinList(string shapeName)
        {
            Random rand = new Random();
            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            CrawlerAlien myAlien = new CrawlerAlien(NewList);
            myAlien.RealPos();
            myAlien.Load(rand.Next(-1600, 301), 50);
            myAlien.SpriteMove(1, 4);
            return myAlien;

        }
        //Gets the Hit boxes from Shape List or Enemy List
        protected void RetrieveShapes()
        {
            string Resource = "";
            while (true)
            {

                if (Resource == "Shapes/EnemyList.txt")
                {
                    break;
                }
                if (Resource == "Shapes/shapeList.txt")
                {
                    Resource = "Shapes/EnemyList.txt";
                }
                else
                {
                    Resource = "Shapes/shapeList.txt";
                }

                StreamReader shapeConfig = new StreamReader(Path.Combine(Main.GameContent.RootDirectory, Resource));

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
        }


        //Uses the Positions from Shape list to make collision
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

        //allows the enemies to chase the player
        protected double Distance(Vector2 point1, Vector2 point2)
        {
            double D = point2.X - point1.X;

            double X = Math.Pow((point2.X - point1.X), 2);
            double Y = Math.Pow((point2.Y - point1.Y), 2);

            double unit = Math.Sqrt(X + Y);

            if (D < 0)
            {
                return -unit;
            }
            else if (D > 0)
            {
                return unit;
            }
            return 0;
        }

        public event EventHandler changeScreen;
        protected void OnScreenChanged(object sender, EventArgs eventArgs)
        {
            changeScreen?.Invoke(this, EventArgs.Empty);
        }

    }
}

