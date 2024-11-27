using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pongo2;

namespace Pongo3.Characters
{
    internal class Ball
    {
        private Texture2D texture;
        private Vector2 velocity;
        public RectangleF Bounds { get; private set; }
        private float moveSpeed;
        private float normalize;
        private Vector2 screenSize;
        public Ball(Game game, Texture2D texture, float scale)
        {
            this.screenSize = game.GraphicsDevice.Viewport.Bounds.Size.ToVector2();
            this.texture = texture;
            this.Bounds = new RectangleF(Vector2.Zero, this.texture.Bounds.Size.ToVector2() * scale);
            this.moveSpeed = 4f;
            this.velocity = new Vector2(-1f, -1f) / 1.25f;
        }
        public void Update()
        {
            if (this.Bounds.Top < 0f)
            {
                this.Bounds.Top = 0f;
                this.velocity.Y *= -1f;
            }
            if (this.Bounds.Bottom > this.screenSize.Y)
            {
                this.Bounds.Bottom = this.screenSize.Y;
                this.velocity.Y *= -1f;
            }

            this.Bounds.Position += this.velocity * this.moveSpeed;
        }
        public void InvertVelocityX()
        {
            this.velocity.X *= -1;
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
    }
}
