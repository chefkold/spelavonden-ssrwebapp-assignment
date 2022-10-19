using dutchonboard.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace dutchonboard.Controllers
{
    public class GameNightController : Controller
    {
        public IActionResult AllGameNights()
        {
            var nights = new List<GameNight>();
            return View(nights);
        }

        public IActionResult GameNightDetailPage() => View(null);
    }
}
