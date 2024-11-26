using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pongo2;
using System.Windows.Forms.VisualStyles;

namespace Pongo3.Characters
{
    internal class Paddle
    {
        protected RectangleF Bounds;
        protected Rectangle ScreenSize;
        protected Texture2D Texture;
        protected Vector2 Velocity;
        protected Vector2 TextureOrigin;
        protected bool IsFlipX { get; private set; }
        protected float Scale { get; private set; }
        private SpriteEffects spriteEffects;
        public Paddle(Game game, Texture2D texture, float scale, Vector2 position)
        {
            this.ScreenSize = game.GraphicsDevice.Viewport.Bounds;
            this.Scale = scale;
            this.Texture = texture;
            this.Bounds = new RectangleF(Vector2.Zero, this.Texture.Bounds.Size.ToVector2() * this.Scale);
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
    }
}
