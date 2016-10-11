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
        List<string> inventory = new List<string> ();

        public int health = 20;
        public int ability = 10;
        public int attack = 10;
        public int level = 1;
        public int xp = 0;

        protected Random rand = new Random();

        public void LevelUp()
        {
            level = level + 1;
            health = health + rand.Next(1, 7);
            ability = ability + rand.Next(1, 7);
            attack = attack + rand.Next(1, 7);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Initialize()
        {
            inventory.Add("THE MOP");
            inventory.Add("Spray bottle 1");
            inventory.Add("Spray bottle 2");
            inventory.Add("Janitor's armour");
            inventory.Add("CAUTION: WET FLOOR sign x5");
            inventory.Add("SUPER SOAP x5");
        }
    }
}