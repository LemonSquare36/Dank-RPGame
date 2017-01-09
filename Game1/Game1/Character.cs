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
using Microsoft.Xna.Framework.Storage;

namespace RPGame
{
    public class Character : Entity
    {
        public Character(List<Vector2> numbers) : base(numbers) { }

        private class Keycard{ }
        private class Mop { }
        private class KeyChain { }
        private class Plungers { }

        public struct Save
        {
            public string Name;
            public Vector2 position;
            public int Level;
            public List<string> Inventory;
        }

        enum SaveState
        {
            NotSaving,
            ReadyStorageDevice,
            SelectingStorageDevice,

            ReadyToOpenStorageDevice,
            OpenStorageDevice,
            ReadyToSave
        }

        KeyboardState Key;

        public int health = 20;
        public int ability = 10;
        public int attack = 10;
        public int level = 1;

        SpriteBatch spriteBatch;


        StorageDevice storageDevice;
        SaveState save = SaveState.NotSaving;
        IAsyncResult aSyncResult;
        PlayerIndex playerIndex = PlayerIndex.One;
        StorageContainer storageContainer;
        string fileName = "Janitor.sav";


        Save newSave = new Save()
        {
            position = new Vector2(400, 100),
            Level = 1,
            Inventory = new List<string>()
        {

        }
    };

        private void UpdateSave()
        {
            switch (save)
            {
                //case SaveState.ReadyToOpenStorageDevice:

                    //if (!Guid.IsVisible)
                    {

                    }
            }
        }
      

        protected Random rand = new Random();

        public void LevelUp()
        {
            spriteBatch.DrawString(font, "Congratulations! You have leveled up! Press X to upgrade attack. Press C to upgrade health. Press V to upgrade ability", new Vector2(200, 200), Color.Red);

            if (Key.IsKeyDown(Keys.X))
            {
                attack += 5;
            }
            else if (Key.IsKeyDown(Keys.C))
            {
                health += 10;
            }
            else if (Key.IsKeyDown(Keys.V))
            {
                ability += 5;
            }
        }

        public void Load()
        {

        }

        public void MoveChar(KeyboardState Key)
        {
            Movement = Vector2.Zero;

            if (Key.IsKeyDown(Keys.D))
            {
                Movement = new Vector2(Movement.X + 2f, Movement.Y);
            }
            if (Key.IsKeyDown(Keys.A))
            {
                Movement = new Vector2(Movement.X - 2f, Movement.Y);
            }
            OldPosition = Placement;
            Placement += Movement;
        }
    }
}