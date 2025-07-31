
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Platform;
using System.Reflection;

namespace MauiWeather.Views;

public partial class WeatherHeader : Microsoft.Maui.Controls.ContentView
{
    public WebView MapWebView { get; private set; }

    public WeatherHeader()
    {
        string fileName = "MauiWeather.Views.WeatherHeader.xaml";

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            fileName = "MauiWeather.Views.WeatherHeader_Android.xaml";
        }
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            fileName = "MauiWeather.Views.WeatherHeader_Windows.xaml";
        }

        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName);
        using var reader = new StreamReader(stream!);
        string xaml = reader.ReadToEnd();
        this.LoadFromXaml(xaml);

        string resourceName;

        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            MapWebView = this.FindByName<WebView>("MapWebViewAndroid");
        }
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            MapWebView = this.FindByName<WebView>("MapWebViewWindows");
        }
    }
}