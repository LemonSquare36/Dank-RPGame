using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGame
{
    class Habitation : Areas
    {
        Polygons FloorbytheDoor, FloorHump, LongFloor1, LongFloor2, Mramp;
        Texture2D StairsDoor, JanitorDoor;
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

            ListAdd();
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            Player.Gravity();
            camera.Follow(Player.getRealPos(0));
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
            LongFloor1.Draw(spriteBatch);
            LongFloor2.Draw(spriteBatch);
            Mramp.Draw(spriteBatch);

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
