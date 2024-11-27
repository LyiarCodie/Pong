using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pongo3.Characters
{
    internal class CPUPaddle : Paddle
    {
        private bool IsControllable;
        public CPUPaddle(Game game, Texture2D texture, float scale, Vector2 position) : base(game, texture, scale, position)
        {
            this.SetFlipX();
            this.IsControllable = true;
        }
        public override void Update()
        {
            if (this.IsControllable)
                this.Move(Keys.I, Keys.K);
            else
            {
                //
            }

            this.ConstrainToScreenBounds();
        }
        public override void Draw(SpriteBatch sb, Texture2D pixel)
        {
            base.Draw(sb, pixel);
            base.Draw(sb);
        }
    }
}
