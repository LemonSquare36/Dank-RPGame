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
      
        protected MouseState mouse;


        protected void ButtonClicked(object sender, EventArgs e)
        {
            //Sets next screen to button name and calls the event.
            nextScreen = ((Button)sender).bName;
            OnScreenChanged();
        }

        public event EventHandler ChangeScreen;
        public void OnScreenChanged()
        {
            ChangeScreen?.Invoke(this, EventArgs.Empty);
        }

        public virtual void ButtonReset()
        {

        }

    }
}
