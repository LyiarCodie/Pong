using System;
using System.Runtime.Intrinsics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pongo3.Characters
{
    internal class CPUPaddle : Paddle
    {
        public bool IsControllable;
        private Ball ball;
        private float maxSpeed;
        private float acceleration;
        private float resistence;
        private Vector2 screenCenter;
        public CPUPaddle(Game game, Texture2D texture, float scale, Vector2 position, Ball ball) : base(game, texture, scale, position)
        {
            this.SetFlipX();
            this.IsControllable = true;
            this.ball = ball;

            this.maxSpeed = 7f;
            this.acceleration = 0.25f;
            this.resistence = 0.99f;
            this.screenCenter = this.ScreenSize * 0.5f;
        }
        public override void Update()
        {
            if (this.IsControllable)
                this.Move(Keys.I, Keys.K);
            else
            {
                float dist = MathHelper.Distance(this.ball.Bounds.Center.Y, this.Bounds.Center.Y);
                if (dist > 5f && this.ball.Velocity.X > 0f)
                {
                    // paddle is above the ball
                    if (this.Bounds.Center.Y < this.ball.Bounds.Center.Y)
                    {
                        if (this.Velocity.Y < this.maxSpeed)
                            this.Velocity.Y += this.acceleration;
                    }
                    // paddle is under the ball
                    if (this.Bounds.Center.Y > this.ball.Bounds.Center.Y)
                    {
                        if (this.Velocity.Y > -this.maxSpeed)
                            this.Velocity.Y -= this.acceleration;
                    }
                    this.Bounds.Position += this.Velocity;
                }
                else
                {
                    // paddle is above the screen center
                    if (this.Bounds.Center.Y < this.screenCenter.Y)
                    {
                        if (this.Velocity.Y < this.maxSpeed)
                            this.Velocity.Y += this.acceleration;
                    }
                    // paddle is under the screen center
                    if (this.Bounds.Center.Y > this.screenCenter.Y)
                    {
                        if (this.Velocity.Y > -this.maxSpeed)
                            this.Velocity.Y -= this.acceleration;
                    }

                    if (MathHelper.Distance(this.Bounds.Center.Y, this.screenCenter.Y) < 0.1f) 
                        this.Velocity.Y = 0f;

                    this.Velocity.Y *= this.resistence;

                    this.Bounds.Position += this.Velocity;
                }

            }

            this.ConstrainToScreenBounds();
            base.Update();
        }
        public override void Draw(SpriteBatch sb, Texture2D pixel)
        {
            //base.Draw(sb, pixel);
            base.Draw(sb);
        }
    }
}
