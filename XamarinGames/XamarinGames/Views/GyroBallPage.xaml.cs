using GyroBall;
using GyroBall.GameObjects;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Numerics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinGames.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GyroBallPage : ContentPage
    {
        GyroBallEngine engine;
        bool pageIsActive;
        public GyroBallPage()
        {
            InitializeComponent();
            engine = new GyroBallEngine(new Vector2((float)canvasView.CanvasSize.Width, (float)canvasView.CanvasSize.Height));
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            pageIsActive = true;
            try
            {
                Accelerometer.Start(SensorSpeed.Default);
                GameLoop();
            }
            catch
            {
                Label label = new Label
                {
                    Text = "Sorry, an accelerometer is not available on this device",
                    FontSize = 24,
                    TextColor = Color.White,
                    BackgroundColor = Color.DarkGray,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Margin = new Thickness(48, 0)
                };


            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            pageIsActive = false;
            Accelerometer.Stop();
        }
        async Task GameLoop()
        {

            while (pageIsActive)
            {
                canvasView.InvalidateSurface();
                await Task.Delay(TimeSpan.FromSeconds(1.0 / 30));
                engine.MoveObjects();
            }
        }
        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            canvas.DrawOval(engine.PlayerBall.Position.X, engine.PlayerBall.Position.Y, engine.PlayerBall.Radius, engine.PlayerBall.Radius, engine.PlayerBall.Paint);

            foreach (Ball ball in engine.DeadlyBalls)
            {
                canvas.DrawOval(ball.Position.X, ball.Position.Y, ball.Radius, ball.Radius, ball.Paint);
            }
        }
    }
}