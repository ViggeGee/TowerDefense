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
        public Vector2 pos;
        public Rectangle hitBox;


        public GameObject() { }
        public virtual void Update(GameTime gameTime)
        {
            

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
      
        }
    }


}
