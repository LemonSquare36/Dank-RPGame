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
        // FilePath for the ShapeList and ShapePlace
        static string SourceFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filePath = Path.Combine(SourceFolder, "Source/Repos/Dank-RPGame/Game1/Game1/Shapes/shapeplace.txt");

        // declaring texture 2D's
        Texture2D texture;

        private float rotation;
        private List<Vector2> realPos = new List<Vector2>();
        Vector2 Movement = Vector2.Zero;

        private Vector2 Placement;
        //Holds Shapes Verticies
        private List<Vector2> verticies = new List<Vector2>();
        public Polygons(List<Vector2> numbers)
        {
            rotation = 0;
            foreach (Vector2 num in numbers)
            {
                verticies.Add(num);
            }
        }

        //Get the RealPosition
        public Vector2 getRealPos(int Index)
        {
            return realPos[Index];
        }
        //Gets the list the verticies
        public List<Vector2> getVerticiesList()
        {
            return verticies;
        }
        //Gets the verticie not in its real position
        public Vector2 getVerticies(int vertNumber)
        {
            return verticies[vertNumber];
        }
        //Gets how many verticies there are 
        public int getNumVerticies()
        {
            return verticies.Count;
        }

        //Loads the texture 2D's using image name
        public void LoadContent(string ShapeName, string ShapeImage)
        {

            Placement = SetShapePlacement(ShapeName);

            if (ShapeImage == "GreenTriangle")
                texture = Main.GameContent.Load<Texture2D>("Sprites/GreenTriangle");
            if (ShapeImage == "GreyPentagon")
                texture = Main.GameContent.Load<Texture2D>("Sprites/GreyPentagon");
        }
        //Draws the Images with current Texture
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Placement, null, null, verticies[0], rotation, null, Color.White);
        }

        //Roatates the Shape
        public void Rotate(float rotate)
        {
            rotation += rotate;
        }
        //MOve shape with arrow keys
        public void MoveShape(KeyboardState Key)
        {
            Movement = Vector2.Zero;

            if (Key.IsKeyDown(Keys.D))
            {
                Movement = new Vector2(Movement.X + 1f, Movement.Y);
            }
            if (Key.IsKeyDown(Keys.S))
            {
                Movement = new Vector2(Movement.X, Movement.Y + 1f);
            }
            if (Key.IsKeyDown(Keys.A))
            {
                Movement = new Vector2(Movement.X - 1f, Movement.Y);
            }
            if (Key.IsKeyDown(Keys.W))
            {
                Movement = new Vector2(Movement.X, Movement.Y - 1f);
            }
            Placement += Movement;
        }

        //Project the shape along its normals to check for gaps (Collision Detection)
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
        //Find the realPos of the shapes using the images verticies
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
        //Gets shape Placement from a file
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
        //Moves the shape away from collided objects
        public void Rebuff(float rotate, Polygons Shape)
        {
            float Slope;
            Vector2 Slope1 = new Vector2();
            Vector2 Slope2 = new Vector2();
            Vector2 Reflection = new Vector2();

            Slope1 = new Vector2(Shape.getVerticies(2).X - Shape.getVerticies(1).X, Shape.getVerticies(2).Y - Shape.getVerticies(1).X);
            Slope2 = new Vector2(verticies[2].X - verticies[1].X, verticies[2].Y - verticies[1].Y);
            Slope = Slope1.Y / Slope1.X;
            Slope1.Normalize();
            Reflection = -(Slope1 * Slope2) * (Slope1 - Slope2);
            Reflection.Normalize();
            Placement -= Movement;

            if (Slope < 2)
            {
                Placement += Reflection;
            }
            else
            {
                Placement += Slope1;
            }
        }
    }
}

