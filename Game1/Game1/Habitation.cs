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
        Polygons FloorbytheDoor, FloorHump, LongFloor1, LongFloor2, Mramp;
        Texture2D StairsDoor, JanitorDoor, CeilingbytheDoor, CeilingHump, CeilingFloor1, CeilingFloor2, CeilingMramp;
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

            FloorbytheDoor.LoadContent("floorbythedoor", "floorbythedoor");
            FloorHump.LoadContent("floorhump", "floorhump");
            LongFloor1.LoadContent("longfloor1", "longfloor");
            LongFloor2.LoadContent("longfloor2", "longfloor");
            Mramp.LoadContent("mramp", "mramp");

            Player.LoadCharacter("HabitationJanitorDoor");

            CeilingbytheDoor = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/FloorByTheDoor");
            CeilingHump = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/FloorHump");
            CeilingFloor1 = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/LongFloor");
            CeilingFloor2 = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/LongFloor");
            CeilingMramp = Main.GameContent.Load<Texture2D>("Sprites/Habitation Sprites/Mramp");

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

            spriteBatch.Draw(CeilingbytheDoor, new Vector2(330, -60), null);
            spriteBatch.Draw(CeilingHump, new Vector2(-265, -150), null);
            spriteBatch.Draw(CeilingFloor1, new Vector2(-1200, -60), null);
            spriteBatch.Draw(CeilingFloor2, new Vector2(-2170, 0), null);
            spriteBatch.Draw(CeilingMramp, new Vector2(-1480, -60), null);

            Player.Draw(spriteBatch);
        }
        private void MakeShapes()
        {
            RetrieveShapes(1);

            FloorbytheDoor = CreateShape("floorbythedoor");
            FloorHump = CreateShape("floorhump");
            LongFloor1 = CreateShape("longfloor");
            LongFloor2 = CreateShape("longfloor");
            Mramp = CreateShape("mramp");

            Player = CreateChar("janitor");
        }
        private void ListAdd()
        {
            PolyList.Add(FloorbytheDoor);
            PolyList.Add(FloorHump);
            PolyList.Add(LongFloor1);
            PolyList.Add(LongFloor2);
            PolyList.Add(Mramp);
        }
    }
}
