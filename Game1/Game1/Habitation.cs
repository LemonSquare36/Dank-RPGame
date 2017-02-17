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
        //sets Habitation to be an "Area"
        public Habitation(bool isArea) : base(isArea){ isarea = isArea; }

        Polygons goop, FloorbytheDoor, FloorHump, LongFloor1, LongFloor2, Mramp, HWall1, HWall2;
        Texture2D CeilingbytheDoor, CeilingHump, CeilingFloor1, CeilingFloor2, CeilingMramp, jDoor, sDoor, cTable1, cTable2, cTable3, cCounter;
        CrawlerAlien Crawler1, Crawler2, Crawler3, Crawler4, Crawler5, Crawler6, Crawler7;
        List<Polygons> PolyList;
        List<Entity> Enemies;
        List<Color> ColorList;
        Color color;
        int EnemyNumber;

        //sets up points for the goop to be drawn
        List<Vector2> spawnPoints = new List<Vector2>();
        Vector2 point1 = new Vector2(100, 100);
        Vector2 point2 = new Vector2(-75, 100);
        Vector2 point3 = new Vector2(-300, 185);
        Vector2 point4 = new Vector2(-500, 185);
        Vector2 point5 = new Vector2(-900, 185);
        Vector2 point6 = new Vector2(-1100, 185);
        Vector2 point7 = new Vector2(-2300, 315);
        Vector2 point8 = new Vector2(-1700, 315);
        Vector2 point9 = new Vector2(-1900, 315);
        Vector2 point10 = new Vector2(-2000, 315);


        public override void Initialize()
        {
            PolyList = new List<Polygons>();
            Enemies = new List<Entity>();
            ColorList = new List<Color>();

            EnemyNumber = 8;

            ColorListAdd();
            SpawnPointsAdd();

            color = GetrandColor();
        }
        //loads assets
        public override void LoadContent(SpriteBatch spriteBatchMain)
        {

            MakeShapes();

            Player.ChangeScreen += OnScreenChanged;

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

            goop.LoadContent("goop", "goop", false);

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

            //movement for enemies
            foreach (Entity enemy in Enemies)
            {
                enemy.SpriteMove(1, 4);
            }
        }
        //allows the camera to follow the player
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager,GraphicsDevice graphicsDevice)
        {
            Player.Gravity();
            Player.RealPos();
            goop.RealPos();

            camera.Follow(-Player.getRealPos(0));
            getKey();
            bool PlayerCollision;
            bool CrawlerCollision;


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

            //gravity and collision for enemies
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
            //Range Detection for enemies with player
            foreach (Entity enemy in Enemies)
            {
                enemy.IsMoving = false;

                double distance = Distance(enemy.getRealPos(0), Player.getRealPos(0));
                if (distance <= 150 && distance > 0)
                {
                    enemy.MoveRight();
                }
                if (distance >= -205 && distance < -40)
                {
                    enemy.MoveLeft();
                }
            }

            //collides the enemies with each other and Player
            bool AlreadyCollided = false;
            foreach (Entity enemy in Enemies)
            {
                PlayerCollision = Collision(Player, enemy);
                if (PlayerCollision)
                {
                    Player.health--;
                    if (!AlreadyCollided)
                    {
                        Player.Rebuff(enemy);
                    }
                    AlreadyCollided = true;
                    Player.FloorReset(enemy.getisWall());
                }
            }

            PlayerCollision = Collision(Player, goop);
            if (PlayerCollision)
            {
                Player.AddScore();
                Player.CheckLevelUp();
                //move goop texture
                SetGoopPlacement();
                //gets a color for the goop
                color = GetrandColor();
                //adds a new enemy
                AddNewEnemy();
            }
                //Update Textures Here
            foreach (CrawlerAlien crawler in Enemies)
            {
                crawler.UpdateTexture();
            }
            Player.Jump();
            Player.MoveChar(Key);

            if (Player.IsMoving)
                    Player.Update(time);
            
        }
        //draw assets
        public override void Draw()
        {
            goop.Draw(spriteBatch, color);
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
            Player.DrawHud(spriteBatch, time);

            Player.CheckIfBeDead(spriteBatch);

            if (Player.levelAllowed)
                Player.LevelUp(spriteBatch);
        }
        //draws the environment
        private void MakeShapes()
        {
            RetrieveShapes();

            FloorbytheDoor = CreateShape("floorbythedoor");
            FloorHump = CreateShape("floorhump");
            Mramp = CreateShape("mramp");
            LongFloor1 = CreateShape("longfloor");
            LongFloor2 = CreateShape("longfloor");
            HWall1 = CreateShape("hwall");
            HWall2 = CreateShape("hwall");
            goop = CreateShape("goop");

            Player = CreateChar("janitor");

            Crawler1 = CreateCrawler("Crawler");
            Crawler3 = CreateCrawler("Crawler");
            Crawler2 = CreateCrawler("Crawler");
            Crawler4 = CreateCrawler("Crawler");
            Crawler5 = CreateCrawler("Crawler");
            Crawler6 = CreateCrawler("Crawler");
            Crawler7 = CreateCrawler("Crawler");
        }
        //adds assets to the list of polygons that need to be drawn
        private void ListAdd()
        {
            PolyList.Add(FloorbytheDoor);
            PolyList.Add(FloorHump);
            PolyList.Add(Mramp);
            PolyList.Add(LongFloor1);
            PolyList.Add(LongFloor2);
            PolyList.Add(HWall1);
            PolyList.Add(HWall2);
        }
        //adds aliens to the list of enemies to be drawn
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
        //Adds colors to colorlist
        private void ColorListAdd()
        {
            ColorList.Add(Color.Blue);
            ColorList.Add(Color.Green);
            ColorList.Add(Color.Red);
            ColorList.Add(Color.Orange);
            ColorList.Add(Color.GreenYellow);
        }
        //gets a random point for goop to spawn
        private void SetGoopPlacement()
        {
            Random rand = new Random();
            int goopIndex = rand.Next(0 ,spawnPoints.Count);
            goop.Placement = spawnPoints[goopIndex];            
        }
        //gets a random color
        private Color GetrandColor()
        {
            Random rand = new Random();
            int ColorSelect = rand.Next(0, ColorList.Count);
            Color Color = ColorList[ColorSelect];
            return Color;
        }
        //Spawn points for the goop
        private void SpawnPointsAdd()
        {
            spawnPoints.Add(point1);
            spawnPoints.Add(point2);
            spawnPoints.Add(point3);
            spawnPoints.Add(point4);
            spawnPoints.Add(point5);
            spawnPoints.Add(point6);
            spawnPoints.Add(point7);
            spawnPoints.Add(point8);
            spawnPoints.Add(point9);
            spawnPoints.Add(point10);
        }
        //Add a new enemy to area.
        private void AddNewEnemy()
        {
            if (Player.getScore() % 5 == 0 && Player.getScore() > 0)
            {
                Enemies.Add(CreateCrawlerinList("Crawler"));
                
            }
        }
    }
}
