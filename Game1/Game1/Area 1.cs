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
using System.Collections;

namespace RPGame
{
    class Area_1
    {
        private static Hashtable shapeVerts = new Hashtable();

        Polygons Triangle1;
        Polygons Triangle2;
        Polygons Triangle3;
        float Rotate = 1;
        Texture2D RedCube;
        

        SpriteBatch spriteBatch;

        public void LoadContent(SpriteBatch spriteBatchMain)
        {
            MakeShapes();
            spriteBatch = spriteBatchMain;
            Triangle1.LoadContent("triangle1");
            Triangle2.LoadContent("triangle2");
            Triangle3.LoadContent("triangle3");

            RedCube = Main.GameContent.Load<Texture2D>("Sprites/RedCube");
        }

        public void Draw()
        {
            Triangle1.RealPos();
            Triangle2.RealPos();
            spriteBatch.Begin();
            Triangle1.Draw(spriteBatch);
            Triangle2.Draw(spriteBatch);
            Triangle3.Draw(spriteBatch);
            spriteBatch.Draw(RedCube, Triangle1.getRealPos(1));
            spriteBatch.Draw(RedCube, Triangle1.getRealPos(2));
            spriteBatch.Draw(RedCube, Triangle1.getRealPos(3));
            spriteBatch.Draw(RedCube, Triangle2.getRealPos(1));
            spriteBatch.Draw(RedCube, Triangle2.getRealPos(2));
            spriteBatch.Draw(RedCube, Triangle2.getRealPos(3));
            spriteBatch.End();
        }
        public void Update()
        {
            Triangle1.TriangleMove();
            Triangle1.Rotate(Rotate);
            Rotate = 0;
            bool Collide = Collision(Triangle1, Triangle2);
            //Rotate = Rotate + .01f;
            if (Collide)
            {
                Debug.WriteLine("Yes");
            }
            if (!Collide)
            {
                Debug.WriteLine("This makes me anfry");
            }
        }

        public bool Collision(Polygons Shape, Polygons Shape2)
        {
            bool collision = true;

            // Y is for the verticies one higher than i; I named it Y since it rhymes with i;
            int Y = 2;
            // Z is the same as Y but for Shape2; Named that since it is after Y;
            int Z = 2;
            Shape.RealPos();
            Shape2.RealPos();
            for (int i = 1; i < Shape.getNumVerticies(); i++)
            {
                if (Y == Shape.getNumVerticies())
                {
                    Y = 1;
                }
                if (!Shape.Projection(Shape2, new Vector2(Shape.getRealPos(i).Y - Shape.getRealPos(Y).Y, Shape.getRealPos(Y).X - Shape.getRealPos(i).X)))
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
                if (!Shape2.Projection(Shape, new Vector2(Shape2.getRealPos(i).Y - Shape2.getRealPos(Z).Y, Shape2.getRealPos(Z).X - Shape2.getRealPos(i).X)))
                {
                    collision = false;
                }
                Z++;
            } 
            return collision;
        }

        //Make YourShapes Here
        private void MakeShapes()
        {
            //Create the Polygon
            RetrieveShapes();

            Triangle1 = CreateShape("triangle1");
            Triangle2 = CreateShape("triangle2");
            Triangle3 = CreateShape("triangle3");

        }

        //Creates the Shapes of Polygon Class
        private static Polygons CreateShape(string shapeName)
        {
            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            Polygons myPolygon = new Polygons(NewList);
            return myPolygon;
        }

        private void RetrieveShapes()
        {
            StreamReader shapeConfig = new StreamReader("shapeList.txt");
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
}
