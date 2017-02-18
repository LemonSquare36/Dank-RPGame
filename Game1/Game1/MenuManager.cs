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
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace RPGame
{
    class MenuManager : Screen
    {
        Sound snd = new Sound();
        protected MouseState mouse;

        public override void Initialize()
        {
            snd.LoadSound("Sound/ButtonClick.wav");
        }

        //ButtonCLicked leads Here
        protected void ButtonClicked(object sender, EventArgs e)
        {
            snd.PlaySound();

            //Sets next screen to button name and calls the event.
            nextScreen = ((Button)sender).bName;
            OnScreenChanged();
        }
        //Event for Changing the Screen
        public event EventHandler ChangeScreen;
        public void OnScreenChanged()
        {
            ChangeScreen?.Invoke(this, EventArgs.Empty);
        }
        //Holds the Function
        public virtual void ButtonReset()
        {

        }
        protected Vector2 MousePos()
        {
            Vector2 worldPosition = Vector2.Zero;
            mouse = Mouse.GetState();
            try
            {
                worldPosition.X = mouse.X / (float)(Main.gameWindow.ClientBounds.Width / 800.0);
                worldPosition.Y = mouse.Y / (float)(Main.gameWindow.ClientBounds.Height / 480.0);
            }
            catch { }
            return worldPosition;
        }

    }
}
