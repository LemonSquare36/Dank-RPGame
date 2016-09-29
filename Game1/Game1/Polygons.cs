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
        Texture2D triangle;

        private List<Vector2> realPos = new List<Vector2>();

        private Vector2 Placement;

        private List<Vector2> verticies = new List<Vector2>();
        public Polygons(List<Vector2> numbers)
        {
            foreach (Vector2 num in numbers)
            {
                verticies.Add(num);
            }
        }


        public Vector2 getRealPos(int Index)
        {
            Vector2 RP = realPos[Index];
            return RP;
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

        public void LoadContent(string ShapeName)
        {

            Placement = SetShapePlacement(ShapeName);

            triangle = Main.GameContent.Load<Texture2D>("Sprites/GreenTriangle");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(triangle, Placement, null, null, verticies[0], 0, null, Color.White);
        }

        public void TriangleMove()
        {
            Placement = new Vector2(Placement.X, Placement.Y + .5f);
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
                    A = new Vector2((getRealPos(0).X - getRealPos(Y).X), (getRealPos(0).Y - Shape.getRealPos(Y).Y));
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
            Vector2 Pos = Placement;
            List<Vector2> realPosTemp = new List<Vector2>();
            foreach (Vector2 verts in verticies)
            {
                realPosTemp.Add(Pos = Placement + verts);
            }
            realPos = realPosTemp;
        }

        private Vector2 SetShapePlacement(string ShapeName)
        {
            StreamReader PlaceReader = new StreamReader("shapeplace.txt");
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
