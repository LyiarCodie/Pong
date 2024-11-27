using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pongo3.Characters
{
    internal class DividingLine
    {
        private Texture2D texture;
        private int amountOfDashes;
        private float scale;
        private Vector2 position;
        public DividingLine(Texture2D texture, Vector2 position, float scale)
        {
            this.texture = texture;
            this.amountOfDashes = 20;
            this.scale = scale;
            this.position = position;
        }
        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < this.amountOfDashes; i++)
            {
                sb.Draw(
                    this.texture,
                    new Vector2(this.position.X, this.position.Y + this.texture.Height * this.scale * i),
                    null,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    this.scale,
                    SpriteEffects.None,
                    0f);
            }
        }
    }
}
