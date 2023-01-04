using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
        public static List<Texture2D> textures = new List<Texture2D>();
        public static SoundEffect popAudio, shootAudio;
        public static void LoadTextures(ContentManager Content)
        {
            spriteFont = Content.Load<SpriteFont>("File");
            square = Content.Load<Texture2D>("Square");
            ball = Content.Load<Texture2D>("ball");
            backgroundTexture = Content.Load<Texture2D>("transparentSquareBackground");
            textures.Add(Content.Load<Texture2D>("circle"));
            textures.Add(Content.Load<Texture2D>("circle"));
            textures.Add(Content.Load<Texture2D>("circle"));
            popAudio = Content.Load<SoundEffect>("Tiny_Pop_06");
            shootAudio = Content.Load<SoundEffect>("Select_Item_03");
        }
    }
}
