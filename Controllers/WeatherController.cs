using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
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
        public IActionResult Index()
        {
            return View(new WeatherViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                ModelState.AddModelError("", "Please enter a city name.");
                return View();
            }

            string apiKey = _configuration["WeatherApiSettings:ApiKey"];
            string baseUrl = _configuration["WeatherApiSettings:BaseUrl"];
            string url = $"{baseUrl}weather?q={city}&appid={apiKey}&units=metric";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                JObject weatherData = JObject.Parse(jsonString);

                var viewModel = new WeatherViewModel
                {
                    City = weatherData["name"]?.ToString(),
                    Temperature = weatherData["main"]?["temp"]?.Value<double>() ?? 0,
                    Description = weatherData["weather"]?[0]?["description"]?.ToString(),
                    Icon = weatherData["weather"]?[0]?["icon"]?.ToString(),
                    Humidity = weatherData["main"]?["humidity"]?.Value<double>() ?? 0
                };

                return View(viewModel);
            }

            ModelState.AddModelError("", "City not found or API error.");
            return View();
        }
    }
}
