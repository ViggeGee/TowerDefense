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
        public int level = 1;
        float speed = 2f;
        Color color;
        Texture2D tex = Assets.ball;

        public Enemy(SimplePath simplePath, int level) : base()
        {
            this.level = level;
            this.simplePath = simplePath;
            hitBox2 = new Rectangle(0, 0, tex.Width, tex.Height);
        }

        public void Update()
        {

            ColorPicker();
            EnemyLevel();
            if (level <= 0)
            {
                alive = false;
            }
            if (!alive)
                return;
            
            
                hitBox2.Location = pos.ToPoint();
                floatPos += speed;
                pos = simplePath.GetPos(floatPos);

            if(floatPos >= simplePath.endT)
            {
                alive = false;
                Stats.lives = Stats.lives - level;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive)
            {
                spriteBatch.Draw(tex, hitBox2, color);
            }
        }
        public float GetFloatPos()
        {
            return floatPos;
        }

        public void EnemyLevel()
        {
            speed = (speed + level) / 3;
        }

        public void ColorPicker()
        {
            if(level == 1)
                color = Color.Red;
            else if(level == 2)
                color = Color.Blue;
            else if(level == 3)
                color = Color.Green;
            else if(level == 4)
                color = Color.Pink;
            else if(level == 5)
                color = Color.Purple;
            else if (level == 6)
                color = Color.RosyBrown;
            else if (level == 7)
                color = Color.Black;
        }
    }
}
