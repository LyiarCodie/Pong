using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pongo3.Characters
{
    internal class UI
    {
        private Game1 game;
        private SpriteFont gameFont;
        private bool isDisposed;
        private Vector2 screenCenter;

        private string text1, text2, text3;
        private Vector2 text1Center, text2Center, text3Center;

        private string playerInstructions, cpuInstructions;
        private Vector2 playerInstructionsCenter, cpuInstructionsCenter;

        public bool PrepareToPlay;
        public bool PlayingWithCPU;
        public bool ShowInstructions;

        public UI(SpriteFont gameFont, Game1 game)
        {
            this.ShowInstructions = true;
            this.gameFont = gameFont;
            this.game = game;
            this.screenCenter = this.game.ScreenSize * 0.5f;

            this.text1 = "Press 1 for Player vs Player";
            this.text1Center = this.gameFont.MeasureString(this.text1) * 0.5f;

            this.text2 = "Press 2 for Player vs CPU";
            this.text2Center = this.gameFont.MeasureString(this.text2) * 0.5f;

            this.playerInstructions = "Use W and S to \nmove the Left Paddle";
            this.cpuInstructions = "Use I and J to \nmove the Right Paddle";
            this.playerInstructionsCenter = this.gameFont.MeasureString(this.playerInstructions) * 0.5f;
            this.cpuInstructionsCenter = this.gameFont.MeasureString(this.cpuInstructions) * 0.5f;

            this.text3 = "Press Space to Start";
            this.text3Center = this.gameFont.MeasureString(this.text3) * 0.5f;
        }
        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(
                  this.gameFont,
                  this.game.CPUScore.ToString(),
                  new Vector2(this.screenCenter.X + 20f, 10f),
                  Color.White,
                  0f,
                  Vector2.Zero,
                  2f,
                  SpriteEffects.None,
                  0f
              );

            Vector2 stringSize = this.gameFont.MeasureString(this.game.PlayerScore.ToString());
            sb.DrawString(
                this.gameFont,
                this.game.PlayerScore.ToString(),
                new Vector2(this.screenCenter.X - 15f, 10f),
                Color.White,
                0f,
                new Vector2(stringSize.X, 0f),
                2f,
                SpriteEffects.None,
                0f
            );

            if (!this.PrepareToPlay)
            {
                sb.DrawString(
                    this.gameFont,
                    this.text1,
                    new Vector2(this.screenCenter.X, this.screenCenter.Y - 20f),
                    Color.White, 0f, this.text1Center, 1f, SpriteEffects.None, 0f
                );
                sb.DrawString(
                    this.gameFont,
                    this.text2,
                    new Vector2(this.screenCenter.X, this.screenCenter.Y + 20f),
                    Color.White, 0f, this.text2Center, 1f, SpriteEffects.None, 0f
                );
            }
            if (this.PrepareToPlay && this.ShowInstructions)
            {
                sb.DrawString(
                    this.gameFont,
                    this.playerInstructions,
                    new Vector2(this.screenCenter.X * 0.5f, this.screenCenter.Y),
                    Color.White,
                    0f,
                    this.playerInstructionsCenter,
                    1f,
                    SpriteEffects.None,
                    0f
                );

                if (!this.PlayingWithCPU)
                {
                    sb.DrawString(
                        this.gameFont,
                        this.cpuInstructions,
                        new Vector2(this.screenCenter.X + this.screenCenter.X * 0.5f, this.screenCenter.Y),
                        Color.White,
                        0f,
                        this.cpuInstructionsCenter,
                        1f,
                        SpriteEffects.None,
                        0f
                    );
                }

                sb.DrawString(
                    this.gameFont,
                    this.text3,
                    new Vector2(this.screenCenter.X, this.screenCenter.Y + 100f),
                    Color.White,
                    0f,
                    this.text3Center,
                    1f,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
