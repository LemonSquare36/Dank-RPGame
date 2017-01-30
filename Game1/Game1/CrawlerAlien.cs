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
using System.IO;

namespace RPGame
{
    class CrawlerAlien : Entity
    {
        public CrawlerAlien(List<Vector2> numbers) : base(numbers) { }

        double ElapsedTime = 1.5;
        double StartTime = 0;

        public void Attack(Character ch, GameTime gameTime)
        {
            StartTime += gameTime.ElapsedGameTime.Seconds;

            if (StartTime >= ElapsedTime)
            ch.health -= 2;
        }
    }
}
