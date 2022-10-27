using dutchonboard.Core.DomainServices.Managers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dutchonboard.Controllers
{
    [Authorize]
    public class GameNightController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly IGameNightRepo _iGameNightRepo;
        private readonly IOrganizerRepo _iOrganizerRepo;
        private readonly IPlayerRepo _iPlayerRepo;

        public GameNightController(UserManager<IdentityUser> userManager, IGameNightRepo iGameNightRepo, IOrganizerRepo iOrganizerRepo, IPlayerRepo iPlayerRepo)
        {
            _userManager = userManager;
            _iGameNightRepo = iGameNightRepo;
            _iOrganizerRepo = iOrganizerRepo;
            _iPlayerRepo = iPlayerRepo;
        }

        public IActionResult AllGameNights()
        {
            var nights = _iGameNightRepo.GetAllGameNights();

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

        public IActionResult GameNightDetailPage(int id) => View(_iGameNightRepo.GetGameNightById(id));

        [Authorize(Policy = "GameNightOrganizer")]
        public IActionResult CreateGameNight()
        {
            var model = new GameNightViewModel
            {
                GamesDropdown = 
                {
                    ChoosableBoardGames = new List<BoardGame>()
                    {
                        new BoardGame()
                        {
                            Id = 1,
                            Name = "Henk"
                        },
                        new BoardGame()
                        {
                            Id = 2,
                            Name = "Henk"
                        }

                    }
                }
            };
            return View(model);
        }

        [Authorize(Policy = "GameNightOrganizer")]
        [HttpPost]
        public async Task<IActionResult> CreateGameNight(GameNightViewModel model)
        {
            foreach (var s in model.GamesDropdown.ChosenBoardGames)
            {
                Console.WriteLine(s);
            }
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}

            //var gameNight = model.ConvertToGameNight();

            //var user = await _userManager.GetUserAsync(HttpContext.User);
            //var organizer = _iOrganizerRepo.GetOrganizerByEmail(user.Email);
            //gameNight.Organizer = organizer;



            //_iGameNightRepo.AddGameNight(gameNight);

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

            var updatedData = model.ConvertToGameNight();
            updatedData.Id = model.UpdatedGameNightId;
            try
            {
                _iGameNightRepo.UpdateGameNight(updatedData);
                return RedirectToAction("GameNightsOfOrganizer");

            }
            catch (GameNightManagement.GameNightCrudException error)
            {
                ModelState.AddModelError("UpdateError", error.Message);
            }

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

        //private List<MultiSelectList> PrepareDropDownOfBoardGames()
        //{
        //    var list = new List<MultiSelectList>();
        //    list.Add(new MultiSelectList());
        //    return list;
        //}
    }
}

