using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;



namespace RPGame
{

    class Camera : Global
    {

        public float Zoom { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        bool Size = true;
        //Constructor
        public Camera()
        {
            Zoom = 1f;
            Position = Vector2.Zero;
            Rotation = 0;
            Origin = Vector2.Zero;
        }
        //MOves the Camera with arrow keys
        public void Move(KeyboardState CurrentKeyBoardState)
        {
            if (CurrentKeyBoardState.IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X - 2, Position.Y);
            }
            if (CurrentKeyBoardState.IsKeyDown(Keys.Left))
            {
                Position = new Vector2(Position.X + 2, Position.Y);
            }
            if (CurrentKeyBoardState.IsKeyDown(Keys.Up))
            {
                Position = new Vector2(Position.X, Position.Y + 2);
            }
            if (CurrentKeyBoardState.IsKeyDown(Keys.Down))
            {
                Position = new Vector2(Position.X, Position.Y - 2);
            }
        }
        //Cerates what the camera actaully sees. This inculdes scale,zoom, rotaion. Done in a matrix
        public Matrix Transform(GraphicsDevice graphicsDevice)
        {
            //game is scaled to these amounts yo
            var scaleX = (float)graphicsDevice.Viewport.Width / 800;
            var scaleY = (float)graphicsDevice.Viewport.Height / 480;

            var translationMatrix = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            var rotationMatrix = Matrix.CreateRotationZ(Rotation);
            var scaleMatrix = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));
            var originMatrix = Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0));

            return translationMatrix * rotationMatrix * scaleMatrix * originMatrix;
        }
        //Toggle full screen or not
        public void ChangeScreenSize(GraphicsDeviceManager graphics)
        {
            //Super nice funtion :D
            graphics.ToggleFullScreen();

            graphics.ApplyChanges();

        }
        //Camera Follows the position provide (inteneded for character)
        public void Follow(Vector2 characterPosition)
        {
            Position = new Vector2(characterPosition.X + 400f, characterPosition.Y + 240f);
        }
        public void posReset()
        {
            Position = Vector2.Zero;
        }
    }
}
