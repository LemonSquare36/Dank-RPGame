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

namespace RPGame
{
    class TutorialZone : Areas
    {
        //Tells the Game wether or not if its an Area
        public TutorialZone(bool isArea) : base(isArea)
        {
            isarea = isArea;
        }

        Polygons Twall, Twall2, TFloor, TFloor2, goop;
        CrawlerAlien Crawler1;
        List<Entity> Enemies;

        Texture2D Background;
        bool Wleft;
        bool Wright;
        bool jump;
        bool colected;
        bool goopCollision;
        bool SpawnEnemy;
        bool enemyCollision;
        bool start;
        bool dash;
        bool readDash;

        List<Polygons> PolyList;
        //For starting the Tutorial
        public override void Initialize()
        {
            Enemies = new List<Entity>();
            PolyList = new List<Polygons>();

            Wleft = false;
            Wright = false;
            jump = false;
            colected = false;
            goopCollision = false;
            SpawnEnemy = false;
            enemyCollision = false;
            start = false;
            dash = false;
            readDash = false;
        }
        //Load
        public override void LoadContent(SpriteBatch spriteBatchMain)
        {
            MakeShapes();
            base.LoadContent(spriteBatch);

            Player.ChangeScreen += OnScreenChanged;

            spriteBatch = spriteBatchMain;
            Twall.LoadContent("twall1", "TWall", true);
            Twall2.LoadContent("twall2", "TWall", true);
            TFloor.LoadContent("tfloor1", "TFloor", false);
            TFloor2.LoadContent("tfloor2", "TFloor", false);
            goop.LoadContent("goop", "goop", false);
            goop.Placement = new Vector2(500, 400);

            Crawler1.Load(100, 400);

            Player.LoadCharacter("TutorialSpawn");
            Player.SpriteMove(1, 3);

            Background = Main.GameContent.Load<Texture2D>("Sprites/TutorialSprites/TBack");

            ListAdd();
            AlienListAdd();

            //movement for enemies
            foreach (Entity enemy in Enemies)
            {
                enemy.SpriteMove(1, 4);
            }
        }
        //Draw
        public override void Draw()
        {

            Player.RealPos();
            Twall.RealPos();
            Twall2.RealPos();
            TFloor.RealPos();
            TFloor2.RealPos();
            goop.RealPos();

            spriteBatch.Draw(Background, new Vector2(50, 40), null, null);
            Twall.Draw(spriteBatch);
            Twall2.Draw(spriteBatch);
            TFloor.Draw(spriteBatch);
            TFloor2.Draw(spriteBatch);
            if (goopCollision)
            {
                goop.Draw(spriteBatch);
            }

            Player.Draw(spriteBatch);
            Player.DrawHud(spriteBatch, time);


            foreach (Entity enemy in Enemies)
            {
                if (enemyCollision)
                {
                    enemy.Draw(spriteBatch);
                }
                enemy.RealPos();
            }

            Player.CheckIfBeDead(spriteBatch);

            try
            {
                TutorialCommands();
            }
            catch (Exception ex) { ErrorHandling(ex.Message, GetType().Name, ex); }
        }
        //Updates the area/Game
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager, GraphicsDevice graphicsDevice)
        {
            bool PlayerCollision;
            bool EnemyCollision;

            Player.Gravity();
            getKey();
            try
            {


                if (goopCollision)
                {
                    bool goopCollide = Collision(Player, goop);
                    if (goopCollide)
                    {
                        Player.AddScore();
                        colected = true;
                        goopCollision = false;
                    }
                }

                if (enemyCollision)
                {
                    //Collion with enemys nd polygons
                    foreach (Entity enemy in Enemies)
                    {
                        enemy.Gravity();

                        foreach (Polygons poly in PolyList)
                        {
                            EnemyCollision = Collision(enemy, poly);
                            if (EnemyCollision)
                            {
                                enemy.Rebuff(poly);
                            }
                        }
                        if (enemy.IsMoving)
                        {
                            enemy.Update(time);
                        }
                    }

                    //collision for enemies with player
                    foreach (Entity enemy in Enemies)
                    {
                        enemy.IsMoving = false;

                        double distance = Distance(enemy.getRealPos(0), Player.getRealPos(0));
                        if (distance <= 150 && distance > 0)
                        {
                            enemy.MoveRight();
                        }
                        if (distance >= -205 && distance < -45)
                        {
                            enemy.MoveLeft();
                        }

                        PlayerCollision = Collision(Player, enemy);
                        if (PlayerCollision)
                        {
                            Player.health--;
                            Player.Rebuff(enemy);
                            Player.FloorReset(enemy.getisWall());
                        }
                    }
                }
                //Collision with the player and polygons
                foreach (Polygons poly in PolyList)
                {
                    PlayerCollision = Collision(Player, poly);
                    if (PlayerCollision)
                    {
                        Player.Rebuff(poly);
                        Player.FloorReset(poly.getisWall());
                    }
                }
            }

            catch (Exception ex) { ErrorHandling(ex.Message, GetType().Name, ex); }

            Player.MoveChar(Key);
            Player.Jump();

            //Update Textures Here
            Crawler1.UpdateTexture();

            if (Player.IsMoving)
                Player.Update(time);
        }
        //Create your objects here
        private void MakeShapes()
        {
            //Create the Polygon 
            RetrieveShapes();

            Twall = CreateShape("twall");
            Twall2 = CreateShape("twall");
            TFloor = CreateShape("tfloor");
            TFloor2 = CreateShape("tfloor");
            goop = CreateShape("goop");

            Player = CreateChar("janitor");

            Crawler1 = CreateCrawler("Crawler");
        }
        //Adds the aliens to a list
        private void AlienListAdd()
        {
            Enemies.Add(Crawler1);
        }

