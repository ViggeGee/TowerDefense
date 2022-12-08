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
       
        SimplePath simplePath;

        int width = 850;
        int height = 650;

        float texPos;

        RenderTarget2D renderTarget;

        Texture2D backgroundTexture;
        Texture2D ball;


        //Tower
        List<Tower> TowerList = new List<Tower>();
        Tower tower;
        Texture2D towerTex;

        //Mouse & Keyboard
        KeyboardState ks;
        public static Point mousePos;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        //Bullets
        //Bullet bullet;
        //List<Bullet> bulletList = new List<Bullet>();
        //int timer = 2;
        //double frameInterval = 550, frameTimer = 550;


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch1 = new SpriteBatch(GraphicsDevice);
            
            simplePath = new SimplePath(GraphicsDevice);
            simplePath.Clean(); // tar bort alla punkter
            //simplePath.generateDefaultPath();
            
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            graphics.ApplyChanges();
            
            renderTarget = new RenderTarget2D(GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);

            ball = Content.Load<Texture2D>("ball");
            backgroundTexture = Content.Load<Texture2D>("transparentSquareBackground");
            towerTex = Content.Load<Texture2D>("Tower");


            simplePath.AddPoint(new Vector2(0,0));
            simplePath.AddPoint(new Vector2(100,100));
            simplePath.AddPoint(new Vector2(100,200));
            simplePath.AddPoint(new Vector2(300,200));
            simplePath.AddPoint(new Vector2(400, 100));
            simplePath.AddPoint(new Vector2(500,400));
            simplePath.AddPoint(new Vector2(600, 200));
            simplePath.AddPoint(new Vector2(700,300));
            simplePath.AddPoint(new Vector2(850, 650));
            
            //sätter bildens startpunkt till början av kurvan
            texPos = simplePath.beginT;
            simplePath.SetPos(0, Vector2.Zero);
           
            simplePath.GetPos(simplePath.beginT);
            //Bullet
            //bullet = new Bullet(towerTex, new Vector2(0,0));
            //bulletList.Add(bullet);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            mousePos = new Point(mouseState.X, mouseState.Y);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach(Tower go in TowerList)
            {
                go.Update(gameTime);
            }
            if(mouseState.LeftButton == ButtonState.Pressed)
            {
                tower = new Tower(ball, new Vector2(mousePos.X, mousePos.Y), new Rectangle(mousePos.X, mousePos.Y, ball.Width, ball.Height));
                if(CanPlace(tower))
                {
                TowerList.Add(tower);
                }

            }
            foreach(Tower go in TowerList)
            {
                go.placed = true;

                //frameTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
                //if (frameTimer <= 0)
                //{
                //    frameTimer = frameInterval;
                //    bulletList.Add(bullet = new Bullet(ball, go.pos));
                //}
            }
                //foreach(Bullet bullet in bulletList)
                //{
                //    bullet.Update();
                //}
            //förflyttar positionen längs kurvan
            texPos++; //bestämmer hastigheten

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            DrawOnRenderTarget();


            spriteBatch.Draw(renderTarget, new Rectangle(0,0, width, height), new Rectangle(0,0,width,height), Color.White, 0, Vector2.Zero, SpriteEffects.None, 0f);

            GraphicsDevice.Clear(Color.White);
            //simplePath.Draw(spriteBatch);
            simplePath.DrawPoints(spriteBatch);

            //ritar ut fienden på kurvan
            if (texPos < simplePath.endT)
                spriteBatch.Draw(ball, simplePath.GetPos(texPos), new Rectangle(0, 0, ball.Width, ball.Height),
                     Color.White, 0f, new Vector2(ball.Width / 2, ball.Height / 2), 1f, SpriteEffects.None, 0f);

            foreach(Tower obj in TowerList)
            {
                obj.Draw(spriteBatch);
            }
                //foreach(Bullet bullet in bulletList)
                //{
                //    bullet.Draw(spriteBatch);
                //}
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawOnRenderTarget()
        {
            //Ändra så att GraphicsDevice ritar mot vårt render target
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch1.Begin();
            foreach (Tower obj in TowerList)
            {
                obj.Draw(spriteBatch1);
            }
            //Rita ut texturen. Den ritas nu ut till vårt render target istället
            //för på skärmen.
            spriteBatch1.Draw(backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch1.End();

            //Sätt GraphicsDevice att åter igen peka på skärmen
            GraphicsDevice.SetRenderTarget(null);
        }

        public bool CanPlace(Tower g)
        {
            Color[] pixels = new Color[g.tex.Width * g.tex.Height];
            Color[] pixels2 = new Color[g.tex.Width * g.tex.Height];
            g.tex.GetData<Color>(pixels2);
            renderTarget.GetData(0, g.hitBox, pixels, 0, pixels.Length);
            
            for (int i = 0; i < pixels.Length; i++)
            {
                if(pixels[i].A > 0.0f && pixels2[i].A > 0.0f) { return false; }
            }
            return true;
        }

        
    }
}