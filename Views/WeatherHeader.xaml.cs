namespace MauiWeather.Views;

public partial class WeatherHeader : ContentView
{
    public WebView MapWebView { get; private set; }

    public WeatherHeader()
    {
        InitializeComponent();

        // Pobierz platformowo nazwany WebView
        if (DeviceInfo.Platform == DevicePlatform.Android)
            MapWebView = this.FindByName<WebView>("MapWebViewAndroid");
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            MapWebView = this.FindByName<WebView>("MapWebViewWindows");
    }
}