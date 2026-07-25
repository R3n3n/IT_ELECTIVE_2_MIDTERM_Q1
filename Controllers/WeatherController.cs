using IT_ELECTIVE_2_MIDTERM_Q1.Services;
using Microsoft.AspNetCore.Mvc;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<IActionResult> Index()
        {
            var weather = await _weatherService.GetForecast();

            return View(weather);
        }
    }
}