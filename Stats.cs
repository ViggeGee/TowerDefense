using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    static public class Stats
    {
        public static SpriteFont spriteFont = Assets.spriteFont;
        
        public static int currency = 200;
        public static Vector2 currencyPos = new Vector2(700, 0);
        
        public static int lives = 5;
        public static Vector2 livesPos = new Vector2(50, 0);

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "Currency: " + currency,currencyPos , Color.Black);
            spriteBatch.DrawString(spriteFont, "Lives: " + lives, livesPos, Color.Black);
        }

    }
}
