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

        Polygons Twall, Twall2, TFloor, TFloor2;
        Character Player;
        Texture2D Background;
        bool Wleft;
        bool Wright;
        bool jump;
        bool attack;

        public override void Initialize()
        {
            Wleft = false;
            Wright = false;
            jump = false;
            attack = false;
        }

        public override void LoadContent(SpriteBatch spriteBatchMain)
        {
            MakeShapes();
            base.LoadContent(spriteBatch);

            spriteBatch = spriteBatchMain;
            Twall.LoadContent("twall1", "TWall");
            Twall2.LoadContent("twall2", "TWall");
            TFloor.LoadContent("tfloor1", "TFloor");
            TFloor2.LoadContent("tfloor2", "TFloor");

            Player.LoadCharacter("TutorialSpawn");

            Background = Main.GameContent.Load<Texture2D>("Sprites/TutorialSprites/TBack");
        }

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

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            Player.Gravity();
            base.Update(camera, graphicsManager);
            try
            {
                bool PlayerCollision = Collision(Player, TFloor);
                bool PlayerCollision1 = Collision(Player, TFloor2);
                bool PlayerCollision2 = Collision(Player, Twall);
                bool PlayerCollision3 = Collision(Player, Twall2);

                if (PlayerCollision)
                {
                    Player.Rebuff(TFloor);
                    Player.FloorReset();
                }
                if (PlayerCollision1)
                {
                    Player.Rebuff(TFloor2);
                    Player.FloorReset();
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
            Player.Jump(Key);
        }

        private void MakeShapes()
        {
            //Create the Polygon 
            RetrieveShapes(1);

            Twall = CreateShape("twall");
            Twall2 = CreateShape("twall");
            TFloor = CreateShape("tfloor");
            TFloor2 = CreateShape("tfloor");
            Player = CreateChar("janitor");
        }    

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
                // Press Space to continue
                if (!jump)
                {
                    spriteBatch.DrawString(font, "Press Space to Jump", new Vector2(300, 200), Color.Red);
                    if (Key.IsKeyDown(Keys.Space))
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
