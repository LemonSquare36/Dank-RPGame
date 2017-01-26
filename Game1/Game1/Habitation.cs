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
        List<Polygons> PolyList;
        public override void Initialize()
        {
            PolyList = new List<Polygons>();
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
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            Player.Gravity();
            camera.Follow(-Player.getRealPos(0));
            Debug.WriteLine(Player.getRealPos(0));
            getKey();
            bool PlayerCollision;

            foreach (Polygons poly in PolyList)
            {
                PlayerCollision = Collision(Player, poly);
                if (PlayerCollision)
                {
                    Player.Rebuff(poly);
                    Player.FloorReset();
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
    }
}
