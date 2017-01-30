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
//using Microsoft.Xna.Framework.Storage;

namespace RPGame
{
    public class Character : Entity
    {
        public Character(List<Vector2> numbers) : base(numbers) { }

        KeyboardState Key;

        SpriteBatch spriteBatch;

        public int health = 20;
        public int ability = 10;
        public int attack = 10;
        public int level = 1;

        /// <summary>
        /// The Area in which you are trying to load the character into and the place; Please use clear names
        /// </summary>
        /// <param name="area"></param>
        public void LoadCharacter(string area)
        {
            texture = Main.GameContent.Load<Texture2D>("Sprites/WalkCycleLeft");

            if (area == "HabitationJanitorDoor")
                Placement = new Vector2(400, 100);
            if (area == "TutorialSpawn")
                Placement = new Vector2(400, 300);

        }

        public void MoveChar(KeyboardState Key)
        {
            IsMoving = false;
            Movement = Vector2.Zero;

            if (Key.IsKeyDown(Keys.D))
            {
                Movement = new Vector2(Movement.X + 2f, Movement.Y);
                IsMoving = true;
            }
            if (Key.IsKeyDown(Keys.A))
            {
                Movement = new Vector2(Movement.X - 2f, Movement.Y);
                IsMoving = true;
            }
            OldPosition = Placement;
            Placement += Movement;
        }

        public void LevelUp()
        {
            spriteBatch.DrawString(font, "Congratulations! You have leveled up! Press X to upgrade attack. Press C to upgrade health. Press V to upgrade ability", new Vector2(200, 200), Color.Red);

            if (Key.IsKeyDown(Keys.X))
            {
                attack += 5;
            }
            else if (Key.IsKeyDown(Keys.C))
            {
                health += 10;
            }
            else if (Key.IsKeyDown(Keys.V))
            {
                ability += 5;
            }
        }
    }
}