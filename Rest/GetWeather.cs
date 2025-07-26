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
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={(latitude + "").Replace(",", ".")}&longitude={(longitude + "").Replace(",", ".")}&daily=weather_code,temperature_2m_max,temperature_2m_min,rain_sum,wind_speed_10m_max&current=temperature_2m,relative_humidity_2m,apparent_temperature,is_day,rain,weather_code,wind_speed_10m&timeformat=unixtime";
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
