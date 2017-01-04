using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGame
{
    class Habitation : Areas
    {


        public override void Initialize()
        {
            
        }

        public override void LoadContent(SpriteBatch spriteBatchMain)
        {
            MakeShapes();
            spriteBatch = spriteBatchMain;
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            
        }

        public override void Draw()
        {
            
        }
        private void MakeShapes()
        {

        }
    }
}
