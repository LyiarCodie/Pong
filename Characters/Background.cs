using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pongo2;

namespace Pongo3.Characters
{
    internal class Background
    {
        private Vector2 screenSize;
        private RectangleF spawnRegion;
        private Vector2[] particlePos;
        private Vector2[] particleDirections;
        private int particleSets;
        private int particlesPerSet;
        private int particleSize;
        private Vector2[] initialPositions;
        private float moveSpeed;
        public Background(Vector2 screenSize)
        {
            this.screenSize = screenSize;
            this.spawnRegion = new RectangleF(Vector2.Zero, new Vector2(64f));
            this.spawnRegion.Center = this.screenSize * 0.5f;
            this.particleSets = 5;
            this.particlesPerSet = 8;
            this.particleSize = 2;
            this.particlePos = new Vector2[this.particlesPerSet * this.particleSets];
            this.moveSpeed = 2f;

            this.SetParticlesInitialPosition();
        }
        public void Draw(SpriteBatch sb, Texture2D pixel)
        {
            //sb.Draw(pixel, this.spawnRegion.ToRectangle(), Color.MonoGameOrange * 0.5f);

            for (int i = 0; i < this.particlePos.Length; i++)
            {
                sb.Draw(pixel, new Rectangle(this.particlePos[i].ToPoint(), new Point(this.particleSize)), Color.White);
            }
        }
        public void Update()
        {
            for (int i = 0; i < this.particlePos.Length; i++)
            {
                this.particlePos[i] += this.particleDirections[i % this.particlesPerSet] * this.moveSpeed;

                if ((this.particlePos[i].X + this.particleSize) < 0f || this.particlePos[i].X > this.screenSize.X ||
                    (this.particlePos[i].Y + this.particleSize) < 0f || this.particlePos[i].Y > this.screenSize.Y)
                {
                    this.particlePos[i] = this.initialPositions[i % this.particlesPerSet];
                }
            }
        }
        private void SetParticlesInitialPosition()
        {
            this.initialPositions = [
                new Vector2(this.spawnRegion.Left, this.spawnRegion.Top),
                new Vector2(this.spawnRegion.Center.X, this.spawnRegion.Top),
                new Vector2(this.spawnRegion.Right, this.spawnRegion.Top),
                new Vector2(this.spawnRegion.Left, this.spawnRegion.Center.Y),
                new Vector2(this.spawnRegion.Right, this.spawnRegion.Center.Y),
                new Vector2(this.spawnRegion.Left, this.spawnRegion.Bottom),
                new Vector2(this.spawnRegion.Center.X, this.spawnRegion.Bottom),
                new Vector2(this.spawnRegion.Right, this.spawnRegion.Bottom)
            ];

            this.particleDirections = [
                new Vector2(-1f),
                new Vector2(0f, -1f),
                new Vector2(1f, -1f),
                new Vector2(-1f, 0f),
                new Vector2(1f, 0f),
                new Vector2(-1f, 1f),
                new Vector2(0f, 1f),
                new Vector2(1f)
            ];

            float distance = 80f;
            Vector2[] distances = [
                new Vector2(-distance),
                new Vector2(0f, -distance),
                new Vector2(distance, -distance),
                new Vector2(-distance, 0f),
                new Vector2(distance, 0f),
                new Vector2(-distance, distance),
                new Vector2(0f, distance),
                new Vector2(distance)
            ];

            int posId = 0;
            // 24 particles -> 8 particles per set -> 24 / 8 = 3 set
            for (int i = 0; i < this.particleSets; i++)
            {
                for (int j = 0; j < this.particlesPerSet; j++)
                {
                    this.particlePos[posId] = this.initialPositions[j] + distances[j] * i;
                    posId++;
                }
            }

            /* old massive code
            //float distance = 80f;
            //this.particlesPos[0] = new Vector2(this.spawnRegion.Left, this.spawnRegion.Top);
            //this.particlesPos[1] = new Vector2(this.spawnRegion.Center.X, this.spawnRegion.Top);
            //this.particlesPos[2] = new Vector2(this.spawnRegion.Right, this.spawnRegion.Top);
            //this.particlesPos[3] = new Vector2(this.spawnRegion.Left, this.spawnRegion.Center.Y);
            //this.particlesPos[4] = new Vector2(this.spawnRegion.Right, this.spawnRegion.Center.Y);
            //this.particlesPos[5] = new Vector2(this.spawnRegion.Left, this.spawnRegion.Bottom);
            //this.particlesPos[6] = new Vector2(this.spawnRegion.Center.X, this.spawnRegion.Bottom);
            //this.particlesPos[7] = new Vector2(this.spawnRegion.Right, this.spawnRegion.Bottom);

            //this.particlesPos[8] = new Vector2(this.spawnRegion.Left, this.spawnRegion.Top) + new Vector2(-distance);
            //this.particlesPos[9] = new Vector2(this.spawnRegion.Center.X, this.spawnRegion.Top) + new Vector2(0f, -distance);
            //this.particlesPos[10] = new Vector2(this.spawnRegion.Right, this.spawnRegion.Top) + new Vector2(distance, -distance);
            //this.particlesPos[11] = new Vector2(this.spawnRegion.Left, this.spawnRegion.Center.Y) + new Vector2(-distance, 0f);
            //this.particlesPos[12] = new Vector2(this.spawnRegion.Right, this.spawnRegion.Center.Y) + new Vector2(distance, 0f);
            //this.particlesPos[13] = new Vector2(this.spawnRegion.Left, this.spawnRegion.Bottom) + new Vector2(-distance, distance);
            //this.particlesPos[14] = new Vector2(this.spawnRegion.Center.X, this.spawnRegion.Bottom) + new Vector2(0f, distance);
            //this.particlesPos[15] = new Vector2(this.spawnRegion.Right, this.spawnRegion.Bottom) + new Vector2(distance);

            //this.particlesPos[16] = new Vector2(this.spawnRegion.Left, this.spawnRegion.Top) + new Vector2(-distance) * 2f;
            //this.particlesPos[17] = new Vector2(this.spawnRegion.Center.X, this.spawnRegion.Top) + new Vector2(0f, -distance) * 2f;
            //this.particlesPos[18] = new Vector2(this.spawnRegion.Right, this.spawnRegion.Top) + new Vector2(distance, -distance) * 2f;
            //this.particlesPos[19] = new Vector2(this.spawnRegion.Left, this.spawnRegion.Center.Y) + new Vector2(-distance, 0f) * 2f;
            //this.particlesPos[20] = new Vector2(this.spawnRegion.Right, this.spawnRegion.Center.Y) + new Vector2(distance, 0f) * 2f;
            //this.particlesPos[21] = new Vector2(this.spawnRegion.Left, this.spawnRegion.Bottom) + new Vector2(-distance, distance) * 2f;
            //this.particlesPos[22] = new Vector2(this.spawnRegion.Center.X, this.spawnRegion.Bottom) + new Vector2(0f, distance) * 2f;
            //this.particlesPos[23] = new Vector2(this.spawnRegion.Right, this.spawnRegion.Bottom) + new Vector2(distance) * 2f;
            */
        }
    }
}
