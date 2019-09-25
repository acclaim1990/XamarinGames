using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinGames.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameSelectPage : ContentPage
    {
        public GameSelectPage()
        {
            InitializeComponent();
        }

        async void BtnGyroBall_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GyroBallPage());
        }
    }
}