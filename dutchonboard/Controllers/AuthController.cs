using Microsoft.AspNetCore.Mvc;

namespace dutchonboard.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
