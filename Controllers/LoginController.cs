using Microsoft.AspNetCore.Mvc;
using IT_ELECTIVE_2_MIDTERM_Q1.Models;

namespace IT_ELECTIVE_2_MIDTERM_Q1.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Username == "Admin" &&
                model.Password == "admin@!")
            {
                return RedirectToAction("Index", "Home", "profile");
            }

            ViewBag.Error = "Invalid username or password.";

            return View(model);
        }
    }
}