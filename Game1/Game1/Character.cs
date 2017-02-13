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
        public Character(List<Vector2> numbers) : base(numbers) { }

        KeyboardState Key;

        SpriteBatch spriteBatch;

        private Texture2D Htex;

        public int health = 60;
        public int ability = 10;
        public int attack = 10;
        public int level = 1;
        private int score = 0;

        Rectangle HPbar = new Rectangle();
        HighScores heyscores = new HighScores();

        /// <summary>
        /// The Area in which you are trying to load the character into and the place; Please use clear names
        /// </summary>
        /// <param name="area"></param>
        // Loads the Texture and Placement based on his area.
        public void LoadCharacter(string area)
        {
            Htex = Main.GameContent.Load<Texture2D>("Sprites/HPTexture");
            texture = Main.GameContent.Load<Texture2D>("Sprites/WalkCycleLeft");
            HPbar.Height = 30;
            font = Main.GameContent.Load<SpriteFont>("myFont");

            if (area == "HabitationJanitorDoor")
                Placement = new Vector2(400, 140);
            if (area == "TutorialSpawn")
                Placement = new Vector2(400, 300);

        }
        //Move him around with WASD
        public void MoveChar(KeyboardState Key)
        {
            IsMoving = false;
            Movement = Vector2.Zero;

            if (Key.IsKeyDown(Keys.D))
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/WalkCycleRight");
                Movement = new Vector2(Movement.X + 2f, Movement.Y);
                IsMoving = true;
            }
            if (Key.IsKeyDown(Keys.A))
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/WalkCycleLeft");
                Movement = new Vector2(Movement.X - 2f, Movement.Y);
                IsMoving = true;
            }
            OldPosition = Placement;
            Placement += Movement;
        }
        //Framework for a levelup function that isnt used at the time of me commenting this.
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
        //Checks if is HP is 0 and the does stuff if it is.
        public void CheckIfBeDead(SpriteBatch spriteBatch)
        {
            if (health <= 0)
            {
                spriteBatch.DrawString(font, "YOU DIED", Placement + new Vector2(-52, -50), Color.Red);
                heyscores.ChangeScores(score);
            }
        }
        //Draws the Hud
        public void DrawHud(SpriteBatch spritebatch)
        {
            HPbar.Width = health * 2;
            spritebatch.Draw(Htex, new Vector2(-350, -180) + Placement, HPbar, Color.White);
            spritebatch.DrawString(font, "HP", Placement + new Vector2(-384, -180), Color.Green);
            spritebatch.DrawString(font, "Score : " + score, Placement + new Vector2(-384, -150), Color.Blue);
        }

        public void AddScore()
        {
            score++;
        }
    }
}