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

// Loads the Content for the various GameStates and allows the switching between GameStates
namespace RPGame
{
    class GameState
    {
        private Hashtable shapeVerts = new Hashtable();

        KeyboardState mPreviousKeyboardState;

        Play GamePlaying;
        Menu menu;

        //The Game States get defined here
        public enum GameStates { Menu, Playing }

        private GameStates gameState;
        event EventHandler GameStateChanged;

        public GameStates Gamestate
        {
            get { return gameState; }
            set
            {
                gameState = value;
                OnGameStateChanged();
            }
        }

        //Loads the Content for The GameStates
        public void LoadContent()
        {
            switch (gameState)
            {
                case GameStates.Playing:
                    MakeShapes();
                    break;

                case GameStates.Menu:
                    
                    break;
            }

        }


        //The update function for changing the GameStates and for using functions of the current GameStates
        public void Update(GameTime gameTime)
            {
            KeyboardState CurrentKeyBoardState = Keyboard.GetState();
            mPreviousKeyboardState = CurrentKeyBoardState;
            ChangeGameState(CurrentKeyBoardState);

            switch (gameState)
            {
                case GameStates.Playing:

                    break;

                case GameStates.Menu:

                    break;
            }
        }
        //Draws the images and textures we use
        public void Draw(SpriteBatch spriteBatch)
        {

            switch (gameState)
            {
                case GameStates.Playing:
                    break;

                case GameStates.Menu:
                    break;
            }

        }
        //Change the GameState with a button click
        private void ChangeGameState(KeyboardState CurrentKeyBoardState)
        {
            if (CurrentKeyBoardState.IsKeyDown(Keys.Z) == true)
            {
                if (gameState == GameStates.Menu)
                {
                    gameState = GameStates.Playing;
                    LoadContent();
                }

                else if (gameState == GameStates.Playing)
                {
                    gameState = GameStates.Menu;
                }
            }
        }
        //Prevents errors in GameStates
    private void OnGameStateChanged()
        {
            GameStateChanged?.Invoke(this, EventArgs.Empty);
        }

        //Make YourShapes Here
        private void MakeShapes()
        {
            RetrieveShapes();
            Polygons Triangle1 = CreateShape("triangle1");
            for (int i = 0; i < Triangle1.getNumVerticies(); i++) 
            {
                Vector2 myVector2 = Triangle1.getVerticies(i);
                Debug.WriteLine("{0} , {1}", myVector2.X, myVector2.Y);
            }
        }

        //Creates the Shapes of Polygon Class
        private Polygons CreateShape(string shapeName)
        {
            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            Polygons myPolygon = new Polygons(NewList);
            return myPolygon;
        }

        private void RetrieveShapes()
        {
            StreamReader shapeConfig = new StreamReader("shapeList.txt");
            string line;
            string key = null;
            List<Vector2> verticies = new List<Vector2>();
            while ((line = shapeConfig.ReadLine()) != null)
            {
                try
                {
                    string[] VertCords = (line.Split(','));
                    float xVert = (float)Convert.ToDouble(VertCords[0]);
                    float yVert = (float)Convert.ToDouble(VertCords[1]);
                    Vector2 myVector2 = new Vector2(xVert, yVert);
                    verticies.Add(myVector2);

                }
                catch
                {
                    if (key != null)
                    {
                        shapeVerts[key] = verticies;
                        verticies = new List<Vector2>();
                    }
                    key = line;
                }
            }
        }


    }
}
