using SkiaSharp;
using System;
using System.Numerics;

namespace Base.Objects
{
    public class Ball
    {
        public int Id { get; set; }
        public int Speed { get; set; }
        public SKPaint Paint { get; set; }
        public Vector2 Position { get; set; }
        public float Radius { get; set; }

        /// <summary>
        /// Gets a color based on the current date where R = year, G = month, B = day
        /// </summary>
        public static SKColor GetDateBasedColor()
        {
            var now = DateTime.Now;
            byte r = (byte)(now.Year % 256);
            byte g = (byte)now.Month;
            byte b = (byte)now.Day;
            return new SKColor(r, g, b);
        }

        #region Constructors
        public Ball(float x, float y, float radius)
        {
            InitBall(new Vector2(x, y), radius, GetDateBasedColor());
        }
        public Ball(float x, float y, float radius, SKColor color)
        {
            InitBall(new Vector2(x, y), radius, color);
        }

        public Ball(Vector2 position, float radius)
        {
            InitBall(position, radius, GetDateBasedColor());
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
            Speed = 150;
        }
    }
}
