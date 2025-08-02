using MauiWeather.Views;

namespace MauiWeather
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            LoadPlatformSpecificResources();
        }

        private void LoadPlatformSpecificResources()
        {
            ResourceDictionary? platformDict = null;

            if (DeviceInfo.Platform == DevicePlatform.Android)
                platformDict = new MauiWeather.Resources.WeatherParametersGridRowDefsAndroid();
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
                platformDict = new MauiWeather.Resources.WeatherParametersGridRowDefsWindows();

            if (platformDict is not null)
                Resources.MergedDictionaries.Add(platformDict);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new WeatherView());
        }
    }
}