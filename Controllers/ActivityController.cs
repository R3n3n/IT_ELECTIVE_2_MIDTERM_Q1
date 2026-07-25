using Microsoft.AspNetCore.Mvc;
using IT_ELECTIVE_2_MIDTERM_Q1.Data;
using IT_ELECTIVE_2_MIDTERM_Q1.Services;
using System.Linq;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Controllers
{
    public class ActivityController : Controller
    {
        private readonly WeatherService _weatherService;

        public ActivityController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public IActionResult Index()
        {
            return View(ActivityData.Activities);
        }

        public async Task<IActionResult> CheckWeather(DateTime date)
        {
            // Find the selected activity
            var activity = ActivityData.Activities
                .FirstOrDefault(a => a.ActivityDate.Date == date.Date);

            if (activity == null)
            {
                return NotFound();
            }

            // Get the weather forecast from the API
            var weather = await _weatherService.GetForecast();

            // Find the forecast for the activity date
            var forecast = weather.Forecasts
                .FirstOrDefault(f => f.Date.Date == activity.ActivityDate.Date);

            string weatherCondition = forecast?.Description ?? "Unknown";

            // Determine if the activity can continue
            bool canContinue = false;

            if (forecast != null)
            {
                if (activity.PreferredWeather.Equals("Clear", StringComparison.OrdinalIgnoreCase))
                {
                    canContinue =
                        weatherCondition.Contains("Sunny", StringComparison.OrdinalIgnoreCase) ||
                        weatherCondition.Contains("Clear", StringComparison.OrdinalIgnoreCase);
                }
                else if (activity.PreferredWeather.Equals("Clouds", StringComparison.OrdinalIgnoreCase))
                {
                    canContinue =
                        weatherCondition.Contains("Cloud", StringComparison.OrdinalIgnoreCase);
                }
            }

            ViewBag.Activity = activity;
            ViewBag.Forecast = weatherCondition;
            ViewBag.CanContinue = canContinue;

            return View();
        }
    }
}