﻿using System;
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

        public void Gravity()
        {
            Movement = Vector2.Zero;

            Movement = new Vector2(Movement.X, Movement.Y + 1f);

            Placement += Movement;
        }
        public Entity(List<Vector2> numbers):base(numbers) { }

        public void LoadContent()
        {
            player1 = Main.GameContent.Load<Texture2D>("TestCharWalk1");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(player1, Placement, null, null, verticies[0], rotation, null, Color.White);
        }
    }
}
