using Microsoft.AspNetCore.Mvc;
using IT_ELECTIVE_2_MIDTERM_Q1.Data;
using System.Linq;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult Index()
        {
            return View(ActivityData.Activities);
        }

        public IActionResult CheckWeather(DateTime date)
        {
            var activity = ActivityData.Activities
                .FirstOrDefault(a => a.ActivityDate.Date == date.Date);

            if (activity == null)
            {
                return NotFound();
            }

            // Temporary until Weather API is connected
            string forecast = "Clear";

            bool canContinue = forecast.Equals(
                activity.PreferredWeather,
                System.StringComparison.OrdinalIgnoreCase);

            ViewBag.Activity = activity;
            ViewBag.Forecast = forecast;
            ViewBag.CanContinue = canContinue;

            return View();
        }
    }
}