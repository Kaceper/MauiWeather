using MauiWeather.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiWeather.Rest
{
    public class LoadMeteoWarning
    {
        private HttpClient httpClient = new();

        public LoadMeteoWarning() { }

        public async Task<List<WarningsMeteo>> GetMeteoWarning()
        {
            string url = "https://danepubliczne.imgw.pl/api/data/warningsmeteo";
            try
            {
                var response = await httpClient.GetFromJsonAsync<List<WarningsMeteo>>(url);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas pobierania ostzeżeń o pogodzie: {ex.Message}");
                return null;
            }
        }
    }
}
