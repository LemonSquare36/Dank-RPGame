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
    public class Menu : Button
    {

        public void Initialize()
        {
            int x = Window.ClientBounds.Width / 2 - buttonWidth / 2;
            int y = Window.ClientBounds.Height / 2 -
                numberOfButtons / 2 * buttonHeight -
                (numberOfButtons % 2) * buttonHeight / 2;
            for (int i = 0; i < numberOfButtons; i++)
            {
                button_state[i] = BState.UP;
                button_color[i] = Color.White;
                button_timer[i] = 0.0;
                button_rectangle[i] = new Rectangle(x, y, buttonWidth, buttonHeight);
                y += buttonHeight;
            }
            IsMouseVisible = true;
            background_color = Color.CornflowerBlue;
        }
        protected override void LoadContent()
        {
            button_texture[Play] =
                Content.Load<Texture2D>(@"buttons/Play");
            button_texture[Play_Hover] =
                Content.Load<Texture2D>(@"buttons/Ply_Hover");
        }
    }
}
