using System.Net.Http.Json;
using System.Text.Json.Serialization;

public class AzureMapsGeocoder
{
    private readonly string apiKey = "9pckdGSw1JnbdOsmYbowjniRNjYRxeMbWxrNCR9BCqDCLagPKQFZJQQJ99BGAC5RqLJGxaSnAAAgAZMP1J6d";
    private readonly HttpClient httpClient = new();

    public async Task<List<Location>> GeocodeAsync(string address)
    {
        var url = $"https://atlas.microsoft.com/search/address/json?api-version=1.0&subscription-key={apiKey}&query={Uri.EscapeDataString(address)}";

        var response = await httpClient.GetFromJsonAsync<AzureMapsResponse>(url);

        if (response?.Results == null)
            return new List<Location>();

        return response.Results
            .Select(r => new Location
            {
                Latitude = r.Position.Latitude,
                Longitude = r.Position.Longitude
            })
            .ToList();

        return null;
    }
}

public class AzureMapsResponse
{
    [JsonPropertyName("results")]
    public List<Result>? Results { get; set; }
}

public class Result
{
    [JsonPropertyName("position")]
    public Position Position { get; set; }
}

public class Position
{
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lon")]
    public double Longitude { get; set; }
}