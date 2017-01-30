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

        public bool IsMoving = true;
        public bool IsJumping = false;

        public int Rows { get; set; }
        public int Cols { get; set; }

        protected int currentFrame;
        protected int totalFrames;

        //slow framerate
        protected int timeSinceLastFrame = 0;
        protected int millisecondsPerFrame = 100;

        float gravity = 0f;
        bool air = false;
        float traveltime = 0;
        float jump = 7f;
        bool canJump = true;

        public void SpriteMove(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            currentFrame = 0;
            totalFrames = Rows * Cols; 
        }

        public void Update(GameTime gameTime)
        {
            if (IsJumping == false)
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int width = texture.Width / Cols;
            int height = texture.Height / 1;
            int row = (int)((float)currentFrame / Cols);
            int cols = currentFrame % Cols;

            Rectangle sourceRectangle = new Rectangle(width * cols, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)Placement.X, (int)Placement.Y, width, height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, rotation, verticies[0], SpriteEffects.None, 0);
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
                IsJumping = true;
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

    }
}
