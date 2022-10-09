using Microsoft.AspNetCore.Mvc;

namespace dutchonboard.Controllers
{
    public class GameNightController : Controller
    {
        public IActionResult AllGameNights()
        {
            return View();
        }
    }
}
