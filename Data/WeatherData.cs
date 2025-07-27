using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Data
{
    public class WeatherData
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public float generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public float elevation { get; set; }
        public Current_Units current_units { get; set; }
        public Current current { get; set; }
        public Daily_Units daily_units { get; set; }
        public Daily daily { get; set; }
        public ObservableCollection<Daily2> daily2 { get; set; } = new ObservableCollection<Daily2>();
    }

    public class Current_Units
    {
        public string time { get; set; }
        public string interval { get; set; }
        public string temperature_2m { get; set; }
        public string relative_humidity_2m { get; set; }
        public string apparent_temperature { get; set; }
        public string is_day { get; set; }
        public string rain { get; set; }
        public string weather_code { get; set; }
    }

    public class Current
    {
        public int time { get; set; }
        public int interval { get; set; }
        public float temperature_2m { get; set; }
        public float wind_speed_10m { get; set; }
        public int relative_humidity_2m { get; set; }
        public float apparent_temperature { get; set; }
        public int is_day { get; set; }
        public float rain { get; set; }
        public int weather_code { get; set; }
    }

    public class Daily_Units
    {
        public string time { get; set; }
        public string weather_code { get; set; }
        public string temperature_2m_max { get; set; }
        public string temperature_2m_min { get; set; }
        public string rain_sum { get; set; }
        public string wind_speed_10m_max { get; set; }
    }

    public class Daily
    {
        public int[] time { get; set; }
        public int[] weather_code { get; set; }
        public float[] temperature_2m_max { get; set; }
        public float[] temperature_2m_min { get; set; }
        public float[] rain_sum { get; set; }
        public float[] wind_speed_10m_max { get; set; }
    }

    public class Daily2
    {
        public int time { get; set; }
        public int weather_code { get; set; }
        public float temperature_2m_max { get; set; }
        public float temperature_2m_min { get; set; }
        public float rain_sum { get; set; }
        public float wind_speed_10m_max { get; set; }
    }
}
