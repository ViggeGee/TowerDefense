using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    internal class Tower : GameObject
    {
        bool shooting = false;
        public Vector2 pos;
        public Tower(Texture2D tex) :base(tex)
        {
            this.tex = tex;
        }

        public override void Update()
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            if (!placed)
            {
            pos = Game1.mousePos.ToVector2();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}
