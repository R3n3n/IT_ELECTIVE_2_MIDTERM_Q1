using Microsoft.AspNetCore.Mvc;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}