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
    public class Entity : Polygons
    {
        protected Texture2D janitor;
        KeyboardState oldState;
        float gravity = 0f;
        bool air = false;
        float traveltime = 0;
        float jump = 7f;
        bool canJump = true;

        string savePath = Path.Combine(SourceFolder, "Source/Repos/Dank-RPGame/Game1/Game1/Shapes/shapeplace.txt");

        public void Gravity()
        {
            Movement = Vector2.Zero;
            Movement = new Vector2(Movement.X, Movement.Y + gravity);
            Placement += Movement;

            if (gravity != 3 && gravity < 3)
            {
                gravity += .05f;
            }
        }
        public void GravityReset()
        {
            gravity = 0.5f;
        }

        public void Jump()
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.W))
            {
                if (!oldState.IsKeyDown(Keys.W))
                {
                    if (canJump)
                        air = true;
                    canJump = false;
                }
            }
            else if (oldState.IsKeyDown(Keys.W))
            {

            }
            oldState = newState;
            if (air)
            {
                Movement = Vector2.Zero;
                Movement = new Vector2(Movement.X, Movement.Y - jump);
                jump -= .5f;
                Placement += Movement;
            }
            if (jump < 0)
            {
                air = false;
                jump = 10;
            }
        }

        public void jumpReset()
        {
            canJump = true;
        }

        public Entity(List<Vector2> numbers) : base(numbers) { }

        public void LoadContent()
        {
            janitor = Main.GameContent.Load<Texture2D>("TestCharWalk1");
        }

        public void FloorReset()
        {
            GravityReset();

            if (!canJump)
                jumpReset();
        }

        /*protected new Vector2 SetShapePlacement(string ShapeName)
        {
            var PlaceReader = new StreamReader(savePath);

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
        }*/
    }
}
