using Microsoft.Xna.Framework;

namespace Pongo2
{
    public class RectangleF
    {
        public Vector2 Position;
        public Vector2 Size;
        public float X 
        {
            get => Position.X;
            set => this.Position = new Vector2(value, this.Position.Y);
        }
        public float Y
        {
            get => Position.Y;
            set => this.Position = new Vector2(this.Position.X, value);
        }
        public float Width
        {
            get => Size.X;
            set => Size = new Vector2(value, Size.Y);
        }
        public float Height
        {
            get => Size.Y;
            set => Size = new Vector2(Size.X, value);
        }
        public float Left
        {
            get => X;
            set => X = value;
        }
        public float Right
        {
            get => X + Width;
            set => X = value - Width;
        }
        public float Top
        {
            get => Y;
            set => Y = value;
        }
        public float Bottom
        {
            get => Y + Height;
            set => Y = value - Height;
        }
        public Vector2 Center
        {
            get => new Vector2(this.X + this.Width * 0.5f, this.Y + this.Height * 0.5f);
            set => this.Position = new Vector2(value.X - this.Width * 0.5f, value.Y + this.Height * 0.5f);
        }
        public RectangleF(float x, float y, float width, float height)
        {
            Position = new Vector2(x, y);
            Size = new Vector2(width, height);
        }
        public RectangleF(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }
        public Rectangle ToRectangle()
        {
            return new Rectangle(Position.ToPoint(), Size.ToPoint());
        }
        public bool Intersects(RectangleF other)
        {
            return Top < other.Bottom && Right > other.Left && Bottom > other.Top && Left < Right;
        }
    }
}