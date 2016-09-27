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

        Vector2 Placement;

        private List<Vector2> verticies = new List<Vector2>();
        public Polygons(List<Vector2> numbers)
        {
                foreach (Vector2 num in numbers)
                {
                    verticies.Add(num);
                }
        }

        public List<Vector2> getVerticiesList()
        {
              return verticies; 
        }
        public Vector2 getVeticies(int vertNumbers)
        {
            return verticies[vertNumbers];
        }
        public int getNumVerticies()
        {
            return verticies.Count;
        }

        public void LoadContent()
        {
            triangle = Main.GameContent.Load<Texture2D>("Sprites/GreenTriangle");
        }

        public void Draw(SpriteBatch spriteBatch, string ShapeName)
        {
            Placement = SetShapePlacement(ShapeName);
            spriteBatch.Draw(triangle, Placement, null, null, verticies[0], 0, null, Color.White);
        }

        public void Collision(Polygons Shape)
        {
            getRealPos();
        }
        public void getRealPos()
        {
            int i = 0;
            List<Vector2> realPos = new List<Vector2>();
            foreach (Vector2 verts in verticies)
            {
                realPos.Add(Placement += verts);
                Debug.WriteLine(realPos[i]);
                i++;
            }
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
