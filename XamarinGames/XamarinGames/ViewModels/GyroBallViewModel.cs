using GyroBall;

namespace XamarinGames.ViewModels
{
    public class GyroBallViewModel : BaseViewModel
    {
        private int test;

        public int Test
        {
            get { return test; }
            set { SetProperty(ref test, value); }
        }


        private GyroBallEngine engine;
        public GyroBallEngine Engine
        {
            get { return engine; }
            set { SetProperty(ref engine, value); }
        }

        public GyroBallViewModel()
        {
            // Title = "Gyro Ball";
            Test = 30;
        }
    }
}