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
    public class Character : Entity
    {
        public Character(List<Vector2> numbers):base(numbers) { }

        //meh
        List<string> inventory = new List<string> ();

        public int health = 20;
        public int ability = 10;
        public int attack = 10;
        public int level = 1;

        protected Random rand = new Random();

        public void LevelUp()
        {

        }

        public void LoadItems()
        {
 
        }
        public void SaveItems()
        {
        }

        public void LoadCharacter()
        {

        }
        public void SaveCharacter()
        {

        }


        public void MoveChar(KeyboardState Key)
        {
            Movement = Vector2.Zero;

            if (Key.IsKeyDown(Keys.D))
            {
                Movement = new Vector2(Movement.X + 2f, Movement.Y);
            }
            if (Key.IsKeyDown(Keys.A))
            {
                Movement = new Vector2(Movement.X - 2f, Movement.Y);
            }
            OldPosition = Placement;
            Placement += Movement;
        }
    }
}