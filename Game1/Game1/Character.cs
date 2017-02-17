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
        //Constructor
        public Character(List<Vector2> numbers) : base(numbers)
        {
            deadTime.Elapsed += deadTimeEvent;
            deadTime.Interval = 1500;

            levelTimer.Elapsed += levelTimerEvent;
            levelTimer.Interval = 1500;

            dashTimer.Elapsed += DashTimerEvent;
            dashTimer.Interval = 200;
            dashCD.Elapsed += DashCDTimerEvent;
            dashCD.Interval = 4000;
        }

        Texture2D Htex;

        int levelKeeper = 0;
        public int health = 60;
        private int score = 0;
        int dashBarSize = 0;
        public bool levelAllowed = false;
        private bool hpincrease = false;
        int ticktimer = 0;
        int cdTicks = 1000;

        public int getscore()
        {
            return score;
        }

        Rectangle HPbar = new Rectangle();
        Rectangle dashBar = new Rectangle();

        HighScores heyscores = new HighScores();
        Timer deadTime = new Timer();
        Timer levelTimer = new Timer();
        Timer dashTimer = new Timer();
        Timer dashCD = new Timer();

        bool written = false;
        bool CanDash = true;
        bool isDash = false;
        float jump = 7f;
        bool canJump = true;
        bool air = false;

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
            dashBar.Height = 3;
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
                DashSpeed = Vector2.Zero;

                if (Key.IsKeyDown(Keys.D) || Key.IsKeyDown(Keys.Right))
                {
                    texture = Main.GameContent.Load<Texture2D>("Sprites/WalkCycleRight");
                    Movement = new Vector2(Movement.X + 2f, Movement.Y);
                    IsMoving = true;
                }
                if (Key.IsKeyDown(Keys.A) || Key.IsKeyDown(Keys.Left))
                {
                    texture = Main.GameContent.Load<Texture2D>("Sprites/WalkCycleLeft");
                    Movement = new Vector2(Movement.X - 2f, Movement.Y);
                    IsMoving = true;
                }
                if (CanDash)
                {
                    if (Key.IsKeyDown(Keys.Space))
                    {
                        DashSpeed = Movement * 5;
                        isDash = true;
                        CanDash = false;
                        dashTimer.Start();
                    }
                }
                if (isDash)
                {
                    DashSpeed = Movement * 5;
                }
                OldPosition = Placement;
                if (DashSpeed != Vector2.Zero)
                {
                    Placement += DashSpeed;
                }
                else
                {
                    Placement += Movement;
                }
            }
        }
        //allows the player to perform a jump
        public void Jump()
        {
            if (alive)
            {
                KeyboardState newState = Keyboard.GetState();

                if (newState.IsKeyDown(Keys.W) || newState.IsKeyDown(Keys.Up))
                {
                    if (air)
                        IsJumping = true;

                    if (!oldState.IsKeyDown(Keys.W) || !oldState.IsKeyDown(Keys.Up))
                    {
                        if (canJump)
                            air = true;

                        canJump = false;
                    }
                }
                else if (oldState.IsKeyDown(Keys.W) || oldState.IsKeyDown(Keys.Up))
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
        //Reset the floor for the cahracter
        public void FloorReset(bool isWall)
        {
            GravityReset();

            if (!canJump)
                jumpReset(isWall);
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
        public void DrawHud(SpriteBatch spritebatch, GameTime gameTime)
        {
            HPbar.Width = health * 2;
            spritebatch.Draw(Htex, new Vector2(-350, -180) + Placement, HPbar, Color.White);
            spritebatch.DrawString(font, "HP", Placement + new Vector2(-384, -180), Color.Green);
            spritebatch.DrawString(font, "Score : " + score, Placement + new Vector2(-384, -150), Color.Blue);

            if (!CanDash && !isDash)
            {
                if (dashBarSize == 0)
                {
                    dashBarSize = 4;
                }
                ticktimer += gameTime.ElapsedGameTime.Milliseconds;
                if (ticktimer > cdTicks)
                {
                    ticktimer -= cdTicks;
                    dashBarSize--;
                }

                if (dashBarSize != 0)
                {
                    dashBar.Width = dashBarSize * 10;
                    spritebatch.Draw(Htex, new Vector2(-15, 30) + Placement, dashBar, Color.Black);
                }
            }
            else
            {
                ticktimer = 0;
                dashBarSize = 0;
            }
        }
        //Add 1 to player score
        public void AddScore()
        {
            score++;
        }
        //Retrieve the score
        public int getScore()
        {
            return score;
        }
        //Event for the timer when the player dies
        public event EventHandler ChangeScreen;
        private void deadTimeEvent(object source, ElapsedEventArgs e)
        {
            deadTime.Stop();
            ChangeScreen?.Invoke(this, EventArgs.Empty);
        }
        //Event for the timer if the player gets 5 score
        private void levelTimerEvent(object source, ElapsedEventArgs e)
        {
            levelAllowed = false;
            levelTimer.Stop();
        }
        //Event for after you dash. Starts the CD and stops you from continueing to dash
        private void DashTimerEvent(object source, ElapsedEventArgs e)
        {
            isDash = false;
            dashTimer.Stop();
            dashCD.Start();
        }
        private void DashCDTimerEvent(object source, ElapsedEventArgs e)
        {
            CanDash = true;
            dashCD.Stop();
        }
        //checks to see if the player can level up and levels him up if he can
        public void LevelUp(SpriteBatch spriteBatch)
        {
            if (levelAllowed)
            {
                spriteBatch.DrawString(font, "Level up! HP increased by 20", Placement + new Vector2(-52, -50), Color.Red);
                if (hpincrease)
                {
                    health += 10;
                    hpincrease = false;
                }
            }
        }
        //Checks if score is  multiple of 5
        public void CheckLevelUp()
        {
            levelKeeper = score % 10;
            Debug.WriteLine(levelKeeper);
            if (levelKeeper == 0)
            {
                levelAllowed = true;
                hpincrease = true;
                levelTimer.Start();
            }
        }
    }
}