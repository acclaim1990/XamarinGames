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
using XamarinGames.ViewModels;

namespace XamarinGames.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GyroBallPage : ContentPage
    {

        GyroBallViewModel viewModel;


        bool pageIsActive;
        public GyroBallPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new GyroBallViewModel();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            pageIsActive = true;
            try
            {

                Intro();

                Accelerometer.Start(SensorSpeed.Default);

                GameLoop();
            }
            catch (Exception ex)
            {
                Label label = new Label
                {
                    Text = $"{ex.Message}Sorry, an accelerometer is not available on this device",
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

        private async void Intro()
        {
            await DisplayAlert($"Game using the gyroscope.", "Move your phone to move the ball!", "Ok");
        }
        async Task GameLoop()
        {

            while (pageIsActive)
            {
                canvasView.InvalidateSurface();
                await Task.Delay(TimeSpan.FromSeconds(1.0 / 30));

                if (viewModel.Engine == null)
                {
                    if (canvasView.CanvasSize.Width > 0)
                        viewModel.Engine = new GyroBallEngine(new Vector2((float)canvasView.CanvasSize.Width, (float)canvasView.CanvasSize.Height));
                }
                else
                {
                    if (!viewModel.Engine.Dead)
                        viewModel.Engine.MainEngineLoop();
                    else
                    {
                        if (await DisplayAlert($"You reached level {viewModel.Engine.Level}! {Environment.NewLine} Restart?", "Would you like to play again?", "Yes", "No"))
                        {
                            viewModel.Engine = new GyroBallEngine(new Vector2((float)canvasView.CanvasSize.Width, (float)canvasView.CanvasSize.Height));
                        }
                        else
                        {
                            await Navigation.PopAsync();
                        }
                    }
                }
            }
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            if (viewModel.Engine != null)
            {
                canvas.DrawOval(viewModel.Engine.PlayerBall.Position.X, viewModel.Engine.PlayerBall.Position.Y, viewModel.Engine.PlayerBall.Radius, viewModel.Engine.PlayerBall.Radius, viewModel.Engine.PlayerBall.Paint);

                foreach (Ball ball in viewModel.Engine.DeadlyBalls)
                {
                    canvas.DrawOval(ball.Position.X, ball.Position.Y, ball.Radius, ball.Radius, ball.Paint);
                }
            }
        }
    }
}