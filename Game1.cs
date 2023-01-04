using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using Spline;
using System;
using System.Collections.Generic;
using System.IO;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch, spriteBatch1;

        //GameState
        bool paused;
        enum GameState
        {
            start,
            build,
            run,
            win,
            over
        }
        GameState gameState;

        //Level
        LevelManager levelManager;
        SimplePath simplePath;
        Rectangle lvl1Rec;
        Rectangle lvl2Rec;

        int width = 850;
        int height = 650;


        RenderTarget2D renderTarget;

        //Particles
        ParticleSystem particleSystem;
        List<Texture2D> textures = new List<Texture2D>();


        //Enemy
        Enemy enemy;
        public List<Enemy> enemyList = new List<Enemy>();
        double timer = 0;
        double timerInterval = 0.5;
        float enemyPosF;
        int enemyCounter = 0;


        //Tower
        List<Tower> TowerList = new List<Tower>();
        Tower tower;

        //Mouse & Keyboard
        MouseState mouseState;
        KeyboardState ks;
        public static Point mousePos;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {

            Assets.LoadTextures(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch1 = new SpriteBatch(GraphicsDevice);

            simplePath = new SimplePath(GraphicsDevice);
            levelManager = new LevelManager(simplePath);
            //levelManager.Load();
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            graphics.ApplyChanges();

            renderTarget = new RenderTarget2D(GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);


            textures.Add(Content.Load<Texture2D>("circle"));
            textures.Add(Content.Load<Texture2D>("star"));
            textures.Add(Content.Load<Texture2D>("diamond"));
            particleSystem = new ParticleSystem(textures, new Vector2(400, 240));

            enemyPosF = simplePath.beginT;

            simplePath.GetPos(simplePath.beginT);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && !paused)
                paused = true;
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && paused)
                paused = false;

            mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);
            levelManager.Update();

            if (paused)
                return;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            switch (gameState)
            {
                case GameState.start:
                    {
                        particleSystem.EmitterLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                        particleSystem.Update();
                        lvl1Rec = new Rectangle(200, 150, 200, 200);
                        lvl2Rec = new Rectangle(500, 150, 200, 200);

                            //Lvl1 true
                        if (mouseState.RightButton == ButtonState.Pressed && lvl1Rec.Contains(mousePos))
                        {
                            levelManager.level1 = true;
                            levelManager.Load();
                            gameState = GameState.build;
                        }
                            //Lvl2 true
                        else if (mouseState.RightButton == ButtonState.Pressed && lvl2Rec.Contains(mousePos))
                        {
                            levelManager.level2 = true;
                            levelManager.Load();
                            gameState = GameState.build;
                        }

                        //if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        //    gameState = GameState.build;

                        break;
                    }
                case GameState.build:
                    {
                        TowerPlacer();
                        if (Keyboard.GetState().IsKeyDown(Keys.A))
                        {
                            gameState = GameState.run;
                        }
                        break;
                    }
                case GameState.run:
                    {
                        //TowerPlacer
                        foreach (Tower go in TowerList)
                        {
                            go.Update(gameTime);
                        }


                        //Shooter
                        enemyList.RemoveAll(x => x.alive == false);
                        foreach (Tower tower in TowerList)
                        {
                            tower.GetList(enemyList);
                        }

                        EnemySpawner(gameTime);
                        WaveReset();

                        foreach (Enemy enemy in enemyList)
                        {
                            enemy.Update();
                        }

                        if (Stats.lives <= 0)
                            gameState = GameState.over;
                        break;
                    }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawOnRenderTarget();
            GraphicsDevice.Clear(Color.DimGray);

            switch (gameState)
            {
                case GameState.start:
                    {
                        spriteBatch.Draw(Assets.square, lvl1Rec, Color.Red);
                        spriteBatch.Draw(Assets.square, lvl2Rec, Color.Blue);
                        particleSystem.Draw(spriteBatch);
                        break;
                    }
                case GameState.build:
                    {
                        levelManager.Draw(spriteBatch);

                        foreach (Enemy enemy in enemyList)
                        {
                            enemy.Draw(spriteBatch);
                        }

                        foreach (Tower obj in TowerList)
                        {
                            obj.Draw(spriteBatch);
                        }
                        Stats.Draw(spriteBatch);
                        break;
                    }
                case GameState.run:
                    {
                        levelManager.Draw(spriteBatch);

                        foreach (Enemy enemy in enemyList)
                        {
                            enemy.Draw(spriteBatch);
                        }


                        foreach (Tower obj in TowerList)
                        {
                            obj.Draw(spriteBatch);
                        }
                        Stats.Draw(spriteBatch);

                        break;
                    }
                case GameState.over:
                    {
                        spriteBatch.DrawString(Assets.spriteFont, "Game Over", new Vector2(50, 200), Color.Red, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
                        break;
                    }
                case GameState.win:
                    {
                        spriteBatch.DrawString(Assets.spriteFont, "You Win", new Vector2(50, 200), Color.Yellow, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
                        break;
                    }

            }

            if (paused)
                spriteBatch.DrawString(Assets.spriteFont, "Game Paused", new Vector2(50, 200), Color.Orange, 0, Vector2.Zero, 5, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawOnRenderTarget()
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch1.Begin();
            foreach (Tower obj in TowerList)
            {
                obj.Draw(spriteBatch1);
            }


            spriteBatch1.Draw(Assets.backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch1.End();


            GraphicsDevice.SetRenderTarget(null);
        }

        public bool CanPlace(Tower g)
        {
            Color[] pixels = new Color[g.shooterTex.Width * g.shooterTex.Height];
            Color[] pixels2 = new Color[g.shooterTex.Width * g.shooterTex.Height];
            g.shooterTex.GetData<Color>(pixels2);
            renderTarget.GetData(0, g.hitBox, pixels, 0, pixels.Length);


            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].A > 0.0f && pixels2[i].A > 0.0f) { return false; }
            }
            return true;
        }

        public void TowerPlacer()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                tower = new Tower(new Vector2(mousePos.X, mousePos.Y), new Rectangle(mousePos.X, mousePos.Y, Assets.ball.Width, Assets.ball.Height), simplePath, Tower.TowerType.shooter);
                if (CanPlace(tower) && Stats.currency >= 50)
                {
                    TowerList.Add(tower);
                    Stats.currency = Stats.currency - 50;
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                tower = new Tower(new Vector2(mousePos.X, mousePos.Y), new Rectangle(mousePos.X, mousePos.Y, Assets.square.Width, Assets.square.Height), simplePath, Tower.TowerType.miner);
                if (CanPlace(tower) && Stats.currency >= 70)
                {
                    TowerList.Add(tower);
                    Stats.currency = Stats.currency - 70;
                }
            }

            foreach (Tower go in TowerList)
            {
                go.placed = true;
            }
        }
        public void EnemySpawner(GameTime gameTime)
        {
            if (enemyCounter <= levelManager.numberOfEnemies)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                {
                    enemyList.Add(new Enemy(simplePath, levelManager.levelOfEnemies));
                    timer = timerInterval;
                    enemyCounter++;
                }
            }
        }
        public void WaveReset()
        {
            if (enemyCounter >= levelManager.numberOfEnemies && enemyList.Count == 0)
            {
                gameState = GameState.build;
                enemyCounter = 0;
                foreach (Tower tower in TowerList)
                {
                    tower.bulletList.Clear();
                }
                enemyList.Clear();
                levelManager.waveCounter++;
                if (levelManager.waveCounter >= 6)
                    gameState = GameState.win;
            }
        }
    }
}