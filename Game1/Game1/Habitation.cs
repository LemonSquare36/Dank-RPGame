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

        public override void Initialize()
        {
            
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
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            Player.Gravity();
            base.Update(camera, graphicsManager);

            Player.MoveChar(Key);
            Player.Jump(Key);
        }

        public override void Draw()
        {
            
        }
        private void MakeShapes()
        {
            RetrieveShapes(1);

            FloorbytheDoor = CreateShape("floorbythedoor");
            FloorHump = CreateShape("floorhump");
            LongFloor1 = CreateShape("longfloor");
            LongFloor2 = CreateShape("longfloor");
            Mramp = CreateShape("mramp");

            Player = CreateChar("player1");
        }
    }
}
