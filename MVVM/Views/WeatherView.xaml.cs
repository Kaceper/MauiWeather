namespace MauiWeather.MVVM.Views;

public partial class WeatherView : ContentPage
{
	public WeatherView()
	{
		InitializeComponent();
		BindingContext = new ViewModels.WeatherViewModel();

		if (DeviceInfo.Platform != DevicePlatform.WinUI)
		{
			SearchIcon.IsVisible = false;
		}
    }
}