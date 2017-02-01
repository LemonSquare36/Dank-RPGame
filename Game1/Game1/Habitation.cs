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
        Polygons FloorbytheDoor, FloorHump, LongFloor1, LongFloor2, Mramp, HWall1, HWall2;
        Texture2D CeilingbytheDoor, CeilingHump, CeilingFloor1, CeilingFloor2, CeilingMramp, jDoor, sDoor, cTable1, cTable2, cTable3, cCounter;
        Character Player;
        CrawlerAlien Crawler1;
        List<Polygons> PolyList;
        List<Entity> Enemies;
        public override void Initialize()
        {
            PolyList = new List<Polygons>();
            Enemies = new List<Entity>();
        }

        public override void LoadContent(SpriteBatch spriteBatchMain)
        {
            MakeShapes();
            spriteBatch = spriteBatchMain;

            base.LoadContent(spriteBatch);

            FloorbytheDoor.LoadContent("floorbythedoor", "floorbythedoor");
            FloorHump.LoadContent("floorhump", "floorhump");
            LongFloor1.LoadContent("longfloor1", "longfloor");
            LongFloor2.LoadContent("longfloor2", "longfloor");
            Mramp.LoadContent("mramp", "mramp");
            HWall1.LoadContent("hwall1", "hwall");
            HWall2.LoadContent("hwall2", "hwall");

            Player.LoadCharacter("HabitationJanitorDoor");

            Crawler1.Load(-300, 100);

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
            Crawler1.SpriteMove(1, 4);

            ListAdd();
            AlienListAdd();
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            Player.Gravity();
            camera.Follow(-Player.getRealPos(0));
            getKey();
            bool PlayerCollision;
            bool CrawlerCollision;

            foreach (Polygons poly in PolyList)
            {
                PlayerCollision = Collision(Player, poly);
                if (PlayerCollision)
                {
                    Player.Rebuff(poly);
                    Player.FloorReset();
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
                if (distance >= -210 && distance < -50)
                {
                    enemy.MoveLeft();
                }

                PlayerCollision = Collision(Player, enemy);
                if (PlayerCollision)
                {
                    Player.Rebuff(enemy);
                    Player.FloorReset();
                }
            }
            CrawlerCollision = Collision(Crawler1, Mramp);
            if (CrawlerCollision)
            {
                Crawler1.Rebuff(Mramp);
            }

            //Update Textures Here
            Crawler1.UpdateTexture();

            foreach (Entity enemy in Enemies)
            {

                enemy.Gravity();
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
                    }
                }
            }


            Player.MoveChar(Key);
            Player.Jump();

            if (Player.IsMoving)
                Player.Update(time);

            camera.ChangeScreenSize(Key, graphicsManager);
        }

        public override void Draw()
        {
            foreach (Polygons poly in PolyList)
            {
                poly.RealPos();
            }
            Player.RealPos();
            Crawler1.RealPos();

            FloorbytheDoor.Draw(spriteBatch);
            FloorHump.Draw(spriteBatch);
            Mramp.Draw(spriteBatch);
            LongFloor1.Draw(spriteBatch);
            LongFloor2.Draw(spriteBatch);
            HWall1.Draw(spriteBatch);
            HWall2.Draw(spriteBatch);

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

            spriteBatch.DrawString(font, "Hanger\n <----", new Vector2(-2100, 150), Color.White);
            spriteBatch.DrawString(font, "Cafeteria", new Vector2(-1800, 150), Color.DarkRed);

            Player.Draw(spriteBatch);
            Crawler1.Draw(spriteBatch);
        }
        private void MakeShapes()
        {
            RetrieveShapes();

            FloorbytheDoor = CreateShape("floorbythedoor");
            FloorHump = CreateShape("floorhump");
            LongFloor1 = CreateShape("longfloor");
            LongFloor2 = CreateShape("longfloor");
            Mramp = CreateShape("mramp");
            HWall1 = CreateShape("hwall");
            HWall2 = CreateShape("hwall");

            Player = CreateChar("janitor");

            Crawler1 = CreateCrawler("Crawler");
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
