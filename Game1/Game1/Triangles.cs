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
    class Triangles : Polygons
    {
        Texture2D triangle, Trapazoid;
        public enum Shape { Triangle }
        Shape Ptype;
        float rotation = 2;
        SpriteEffects effects;
        public Vector2 Placement { get; set; }


        public List<Vector2> Verticies = new List<Vector2>();

        public void LoadContent(ContentManager theContentManager)
        {
            triangle = Main.GameContent.Load<Texture2D>("Sprites/GreenTriangle");
        }

        public void update(GameTime gameTime)
        {
            //rotation = rotation + .04f;
            Placement = new Vector2(Placement.X + 0.5f, Placement.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(triangle, Placement, null, null, Verticies[0], rotation, null, null);
        }
    }
}
