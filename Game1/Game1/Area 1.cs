﻿using System;
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
        Polygons Pentagon1;
        Polygons Pentagon2;
        float Rotate = .02f;
        Texture2D RedCube;
        Camera camera = new Camera();
        

        SpriteBatch spriteBatch;

        public void LoadContent(SpriteBatch spriteBatchMain)
        {
            MakeShapes();
            spriteBatch = spriteBatchMain;
            Triangle1.LoadContent("triangle1", "GreenTriangle");
            Triangle2.LoadContent("triangle2", "GreenTriangle");
            Triangle3.LoadContent("triangle3", "GreenTriangle");
            Pentagon1.LoadContent("pentagon1", "GreyPentagon");
            Pentagon2.LoadContent("pentagon2", "GreyPentagon");

            RedCube = Main.GameContent.Load<Texture2D>("Sprites/RedCube");
        }

        public void Draw()
        {
            Triangle1.RealPos();
            Triangle2.RealPos();
            Pentagon1.RealPos();
            Pentagon2.RealPos();
            var viewMatrix = camera.Transform();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, viewMatrix * Matrix.CreateScale(1));
            Triangle1.Draw(spriteBatch, "GreenTriangle");
            Triangle2.Draw(spriteBatch, "GreenTriangle");
            Triangle3.Draw(spriteBatch, "GreenTriangle");
            Pentagon1.Draw(spriteBatch, "GreyPentagon");
            Pentagon2.Draw(spriteBatch, "GreyPentagon");
            /*spriteBatch.Draw(RedCube, Triangle1.getRealPos(1));
            spriteBatch.Draw(RedCube, Triangle1.getRealPos(2));
            spriteBatch.Draw(RedCube, Triangle1.getRealPos(3));
            spriteBatch.Draw(RedCube, Triangle2.getRealPos(1));
            spriteBatch.Draw(RedCube, Triangle2.getRealPos(2));
            spriteBatch.Draw(RedCube, Triangle2.getRealPos(3));
            spriteBatch.Draw(RedCube, Pentagon1.getRealPos(1));
            spriteBatch.Draw(RedCube, Pentagon1.getRealPos(2));
            spriteBatch.Draw(RedCube, Pentagon1.getRealPos(3));
            spriteBatch.Draw(RedCube, Pentagon1.getRealPos(4));
            spriteBatch.Draw(RedCube, Pentagon1.getRealPos(5));*/
            spriteBatch.Draw(RedCube, new Vector2(-23, 116));
            spriteBatch.Draw(RedCube, new Vector2(-38, -45), Color.Black);
            spriteBatch.Draw(RedCube, new Vector2(0, -143), Color.Gray);
            spriteBatch.Draw(RedCube, new Vector2(38, -45), Color.DarkBlue);
            spriteBatch.Draw(RedCube, new Vector2(23, 117), Color.DarkRed);
            spriteBatch.Draw(RedCube, new Vector2(0, 0));
            spriteBatch.End();
        }
        public void Update()
        {
            KeyboardState Key = Keyboard.GetState();
            Triangle1.MoveShape(Key);
            Triangle1.Rotate(Rotate);
            camera.Move(Key);
            bool Collide = Collision(Triangle1, Pentagon1);
            bool Collide2 = Collision(Triangle1, Pentagon2);
            if (Collide)
            {
                Debug.WriteLine("Yes");
            }
            else if (!Collide)
            {
                //Debug.WriteLine("This makes me angry");
            }
            if (Collide2)
            {
                Debug.WriteLine("Yes");
            }
            else if (!Collide2)
            {
                Debug.WriteLine("This makes me angry");
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
                if (!Shape.Projection(Shape2, new Vector2(Shape.getRealPos(i).Y - Shape.getRealPos(Y).Y, -(Shape.getRealPos(i).X - Shape.getRealPos(Y).X))))
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
                if (!Shape2.Projection(Shape, new Vector2(Shape2.getRealPos(i).Y - Shape2.getRealPos(Z).Y, -(Shape2.getRealPos(i).X - Shape2.getRealPos(Z).X))))
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
            Pentagon1 = CreateShape("pentagon1");
            Pentagon2 = CreateShape("pentagon2");

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
