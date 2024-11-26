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

        private PlayerPaddle playerPaddle;
        private CPUPaddle cpuPaddle;
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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.pixel = Content.Load<Texture2D>("whitepixel");

            var paddleTexture = Content.Load<Texture2D>("paddle");
            this.playerPaddle = new PlayerPaddle(this, paddleTexture, this.Scale, new Vector2(15f, this.GraphicsDevice.Viewport.Height * 0.5f));
            this.cpuPaddle = new CPUPaddle(this, paddleTexture, this.Scale, new Vector2(this.GraphicsDevice.Viewport.Width - 15f,
                                                                                        this.GraphicsDevice.Viewport.Height * 0.5f));
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardManager.Update();
            if (KeyboardManager.IsKeyPress(Keys.Escape)) this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            this.playerPaddle.Draw(this.spriteBatch, this.pixel);
            this.cpuPaddle.Draw(this.spriteBatch, this.pixel);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
