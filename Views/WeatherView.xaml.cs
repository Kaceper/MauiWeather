using MauiWeather.Data;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Platform;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;

namespace MauiWeather.Views;

public partial class WeatherView : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private WeatherData _weatherData;
    public WeatherData WeatherData
    {
        get => _weatherData;
        set
        {
            if (_weatherData != value)
            {
                _weatherData = value;
                OnPropertyChanged(nameof(WeatherData));
            }
        }
    }

    private string _placeName;
    public string PlaceName
    {
        get => _placeName;
        set
        {
            if (_placeName != value)
            {
                _placeName = value;
                OnPropertyChanged(nameof(PlaceName));
            }
        }
    }

    private DateTime _date = DateTime.Now;
    public DateTime Date
    {
        get => _date;
        set
        {
            if (_date != value)
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
    }

    public WeatherView()
    {
        InitializeComponent();

        BindingContext = this;

        MyEntry.Text = "Inowroc³aw";

        MyEntry.Completed += OnEntryCompleted;

        OnEntryCompleted(null, null);
        MyEntry.Unfocus();
    }

    private async void OnEntryCompleted(object? sender, EventArgs e)
    {
        string searchText = MyEntry.Text;

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            PlaceName = searchText;

            var location = await GetCoordinatesAsync(searchText);

            if (location != null)
            {
                WeatherData = await new Rest.GetWeather().GetWeatherAsync(location.Latitude, location.Longitude);

                bool isFirst = true;
                for (int i = 0; i < WeatherData.daily.time.Length; i++)
                {
                    WeatherData.daily2.Add(new Daily2
                    {
                        isFirst = isFirst,
                        time = WeatherData.daily.time[i],
                        weather_code = WeatherData.daily.weather_code[i],
                        temperature_2m_max = WeatherData.daily.temperature_2m_max[i],
                        temperature_2m_min = WeatherData.daily.temperature_2m_min[i],
                        rain_sum = WeatherData.daily.rain_sum[i],
                        wind_speed_10m_max = WeatherData.daily.wind_speed_10m_max[i]
                    });

                    if (isFirst)
                    {
                        isFirst = false;
                    }
                }

                isFirst = true;
                for (int i = 0; i < WeatherData.hourly.time.Length; i++)
                {
                    if (WeatherData.hourly.time[i] <= DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                    {
                        continue;
                    }

                    WeatherData.hourly2.Add(new Hourly2
                    {
                        isFirst = isFirst,
                        time = WeatherData.hourly.time[i],
                        weather_code = WeatherData.hourly.weather_code[i],
                        temperature_2m = WeatherData.hourly.temperature_2m[i],
                        rain = WeatherData.hourly.rain[i],
                        wind_speed_10m = WeatherData.hourly.wind_speed_10m[i]
                    });

                    if (isFirst)
                    {
                        isFirst = false;
                    }

                    if (WeatherData.hourly2.Count == 7)
                    {
                        break;
                    }
                }

                ReloadMap();
            }
            else
            {
                Console.WriteLine("Nie znaleziono lokalizacji");
            }
        }
    }

    private async Task<Location> GetCoordinatesAsync(string address)
    {
        IEnumerable<Location> locations = new List<Location>();

        try
        {
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                // Na Windows korzystam z Azure Maps REST API (koniecznie uzyskanie klucza API)
                locations = await new AzureMapsGeocoder().GeocodeAsync(address);
            }
            else
            {
                locations = await Geocoding.Default.GetLocationsAsync(address);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Uwaga", "Wyst¹pi³ b³¹d podczas pobierania wspó³rzêdnych lokalizacji:\n\n" + ex.Message, "OK");
        }

        return locations?.FirstOrDefault();
    }

    private void ReloadMap()
    {
        HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        Task.Run(async () =>
        {
            string html;

            using (var stream = await FileSystem.OpenAppPackageFileAsync("azuremap.html"))
            using (var reader = new StreamReader(stream))
            {
                html = await reader.ReadToEndAsync();
            }

            double lat = WeatherData.latitude;
            double lon = WeatherData.longitude;
            int zoom = 12;

            html = html.Replace("LAT_PLACEHOLDER", lat.ToString(CultureInfo.InvariantCulture))
                        .Replace("LON_PLACEHOLDER", lon.ToString(CultureInfo.InvariantCulture))
                        .Replace("ZOOM_PLACEHOLDER", zoom.ToString());


            htmlSource = new HtmlWebViewSource
            {
                Html = html
            };
        }).Wait();

        WeatherHeader.MapWebView.Source = htmlSource;
    }

    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}