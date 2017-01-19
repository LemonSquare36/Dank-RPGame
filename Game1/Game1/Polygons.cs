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
    public class Polygons : PolygonHolder
    {
        // FilePath for the ShapeList and ShapePlace
        protected static string SourceFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string filePath = Path.Combine(SourceFolder, "Source/Repos/Dank-RPGame/Game1/Game1/Shapes/shapeplace.txt");

        // declaring texture 2D's
        protected Texture2D texture;

        protected float rotation;
        private List<Vector2> realPos = new List<Vector2>();
        public Vector2 Movement = Vector2.Zero;
        protected Vector2 OldPosition = new Vector2();

        protected Vector2 Placement;
        
        //Holds Shapes Verticies
        protected List<Vector2> verticies = new List<Vector2>();

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
        public List<Vector2> getRealPosList()
        {
            return realPos;
        }

        //Loads the texture 2D's using image name
        public void LoadContent(string ShapeName, string ShapeImage)
        {

            Placement = SetShapePlacement(ShapeName);

            if (ShapeImage == "GreenTriangle")
                texture = Main.GameContent.Load<Texture2D>("Sprites/GreenTriangle");
            if (ShapeImage == "GreyPentagon")
                texture = Main.GameContent.Load<Texture2D>("Sprites/GreyPentagon");
            if (ShapeImage == "Floor")
                texture = Main.GameContent.Load<Texture2D>("Sprites/Floor");
            if (ShapeImage == "TFloor")
                texture = Main.GameContent.Load<Texture2D>("Sprites/TutorialSprites/TFloor");
            if (ShapeImage == "TWall")
                texture = Main.GameContent.Load<Texture2D>("Sprites/TutorialSprites/TWall");
            if (ShapeImage == "floorbythedoor")
                texture = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/FloorByTheDoor");
            if (ShapeImage == "floorhump")
                texture = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/FloorHump");
            if (ShapeImage == "longfloor")
                texture = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/LongFloor");
            if (ShapeImage == "mramp")
                texture = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/Mramp");

        }
        //Draws the Images with current Texture
        public override void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                spriteBatch.Draw(texture, Placement, null, null, verticies[0], rotation, new Vector2(1, 1), Color.White);
            }
            catch (Exception ex) { ErrorHandling(ex.Message, GetType().Name, ex); }
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
                Movement = new Vector2(Movement.X + 2f, Movement.Y);
            }
            if (Key.IsKeyDown(Keys.S))
            {
                Movement = new Vector2(Movement.X, Movement.Y + 1f);
            }
            if (Key.IsKeyDown(Keys.A))
            {
                Movement = new Vector2(Movement.X - 2f, Movement.Y);
            }
            if (Key.IsKeyDown(Keys.W))
            {
                Movement = new Vector2(Movement.X, Movement.Y - 1f);
            }
            OldPosition = Placement;
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
        // returns the Normal Vector of a Line
        public Vector2 NormalVector(int Vert1, int Vert2)
        {
            return new Vector2(getRealPos(Vert1).Y - getRealPos(Vert2).Y, -(getRealPos(Vert1).X - getRealPos(Vert2).X));
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
        protected Vector2 SetShapePlacement(string ShapeName)
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

        public bool CrossProduct(Vector2 A, Vector2 B, Vector2 C)
        {
            float crossProduct;
            float dotProduct;
            double baSquared;

            crossProduct = (C.Y - A.Y) * (B.X - A.X) - (C.X - A.X) * (B.Y - A.Y);
            if (Math.Abs(crossProduct) > 150)
                return false;

            dotProduct = ((C.X - A.X) * (B.X - A.X)) + ((C.Y - A.Y) * (B.Y - A.Y));
            if (dotProduct < -150)
                return false;

            baSquared = Math.Pow((B.X - A.X), 2) + Math.Pow((B.Y - A.Y), 2);
            if (dotProduct > (float)baSquared + 150)
                return false;

            return true;
        }

        //Moves the shape away from collided objects
        public void Rebuff(Polygons Shape)
        {
            float Slope;
            Vector2 Slope1 = new Vector2();
            bool check = true;
            int value = 0;
            int X = 0;
            int Y = 0;

            if (check)
            {
                foreach (Vector2 verts in getRealPosList())
                {
                    for (int A = 1; A < Shape.getNumVerticies(); A++)
                    {
                        int B = A + 1;
                        if (B == Shape.getNumVerticies())
                        {
                            B = 1;
                        }
                        X = A;
                        Y = B;
                        bool hole = false;
                        bool Positive = true;
                        bool isBetween = false;

                        if (isBetween)
                        {
                            isBetween = CrossProduct(Shape.getRealPos(A), Shape.getRealPos(B), verts);
                            if (isBetween)
                            {
                                hole = true;
                            }
                        }

                        isBetween = CrossProduct(Shape.getRealPos(A), Shape.getRealPos(B), verts);

                        if (!isBetween)
                        {
                            if (value == getNumVerticies() * (Shape.getNumVerticies() - 1))
                            {
                                check = false;
                                break;
                            }
                            if (value == (getNumVerticies() - 1) * (Shape.getNumVerticies() - 1))
                            {
                                value++;
                            }
                            value++;
                        }

                        if (isBetween)
                        {

                            if (Shape.getRealPos(A).X < Shape.getRealPos(B).X && Shape.getRealPos(A).Y < Shape.getRealPos(B).Y)
                            {
                                Positive = false;
                                Placement = OldPosition;
                            }
                            else if (Shape.getRealPos(A).X > Shape.getRealPos(B).X && Shape.getRealPos(A).Y > Shape.getRealPos(B).Y)
                            {
                                Positive = false;
                                Placement = OldPosition;
                            }


                            Slope1 = new Vector2(Shape.getRealPos(B).X - Shape.getRealPos(A).X, Shape.getRealPos(B).Y - Shape.getRealPos(A).Y);
                            Slope = Slope1.Y / Slope1.X;
                            Slope1.Normalize();

                            Placement -= Movement;

                            if (!hole)
                            {
                                if (Slope > -2 && Positive == true)
                                {
                                    if (Movement.X < 0 && Movement.Y > 0)
                                    {
                                        Placement = new Vector2(Placement.X + Slope, Placement.Y + Slope);
                                    }
                                    else if (Movement.X < 0)
                                    {
                                        Placement = new Vector2(Placement.X, Placement.Y - Slope);
                                    }
                                    if (Movement.X > 0 && Movement.Y < 0)
                                    {
                                        Placement = new Vector2(Placement.X, Placement.Y - (Slope * 2));
                                    }
                                    else if (Movement.X > 0)
                                    {
                                        Placement = new Vector2(Placement.X, Placement.Y + (Slope * 2));
                                    }
                                }
                                else if (Slope < -2 && Positive == true)
                                {
                                    Placement = OldPosition;
                                    Placement.X -= 2;
                                }


                                if (Slope < 2 && Positive == false)
                                {
                                    if (Movement.X > 0 && Movement.Y > 0)
                                    {
                                        Placement = new Vector2(Placement.X + Slope, Placement.Y - Slope);
                                    }
                                    else if (Movement.X > 0)
                                    {
                                        Placement = new Vector2(Placement.X, Placement.Y + Slope);
                                    }
                                    if (Movement.X < 0 && Movement.Y < 0)
                                    {
                                        Placement = new Vector2(Placement.X - Slope, Placement.Y + (Slope * 2));
                                    }
                                    else if (Movement.X < 0)
                                    {
                                        Placement = new Vector2(Placement.X, Placement.Y - (Slope * 2));
                                    }
                                }
                                else if (Slope > 2 && Positive == true)
                                {
                                    Placement = OldPosition;
                                    Placement.X += 2;
                                }
                            }
                        }
                    }
                }
            }
            if (!check)
            {
                

                        Slope1 = new Vector2(Shape.getRealPos(Y).X - Shape.getRealPos(X).X, Shape.getRealPos(Y).Y - Shape.getRealPos(X).Y);
                        Slope = Slope1.Y / Slope1.X;
                        if (Slope == 0)
                        {

                        }

                        else if (Slope > -2 && Slope < 0)
                        {
                            Placement = new Vector2(Placement.X, Placement.Y + (Slope * 2));
                        }
                Placement -= Movement;
                }
            }
        }

    }



