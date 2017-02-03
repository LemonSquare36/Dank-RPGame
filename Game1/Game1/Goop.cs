﻿using System;
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
using System.IO;

namespace RPGame
{
    class Goop : Polygons

    {
        public Goop(List<Vector2> numbers) : base(numbers) { }

        public void LoadGoop()
        {
            texture = Main.GameContent.Load<Texture2D>("Sprites/TheBlob");
        }
    }
}
