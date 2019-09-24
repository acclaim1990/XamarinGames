using GyroBall;

namespace XamarinGames.ViewModels
{
    public class GyroBallViewModel : BaseViewModel
    {
        public int Level
        {
            get => Engine.Level;
        }

        public GyroBallEngine Engine;

        public GyroBallViewModel()
        {
            Title = "Gyro Ball";

        }

    }
}