        //adds polygons to a list
        private void ListAdd()
        {
            PolyList.Add(Twall);
            PolyList.Add(Twall2);
            PolyList.Add(TFloor);
            PolyList.Add(TFloor2);
        }
        //Runs the Tutrial
        private void TutorialCommands()
        {
            if (!start)
            {
                spriteBatch.DrawString(font, "The green bar is your HP", new Vector2(280, 150), Color.Red);
                spriteBatch.DrawString(font, "I know it looks funny but it wont after the tutorial is over", new Vector2(100, 200), Color.Red);
                spriteBatch.DrawString(font, "Press S to continue", new Vector2(300, 250), Color.Red);
                if (Key.IsKeyDown(Keys.S))
                {
                    start = true;
                }
            }
            else
            {
                // Press A and D to continue
                if (!Wleft || !Wright)
                {
                    spriteBatch.DrawString(font, "Press A to Left", new Vector2(200, 200), Color.Red);
                    spriteBatch.DrawString(font, "Press D to Right", new Vector2(400, 200), Color.Red);
                    if (Key.IsKeyDown(Keys.A))
                    {
                        Wleft = true;
                    }
                    else if (Key.IsKeyDown(Keys.D))
                    {
                        Wright = true;
                    }
                }
                else
                {
                    // Press W to continue
                    if (!jump)
                    {
                        spriteBatch.DrawString(font, "Press W to Jump", new Vector2(300, 200), Color.Red);
                        if (Key.IsKeyDown(Keys.W))
                        {
                            jump = true;
                        }
                    }
                    else
                    {
                        // Press Space to Continue
                        if (!dash)
                        {
                            spriteBatch.DrawString(font, "Space to Dash", new Vector2(300, 200), Color.Red);
                            spriteBatch.DrawString(font, "(Due to a bug the tutorial dash only works in air)", new Vector2(150, 250), Color.Red);
                            if (Key.IsKeyDown(Keys.Space))
                            {
                                dash = true;
                            }
                        }
                        else
                        {
                            if (!readDash)
                            {
                                spriteBatch.DrawString(font, "The black bar that apears under you character after dashing", new Vector2(100, 150), Color.Red);
                                spriteBatch.DrawString(font, "is the Cool Down timer of the your ability to Dash", new Vector2(120, 200), Color.Red);
                                spriteBatch.DrawString(font, "Press S to continue", new Vector2(300, 250), Color.Red);
                                if (Key.IsKeyDown(Keys.S))
                                {
                                    readDash = true;
                                    goopCollision = true;
                                }
                            }
                            //Collect the goop
                            else
                            {
                                if (!colected)
                                {
                                    spriteBatch.DrawString(font, "That is goop. Collect it for score", new Vector2(200, 200), Color.Red);

                                }
                                //Enemy Spawns - Die to continue
                                else
                                {
                                    if (!SpawnEnemy)
                                    {
                                        enemyCollision = true;
                                        spriteBatch.DrawString(font, "The Aliens have Invaded!", new Vector2(250, 100), Color.Red);
                                        spriteBatch.DrawString(font, "Don't Let them stop you from you Janitorial duties", new Vector2(140, 150), Color.Red);
                                        spriteBatch.DrawString(font, "Avoid dashing into enemies or you will take massive damage", new Vector2(85, 200), Color.Red);
                                        spriteBatch.DrawString(font, "(Die to Continue)", new Vector2(300, 250), Color.Red);

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
