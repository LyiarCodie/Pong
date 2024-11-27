using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pongo3.Characters;
using Pongo3.Utils;

namespace Pongo3
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Texture2D pixel;

        private DividingLine dividingLine;
        private PlayerPaddle playerPaddle;
        private CPUPaddle cpuPaddle;
        private Ball ball;
        private float Scale;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.Scale = 2f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Vector2 screenSize = this.GraphicsDevice.Viewport.Bounds.Size.ToVector2();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.pixel = Content.Load<Texture2D>("whitepixel");
            var dividingLineTexture = Content.Load<Texture2D>("dividing_line");
            this.dividingLine = new DividingLine(dividingLineTexture, new Vector2(screenSize.X * 0.5f, 0f), this.Scale);

            var paddleTexture = Content.Load<Texture2D>("paddle");
            this.playerPaddle = new PlayerPaddle(this, paddleTexture, this.Scale, new Vector2(15f, screenSize.Y * 0.5f));
            this.cpuPaddle = new CPUPaddle(this, paddleTexture, this.Scale, new Vector2(screenSize.X - 15f, screenSize.Y * 0.5f));

            var ballTexture = Content.Load<Texture2D>("ball");
            this.ball = new Ball(this, ballTexture, this.Scale);
            this.ball.Bounds.Center = screenSize * 0.5f;
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardManager.Update();
            if (KeyboardManager.IsKeyPress(Keys.Escape)) this.Exit();

            this.playerPaddle.Update();
            this.cpuPaddle.Update();
            this.ball.Update();

            if (this.ball.Bounds.Intersects(this.playerPaddle.Bounds))
            {
                if (this.ball.Bounds.Left < this.playerPaddle.Bounds.Right)
                {
                    this.ball.Bounds.Left = this.playerPaddle.Bounds.Right;
                    this.ball.InvertVelocityX();
                }
                this.playerPaddle.SetHit();
            }
            if (this.ball.Bounds.Intersects(this.cpuPaddle.Bounds))
            {
                if (this.ball.Bounds.Right > this.cpuPaddle.Bounds.Left)
                {
                    this.ball.Bounds.Right = this.cpuPaddle.Bounds.Left;
                    this.ball.InvertVelocityX();
                }
                this.cpuPaddle.SetHit();
            }

            if (KeyboardManager.IsKeyPress(Keys.Space)) this.ball.NewPositionAndDirection();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(36, 63, 114));

            this.spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            this.dividingLine.Draw(this.spriteBatch);
            this.playerPaddle.Draw(this.spriteBatch, this.pixel);
            this.cpuPaddle.Draw(this.spriteBatch, this.pixel);
            this.ball.Draw(this.spriteBatch, this.pixel);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
