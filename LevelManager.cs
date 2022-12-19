using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spline;


namespace TowerDefense
{
    internal class LevelManager
    {
        SimplePath simplePath;

        public enum Waves
        {
            wave1,
            wave2,
            wave3,
            wave4,
            wave5
        }
        public Waves wave;
        public int numberOfEnemies;
        public int levelOfEnemies;
        public int waveCounter = 1;
        public LevelManager(SimplePath simplePath)
        {
            this.simplePath = simplePath;
        }

        public void Load()
        {
            Update();


            //Level 1
            simplePath.Clean();

            simplePath.AddPoint(new Vector2(0, 0));
            simplePath.AddPoint(new Vector2(100, 100));
            simplePath.AddPoint(new Vector2(200, 200));
            simplePath.AddPoint(new Vector2(300, 200));
            simplePath.AddPoint(new Vector2(300, 400));
            simplePath.AddPoint(new Vector2(500, 400));
            simplePath.AddPoint(new Vector2(600, 500));
            simplePath.AddPoint(new Vector2(700, 500));
            simplePath.AddPoint(new Vector2(850, 500));

            simplePath.SetPos(0, Vector2.Zero);
        }

        public void Update()
        {
            WaveSelector();
            switch (wave)
            {
                case Waves.wave1:
                    {
                        numberOfEnemies = 10;
                        levelOfEnemies = 2;
                        break;
                    }
                case Waves.wave2:
                    {
                        numberOfEnemies = 2;
                        levelOfEnemies = 5;
                        break;
                    }

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            simplePath.DrawPoints(spriteBatch);
            simplePath.Draw(spriteBatch);
        }

        public void WaveSelector()
        {
            if (waveCounter == 1)
            {
                wave = Waves.wave1;
            }
            if (waveCounter == 2)
            {
                wave = Waves.wave2;
            }
        }

    }
}
