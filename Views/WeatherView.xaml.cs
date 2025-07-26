using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;

namespace MauiWeather.MVVM.Views;

public partial class WeatherView : ContentPage
{
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
            var location = await GetCoordinatesAsync(searchText);

            if (location != null)
            {
                Console.WriteLine($"Znaleziono lokalizacjê: {location.Latitude}, {location.Longitude}");
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
}