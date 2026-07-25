using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
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
            string apiKey = _configuration["WeatherApiSettings:ApiKey"];
            string baseUrl = _configuration["WeatherApiSettings:BaseUrl"];
            string url = $"{baseUrl}forecast?q=Manila,PH&appid={apiKey}&units=metric";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            var viewModel = new WeatherViewModel();

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(jsonString);

                viewModel.City = "Philippines (Manila)";

                var list = data["list"] as JArray;
                if (list != null)
                {
                    // Filters the 3-hour chunks down to 1 entry per day (midday) for the 5-day window
                    var dailyList = list
                        .Where(x => x["dt_txt"]?.ToString().Contains("12:00:00") == true)
                        .Select(x => new DailyForecast
                        {
                            Date = System.DateTime.Parse(x["dt_txt"]?.ToString()).ToString("MMM dd, yyyy (ddd)"),
                            Temperature = x["main"]?["temp"]?.Value<double>() ?? 0,
                            Description = x["weather"]?[0]?["description"]?.ToString(),
                            Icon = x["weather"]?[0]?["icon"]?.ToString(),
                            Humidity = x["main"]?["humidity"]?.Value<double>() ?? 0
                        }).Take(5).ToList();

                    viewModel.Forecasts = dailyList;
                }
            }

            return View(viewModel);
        }
    }
}