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
            TFloor.LoadContent("tfloor2", "TFloor");
            Background = Main.GameContent.Load<Texture2D>("Sprites/TutorialSprites/TBack");
        }

        public override void Draw()
        {
            spriteBatch.Draw(Background, new Vector2(50, 40), null, null);
            Twall.Draw(spriteBatch);
            Twall2.Draw(spriteBatch);
            TFloor.Draw(spriteBatch);
            TFloor2.Draw(spriteBatch);

            TutorialCommands();
        }

        public override void Update()
        {
            base.Update();
        }


        private void MakeShapes()
        {
            //Create the Polygon
            RetrieveShapes();

            Twall = CreateShape("twall");
            Twall2 = CreateShape("twall");
            TFloor = CreateShape("tfloor");
            TFloor2 = CreateShape("tfloor");
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
