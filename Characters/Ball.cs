using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pongo2;
using System;

namespace Pongo3.Characters
{
    internal class Ball
    {
        private Texture2D texture;
        public Vector2 Velocity { get; private set; }
        public RectangleF Bounds { get; private set; }
        private float moveSpeed;
        private float normalize;
        private Vector2 screenSize;
        private int bounceCount;
        private Random random;
        public Ball(Game game, Texture2D texture, float scale)
        {
            this.screenSize = game.GraphicsDevice.Viewport.Bounds.Size.ToVector2();
            this.texture = texture;
            this.Bounds = new RectangleF(Vector2.Zero, this.texture.Bounds.Size.ToVector2() * scale);
            this.moveSpeed = 4f;
            this.Velocity = new Vector2(-1f, -1f) / 1.25f;
            this.random = new Random();
        }
        public void Update()
        {
            if (this.Bounds.Top < 0f)
            {
                this.Bounds.Top = 0f;
                this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y * -1f);
            }
            if (this.Bounds.Bottom > this.screenSize.Y)
            {
                this.Bounds.Bottom = this.screenSize.Y;
                this.Velocity = new Vector2(this.Velocity.X, this.Velocity.Y * -1f);
            }

            this.Bounds.Position += this.Velocity * this.moveSpeed;
        }
        public void InvertVelocityX()
        {
            this.Velocity = new Vector2(this.Velocity.X * -1f, this.Velocity.Y);

            this.bounceCount++;
            if (this.bounceCount > 1)
                this.moveSpeed += 0.25f;
        }
        public void Draw(SpriteBatch sb, Texture2D pixel)
        {
            sb.Draw(pixel, this.Bounds.ToRectangle(), Color.MonoGameOrange);
            sb.Draw(this.texture, this.Bounds.ToRectangle(), Color.White);
        }

        public void NewPositionAndDirection()
        {
            this.Bounds.Center = this.screenSize * 0.5f;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(this.texture, this.Bounds.ToRectangle(), Color.White);
        }
        
        public void ResetBallPosition()
        {
            if (this.Bounds.Left > this.screenSize.X || this.Bounds.Right < 0f)
            {
                this.Velocity = Vector2.Zero;
                this.Bounds.Center = this.screenSize / 2f;
                this.moveSpeed = 4f;
            }
        }
    
        public void StartMove()
        {
            this.Velocity = new Vector2(this.GetRandomSingle(), this.GetRandomSingle());
        }
        private float GetRandomSingle()
        {
            // using recursivity
            //float value = this.random.NextSingle() * 2.0f - 1.0f;
            //if (value > -0.5f && value < 0.5f) 
            //    return this.GetRandomSingle();
            //return value;

            float value = 0.0f;
            do
            {
                value = this.random.NextSingle() * 2.0f - 1.0f;
            }
            while (value > -0.5f && value < 0.5f);
            return value;
        }
    }
}
