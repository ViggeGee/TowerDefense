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
        Vector2 speed = new Vector2(1, 0);

        public Bullet(Texture2D tex, Vector2 pos, Rectangle hitBox, SimplePath simplePath, float enemyPos) : base(tex, pos, hitBox, simplePath, enemyPos)
        {
            this.pos = pos;
            
            hitBox2 = new Rectangle((int)pos.X, (int)pos.Y, 5, 5);
        }

        public void Update()
        {
            hitBox.Location = pos.ToPoint();
            direction = Vector2.Normalize(direction);
            pos += direction * 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(tex, pos, null, Color.Blue, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
            spriteBatch.Draw(tex, hitBox2, hitBox2, Color.Yellow);
        }

        public Vector2 GetDirection(Vector2 targetPos)
        {

            direction = targetPos - pos;
            return direction;
        }
    }
}
