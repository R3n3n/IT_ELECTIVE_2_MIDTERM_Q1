using IT_ELECTIVE_2_MIDTERM_Q1.Models;
using Newtonsoft.Json.Linq;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Services
{
    public class WeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherViewModel> GetForecast()
        {
            // Your WeatherAPI key
            string apiKey = "7e156cdd015e4e29b6640448262507";

            // WeatherAPI endpoint
            string url = $"https://api.weatherapi.com/v1/forecast.json?key={apiKey}&q=Manila,PH&days=14";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            var viewModel = new WeatherViewModel();

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(jsonString);

                string cityName = data["location"]?["name"]?.ToString();
                string country = data["location"]?["country"]?.ToString();
                viewModel.City = $"{cityName}, {country}";

                var forecastDays = data["forecast"]?["forecastday"] as JArray;

                if (forecastDays != null)
                {
                    foreach (var day in forecastDays)
                    {
                        viewModel.Forecasts.Add(new DailyForecast
                        {
                            Date = DateTime.Parse(day["date"]?.ToString()!),
                            Temperature = day["day"]?["avgtemp_c"]?.Value<double>() ?? 0,
                            Description = day["day"]?["condition"]?["text"]?.ToString(),
                            Icon = "https:" + day["day"]?["condition"]?["icon"]?.ToString(),
                            Humidity = day["day"]?["avghumidity"]?.Value<double>() ?? 0
                        });
                    }
                }
            }

            return viewModel;
        }
    }
}