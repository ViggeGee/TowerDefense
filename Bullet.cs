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


        public Bullet(Texture2D tex, Vector2 pos, Rectangle hitBox) : base(tex, pos, hitBox)
        {
            this.pos = pos;
        }

        public void Update()
        {
            GetDirection(new Vector2(50, 100));
            direction = Vector2.Normalize(direction);
            pos += direction;
            

            //if (pos.X > direction.X)
            //{
            //    pos.X -= 1;
            //}
            //else
            //{
            //    pos.X += 1;
            //}
            //if (pos.Y > direction.Y)
            //{
            //    pos.Y -= 1;
            //}
            //else { pos.Y += 1; }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, Color.Red, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
        }

        public Vector2 GetDirection(Vector2 targetPos)
        {

            direction = targetPos - pos;
            return direction;
        }
    }
}
