using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using Spline;
using System.IO;

namespace TowerDefense
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        SimplePath simplePath;

        int width = 850;
        int height = 650;

        float texPos;

        RenderTarget2D renderTarget;

        Texture2D backgroundTexture;
        Texture2D ball;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            renderTarget = new RenderTarget2D(GraphicsDevice, Window.ClientBounds.Width, Window.ClientBounds.Height);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            simplePath = new SimplePath(GraphicsDevice);
            simplePath.Clean(); // tar bort alla punkter
            //simplePath.generateDefaultPath();
            
            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;
            graphics.ApplyChanges();

            ball = Content.Load<Texture2D>("ball");
            backgroundTexture = Content.Load<Texture2D>("transparentSquareBackground");

            DrawOnRenderTarget();

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
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //förflyttar positionen längs kurvan
            texPos++; //bestämmer hastigheten

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(renderTarget, new Vector2(100,100), Color.White);
            spriteBatch.Draw(ball, new Vector2(150,100), Color.White); 

            GraphicsDevice.Clear(Color.CornflowerBlue);
            simplePath.Draw(spriteBatch);
            simplePath.DrawPoints(spriteBatch);

            //ritar ut fienden på kurvan
            if (texPos < simplePath.endT)
                spriteBatch.Draw(ball, simplePath.GetPos(texPos), new Rectangle(0, 0, ball.Width, ball.Height),
                     Color.White, 0f, new Vector2(ball.Width / 2, ball.Height / 2), 1f, SpriteEffects.None, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void DrawOnRenderTarget()
        {
            //Ändra så att GraphicsDevice ritar mot vårt render target
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();

            //Rita ut texturen. Den ritas nu ut till vårt render target istället
            //för på skärmen.
            spriteBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            spriteBatch.End();

            //Sätt GraphicsDevice att åter igen peka på skärmen
            GraphicsDevice.SetRenderTarget(null);
        }
    }
}