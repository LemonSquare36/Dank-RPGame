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

        Polygons Twall, Twall2, TFloor, TFloor2;
        Character Player;
        Texture2D Background;
        bool Wleft;
        bool Wright;
        bool jump;
        bool attack;
        //For starting the Tutorial
        public override void Initialize()
        {
            Wleft = false;
            Wright = false;
            jump = false;
            attack = false;
        }
        //Load
        public override void LoadContent(SpriteBatch spriteBatchMain)
        {
            MakeShapes();
            base.LoadContent(spriteBatch);

            spriteBatch = spriteBatchMain;
            Twall.LoadContent("twall1", "TWall", true);
            Twall2.LoadContent("twall2", "TWall", true);
            TFloor.LoadContent("tfloor1", "TFloor", false);
            TFloor2.LoadContent("tfloor2", "TFloor", false);

            Player.LoadCharacter("TutorialSpawn");
            Player.SpriteMove(1, 3);

            Background = Main.GameContent.Load<Texture2D>("Sprites/TutorialSprites/TBack");
        }
        //Draw
        public override void Draw()
        {

            Player.RealPos();
            Twall.RealPos();
            Twall2.RealPos();
            TFloor.RealPos();
            TFloor2.RealPos();

            spriteBatch.Draw(Background, new Vector2(50, 40), null, null);
            Twall.Draw(spriteBatch);
            Twall2.Draw(spriteBatch);
            TFloor.Draw(spriteBatch);
            TFloor2.Draw(spriteBatch);

            Player.Draw(spriteBatch);

            try
            {
                TutorialCommands();
            }
            catch (Exception ex) { ErrorHandling(ex.Message, GetType().Name, ex); }
        }
        //Updates the area/Game
        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager, GraphicsDevice graphicsDevice)
        {
            Player.Gravity();
            getKey();
            CameraMove(camera, graphicsManager);
            try
            {
                bool PlayerCollision = Collision(Player, TFloor);
                bool PlayerCollision1 = Collision(Player, TFloor2);
                bool PlayerCollision2 = Collision(Player, Twall);
                bool PlayerCollision3 = Collision(Player, Twall2);

                if (PlayerCollision)
                {
                    Player.Rebuff(TFloor);
                    Player.FloorReset(TFloor.getisWall());
                }
                if (PlayerCollision1)
                {
                    Player.Rebuff(TFloor2);
                    Player.FloorReset(TFloor.getisWall());
                }
                if (PlayerCollision2)
                {
                    Player.Rebuff(Twall);
                }
                if (PlayerCollision3)
                {
                    Player.Rebuff(Twall2);
                }                
            }
            catch (Exception ex) { ErrorHandling(ex.Message, GetType().Name, ex); }
            Player.MoveChar(Key);
            Player.Jump();

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
            Player = CreateChar("janitor");
        }    
        //Runs the Tutrial
        private void TutorialCommands()
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
                    // Press F to continue
                    if (!attack)
                    {
                        spriteBatch.DrawString(font, "Press F to Attack", new Vector2(300, 200), Color.Red);
                        if (Key.IsKeyDown(Keys.F))
                        {
                            attack = true;
                        }
                    }
                }
            }
        }
    }
}
