using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense
{
    public class GameObject
    {
        public bool placed = false;
        public Texture2D tex;
        Vector2 pos;
        public Rectangle hitBox;

        public GameObject(Texture2D tex) { this.tex = tex; }
        public virtual void Update()
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }


}
