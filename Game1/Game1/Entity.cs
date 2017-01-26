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

        KeyboardState oldState;

        SpriteBatch spriteBatch;

        public int Rows { get; set; }
        public int Cols { get; set; }
        protected int currentFrame;
        int totalFrames;

        //slow framerate
        protected int timeSinceLastFrame = 0;
        protected int millisecondsPerFrame = 100;
        protected int v1;
        protected int v2;

        float gravity = 0f;
        bool air = false;
        float traveltime = 0;
        float jump = 7f;
        bool canJump = true;

        string savePath = Path.Combine(SourceFolder, "Source/Repos/Dank-RPGame/Game1/Game1/Shapes/shapeplace.txt");

        public void SpriteMove(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            currentFrame = 0;
            totalFrames = Rows * Cols; 
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                //increment current frame
                currentFrame++;
                timeSinceLastFrame = 0;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / Cols;
            int height = texture.Height / 1;
            int row = (int)((float)currentFrame / Cols);
            int cols = currentFrame % Cols;

            Rectangle sourceRectangle = new Rectangle(width * cols, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Placement.X, (int)Placement.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

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
            
        }

        public void FloorReset()
        {
            GravityReset();

            if (!canJump)
                jumpReset();
        }

        protected new Vector2 SetShapePlacement(string ShapeName)
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
             return Placement;
         }
    }
}
