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
    class Character
    {
        KeyboardState charKeyBoardState;

        protected int health = 20;
        protected int ability = 10;
        protected int attack = 10;
        protected int level = 1;
        protected int xp = 0;

        protected Random rand;

        private void LevelUp()
        {
            level++;
            health = health + rand.Next(1, 7);
            ability = ability + rand.Next(1, 7);
            attack = attack + rand.Next(1, 7);
        }

        public void Update(GameTime gameTime)
        {
            if (charKeyBoardState.IsKeyDown(Keys.L))
            {
                LevelUp();
                Debug.WriteLine(health);
                Debug.WriteLine(ability);
                Debug.WriteLine(attack);
                Debug.WriteLine(level);
            }
        }
    }
}
