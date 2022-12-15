using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class Bullet : Tower

    {
        public Rectangle hitBox2;
        Vector2 direction;
        public bool alive = true;
        Vector2 enemyPosV;
        float enemyPos;
        Vector2 speed = new Vector2(3, 3);
        Texture2D tex = Assets.ball;
        public Bullet(Vector2 pos, Rectangle hitBox, SimplePath simplePath, float enemyPos) : base(pos, hitBox, simplePath)
        {
            this.pos = pos;
            this.enemyPos = enemyPos;
            hitBox2 = new Rectangle((int)pos.X, (int)pos.Y, 5, 5);
        }

        public void Update()
        {
            if (!alive) 
                return;

            if(Vector2.Distance(pos, enemyPosV) <= 3)
            {
                alive = false;
            }
            enemyPosV = simplePath.GetPos(enemyPos);
            direction = simplePath.GetPos(enemyPos) - pos;
            if (pos == enemyPosV)
            {
                alive = false;
            }
            direction = Vector2.Normalize(direction);
            pos += direction * speed;
            hitBox2.Location = pos.ToPoint();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(tex, pos, null, Color.Blue, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
            }
        }
    }
}
