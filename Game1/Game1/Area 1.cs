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
    class Area_1 : Areas
    {

        Timer levelTimer = new Timer();
        bool elapsed = true;

        Buttons TestButton = new Buttons();

        Polygons Triangle1;
        Polygons Triangle2;
        Polygons Triangle3;
        Polygons Pentagon1;
        Polygons Pentagon2;
        Polygons Floor1;
        Polygons player1;


        public override void LoadContent(SpriteBatch spriteBatchMain)
        {
            MakeShapes();
            spriteBatch = spriteBatchMain;
            Triangle1.LoadContent("triangle1", "GreenTriangle");
            Triangle2.LoadContent("triangle2", "GreenTriangle");
            Triangle3.LoadContent("triangle3", "GreenTriangle");
            Pentagon1.LoadContent("pentagon1", "GreyPentagon");
            Pentagon2.LoadContent("pentagon2", "GreyPentagon");
            player1.LoadContent("player1", "player1");
            Floor1.LoadContent("floor1", "Floor");
        }

        public override void Draw()
        {
            Triangle1.RealPos();
            Triangle2.RealPos();
            Pentagon1.RealPos();
            Pentagon2.RealPos();
            Floor1.RealPos();
            player1.RealPos();
            Triangle1.Draw(spriteBatch);
            //Triangle2.Draw(spriteBatch);
            //Triangle3.Draw(spriteBatch);
            Pentagon1.Draw(spriteBatch);
            //Pentagon2.Draw(spriteBatch);
            Floor1.Draw(spriteBatch);
            player1.Draw(spriteBatch);
        }
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {

            base.Update(camera, graphicsManager);

            bool Collide = Collision(Triangle1, player1);
            bool Collide2 = Collision(Triangle1, Pentagon1);

            if (Collide2)
            {
                Triangle1.Rebuff(Pentagon1);
            }
            if (Collide)
            {
                Triangle1.Rebuff(player1);
            }
            Triangle1.MoveShape(Key);
        }

        //Make YourShapes Here
        private void MakeShapes()
        {
            //Create the Polygon
            RetrieveShapes(1);

            Triangle1 = CreateShape("triangle1");
            Triangle2 = CreateShape("triangle2");
            Triangle3 = CreateShape("triangle3");
            Pentagon1 = CreateShape("pentagon1");
            Pentagon2 = CreateShape("pentagon2");
            Floor1 = CreateShape("floor1");
            player1 = CreateChar("player1");


        }
    }
}
