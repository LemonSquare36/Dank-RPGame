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
    class Character : Entity 
    {
        public Character(List<Vector2> numbers):base(numbers) { }

        //meh
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

        public void LoadInventory(string CharacterName)
        {
            StreamReader CharacterReader = new StreamReader("CharacterInventory.txt");
            string line;

            while (true)
            {
                line = CharacterReader.ReadLine();
                inventory.Add(line);
                Debug.WriteLine(inventory);
            }
        }

        public void SaveInventory(string ChracterName)
        {
            StreamWriter CharacterWriter = new StreamWriter("CharacterInventory.txt");

            while (true)
            {
                CharacterWriter.WriteLine(inventory);
            }
        }

        public void LoadStats(string CharacterName)
        {
            StreamReader CharacterReader = new StreamReader("CharacterStats.txt");
            string line;

            while (true)
            {
                line = CharacterReader.ReadLine();
                //characterStats.Add(line);
                Debug.WriteLine(inventory);
            }
        }


        public void SaveStats(string ChracterName)
        {
            StreamWriter CharacterWriter = new StreamWriter("CharacterStats.txt");

            while (true)
            {
                CharacterWriter.WriteLine(inventory);
            }
        }

        public void MoveChar(KeyboardState Key)
        {
            var cMovement = Vector2.Zero;
            Vector2 cPlacement = new Vector2();

            if (Key.IsKeyDown(Keys.Up))
            {
                cMovement = new Vector2(cMovement.X + 1f, cMovement.Y);
            }
            if (Key.IsKeyDown(Keys.Down))
            {
                cMovement = new Vector2(cMovement.X, cMovement.Y + 1f);
            }
            if (Key.IsKeyDown(Keys.Left))
            {
                cMovement = new Vector2(cMovement.X - 1f, cMovement.Y);
            }
            if (Key.IsKeyDown(Keys.Right))
            {
                cMovement = new Vector2(cMovement.X, cMovement.Y - 1f);
            }

            var cOldPosition = cPlacement;
            cPlacement += cMovement;
        }
    }
}