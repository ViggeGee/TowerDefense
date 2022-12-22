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
        Texture2D minerTex = Assets.square;

       public enum TowerType
        {
            shooter,
            miner
        }
        TowerType towerType;

        double shooterFrameTimer, shooterFrameInterval = 1000;
        double minerFrameTimer, minerFrameInterval = 10000;
        public Tower(Vector2 pos, Rectangle hitBox, SimplePath simplePath, TowerType towerType) : base()
        {
            this.towerType = towerType;
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
            if (towerType == TowerType.shooter)
                Shooter(gameTime);
            if (towerType == TowerType.miner)
                Miner(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (towerType == TowerType.shooter)
            {
                spriteBatch.Draw(shooterTex, pos, Color.White);
                foreach (Bullet bullet in bulletList)
                {
                    bullet.Draw(spriteBatch);
                }
            }
            else if (towerType == TowerType.miner)
                spriteBatch.Draw(minerTex, pos, Color.White);
        }

        public void Shooter(GameTime gameTime)
        {
            if (placed)
            {
                if (towerEnemyList.Count != 0)
                    enemyPos = towerEnemyList.First().GetFloatPos();

                foreach (Enemy enemy in towerEnemyList)
                {
                    foreach (Bullet bullet in bulletList)
                    {
                        if (bullet.hitBox2.Intersects(enemy.hitBox2) && bullet.alive)
                        {
                            Stats.currency = Stats.currency + 5;
                            enemy.level = enemy.level - 1;
                            bullet.alive = false;

                        }
                    }
                }
            }

            shooterFrameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            foreach (Bullet bullet in bulletList)
            {
                bullet.Update();
            }
            if (shooterFrameTimer <= 0)
            {
                shooterFrameTimer = shooterFrameInterval;
                bulletList.Add(bullet = new Bullet(pos, hitBox, simplePath, enemyPos, TowerType.shooter));
            }
        }

        public void Miner(GameTime gameTime)
        {
            minerFrameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if(minerFrameTimer <= 0)
            {
                minerFrameTimer = minerFrameInterval;
                Stats.currency = Stats.currency + 10;
            }
        }

        public void GetList(List<Enemy> tempList)
        {
            towerEnemyList = tempList;
        }
    }
}
