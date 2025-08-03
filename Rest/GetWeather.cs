using MauiWeather.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Rest
{
    public class GetWeather
    {
        private HttpClient httpClient = new();

        public GetWeather() { }

        public async Task<WeatherData> GetWeatherAsync(double latitude, double longitude)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={(latitude + "").Replace(",", ".")}&longitude={(longitude + "").Replace(",", ".")}&daily=weather_code,temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,uv_index_max,uv_index_clear_sky_max,sunshine_duration,daylight_duration,sunset,sunrise,rain_sum,showers_sum,snowfall_sum,precipitation_sum,precipitation_hours,precipitation_probability_max,et0_fao_evapotranspiration,shortwave_radiation_sum,wind_direction_10m_dominant,wind_gusts_10m_max,wind_speed_10m_max&hourly=temperature_2m,rain,weather_code,wind_speed_10m&current=temperature_2m,relative_humidity_2m,apparent_temperature,is_day,rain,weather_code,wind_speed_10m&timeformat=unixtime&start={DateTimeOffset.UtcNow.ToUnixTimeSeconds()}&end={DateTimeOffset.UtcNow.AddHours(24).ToUnixTimeSeconds()}";
            try
            {
                var response = await httpClient.GetFromJsonAsync<WeatherData>(url);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania danych pogodowych: {ex.Message}");
                return null;
            }
        }
    }
}
