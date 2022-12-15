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
        List<Enemy> towerEnemyList = new List<Enemy>();
        bool shooting = false;
        public Vector2 pos;
        public float enemyPos;
        public SimplePath simplePath;
        public Bullet bullet;
        public List<Bullet> bulletList = new List<Bullet>();
        public Texture2D shooterTex = Assets.ball;
        Texture2D minerTex;

        enum TowerType
        {
            shooter,
            miner
        }
        TowerType towerType;

        double frameTimer, frameInterval = 2000;
        public Tower(Vector2 pos, Rectangle hitBox, SimplePath simplePath) : base()
        {
            
            this.pos = pos;
            this.hitBox = hitBox;
            this.simplePath = simplePath;
        }

        public override void Update(GameTime gameTime)
        {
            hitBox = new Rectangle((int)pos.X, (int)pos.Y, shooterTex.Width, shooterTex.Height);
            if (!placed)
            {
                pos = Game1.mousePos.ToVector2();
            }
            Shooter(gameTime);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shooterTex, pos, Color.White);
            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }

        public void Shooter(GameTime gameTime)
        {
            if (placed)
            {
                if(towerEnemyList.Count != 0)
                enemyPos = towerEnemyList.First().GetFloatPos();

                foreach (Enemy enemy in towerEnemyList)
                {
                    foreach (Bullet bullet in bulletList)
                    {
                        if (bullet.hitBox2.Intersects(enemy.hitBox2) && bullet.alive)
                        {

                            enemy.alive = false;
                            bullet.alive = false;
                        }
                    }
                }
            }

            frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            foreach (Bullet bullet in bulletList)
            {
                bullet.Update();
            }
            if (frameTimer <= 0)
            {
                frameTimer = frameInterval;
                bulletList.Add(bullet = new Bullet(pos, hitBox, simplePath, enemyPos));
            }
        }

        public void GetList(List<Enemy> tempList)
        {
            towerEnemyList = tempList;
        }
    }
}
