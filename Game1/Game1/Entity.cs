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

        public Entity(List<Vector2> numbers) : base(numbers) { }

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
        double ElapsedTime = .5f;
        double StartTime = 0;

        float gravity = 0f;
        bool air = false;
        float traveltime = 0;
        float jump = 7f;
        bool canJump = true;

        //sets up variables necessary to allow sprites to be animated
        public void SpriteMove(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            currentFrame = 0;
            totalFrames = Rows * Cols;
        }
        //allows sprites to be animated 
        public void Update(GameTime gameTime)
        {
            if (!IsJumping)
            {
                //increments frames
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
        //draws assets
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
        //pulls the asset back to the ground
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
        //resets gravity
        public void GravityReset()
        {
            gravity = 0.5f;
        }
        //allows the player to perform a jump
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
        //resets the jump
        public void jumpReset(bool isWall)
        {
            if (!isWall)
            {
                canJump = true;
                IsJumping = false;
            }
            
        }

        public void FloorReset(bool isWall)
        {
            GravityReset();

            if (!canJump)
                jumpReset(isWall);
        }
        //movement for the enemies
        public void MoveLeft()
        {
            Movement = Vector2.Zero;

            IsMoving = true;
            Movement.X += -1.8f;

            Placement += Movement;
        }
        //movement for the enemies
        public void MoveRight()
        {
            Movement = Vector2.Zero;

            IsMoving = true;
            Movement.X += 1.8f;

            Placement += Movement;
        }
        //damages the player based on number of frames that the player is touching an enemy
        public void Attack(ref Character ch, GameTime gameTime)
        {
            StartTime += gameTime.ElapsedGameTime.Seconds;

            if (StartTime >= ElapsedTime)
                ch.health -= 2;
        }


    }
}
