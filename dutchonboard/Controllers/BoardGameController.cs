namespace dutchonboard.Controllers
{
    [Authorize]
    public class BoardGameController : Controller
    {

        public IActionResult BoardGameDetailPage()
        {
            return View();
        }
    }
}