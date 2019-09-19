using GyroBall.GameObjects;
using SkiaSharp;
using System.Collections.Generic;
using System.Numerics;
using Xamarin.Essentials;

namespace GyroBall
{
    public class GyroBallEngine
    {
        public Ball PlayerBall;
        public List<DeadlyBall> DeadlyBalls;
        public Vector2 ViewSize { get; set; }

        private Vector3 acceleration;


        public GyroBallEngine(Vector2 viewSize)
        {
            ViewSize = viewSize;

            Accelerometer.ReadingChanged += (sender, args) =>
            {
                // Smooth the reading by averaging with prior values
                acceleration = 0.5f * args.Reading.Acceleration + 0.5f * acceleration;
            };

            PlayerBall = new Ball(200, 200, 50);
            DeadlyBalls = new List<DeadlyBall>();
            DeadlyBalls.Add(new DeadlyBall(100, 300, 20, SKColors.Red));
            DeadlyBalls.Add(new DeadlyBall(200, 300, 20, SKColors.Purple));
            DeadlyBalls.Add(new DeadlyBall(300, 300, 20, SKColors.Plum));
            DeadlyBalls.Add(new DeadlyBall(400, 300, 20, SKColors.Yellow));
            DeadlyBalls.Add(new DeadlyBall(500, 300, 20, SKColors.Green));
            DeadlyBalls.Add(new DeadlyBall(600, 300, 20, SKColors.Honeydew));
            DeadlyBalls.Add(new DeadlyBall(700, 300, 20, SKColors.Orange));
            DeadlyBalls.Add(new DeadlyBall(800, 300, 20, SKColors.Teal));
        }

        public void MoveObjects()
        {
            MoveBall();
            MoveDeadlyBalls();
        }

        private void MoveBall()
        {
            PlayerBall.Position += new Vector2(-acceleration.X, acceleration.Y) * 100;

            if (PlayerBall.Position.X <= PlayerBall.Radius)
            {
                PlayerBall.Position = new Vector2(PlayerBall.Radius, PlayerBall.Position.Y);
            }

            if (PlayerBall.Position.X >= ViewSize.X - PlayerBall.Radius)
            {
                PlayerBall.Position = new Vector2(ViewSize.X - PlayerBall.Radius, PlayerBall.Position.Y);
            }

            if (PlayerBall.Position.Y <= PlayerBall.Radius)
            {
                PlayerBall.Position = new Vector2(PlayerBall.Position.X, PlayerBall.Radius);
            }

            if (PlayerBall.Position.Y >= ViewSize.Y - PlayerBall.Radius)
            {
                PlayerBall.Position = new Vector2(PlayerBall.Position.X, ViewSize.Y - PlayerBall.Radius);
            }
        }

        private void MoveDeadlyBalls()
        {
            foreach (DeadlyBall ball in DeadlyBalls)
            {
                ball.Position += ball.Direction * 100;

                if (ball.Position.X <= ball.Radius)
                {
                    ball.Position = new Vector2(ball.Radius, ball.Position.Y);
                    ball.Direction = new Vector2(ball.Direction.X * -1, ball.Direction.Y);
                }

                if (ball.Position.X >= ViewSize.X - ball.Radius)
                {
                    ball.Position = new Vector2(ViewSize.X - ball.Radius, ball.Position.Y);
                    ball.Direction = new Vector2(ball.Direction.X * -1, ball.Direction.Y);
                }

                if (ball.Position.Y <= ball.Radius)
                {
                    ball.Position = new Vector2(ball.Position.X, ball.Radius);
                    ball.Direction = new Vector2(ball.Direction.X, ball.Direction.Y * -1);
                }

                if (ball.Position.Y >= ViewSize.Y - ball.Radius)
                {
                    ball.Position = new Vector2(ball.Position.X, ViewSize.Y - ball.Radius);
                    ball.Direction = new Vector2(ball.Direction.X, ball.Direction.Y * -1);
                }
            }
        }
    }
}
