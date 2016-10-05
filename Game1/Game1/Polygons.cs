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
    class Polygons
    {
        static string SourceFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filePath = Path.Combine(SourceFolder, "Source/Repos/Dank-RPGame/Game1/Game1/Shapes/shapeplace.txt");

        Texture2D triangle, pentagon;
        private float rotation;
        private List<Vector2> realPos = new List<Vector2>();

        private Vector2 Placement;

        private List<Vector2> verticies = new List<Vector2>();
        public Polygons(List<Vector2> numbers)
        {
            rotation = 0;
            foreach (Vector2 num in numbers)
            {
                verticies.Add(num);
            }
        }


        public Vector2 getRealPos(int Index)
        {
            return realPos[Index];
        }

        public List<Vector2> getVerticiesList()
        {
            return verticies;
        }
        public Vector2 getVerticies(int vertNumber)
        {
            return verticies[vertNumber];
        }
        public int getNumVerticies()
        {
            return verticies.Count;
        }

        public void LoadContent(string ShapeName, string ShapeImage)
        {

            Placement = SetShapePlacement(ShapeName);
            if (ShapeImage == "GreenTriangle")
                triangle = Main.GameContent.Load<Texture2D>("Sprites/GreenTriangle");
            if (ShapeImage == "GreyPentagon")
                pentagon = Main.GameContent.Load<Texture2D>("Sprites/GreyPentagon");
        }

        public void Draw(SpriteBatch spriteBatch, string ShapeImage)
        {
            if (ShapeImage == "GreenTriangle")
            {
                spriteBatch.Draw(triangle, Placement, null, null, verticies[0], rotation, null, Color.White);
            }
            if (ShapeImage == "GreyPentagon")
                spriteBatch.Draw(pentagon, Placement, null, null, verticies[0], rotation, null, Color.White);
        }

        public void TriangleMove()
        {
            Placement = new Vector2(Placement.X, Placement.Y + .5f);
        }

        public void Rotate(float rotate)
        {
            rotation += rotate;
        }
        public void MoveShape(KeyboardState Key)
        {
            if(Key.IsKeyDown(Keys.D))
            {
                Placement = new Vector2(Placement.X + 1f, Placement.Y);
            }
            if (Key.IsKeyDown(Keys.S))
            {
                Placement = new Vector2(Placement.X, Placement.Y + 1f);
            }
            if (Key.IsKeyDown(Keys.A))
            {
                Placement = new Vector2(Placement.X - 1f, Placement.Y);
            }
            if (Key.IsKeyDown(Keys.W))
            {
                Placement = new Vector2(Placement.X, Placement.Y - 1f);
            }
        }


        public bool Projection(Polygons Shape, Vector2 P)
        {
            bool value = true;
            double minGap = 1;
            for (int X = 1; X < Shape.getNumVerticies(); X++)
            {
                for (int Y = 1; Y < getNumVerticies(); Y++)
                {

                    Vector2 C = new Vector2();
                    Vector2 B = new Vector2();
                    Vector2 A = new Vector2();
                    float DPa, DPb, DPc;
                    double gap;

                    RealPos();
                    Shape.RealPos();
                    C = new Vector2((getRealPos(0).X - Shape.getRealPos(0).X), (getRealPos(0).Y - Shape.getRealPos(0).Y));
                    A = new Vector2((getRealPos(0).X - getRealPos(Y).X), (getRealPos(0).Y - getRealPos(Y).Y));
                    B = new Vector2((Shape.getRealPos(0).X - Shape.getRealPos(X).X), (Shape.getRealPos(0).Y - Shape.getRealPos(X).Y));


                    P.Normalize();

                    DPa = Vector2.Dot(A, P);
                    DPb = Vector2.Dot(B, P);
                    DPc = Vector2.Dot(C, P);
                    gap = DPc - DPa + DPb;
                    if (gap < minGap)
                    {
                        minGap = gap;
                    }
                }
            }

                if (minGap <= 0)
                {
                    value = true;
                }
                else if (minGap > 0)
                {
                    value = false;
                }
                else
                {
                    value = false;
                }
            return value;
        }
        public void RealPos()
        {
            Vector2 Pos, vertTemp;
            float theta = 0;
            float H, X, Y;
            List<Vector2> realPosTemp = new List<Vector2>();
            foreach (Vector2 verts in verticies)
            {
                if (verts == getVerticies(0))
                {
                    Pos = verts;
                    realPosTemp.Add(Pos);
                    continue;
                }
                vertTemp.X = verts.X - getVerticies(0).X;
                vertTemp.Y = verts.Y - getVerticies(0).Y;

                theta = (float)Math.Atan(vertTemp.Y / vertTemp.X);
                H = (float)(vertTemp.X / Math.Cos(theta));
                X = (float)(H * Math.Cos(theta + rotation));
                Y = (float)(H * Math.Sin(theta + rotation));
                Pos = new Vector2(X + Placement.X, Y + Placement.Y);

                realPosTemp.Add(Pos);
            }
            realPos = realPosTemp;
        }

        private Vector2 SetShapePlacement(string ShapeName)
        {
            var PlaceReader = new StreamReader(filePath);

            string line;
            Vector2 Placement = new Vector2();
            while (true)
            {
                line = PlaceReader.ReadLine();
                if (line == ShapeName)
                {
                    line = PlaceReader.ReadLine();
                    string[] VertCords = (line.Split(','));
                    float xVert = (float)Convert.ToDouble(VertCords[0]);
                    float yVert = (float)Convert.ToDouble(VertCords[1]);
                    Placement = new Vector2(xVert, yVert);
                    break;
                }
            }
            return Placement;
        }
    }
}
