using GestureBall;
namespace XamarinGames.ViewModels
{
    public class GestureBallViewModel : BaseViewModel
    {
        private int test;

        public int Test
        {
            get { return test; }
            set { SetProperty(ref test, value); }
        }


        private GestureBallEngine engine;
        public GestureBallEngine Engine
        {
            get { return engine; }
            set { SetProperty(ref engine, value); }
        }

        public GestureBallViewModel()
        {
            // Title = "Gyro Ball";
            Test = 30;
        }
    }
}
