using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pongo3.Characters
{
    internal class PlayerPaddle : Paddle
    {
        public PlayerPaddle(Game game, Texture2D texture, float scale, Vector2 position) : base(game, texture, scale, position)
        {
            
        }
        public override void Draw(SpriteBatch sb, Texture2D pixel)
        {
            base.Draw(sb, pixel);
            base.Draw(sb);
        }
    }
}
