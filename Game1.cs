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
        private bool IsGameOver;
        private bool IsGameBegin;
        public Vector2 ScreenSize;
        private UI ui;
        public int PlayerScore, CPUScore;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.Scale = 2f;
            this.IsGameOver = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.ScreenSize = this.GraphicsDevice.Viewport.Bounds.Size.ToVector2();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.pixel = Content.Load<Texture2D>("whitepixel");
            var dividingLineTexture = Content.Load<Texture2D>("dividing_line");
            this.dividingLine = new DividingLine(dividingLineTexture, new Vector2(ScreenSize.X * 0.5f, 0f), this.Scale);

            var ballTexture = Content.Load<Texture2D>("ball");
            this.ball = new Ball(this.ScreenSize, ballTexture, this.Scale);
            this.ball.Bounds.Center = ScreenSize * 0.5f;

            var paddleTexture = Content.Load<Texture2D>("paddle");
            this.playerPaddle = new PlayerPaddle(this, paddleTexture, this.Scale, new Vector2(15f, ScreenSize.Y * 0.5f));
            this.cpuPaddle = new CPUPaddle(this, paddleTexture, this.Scale, new Vector2(ScreenSize.X - 15f, ScreenSize.Y * 0.5f), this.ball);

            this.ui = new UI(Content.Load<SpriteFont>("gameFont"), this);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardManager.Update();
            if (KeyboardManager.IsKeyPress(Keys.Escape)) this.Exit();
            
            if (this.IsGameBegin)
            {
                this.playerPaddle.Update();
                this.cpuPaddle.Update();
            }

            if (!this.IsGameOver && this.IsGameBegin)
            {
                this.ball.Update();

                this.UpdateScore();
                this.InvertBallX();
                this.HandleGameOver();
            }

            if (!this.ui.PrepareToPlay)
            {
                // player vs player
                if (KeyboardManager.IsKeyPress(Keys.D1))
                {
                    this.cpuPaddle.IsControllable = true;
                    this.ui.PrepareToPlay = true;
                    this.ui.PlayingWithCPU = false;
                }
                // player vs cpu
                else if (KeyboardManager.IsKeyPress(Keys.D2))
                {
                    this.cpuPaddle.IsControllable = false;
                    this.ui.PrepareToPlay = true;
                    this.ui.PlayingWithCPU = true;
                }
            }

            if ((this.IsGameOver || !this.IsGameBegin) && this.ui.PrepareToPlay)
            {
                if (KeyboardManager.IsKeyPress(Keys.Space))
                {
                    this.ball.StartMove();
                    this.IsGameOver = false;
                    this.IsGameBegin = true;
                    this.ui.ShowInstructions = false;
                }
            }

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
            this.ui.Draw(this.spriteBatch);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }
        private void UpdateScore()
        {
            if (this.ball.Bounds.Right < 0f) 
                this.CPUScore++;
            if (this.ball.Bounds.Left > this.ScreenSize.X) 
                this.PlayerScore++;
        }
        private void HandleGameOver()
        {
            if (this.ball.Bounds.Right <= 0f || this.ball.Bounds.Left >= this.ScreenSize.X)
            {
                this.IsGameOver = true;
                this.ball.ResetBallPosition();
                this.ui.ShowInstructions = true;
                this.ui.PrepareToPlay = false;
            }
        }
        private void InvertBallX()
        {
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
        }
        
    }
}
