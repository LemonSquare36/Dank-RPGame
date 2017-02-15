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
        public Character(List<Vector2> numbers) : base(numbers)
        {
            deadTime.Elapsed += deadTimeEvent;
            deadTime.Interval = 1000;
        }

        Texture2D Htex;

        int levelKeeper = 0;
        public int health = 60;
        public int ability = 10;
        public int attack = 10;
        public int level = 1;
        private int score = 4;//0
        public int getscore()
        {
            return score;
        }

        Rectangle HPbar = new Rectangle();
        HighScores heyscores = new HighScores();
        Timer deadTime = new Timer();

        bool written = false;

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
            if (alive)
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
        }
        //Checks if is HP is 0 and then does stuff if it is.
        public void CheckIfBeDead(SpriteBatch spriteBatch)
        {
            if (health <= 0)
            {
                alive = false;
                spriteBatch.DrawString(font, "YOU DIED", Placement + new Vector2(-52, -50), Color.Red);
                if (!written)
                {
                    heyscores.ChangeScores(score);
                    written = true;
                    deadTime.Start();
                }
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
        public event EventHandler ChangeScreen;
        private  void deadTimeEvent(object source, ElapsedEventArgs e)
        {
            deadTime.Stop();
            ChangeScreen?.Invoke(this, EventArgs.Empty);
        }

        public void CheckLevelUp(SpriteBatch spriteBatch)
        {

            if (score == 5 || score == 10 || score == 15 || score == 20 || score == 25 || score == 30)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Congratulations! You have leveled up! HP increased by 10", Placement - new Vector2(-52, -50), Color.Red);
                spriteBatch.End();
            }            
        }
    }
}