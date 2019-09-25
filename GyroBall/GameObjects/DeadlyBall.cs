using SkiaSharp;
using System;
using System.Numerics;

namespace GyroBall.GameObjects
{
    public class DeadlyBall : Ball
    {
        public Vector2 Direction { get; set; }
        public DeadlyBall(float x, float y, float radius) : base(x, y, radius) { InitDirection(); }

        public DeadlyBall(float x, float y, float radius, SKColor color, int level) : base(x, y, radius, color) { InitDirection(level); }

        public DeadlyBall(Vector2 position, float radius) : base(position, radius) { InitDirection(); }

        public DeadlyBall(Vector2 position, float radius, SKColor color) : base(position, radius, color) { InitDirection(); }

        private void InitDirection()
        {
            InitDirection(0);
        }

        private void InitDirection(int level)
        {
            var rand = new Random();

            if (level == 0)
            {
                Direction = new Vector2(1, 1);
                Speed = rand.Next(20, 40);
            }
            else
            {
                Direction = new Vector2(1, 1);
                Speed = rand.Next((1 + level / 20) * 10, (1 + level / 30) * 20);
            }
        }

    }
}
