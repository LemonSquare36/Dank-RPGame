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
namespace Main
{
    class Polygons
    {
        public Triangles Triangle;
        public Triangles Triangle2;

        public void ListAdd()
        {
            // When Adding Verticies make the First Vector2 in the list the Center point of the shape!!!!
            Triangle.Verticies.Add(new Vector2(119, 160));//Marking the Center with comment lines
            Triangle.Verticies.Add(new Vector2(112, 0));
            Triangle.Verticies.Add(new Vector2(0, 232));
            Triangle.Verticies.Add(new Vector2(224, 232));

            Triangle2.Verticies.Add(new Vector2(119, 160));//
            Triangle2.Verticies.Add(new Vector2(112, 0));
            Triangle2.Verticies.Add(new Vector2(0, 232));
            Triangle2.Verticies.Add(new Vector2(224, 232));

            Debug.WriteLine(Triangle.Verticies[0]);
            Debug.WriteLine(Triangle.Verticies[1]);
            Debug.WriteLine(Triangle.Verticies[2]);

        }
    }
}
