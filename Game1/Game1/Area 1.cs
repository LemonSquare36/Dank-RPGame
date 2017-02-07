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

//The Test area fot the game. Never used in gameplay but stll kept becuase it was once used.
namespace RPGame
{
    class Area_1 : Areas
    {

        public Area_1(bool isArea) : base(isArea)
        {
            isarea = isArea;
        }

        Timer levelTimer = new Timer();

        Polygons Triangle1;
        Polygons Triangle2;
        Polygons Triangle3;
        Polygons Pentagon1;
        Polygons Pentagon2;
        Polygons Floor1;
        Polygons janitor;

        //Load
        public override void LoadContent(SpriteBatch spriteBatchMain)
        {
            MakeShapes();
            spriteBatch = spriteBatchMain;
            Triangle1.LoadContent("triangle1", "GreenTriangle", false);
            Triangle2.LoadContent("triangle2", "GreenTriangle", false);
            Triangle3.LoadContent("triangle3", "GreenTriangle", false);
            Pentagon1.LoadContent("pentagon1", "GreyPentagon", false);
            Pentagon2.LoadContent("pentagon2", "GreyPentagon", false);
            janitor.LoadContent("janitor", "janitor", false);
            Floor1.LoadContent("floor1", "Floor", false);
        }
        //Draw
        public override void Draw()
        {
            Triangle1.RealPos();
            Triangle2.RealPos();
            Pentagon1.RealPos();
            Pentagon2.RealPos();
            Floor1.RealPos();
            janitor.RealPos();
            Triangle1.Draw(spriteBatch);
            //Triangle2.Draw(spriteBatch);
            //Triangle3.Draw(spriteBatch);
            Pentagon1.Draw(spriteBatch);
            //Pentagon2.Draw(spriteBatch);
            Floor1.Draw(spriteBatch);
            janitor.Draw(spriteBatch);
        }
        //Update
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            getKey();
            CameraMove(camera, graphicsManager);

            bool Collide = Collision(Triangle1, janitor);
            bool Collide2 = Collision(Triangle1, Pentagon1);

            if (Collide2)
            {
                Triangle1.Rebuff(Pentagon1);
            }
            if (Collide)
            {
                Triangle1.Rebuff(janitor);
            }
            Triangle1.MoveShape(Key);
        }

        //Make YourShapes Here
        private void MakeShapes()
        {
            //Create the Polygon
            RetrieveShapes();

            Triangle1 = CreateShape("triangle1");
            Triangle2 = CreateShape("triangle2");
            Triangle3 = CreateShape("triangle3");
            Pentagon1 = CreateShape("pentagon1");
            Pentagon2 = CreateShape("pentagon2");
            Floor1 = CreateShape("floor1");
            janitor = CreateChar("janitor");


        }
    }
}
