using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGame
{
    class CrawlerAlien : Entity
    {
       
        public CrawlerAlien(List<Vector2> numbers) : base(numbers) { }
        //Loads Placement and texture
        public void Load(int Xposition, int Yposition)
        {
            Placement = new Vector2(Xposition, Yposition);

            texture = Main.GameContent.Load<Texture2D>("Enemies/CrawlerAlienSheet");
        }
        //Updates the texture when it walks
        public void UpdateTexture()
        {

            if (Movement.X > 0)
            {
                texture = Main.GameContent.Load<Texture2D>("Enemies/CrawlerAlienSheet");
            }
            if (Movement.X < 0)
            {
                texture = Main.GameContent.Load<Texture2D>("Enemies/CrawlerAlienSheet2");
            }
        }
    }
}
