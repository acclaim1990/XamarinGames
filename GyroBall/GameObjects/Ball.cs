using SkiaSharp;
using System.Numerics;

namespace GyroBall.GameObjects
{
    public class Ball
    {
        public int Id { get; set; }
        public int Speed { get; set; }
        public SKPaint Paint { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        #region Constructors
        public Ball(float x, float y, float radius)
        {
            InitBall(new Vector2(x, y), radius, SKColors.Blue);
        }
        public Ball(float x, float y, float radius, SKColor color)
        {
            InitBall(new Vector2(x, y), radius, color);
        }

        public Ball(Vector2 position, float radius)
        {
            InitBall(position, radius, SKColors.Blue);
        }
        public Ball(Vector2 position, float radius, SKColor color)
        {
            InitBall(position, radius, color);
        }
        #endregion
        private void InitBall(Vector2 position, float radius, SKColor color)
        {
            Position = position;
            Radius = radius;

            Paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = color,
                StrokeWidth = 1
            };
            Speed = 100;
        }
    }
}
