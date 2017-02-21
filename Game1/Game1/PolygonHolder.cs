using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RPGame
{
    public class PolygonHolder : Global
    {
        //Lets polygon have a draw with override so it can be used and overriden
        public virtual void Draw(SpriteBatch spritebatch)
        {

        }
        //Find the cross product used in the Physics section of my collision code
        protected bool CrossProduct(Vector2 A, Vector2 B, Vector2 C)
        {
            float crossProduct;
            float dotProduct;
            double baSquared;

            crossProduct = (C.Y - A.Y) * (B.X - A.X) - (C.X - A.X) * (B.Y - A.Y);
            if (Math.Abs(crossProduct) > 150)
                return false;

            dotProduct = ((C.X - A.X) * (B.X - A.X)) + ((C.Y - A.Y) * (B.Y - A.Y));
            if (dotProduct < -150)
                return false;

            baSquared = Math.Pow((B.X - A.X), 2) + Math.Pow((B.Y - A.Y), 2);
            if (dotProduct > (float)baSquared + 150)
                return false;

            return true;
        }
    }
}
