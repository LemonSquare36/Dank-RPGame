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
    class Button
    {
        private enum ButtonType { Rectangle };
        private float diameter { get; set; }
        private float windowWidth { get; set; }
        private ButtonType type { get; set; }
        private Texture2D button0 { get; set; }
        private event EventHandler buttonPressed;

        public event EventHandler ButtonPressed
        {
            get
            {
                switch (type)
                {
                    case ButtonType.Rectangle:
                        return new Vector2(Collision.Location.X, Collision.Location.Y);
                    default:
                        return Vector2.Zero;
                }
            }
        }

        public Texture2D Texture
        {
            get { return button0; }
        }

        private int bNum;

        public int ButtonNum
        {
            get { return bNum; }
        }

        private MouseState mouseState;

        /// <summary>
        /// Backing store for Collision.
        /// </summary>
        private Rectangle collision;

        /// <summary>
        /// Rectangle structure.
        /// </summary>
        public Rectangle Collison
        {
            get { return collision; }
            set { collision = value; }
        }
    }
}
