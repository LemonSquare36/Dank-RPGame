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

namespace RPGame
{
    class Polygons
    {
        private List<Vector2> verticies = new List<Vector2>();
        public Polygons(List<Vector2> numbers)
        {
            foreach(Vector2 num in numbers)
            {
                Verticies.Add(num);
            }
        }
        public List<Vector2> Verticies
        {
            get { return Verticies; }
        }
        public Vector2 getVerticies(int vertNumbers)
        {
            return verticies[vertNumbers];
        }
        
    }
}
