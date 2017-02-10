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
        protected GameTime time;

        //Holds the isarea variable which determines if its and area and needs loadscreens or not
        protected bool isarea;
        public bool getisarea()
        {
            return isarea;
        }
        //Gets the next screen
        public string getNextScreen()
        {
            return nextScreen;
        }
        //Holds Initialize
        public virtual void Initialize()
        {

        }
        //Holds LoadContent and the font if called
        public virtual void LoadContent(SpriteBatch spriteBatchmain)
        {
            font = Main.GameContent.Load<SpriteFont>("myFont");
        }
        //Holds Update
        public virtual void Update(Camera camera, GraphicsDeviceManager graphicsManager,GraphicsDevice graphicsDevice)
        {

        }
        //Holds Draw
        public virtual void Draw()
        {

        }
        //MOve the camera with arrow keys
        public void CameraMove(Camera camera, GraphicsDeviceManager graphicsManager)
        {         
            camera.Move(Key);
        }
        //Used for keybaord state
        public void getKey()
        {
            Key = Keyboard.GetState();
        }
        //Holds ButtonReset
        public virtual void ButtonReset()
        {

        }

        //Gets the current GameTime (Used for spritesheets)
        public void getGameTimePrime(GameTime gameTime)
        {
            time = gameTime;
        }
    }

}