using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace RPGame
{
    public class PolygonHolder : Global
    {
        //Lets polygon have a draw with override so it can be used and overriden
        public virtual void Draw(SpriteBatch spritebatch)
        {

        }
    }
}
