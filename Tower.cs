using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spline;

namespace TowerDefense
{
    public class Tower : GameObject
    {
        bool shooting = false;
        public Vector2 pos;
        float enemyPos;
        SimplePath simplePath;
        public Bullet bullet;
        public List<Bullet> bulletList = new List<Bullet>();

        double frameInterval = 550, frameTimer = 550;
        public Tower(Texture2D tex, Vector2 pos, Rectangle hitBox, SimplePath simplePath, float enemyPos) : base(tex)
        {
            this.tex = tex;
            this.pos = pos;
            this.hitBox = hitBox;
            this.enemyPos = enemyPos;
            this.simplePath = simplePath;
        }

        public override void Update(GameTime gameTime)
        {
            enemyPos++;
            
            

            hitBox = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            if (!placed)
            {
            pos = Game1.mousePos.ToVector2();
            }

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                bulletList.Add(bullet = new Bullet(tex, pos, hitBox, simplePath, enemyPos));
            }
            foreach (Bullet bullet in bulletList)
            {
                bullet.GetDirection(simplePath.GetPos(enemyPos));
                bullet.Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public Vector2 GetEnemyPos(Vector2 enemyPos)
        {
            return enemyPos;
        }
    }
}
