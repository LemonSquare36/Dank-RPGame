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
    public class Button : Polygons
    {
        public Button(List<Vector2> numbers):base(numbers) { }

        int buttonX;
        int buttonY;
        Polygons Play;

        public int ButtonX
        {
            get
            {
                return buttonX;
            }
        }
        public int ButtonY
        {
            get
            {
                return buttonY;
            }
        }
        public Button(string name, Texture2d texture, int buttonX, int buttonY)
        {
            this.Name = name;
            this.Texture = texture;
            this.buttonX = buttonX;
            this.buttonY = buttonY;
        }
        public bool enterButton()
        {
            if (MouseInput.GetMouseX() < buttonX + Texture.width && MouseInput.getMouseX() > buttonX && MouseInput.getMouseY() < buttonY + Texture.Height && MouseInput.getMouseY() > buttonY)
            {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            if (enterButton() && MouseInput.LastMouseState.LeftButton == ButtonState.Released && MouseInput.MouseState.LeftButton == ButtonState.Pressed)
            {
                ScreenManager.add("Play", new Vector2(0, 0));
            }
        }

        public void Draw()
        {
            Screens.ScreenManager.Sprites.Draw(Texture, new Rectangle((int)ButtonX, (int)ButtonY, Texture.Width, Texture.Height), Color.White);
        }
        SpriteBatch spriteBatch;
        public void LoadContent(SpriteBatch spriteBatchMain)
        {
            MakeShapes();
            spriteBatch = spriteBatchMain;
            Play.LoadContent("Play");
        }
        private void MakeShapes()
        {
            RetrieveShapes();
            Play1 = CreateShape("Play");

        }
    }
}
