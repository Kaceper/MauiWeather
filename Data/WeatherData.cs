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
        public Hourly hourly { get; set; }
        public ObservableCollection<Hourly2> hourly2 { get; set; } = new ObservableCollection<Hourly2>();
        public AdditionalParameters additionalParameters { get; set; } 
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
        public float precipitation { get; set; }
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
        public float[] apparent_temperature_max { get; set; }
        public float[] apparent_temperature_min { get; set; }
        public float[] uv_index_max { get; set; }
        public float[] uv_index_clear_sky_max { get; set; }
        public float[] sunshine_duration { get; set; }
        public float[] daylight_duration { get; set; }
        public int[] sunset { get; set; }
        public int[] sunrise { get; set; }
        public float[] rain_sum { get; set; }
        public float[] showers_sum { get; set; }
        public float[] snowfall_sum { get; set; }
        public float[] precipitation_sum { get; set; }
        public float[] precipitation_hours { get; set; }
        public float[] precipitation_probability_max { get; set; }
        public float[] et0_fao_evapotranspiration { get; set; }
        public float[] shortwave_radiation_sum { get; set; }
        public float[] wind_direction_10m_dominant { get; set; }
        public float[] wind_gusts_10m_max { get; set; }
        public float[] wind_speed_10m_max { get; set; }
    }

    public class Daily2
    {
        public bool isFirst { get; set; }
        public int time { get; set; }
        public int weather_code { get; set; }
        public float temperature_2m_max { get; set; }
        public float temperature_2m_min { get; set; }
        public float apparent_temperature_max { get; set; }
        public float apparent_temperature_min { get; set; }
        public float uv_index_max { get; set; }
        public float uv_index_clear_sky_max { get; set; }
        public float sunshine_duration { get; set; }
        public float daylight_duration { get; set; }
        public int sunset { get; set; }
        public int sunrise { get; set; }
        public float rain_sum { get; set; }
        public float showers_sum { get; set; }
        public float snowfall_sum { get; set; }
        public float precipitation_sum { get; set; }
        public float precipitation_hours { get; set; }
        public float precipitation_probability_max { get; set; }
        public float et0_fao_evapotranspiration { get; set; }
        public float shortwave_radiation_sum { get; set; }
        public float wind_direction_10m_dominant { get; set; }
        public float wind_gusts_10m_max { get; set; }
        public float wind_speed_10m_max { get; set; }
    }

    public class Hourly
    {
        public int[] time { get; set; }
        public int[] weather_code { get; set; }
        public float[] temperature_2m { get; set; }
        public float[] rain { get; set; }
        public float[] wind_speed_10m { get; set; }
    }

    public class Hourly2
    {
        public bool isFirst { get; set; }
        public int time { get; set; }
        public int weather_code { get; set; }
        public float temperature_2m { get; set; }
        public float rain { get; set; }
        public float wind_speed_10m { get; set; }
    }

    public class AdditionalParameters
    {
        public float rain_sum { get; set; }
        public float wind_speed_10m_max { get; set; }
        public float wind_direction_10m_dominant { get; set; }
        public int sunset { get; set; }
        public int sunrise { get; set; }
        public float uv_index_max { get; set; }
    }
}
