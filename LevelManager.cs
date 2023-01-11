using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spline;


namespace TowerDefense
{
    internal class LevelManager
    {
        SimplePath simplePath;
        Rectangle lvl1Rec;
        Rectangle lvl2Rec;
        public bool mapSelected = false;
        public enum Waves
        {
            wave1,
            wave2,
            wave3,
            wave4,
            wave5
        }
        public bool level1=false;
        public bool level2=false; 
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
            lvl1Rec = new Rectangle(200, 150, 200, 200);
            lvl2Rec = new Rectangle(500, 150, 200, 200);
        }
        public void LoadLevel()
        {
            Update();


            //Level 1
            if (level1)
            {
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

            //Level 2
            if (level2)
            {
                simplePath.Clean();

                simplePath.AddPoint(new Vector2(0, 100));
                simplePath.AddPoint(new Vector2(50, 100));
                simplePath.AddPoint(new Vector2(100, 100));
                simplePath.AddPoint(new Vector2(150, 100));
                simplePath.AddPoint(new Vector2(200, 100));

                simplePath.AddPoint(new Vector2(200, 150));
                simplePath.AddPoint(new Vector2(200, 200));
                simplePath.AddPoint(new Vector2(200, 250));
                simplePath.AddPoint(new Vector2(200, 300));
                simplePath.AddPoint(new Vector2(200, 350));

                simplePath.AddPoint(new Vector2(250, 350));
                simplePath.AddPoint(new Vector2(300, 350));
                simplePath.AddPoint(new Vector2(350, 350));
                simplePath.AddPoint(new Vector2(400, 350));
                simplePath.AddPoint(new Vector2(450, 350));

                simplePath.AddPoint(new Vector2(450, 300));
                simplePath.AddPoint(new Vector2(450, 250));
                simplePath.AddPoint(new Vector2(450, 200));
                simplePath.AddPoint(new Vector2(450, 150));
                simplePath.AddPoint(new Vector2(450, 100));

                simplePath.AddPoint(new Vector2(500, 100));
                simplePath.AddPoint(new Vector2(550, 100));

                simplePath.AddPoint(new Vector2(550, 150));
                simplePath.AddPoint(new Vector2(550, 400));
                simplePath.AddPoint(new Vector2(550, 450));
                simplePath.AddPoint(new Vector2(600, 450));
                simplePath.AddPoint(new Vector2(850, 450));

                simplePath.SetPos(0, new Vector2(0, 100));
            }
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
                case Waves.wave3:
                    {
                        numberOfEnemies = 10;
                        levelOfEnemies= 5;
                        break;
                    }
                case Waves.wave4:
                    {
                        numberOfEnemies= 30;
                        levelOfEnemies = 7;
                        break;
                    }
                    case Waves.wave5:
                    {
                        numberOfEnemies= 50;
                        levelOfEnemies= 7;
                        break;
                    }

            }
        }
        public void DrawSquare(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Assets.square, lvl1Rec, Color.Red);
            spriteBatch.Draw(Assets.square, lvl2Rec, Color.Blue);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
           
            simplePath.DrawPoints(spriteBatch);
            simplePath.Draw(spriteBatch);
            spriteBatch.DrawString(Assets.spriteFont, "Wave: " + waveCounter, new Vector2(130, 0), Color.Black);
            
        }

        public void MapSelector(MouseState mouseState, Point mousePos)
        {
            if (mouseState.RightButton == ButtonState.Pressed && lvl1Rec.Contains(mousePos))
            {
                level1 = true;
                LoadLevel();
                mapSelected= true;
            }
            //Lvl2 true
            else if (mouseState.RightButton == ButtonState.Pressed && lvl2Rec.Contains(mousePos))
            {
                level2 = true;
                LoadLevel();
                mapSelected= true;
            }
        }
        public void WaveSelector()
        {
            if (waveCounter == 1)
            {
                wave = Waves.wave1;
            }
            else if (waveCounter == 2)
            {
                wave = Waves.wave2;
            }
            else if (waveCounter == 3)
            {
                wave = Waves.wave3;
            }
            else if (waveCounter == 4)
            {
                wave = Waves.wave4;
            }
           else if (waveCounter == 5)
            {
                wave = Waves.wave5;
            }
        }

    }
}
