using GyroBall.GameObjects;
using SkiaSharp;
using System;
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
        private double NextLevel { get; set; }
        public int Level { get; private set; }
        private DateTime LevelTimeChanged { get; set; }


        public GyroBallEngine(Vector2 viewSize)
        {
            ViewSize = viewSize;
            LevelTimeChanged = DateTime.Now;
            NextLevel = 10;
            Level = 1;

            Accelerometer.ReadingChanged += (sender, args) =>
            {
                // Smooth the reading by averaging with prior values
                acceleration = 0.5f * args.Reading.Acceleration + 0.5f * acceleration;
            };

            PlayerBall = new Ball(200, 200, 50);
            DeadlyBalls = new List<DeadlyBall>();
            DeadlyBalls.Add(new DeadlyBall(100, 300, 20, SKColors.Red));
        }

        public void MainEngineLoop()
        {
            CheckLevel();
            MoveObjects();
        }

        private void CheckLevel()
        {
            if (DateTime.Now > LevelTimeChanged.AddSeconds(NextLevel))
            {
                LevelTimeChanged = DateTime.Now;
                AddLevel();
            }
        }

        private void AddLevel()
        {
            Level++;
            DeadlyBalls.Add(new DeadlyBall(100, 300, 20, SKColors.Red));
        }

        private void MoveObjects()
        {
            MoveBall();
            MoveDeadlyBalls();
        }

        private void MoveBall()
        {
            PlayerBall.Position += new Vector2(-acceleration.X, acceleration.Y) * PlayerBall.Speed;

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
                ball.Position += ball.Direction * ball.Speed;

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
