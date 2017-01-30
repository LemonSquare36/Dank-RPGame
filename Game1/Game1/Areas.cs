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

namespace RPGame
{

    class Areas : Screen
    {

        /*! This Polygon based code is used by polygons in the Areas and Buttons in the Menus. !*/

        //Creates the Shapes of Polygon Clas
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
        //Gets the Hit boxes from Shape List
        protected void RetrieveShapes()
        {
            string Resource = "";
            while (true)
            
                if (Resource == "Shapes/EnemyList")
                {
                    break;
                }
                if (Resource == "Shapes/shapeList")
                {
                    Resource = "Shapes/EnemyList";
                }
                else
                {
                    Resource = "Shapes/shapeList";
                }

                StreamReader shapeConfig = new StreamReader(Path.Combine(filePathFolder, Resource));

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
    }
}
