using dutchonboard.Core.Domain.Models;
using dutchonboard.Core.DomainServices.Repositories;
using dutchonboard.Core.DomainServices.Services;
using dutchonboard.ViewModels;

namespace dutchonboard.Controllers
{
    [Authorize]
    public class GameNightController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IGameNightService _iGameNightService;

        private readonly IBoardGameRepo _iBoardGameRepo;
        private readonly IOrganizerRepo _iOrganizerRepo;
        private readonly IPlayerRepo _iPlayerRepo;

        public GameNightController(UserManager<IdentityUser> userManager, IBoardGameRepo iBoardGameRepo, IOrganizerRepo iOrganizerRepo, IPlayerRepo iPlayerRepo, IGameNightService iGameNightManager)
        {
            _userManager = userManager;

            _iGameNightService = iGameNightManager;

            _iBoardGameRepo = iBoardGameRepo;
            _iOrganizerRepo = iOrganizerRepo;
            _iPlayerRepo = iPlayerRepo;
        }

        public IActionResult AllGameNights()
        {
            var nights = _iGameNightService.GetAllGameNights();

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

            if (TempData.ContainsKey("BusinessLogicError"))
            {
                ModelState.AddModelError("BusinessLogicError", (TempData["BusinessLogicError"] as string)!);
            }

            return View("GameNightsOverview", organizer.HostedNights);
        }

        public IActionResult GameNightDetailPage(int id)
        {
            var gameNight = _iGameNightService.GetGameNightById(id);
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var player = _iPlayerRepo.GetPlayerByEmail(user.Email);

            ViewData["IsOrganizer"] = false;

            if (gameNight.Organizer.Email.Equals(player.Email))
            {
                ViewData["IsOrganizer"] = true;
            }

            if (TempData.ContainsKey("BusinessLogicError"))
            {
                ModelState.AddModelError("BusinessLogicError", (TempData["BusinessLogicError"] as string)!);
            }

            return View(_iGameNightService.GetGameNightById(id));

        }

        public IActionResult EnrollPlayerForGameNight(int gameNightId)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var player = _iPlayerRepo.GetPlayerByEmail(user.Email);

            Result addPlayerResult = _iGameNightService.AddPlayerToGameNight(gameNightId, player);

            if (addPlayerResult.HasError)
            {
                TempData["BusinessLogicError"] = addPlayerResult.ErrorMessage;
                return RedirectToAction("GameNightDetailPage", new { id = gameNightId });
            }

            var gameNight = _iGameNightService.GetGameNightById(gameNightId);
            // Redirect to form for adding consumptions if required for game night, else commit enrollment
            if (gameNight.Potluck)
            {
                return RedirectToAction("PostEnrollmentPotluckForm", new { gameNightId });
            }
            _iGameNightService.CommitEnrollmentOfPlayer(gameNight, player);

            return RedirectToAction("GameNightsWhereUserParticipates");
        }

        public IActionResult PostEnrollmentPotluckForm(int gameNightId)
        {
            return View(new ConsumptionFormViewModel() { GameNightId = gameNightId });
        }

        [HttpPost]
        public IActionResult PostEnrollmentPotluckForm(ConsumptionFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(new ConsumptionFormViewModel() { GameNightId = viewModel.GameNightId });
            }

            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            var player = _iPlayerRepo.GetPlayerByEmail(user.Email);
            var gameNight = _iGameNightService.GetGameNightById(viewModel.GameNightId);
            if (!player.JoinedNights.Contains(gameNight))
            {
                // Player brought in food or drinks and can now join the game night
                _iGameNightService.CommitEnrollmentOfPlayer(gameNight, player);
            }

            _iGameNightService.AddConsumptionsToGameNight(gameNight.Id,
                new List<Consumption>()
                    { new (viewModel.Name!) { DietRestrictions = viewModel.DietRestrictions } });
            return RedirectToAction("PostEnrollmentPotluckForm", viewModel);
        }

        [Authorize(Policy = "GameNightOrganizer")]
        public IActionResult CreateGameNight()
        {
            var model = new GameNightFormViewModel
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
        public async Task<IActionResult> CreateGameNight(GameNightFormViewModel viewModel)
        {
            viewModel.GamesDropdown.ChoosableBoardGames = _iBoardGameRepo.GetAllBoardGames();
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var organizer = _iOrganizerRepo.GetOrganizerByEmail(user.Email);
            Result<GameNight> createGameNightResult = _iGameNightService.NewGameNight(organizer, viewModel.Title!, viewModel.Description!, viewModel.IsAdultsOnly, viewModel.Potluck, viewModel.MaxPlayerAmount!.Value, viewModel.CreateAddress(), viewModel.DateAndTime!.Value);

            if (createGameNightResult.HasError)
            {
                ModelState.AddModelError("BusinessLogicError", createGameNightResult.ErrorMessage);
                return View(viewModel);
            }

            var newGameNight = createGameNightResult.Value;
            var games = viewModel.GamesDropdown.ChoosableBoardGames.FilterByStringListOfIds(viewModel.GamesDropdown.ChosenBoardGames);
            Result addGamesResult = _iGameNightService.AddBoardGames(newGameNight, games);

            if (addGamesResult.HasError)
            {
                ModelState.AddModelError("BusinessLogicError", addGamesResult.ErrorMessage);
                return View(viewModel);
            }

            _iGameNightService.SaveNewGameNight(newGameNight);
            return RedirectToAction("GameNightsOfOrganizer");
        }

        [Authorize(Policy = "GameNightOrganizer")]
        public IActionResult EditGameNight(int id)
        {
            var gN = _iGameNightService.GetGameNightById(id);
            var viewModel = new GameNightFormViewModel
            {
                GamesDropdown =
                {
                    ChoosableBoardGames = _iBoardGameRepo.GetAllBoardGames(),
                }
            };
            viewModel.FillGameNightData(gN);

            return View(viewModel);
        }

        [Authorize(Policy = "GameNightOrganizer")]
        [HttpPost]
        public IActionResult EditGameNight(GameNightFormViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var games = viewModel.GamesDropdown.ChoosableBoardGames!.FilterByStringListOfIds(viewModel.GamesDropdown.ChosenBoardGames);

            var updateResult = _iGameNightService.EditGameNight(viewModel.UpdatedGameNightId, viewModel.Title!, viewModel.Description!, viewModel.IsAdultsOnly, viewModel.MaxPlayerAmount!.Value, viewModel.CreateAddress(), viewModel.DateAndTime!.Value, games);

            if (updateResult.HasError)
            {
                ModelState.AddModelError("BusinessLogicError", updateResult.ErrorMessage);
                return View(viewModel);
            }

            return RedirectToAction("GameNightsOfOrganizer");
        }

        [Authorize(Policy = "GameNightOrganizer")]
        public IActionResult DeleteGameNight(int id)
        {
            var deleteResult = _iGameNightService.DeleteGameNight(id);
            if (deleteResult.HasError)
            {
                // For reuse of the GameNight overview page, use temp data to forward error messages
                TempData["BusinessLogicError"] = deleteResult.ErrorMessage;
            }

            return RedirectToAction("GameNightsOfOrganizer");
        }

    }
}

