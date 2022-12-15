using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spline;

namespace TowerDefense
{
    public class Enemy : GameObject
    {
        float floatPos;
        SimplePath simplePath;
        public Rectangle hitBox2;
        public bool alive = true;

        public Enemy(Texture2D tex, SimplePath simplePath) : base(tex)
        {
            this.tex = tex;
            this.simplePath = simplePath;
            hitBox2 = new Rectangle(0, 0, tex.Width, tex.Height);
        }

        public void Update()
        {
            if (alive)
            {
                hitBox2.Location = pos.ToPoint();
                floatPos++;
                pos = simplePath.GetPos(floatPos);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(tex, hitBox2, Color.Red);
            }
        }
        public float GetFloatPos()
        {
            return floatPos;
        }
    }
}
