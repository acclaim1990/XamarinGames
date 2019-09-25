using GyroBall.GameObjects;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;

namespace GyroBall
{
    public class GyroBallEngine : INotifyPropertyChanged
    {
        public Ball PlayerBall;
        public List<DeadlyBall> DeadlyBalls;
        public Vector2 ViewSize { get; set; }

        private Vector3 acceleration;
        private double NextLevel { get; set; }
        public bool Dead { get; set; }

        private int level;

        public int Level
        {
            get { return level; }
            set { SetProperty(ref level, value); }
        }

        private DateTime LevelTimeChanged { get; set; }


        public GyroBallEngine(Vector2 viewSize) : base()
        {
            ViewSize = viewSize;
            LevelTimeChanged = DateTime.Now;
            NextLevel = 10;
            Level = 0;
            Dead = false;

            Accelerometer.ReadingChanged += (sender, args) =>
            {
                // Smooth the reading by averaging with prior values
                acceleration = 0.5f * args.Reading.Acceleration + 0.5f * acceleration;
            };

            PlayerBall = new Ball(200, 200, 50);
            DeadlyBalls = new List<DeadlyBall>();
            AddLevel();
        }

        public void MainEngineLoop()
        {
            CheckLevel();
            MoveObjects();

            if (CheckDeadlyBallCollision())
                Dead = true;

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
            DeadlyBalls.Add(new DeadlyBall(100, 300, 20, SKColors.Red,Level));
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

        private bool CheckDeadlyBallCollision()
        {
            foreach (DeadlyBall ball in DeadlyBalls)
            {
                var radius = PlayerBall.Radius + ball.Radius;
                var deltaX = PlayerBall.Position.X - ball.Position.X;
                var deltaY = PlayerBall.Position.Y - ball.Position.Y;

                if (deltaX * deltaX + deltaY * deltaY <= radius * radius)
                    return true;
            }
            return false;
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
