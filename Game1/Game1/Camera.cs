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


namespace RPGame
{
    class Camera
    {

        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        bool Size = true;

        public Camera()
        {
            Zoom = 1f;
            Position = Vector2.Zero;
            Rotation = 0;
            Origin = Vector2.Zero;
        }

        public void Move(KeyboardState CurrentKeyBoardState)
        {
            if (CurrentKeyBoardState.IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X - 1, Position.Y);
            }
            if (CurrentKeyBoardState.IsKeyDown(Keys.Left))
            {
                Position = new Vector2(Position.X + 1, Position.Y);
            }
            if (CurrentKeyBoardState.IsKeyDown(Keys.Up))
            {
                Position = new Vector2(Position.X, Position.Y + 1);
            }
            if (CurrentKeyBoardState.IsKeyDown(Keys.Down))
            {
                Position = new Vector2(Position.X, Position.Y - 1);
            }
        }

        public Matrix Transform()
        {
            var translationMatrix = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            var rotationMatrix = Matrix.CreateRotationZ(Rotation);
            var scaleMatrix = Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            var originMatrix = Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0));

            return translationMatrix * rotationMatrix * scaleMatrix * originMatrix;
        }
        public void ChangeScreenSize(KeyboardState CurrentKeyBoardState, GraphicsDeviceManager graphics, GraphicsDevice graphicsDevice)
        { 

            if (CurrentKeyBoardState.IsKeyDown(Keys.F1))
            {
                Size = !Size;
            }
            if (Size)
            {
                graphics.PreferredBackBufferHeight = 600;
                graphics.PreferredBackBufferWidth = 800;
            }
            else if (!Size)
            {
                graphics.PreferredBackBufferHeight = graphicsDevice.DisplayMode.Height;
                graphics.PreferredBackBufferWidth = graphicsDevice.DisplayMode.Width;
            }
            graphics.ApplyChanges();
        }
        /* public Vector3 GetScreenScale(GraphicsDevice graphicsDevice)
         {
             var scaleX = (float)graphicsDevice.Viewport.Width / (float)width;
             var scaleY = (float)graphicsDevice.Viewport.Height / (float)height;
             return new Vector3(scaleX, scaleY, 1.0f);
         }*/
    }
}
