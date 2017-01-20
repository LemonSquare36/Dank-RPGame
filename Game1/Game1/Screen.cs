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
    class Screen : Global
    {
        protected SpriteBatch spriteBatch;
        protected string nextScreen;
        protected KeyboardState Key;

        public string getNextScreen()
        {
            return nextScreen;
        }

        public virtual void Initialize()
        {

        }
        public virtual void LoadContent(SpriteBatch spriteBatchmain)
        {

        }
        public virtual void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {

        }
        public virtual void Draw()
        {

        }
        public void CameraMove(Camera camera, GraphicsDeviceManager graphicsManager)
        {         
            camera.Move(Key);
            camera.ChangeScreenSize(Key, graphicsManager);
        }
        public void getKey()
        {
            Key = Keyboard.GetState();
        }

        public virtual void ButtonReset()
        {

        }
    }

}
