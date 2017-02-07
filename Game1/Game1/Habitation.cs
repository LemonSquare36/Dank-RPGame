using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
namespace RPGame
{
    class Habitation : Areas
    {

        public Habitation(bool isArea) : base(isArea)
        {
            isarea = isArea;
        }

        Polygons FloorbytheDoor, FloorHump, LongFloor1, LongFloor2, Mramp, HWall1, HWall2;
        Texture2D CeilingbytheDoor, CeilingHump, CeilingFloor1, CeilingFloor2, CeilingMramp, jDoor, sDoor, cTable1, cTable2, cTable3, cCounter;
        Character Player;
        CrawlerAlien Crawler1, Crawler2, Crawler3, Crawler4, Crawler5, Crawler6, Crawler7;
        List<Polygons> PolyList;
        List<Entity> Enemies;
       // List<Entity> goops;
        Polygons goop;


        public override void Initialize()
        {
            PolyList = new List<Polygons>();
            Enemies = new List<Entity>();
           // goops = new List<Entity>();
        }

        public override void LoadContent(SpriteBatch spriteBatchMain)
        {

            MakeShapes();
            spriteBatch = spriteBatchMain;

            base.LoadContent(spriteBatch);

            FloorbytheDoor.LoadContent("floorbythedoor", "floorbythedoor", false);
            FloorHump.LoadContent("floorhump", "floorhump", false);
            LongFloor1.LoadContent("longfloor1", "longfloor", false);
            LongFloor2.LoadContent("longfloor2", "longfloor", false);
            Mramp.LoadContent("mramp", "mramp", false);
            HWall1.LoadContent("hwall1", "hwall", true);
            HWall2.LoadContent("hwall2", "hwall", true);

            Player.LoadCharacter("HabitationJanitorDoor");

            //goop.LoadContent("goop", "goop", false);

            Crawler1.Load(-500, 100);
            Crawler2.Load(-1800, 100);
            Crawler3.Load(-1700, 100);
            Crawler4.Load(-1600, 100);
            Crawler5.Load(-2000, 100);
            Crawler6.Load(-1900, 100);
            Crawler7.Load(-2100, 100);

            


            #region LoadSprites
            CeilingbytheDoor = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/FloorByTheDoor");
            CeilingHump = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/FloorHump");
            CeilingFloor1 = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/LongFloor");
            CeilingFloor2 = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/LongFloor");
            CeilingMramp = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/Mramp");
            jDoor = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/Janitor Door");
            sDoor = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/Stairs Door");
            cCounter = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/FoodCounter");
            cTable1 = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/Table with Chairs");
            cTable2 = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/Table with Chairs");
            cTable3 = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/Table with Chairs");

            #endregion

            Player.SpriteMove(1, 3);

            ListAdd();
            AlienListAdd();



            foreach (Entity enemy in Enemies)
            {
                enemy.SpriteMove(1, 4);
            }
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {

            Player.RealPos();

            Player.Gravity();
            camera.Follow(-Player.getRealPos(0));
            getKey();
            bool PlayerCollision;
            bool CrawlerCollision;

            Debug.WriteLine(Player.health);

            foreach (Polygons poly in PolyList)
            {
                poly.RealPos();

                PlayerCollision = Collision(Player, poly);
                if (PlayerCollision)
                {
                    Player.Rebuff(poly);
                    Player.FloorReset(poly.getisWall());
                }
            }

            foreach (Entity enemy in Enemies)
            {
                enemy.Gravity();
                enemy.RealPos();

                if (enemy.IsMoving)
                {
                    enemy.Update(time);
                }
                foreach (Polygons poly in PolyList)
                {
                    CrawlerCollision = Collision(enemy, poly);
                    if (CrawlerCollision)
                    {
                        enemy.Rebuff(poly);
                        enemy.GravityReset();
                    }
                }
            }


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
            foreach (Entity enemy in Enemies)
            {
                foreach (Entity Enemy in Enemies)
                {

                    if (enemy != Enemy)
                    {
                        CrawlerCollision = Collision(Enemy, enemy);
                     if (CrawlerCollision)
                        {
                            enemy.Rebuff(Enemy);
                        }
                    }
                }
            }

                //Update Textures Here
                Crawler1.UpdateTexture();
                Crawler2.UpdateTexture();
                Crawler3.UpdateTexture();
                Crawler4.UpdateTexture();
                Crawler5.UpdateTexture();
                Crawler6.UpdateTexture();
                Crawler7.UpdateTexture();


                Player.MoveChar(Key);
                Player.Jump();

                if (Player.IsMoving)
                    Player.Update(time);

                camera.ChangeScreenSize(Key, graphicsManager);
            
        }

        public override void Draw()
        {

            #region DrawSprites
            spriteBatch.Draw(CeilingbytheDoor, new Vector2(330, -60), null);
            spriteBatch.Draw(CeilingHump, new Vector2(-265, -150), null);
            spriteBatch.Draw(CeilingFloor1, new Vector2(-1200, -60), null);
            spriteBatch.Draw(CeilingMramp, new Vector2(-1480, -60), null);
            spriteBatch.Draw(CeilingFloor2, new Vector2(-2370, 80), null);
            spriteBatch.Draw(jDoor, new Vector2(400, 100), null);
            spriteBatch.Draw(sDoor, new Vector2(-1000, 100), null);
            spriteBatch.Draw(cCounter, new Vector2(-1850, 200), null);
            spriteBatch.Draw(cTable1, new Vector2(-1800, 280), null);
            spriteBatch.Draw(cTable2, new Vector2(-1650, 280), null);
            spriteBatch.Draw(cTable3, new Vector2(-1950, 280), null);
            #endregion

            foreach (Polygons poly in PolyList)
            {
                poly.Draw(spriteBatch);
            }

            foreach (Entity enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }

            spriteBatch.DrawString(font, "Hanger\n <----", new Vector2(-2100, 150), Color.White);
            spriteBatch.DrawString(font, "Cafeteria", new Vector2(-1800, 150), Color.DarkRed);

            Player.Draw(spriteBatch);
            Player.DrawHud(spriteBatch);

            Player.CheckIfBeDead(spriteBatch);
        }



        private void MakeShapes()
        {
            RetrieveShapes();

            FloorbytheDoor = CreateShape("floorbythedoor");
            FloorHump = CreateShape("floorhump");
            Mramp = CreateShape("mramp");
            LongFloor1 = CreateShape("longfloor2");
            LongFloor2 = CreateShape("longfloor");
            HWall1 = CreateShape("hwall");
            HWall2 = CreateShape("hwall");

            //goop = CreateShape("goop");

            Player = CreateChar("janitor");

            Crawler1 = CreateCrawler("Crawler");
            Crawler2 = CreateCrawler("Crawler");
            Crawler3 = CreateCrawler("Crawler");
            Crawler4 = CreateCrawler("Crawler");
            Crawler5 = CreateCrawler("Crawler");
            Crawler6 = CreateCrawler("Crawler");
            Crawler7 = CreateCrawler("Crawler");
        }
        private void ListAdd()
        {
            PolyList.Add(FloorbytheDoor);
            PolyList.Add(FloorHump);
            PolyList.Add(LongFloor1);
            PolyList.Add(LongFloor2);
            PolyList.Add(Mramp);
            PolyList.Add(HWall1);
            PolyList.Add(HWall2);
        }

        private void AlienListAdd()
        {
            Enemies.Add(Crawler1);
            Enemies.Add(Crawler2);
            Enemies.Add(Crawler3);
            Enemies.Add(Crawler4);
            Enemies.Add(Crawler5);
            Enemies.Add(Crawler6);
            Enemies.Add(Crawler7);
        }



        private double Distance(Vector2 point1, Vector2 point2)
        {
            double D = point2.X - point1.X;

            double X = Math.Pow((point2.X - point1.X), 2);
            double Y = Math.Pow((point2.Y - point1.Y), 2);

            double unit = Math.Sqrt(X + Y);

            if (D < 0)
            {
                return -unit;
            }
            else if (D > 0)
            {
                return unit;
            }
            return 0;
        }
    }
}
