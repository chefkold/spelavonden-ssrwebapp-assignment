namespace dutchonboard.Controllers
{
    [Authorize]
    public class BoardGameController : Controller
    {
        private readonly IBoardGameRepo _iBoardGameRepo;

        public BoardGameController(IBoardGameRepo iBoardGameRepo)
        {
            _iBoardGameRepo = iBoardGameRepo;
        }

        public IActionResult BoardGameDetailPage(int id) => View(_iBoardGameRepo.GetBoardGameById(id));
    }
}