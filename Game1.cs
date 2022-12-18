using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using Spline;
using System.Collections.Generic;
using System.IO;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch, spriteBatch1;
       


        //Level
        LevelManager levelManager;
        SimplePath simplePath;

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
        double timerInterval = 2;
        float enemyPosF;

        //Tower
        List<Tower> TowerList = new List<Tower>();
        Tower tower;

        //Mouse & Keyboard
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
            levelManager.Load();
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
            MouseState mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);
            particleSystem.EmitterLocation =  new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            particleSystem.Update();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //EnemySpawner
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if (timer <= 0)
            {
                enemyList.Add(new Enemy(simplePath));
                timer = timerInterval;
            }

            foreach (Enemy enemy in enemyList)
            {
                enemy.Update();

            }

            //TowerPlacer
            foreach (Tower go in TowerList)
            {
                go.Update(gameTime);
            }
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                tower = new Tower(new Vector2(mousePos.X, mousePos.Y), new Rectangle(mousePos.X, mousePos.Y, Assets.ball.Width, Assets.ball.Height), simplePath, Tower.TowerType.shooter);
                if (CanPlace(tower) && Currency.currency >= 50)
                {
                    TowerList.Add(tower);
                    Currency.currency = Currency.currency - 50;
                }
            }
            if (mouseState.RightButton == ButtonState.Pressed)
            {
                tower = new Tower(new Vector2(mousePos.X, mousePos.Y), new Rectangle(mousePos.X, mousePos.Y, Assets.square.Width, Assets.square.Height), simplePath, Tower.TowerType.miner);
                if (CanPlace(tower) && Currency.currency >= 70)
                {
                    TowerList.Add(tower);
                    Currency.currency = Currency.currency - 70;
                }
            }

            foreach (Tower go in TowerList)
            {
                go.placed = true;
            }

            //Shooter
            enemyList.RemoveAll(x => x.alive == false);
            foreach(Tower tower in TowerList)
            {
                tower.GetList(enemyList);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawOnRenderTarget();
            particleSystem.Draw(spriteBatch);

            //spriteBatch.Draw(renderTarget, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0f);

            GraphicsDevice.Clear(Color.White);
            levelManager.Draw(spriteBatch);

            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }



            foreach (Tower obj in TowerList)
            {
                obj.Draw(spriteBatch);
            }

            Currency.Draw(spriteBatch);
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
    }
}