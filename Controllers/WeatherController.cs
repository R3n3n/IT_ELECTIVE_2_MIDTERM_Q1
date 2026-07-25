using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IT_ELECTIVE_2_MIDTERM_Q1.Models;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Using your WeatherAPI key
            string apiKey = "7e156cdd015e4e29b6640448262507";

            // The endpoint for forecast data up to 14 days
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
                    viewModel.Forecasts = new List<DailyForecast>();

                    foreach (var day in forecastDays)
                    {
                        viewModel.Forecasts.Add(new DailyForecast
                        {
                            // Parse date to a readable format
                            Date = DateTime.Parse(day["date"]?.ToString()).ToString("MMM dd (ddd)"),
                            Temperature = day["day"]?["avgtemp_c"]?.Value<double>() ?? 0,
                            Description = day["day"]?["condition"]?["text"]?.ToString(),
                            // WeatherAPI provides URLs starting with "//", so we prepend "https:"
                            Icon = "https:" + day["day"]?["condition"]?["icon"]?.ToString(),
                            Humidity = day["day"]?["avghumidity"]?.Value<double>() ?? 0
                        });
                    }
                }
            }

            return View(viewModel);
        }
    }
}