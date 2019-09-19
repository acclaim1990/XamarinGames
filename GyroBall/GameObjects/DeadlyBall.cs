using SkiaSharp;
using System;
using System.Numerics;

namespace GyroBall.GameObjects
{
    public class DeadlyBall : Ball
    {
        public Vector2 Direction { get; set; }
        public DeadlyBall(float x, float y, float radius) : base(x, y, radius) { InitDirection(); }

        public DeadlyBall(float x, float y, float radius, SKColor color) : base(x, y, radius, color) { InitDirection(); }

        public DeadlyBall(Vector2 position, float radius) : base(position, radius) { InitDirection(); }

        public DeadlyBall(Vector2 position, float radius, SKColor color) : base(position, radius, color) { InitDirection(); }

        private void InitDirection()
        {
            Direction = new Vector2(1, 1);
            var rand = new Random();
            Speed = rand.Next(10,50);
        }

    }
}
