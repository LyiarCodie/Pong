using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pongo2;
using Pongo3.Utils;
using System;

namespace Pongo3.Characters
{
    internal class Paddle
    {
        protected RectangleF Bounds;
        protected Vector2 ScreenSize;
        protected Texture2D Texture;
        protected Vector2 Velocity;
        protected Vector2 TextureOrigin;
        protected bool IsFlipX { get; private set; }
        protected float Scale { get; private set; }
        private SpriteEffects spriteEffects;
        public Paddle(Game game, Texture2D texture, float scale, Vector2 position)
        {
            this.ScreenSize = game.GraphicsDevice.Viewport.Bounds.Size.ToVector2();
            this.Scale = scale;
            this.Texture = texture;
            this.Bounds = new RectangleF(position, this.Texture.Bounds.Size.ToVector2() * this.Scale);
            this.Bounds.Center = position;
            this.TextureOrigin = Vector2.Zero;
            this.spriteEffects = SpriteEffects.None;
        }
        /// <summary>
        /// Draw the bounds
        /// </summary>
        public virtual void Draw(SpriteBatch sb, Texture2D pixel)
        {
            sb.Draw(pixel, this.Bounds.ToRectangle(), Color.MonoGameOrange);
        }
        /// <summary>
        /// Draw the paddle
        /// </summary>
        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(this.Texture, this.Bounds.ToRectangle(), null, Color.White, 0f, this.TextureOrigin, this.spriteEffects, 0f);
        }
        protected void SetFlipX()
        {
            this.IsFlipX = !this.IsFlipX;
            this.spriteEffects = this.IsFlipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public virtual void Update()
        {
            //Console.WriteLine("This method hasn't been overriden!");
        }
        protected void ConstrainToScreenBounds()
        {
            if (this.Bounds.Bottom > this.ScreenSize.Y) this.Bounds.Bottom = this.ScreenSize.Y;
            if (this.Bounds.Top < 0f) this.Bounds.Top = 0f;
        }
        protected void Move(Keys upKey, Keys downKey, float maxSpeed = 7f, float acceleration = 0.25f, float friction = 0.25f)
        {
            if (KeyboardManager.IsKeyDown(upKey))
            {
                if (this.Velocity.Y > 0f) this.Velocity.Y = 0f;
                if (this.Velocity.Y > -maxSpeed) this.Velocity.Y -= acceleration;
            }
            else if (KeyboardManager.IsKeyDown(downKey))
            {
                if (this.Velocity.Y < 0f) this.Velocity.Y = 0f;
                if (this.Velocity.Y < maxSpeed) this.Velocity.Y += acceleration;
            }
            else
            {
                if (this.Velocity.Y > 0f)
                {
                    this.Velocity.Y -= friction;
                    if (this.Velocity.Y < 0f) this.Velocity.Y = 0f;
                }
                else if (this.Velocity.Y < 0f)
                {
                    this.Velocity.Y += friction;
                    if (this.Velocity.Y > 0f) this.Velocity.Y = 0f;
                }
            }

            this.Bounds.Position += this.Velocity;
        }
    }
}
