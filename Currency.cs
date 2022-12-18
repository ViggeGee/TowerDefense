using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    static public class Currency
    {
        public static SpriteFont spriteFont = Assets.spriteFont;
        public static int currency = 200;
        public static Vector2 pos = new Vector2(700, 0);

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, "Currency: " + currency,pos , Color.Black);
        }

    }
}
