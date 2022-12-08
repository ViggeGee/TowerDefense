using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    internal class Bullet : Tower

    {
        
        Vector2 direction;
        Vector2 speed = new Vector2(1, 0);
        float scale = 0.1f;


        public Bullet(Texture2D tex, Vector2 pos) : base(tex)
        {
            this.pos = pos;
        }

        public void Update()
        {
            pos += speed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, Color.Red, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
        }
    }
}
