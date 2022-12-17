using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    static public class Assets
    {
        public static Texture2D ball, square, backgroundTexture;
        public static SpriteFont spriteFont;
        public static void LoadTextures(ContentManager Content)
        {
            spriteFont = Content.Load<SpriteFont>("spriteFont");
            square = Content.Load<Texture2D>("Square");
            ball = Content.Load<Texture2D>("ball");
            backgroundTexture = Content.Load<Texture2D>("transparentSquareBackground");
        }
    }
}
