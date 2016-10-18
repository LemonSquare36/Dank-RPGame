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

        public void LoadSave(string CharacterName)
        {
            StreamReader CharacterReader = new StreamReader("CharacterSave.txt");
            string line;

            while (true)
            {
                line = CharacterReader.ReadLine();
                inventory.Add(line);
                Debug.WriteLine(inventory);
            }
        }

        public void Save(string ChracterName)
        {
            StreamWriter CharacterWriter = new StreamWriter("CharacterSave.txt");

            while (true)
            {
                CharacterWriter.WriteLine(inventory);
            }
        }
    }
}