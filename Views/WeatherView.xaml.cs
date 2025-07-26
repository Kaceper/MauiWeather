using MauiWeather.Data;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
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
        MyEntry.Completed += OnEntryCompleted;
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
                if (WeatherData != null)
                {

                }
                else
                {
                    Console.WriteLine("Nie uda³o siê pobraæ danych pogodowych.");
                }
                // Mo¿esz np. ustawiæ tekst gdzieœ w UI, dodaæ mapê itp.
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


    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}