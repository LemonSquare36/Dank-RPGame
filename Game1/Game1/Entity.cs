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
        protected Texture2D player1;
        KeyboardState oldState;
        float gravity = 0f;
        bool air = false;
        float traveltime = 0;
        float jump = 7f;

        public void Gravity()
        {
            Movement = Vector2.Zero;
            Movement = new Vector2(Movement.X, Movement.Y + gravity);
            Placement += Movement;

            if (gravity != 2 && gravity < 2)
            {
                gravity += .05f;
            }
        }
        public void GravityReset()
        {
            gravity = 0.5f;
        }

        public void Jump(KeyboardState key)
        {
            KeyboardState newState = Keyboard.GetState();

            
            if (newState.IsKeyDown(Keys.Space))
            {
               if (!oldState.IsKeyDown(Keys.Space))
                {
                    air = true;
                    
                } 
            }
            else if (oldState.IsKeyDown(Keys.Space))
            {

            }
            oldState = newState;
            if (air)
            {
                Movement = new Vector2(Movement.X, Movement.Y - jump);
                jump -= .5f;
                Placement += Movement;
            }
            if (jump < 0)
            {
                air = false;
                jump = 7;
            }
        }
        public Entity(List<Vector2> numbers):base(numbers) { }

        public void LoadContent()
        {
            player1 = Main.GameContent.Load<Texture2D>("TestCharWalk1");
        }
    }
}
