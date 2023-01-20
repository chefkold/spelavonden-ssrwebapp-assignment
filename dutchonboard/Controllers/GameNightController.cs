using dutchonboard.Core.Domain.Models;
using dutchonboard.Core.DomainServices.Services;

namespace dutchonboard.Controllers
{
    [Authorize]
    public class GameNightController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IGameNightService _IGameNightService;

        private readonly IGameNightRepo _iGameNightRepo;
        private readonly IBoardGameRepo _iBoardGameRepo;
        private readonly IOrganizerRepo _iOrganizerRepo;
        private readonly IPlayerRepo _iPlayerRepo;

        public GameNightController(UserManager<IdentityUser> userManager, IGameNightRepo iGameNightRepo, IBoardGameRepo iBoardGameRepo, IOrganizerRepo iOrganizerRepo, IPlayerRepo iPlayerRepo, IGameNightService iGameNightManager)
        {
            _userManager = userManager;
            _iGameNightRepo = iGameNightRepo;
            _iBoardGameRepo = iBoardGameRepo;
            _iOrganizerRepo = iOrganizerRepo;
            _iPlayerRepo = iPlayerRepo;
            _IGameNightService = iGameNightManager;
        }

        public IActionResult AllGameNights()
        {
            var nights = _IGameNightService.GetAllGameNights();

            ViewData["PageBodyTitle"] = "Alle bordspellenavonden";
            return View("GameNightsOverview", nights);
        }

        public async Task<IActionResult> GameNightsWhereUserParticipates()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var player = _iPlayerRepo.GetPlayerByEmail(user.Email);

            ViewData["PageBodyTitle"] = "U bent speler bij de volgende avonden";
            return View("GameNightsOverview", player.JoinedNights);
        }

        [Authorize(Policy = "GameNightOrganizer")]
        public async Task<IActionResult> GameNightsOfOrganizer()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var organizer = _iOrganizerRepo.GetOrganizerByEmail(user.Email);


            ViewData["PageBodyTitle"] = "Door u georganiseerde avonden";
            ViewData["ForOrganizer"] = true;

            if (TempData.ContainsKey("GameNightDeletionError"))
            {
                ModelState.AddModelError("UpdateError", (TempData["GameNightDeletionError"] as string)!);
            }

            return View("GameNightsOverview", organizer.HostedNights);
        }

        public IActionResult GameNightDetailPage(int id)
        {
            var gameNight = _iGameNightRepo.GetGameNightById(id);
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var player = _iPlayerRepo.GetPlayerByEmail(user.Email);

            ViewData["IsOrganizer"] = false;

            if (gameNight.Organizer.Email.Equals(player.Email))
            {
                ViewData["IsOrganizer"] = true;
            }

            if (TempData.ContainsKey("EnrollmentError"))
            {
                ModelState.AddModelError("EnrollmentError", (TempData["EnrollmentError"] as string)!);
            }

            return View(_iGameNightRepo.GetGameNightById(id));

        }

        public IActionResult EnrollPlayerForGameNight(int gameNightId)
        {
            var gameNight = _iGameNightRepo.GetGameNightById(gameNightId);
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var player = _iPlayerRepo.GetPlayerByEmail(user.Email);

            try
            {
                _iGameNightRepo.EnrollPlayer(gameNight, player);
                RedirectToAction("GameNightsWhereUserParticipates");
            }
            catch (Exception error)
            {
                TempData["EnrollmentError"] = error.Message;
            }

            return RedirectToAction("GameNightDetailPage", new { id = gameNightId });
        }

        [Authorize(Policy = "GameNightOrganizer")]
        public IActionResult CreateGameNight()
        {
            var model = new GameNightViewModel
            {
                GamesDropdown =
                {
                    ChoosableBoardGames = _iBoardGameRepo.GetAllBoardGames()
                }
            };
            return View(model);
        }

        [Authorize(Policy = "GameNightOrganizer")]
        [HttpPost]
        public async Task<IActionResult> CreateGameNight(GameNightViewModel viewModel)
        {
            viewModel.GamesDropdown.ChoosableBoardGames = _iBoardGameRepo.GetAllBoardGames();
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var organizer = _iOrganizerRepo.GetOrganizerByEmail(user.Email);
            Result<GameNight> createGameNightResult = this._IGameNightService.NewGameNight(organizer, viewModel.Title!, viewModel.Description!, viewModel.IsAdultsOnly, viewModel.MaxPlayerAmount!.Value, viewModel.CreateAddress(), viewModel.DateAndTime!.Value);

            if (createGameNightResult.HasError)
            {
                ModelState.AddModelError("BusinessLogicError", createGameNightResult.ErrorMessage);
                return View(viewModel);
            }

            var games = viewModel.GamesDropdown.ChoosableBoardGames.FilterByStringListOfIds(viewModel.GamesDropdown.ChosenBoardGames);
            Result addGamesResult = _IGameNightService.AddBoardGames(createGameNightResult.Value, games);

            if (addGamesResult.HasError)
            {
                ModelState.AddModelError("BusinessLogicError", addGamesResult.ErrorMessage);
                return View(viewModel);
            }
            return RedirectToAction("GameNightsOfOrganizer");
        }

        [Authorize(Policy = "GameNightOrganizer")]
        public IActionResult EditGameNight(int id)
        {
            var gameNight = _iGameNightRepo.GetGameNightById(id);
            var model = new GameNightViewModel();
            model.FillGameNightData(gameNight);

            return View(model);
        }

        [Authorize(Policy = "GameNightOrganizer")]
        [HttpPost]
        public IActionResult EditGameNight(GameNightViewModel model)
        {
            if (!ModelState.IsValid) return View();
            // TODO
            //var updatedData = model.ConvertToGameNight();
            //updatedData.Id = model.UpdatedGameNightId;
            //try
            //{
            //    _iGameNightRepo.UpdateGameNight(updatedData);
            //    return RedirectToAction("GameNightsOfOrganizer");

            //}
            //catch (GameNightManagement.GameNightCrudException error)
            //{
            //    ModelState.AddModelError("UpdateError", error.Message);
            //}

            return View();
        }

        [Authorize(Policy = "GameNightOrganizer")]
        public IActionResult DeleteGameNight(int id)
        {
            var gameNight = _iGameNightRepo.GetGameNightById(id);
            try
            {
                _iGameNightRepo.DeleteGameNight(gameNight);
            }
            catch (GameNightManagement.GameNightCrudException error)
            {
                TempData["GameNightDeletionError"] = error.Message;
            }

            return RedirectToAction("GameNightsOfOrganizer");
        }
    }
}

