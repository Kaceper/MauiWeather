using MauiWeather.Data;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Platform;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Windows.Input;

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

    private List<WarningInfo> _warningMeteo;
    public List<WarningInfo> WarningsMeteo
    {
        get => _warningMeteo;
        set
        {
            if (_warningMeteo != value)
            {
                _warningMeteo = value;
                OnPropertyChanged(nameof(WarningsMeteo));
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

    private double _frameWidth;
    public double FrameWidth
    {
        get => _frameWidth;
        set
        {
            _frameWidth = value;
            OnPropertyChanged(nameof(FrameWidth));
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

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Launcher.Default.OpenAsync("https://www.imgw.pl");
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
                
                string kodGminy = LoadPlaceGminaTerytCode();

                if (!string.IsNullOrEmpty(kodGminy))
                {
                    LoadMeteroWarnings(kodGminy);
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

    private string LoadPlaceGminaTerytCode()
    {
        string kodGminy = "";

        Task.Run(async () =>
        {
            List<WarningsMeteo> meteoWarning = await new Rest.LoadMeteoWarning().GetMeteoWarning() ?? new List<WarningsMeteo>();
            if (meteoWarning.Count == 0)
            {
                kodGminy = "";
                return;
            }

            using var stream = await FileSystem.OpenAppPackageFileAsync("SIMC.csv");
            using var reader = new StreamReader(stream, Encoding.GetEncoding("windows-1250"));

            string headerLine = await reader.ReadLineAsync(); // pomijamy nag³ówek

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var pola = line.Split(',');

                if (pola.Length < 3)
                {
                    continue;
                }

                string nazwa = pola[2];

                if (string.Equals(nazwa, PlaceName, StringComparison.OrdinalIgnoreCase))
                {
                    string woj = pola[0].PadLeft(2, '0');        // WOJ
                    string pow = pola[1].PadLeft(2, '0');        // POW

                    kodGminy = woj + pow;
                }
            }
        }).Wait();

        return kodGminy;
    }

    private void LoadMeteroWarnings(string gminaTerytCode)
    {
        Task.Run(async () =>
        {
            List<WarningsMeteo> meteoWarning = await new Rest.LoadMeteoWarning().GetMeteoWarning() ?? new List<WarningsMeteo>();
            bool testWPrzedziale = false;

            // Testowo
            if (meteoWarning.Count == 0)
            {
                meteoWarning = JsonConvert.DeserializeObject<List<WarningsMeteo>>("[{\"id\":\"Sk20250731032942620\",\"nazwa_zdarzenia\":\"Burze\",\"stopien\":\"1\",\"prawdopodobienstwo\":\"70\",\"obowiazuje_do\":\"2025-07-3121:00:00\",\"obowiazuje_od\":\"2025-07-3112:00:00\",\"opublikowano\":\"2025-07-3105:29:00\",\"tresc\":\"Miejscami wyst¹pi¹ burze, którym lokalnie bêd¹ towarzyszyæ silniejsze opady deszczu do 30mm. W czasie burz porywy wiatru do 65km/h. Lokalnie drobny grad.\",\"komentarz\":\"Zagro¿enie silniejszymi opadami deszczu mog¹cymi powodowaæ zalania i podtopienia przede wszystkim dotyczy obszarów zurbanizowanych. ŒledŸ komunikaty meteorologiczne i zmiany ostrze¿eñ - https://meteo.imgw.pl/.\",\"biuro\":\"Centralne Biuro Prognoz Meteorologicznych w Warszawie\",\"teryt\":[\"1403\",\"1813\",\"1863\",\"2605\",\"1801\",\"1804\",\"1805\",\"1807\",\"1809\",\"1811\",\"1814\",\"1816\",\"1818\",\"1820\",\"1861\",\"1862\",\"2612\",\"1401\",\"2661\",\"3009\",\"3027\",\"0602\",\"0603\",\"0604\",\"0605\",\"0606\",\"0607\",\"0608\",\"0609\",\"0610\",\"0611\",\"0612\",\"0613\",\"0614\",\"0615\",\"0616\",\"0617\",\"0618\",\"0620\",\"0662\",\"0663\",\"0664\",\"1001\",\"1002\",\"1003\",\"1004\",\"1005\",\"1006\",\"1007\",\"1008\",\"1009\",\"1010\",\"1011\",\"1012\",\"1013\",\"1014\",\"1015\",\"1016\",\"1017\",\"1018\",\"1019\",\"1020\",\"1021\",\"1061\",\"1062\",\"1063\",\"1802\",\"1806\",\"1808\",\"1810\",\"1812\",\"1815\",\"1817\",\"1819\",\"1864\",\"1821\",\"2606\",\"1405\",\"1406\",\"1407\",\"1408\",\"1409\",\"1411\",\"1412\",\"1414\",\"1415\",\"1416\",\"1417\",\"1418\",\"1421\",\"1423\",\"1424\",\"1425\",\"1426\",\"1428\",\"1429\",\"1430\",\"1432\",\"1433\",\"1434\",\"1435\",\"1436\",\"1438\",\"1461\",\"1463\",\"1464\",\"1465\",\"2601\",\"2602\",\"2604\",\"2607\",\"2608\",\"2609\",\"2610\",\"2611\",\"2613\",\"1803\",\"1404\"]}]");
                testWPrzedziale = true;
            }

            WarningsMeteo ??= new List<WarningInfo>();

            foreach (WarningsMeteo warningsMeteo in meteoWarning.ToList())
            {
                string obowiazujeOdStr = "2025-07-31 12:00:00";
                string obowiazujeDoStr = "2025-07-31 21:00:00";

                DateTime obowiazujeOd = DateTime.ParseExact(obowiazujeOdStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime obowiazujeDo = DateTime.ParseExact(obowiazujeDoStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                DateTime teraz = DateTime.Now;

                bool wPrzedziale = teraz >= obowiazujeOd && teraz <= obowiazujeDo;

                if ((wPrzedziale || testWPrzedziale) && warningsMeteo.teryt.Contains(gminaTerytCode) && WarningsMeteo.FirstOrDefault(w => w.tresc == warningsMeteo.tresc) == null)
                {
                    WarningsMeteo.Add(new WarningInfo()
                    {
                        obowiazuje_od = warningsMeteo.obowiazuje_od,
                        obowiazuje_do = warningsMeteo.obowiazuje_do,
                        tresc = warningsMeteo.tresc,
                    });
                }
            }
        }).Wait();
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

        double screenWidth = Application.Current?.MainPage?.Width - 30 ?? 360;

        FrameWidth = DeviceInfo.Platform == DevicePlatform.Android
            ? screenWidth
            : 600;

        WeatherHeader.MapWebView.Source = htmlSource;
    }

    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}