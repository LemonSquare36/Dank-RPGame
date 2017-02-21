using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace RPGame
{
    class MenuManager : Screen
    {
        Sound snd = new Sound();
        Sound PlaySnd = new Sound();
        protected MouseState mouse;

        public override void Initialize()
        {
            snd.LoadSound("Sound/ButtonClick.wav");
            PlaySnd.LoadSound("Sound/PlayButtonClick.wav");
        }

        //ButtonCLicked leads Here
        protected void ButtonClicked(object sender, EventArgs e)
        {
            if (((Button)sender).bName == "Play" || ((Button)sender).bName == "Tutorial")
            {
                PlaySnd.PlaySound();
            }
            else
            {
                snd.PlaySound();
            }

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
