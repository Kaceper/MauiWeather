using MauiWeather.MVVM.ViewModels;

namespace MauiWeather.MVVM.Views;

public partial class WeatherView : ContentPage
{
	public WeatherView()
	{
		InitializeComponent();
		BindingContext = new ViewModels.WeatherViewModel();

        MyEntry.Completed += (s, e) =>
        {
            if (BindingContext is WeatherViewModel vm && vm.SearchCommand.CanExecute(null))
            {
                vm.SearchCommand.Execute(null);
            }
        };
    }
}