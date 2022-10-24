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

            ViewData["Header"] = "Alle bordspellenavonden";
            return View("GameNightsOverview", nights);
        }

        public async Task<IActionResult> GameNightsWhereUserParticipates()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var player = _iPlayerRepo.GetPlayerByEmail(user.Email);

            ViewData["Header"] = "U bent speler bij de volgende avonden";
            return View("GameNightsOverview", player.JoinedNights);
        }

        [Authorize(Policy = "GameNightOrganizer")]
        public async Task<IActionResult> GameNightsOfOrganizer()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var organizer = _iOrganizerRepo.GetOrganizerByEmail(user.Email);


            ViewData["Header"] = "Door u georganiseerde avonden";
            return View("GameNightsOverview", organizer.HostedNights);
        }

        public IActionResult GameNightDetailPage(int id) => View(_iGameNightRepo.GetGameNightById(id));

        [Authorize(Policy = "GameNightOrganizer")]
        public IActionResult NewGameNight() => View();

        [Authorize(Policy = "GameNightOrganizer")]
        [HttpPost]
        public async Task<IActionResult> NewGameNight(NewGameNightViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var gameNight = model.ConvertToGameNight();

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var organizer = _iOrganizerRepo.GetOrganizerByEmail(user.Email);
            gameNight.Organizer = organizer;

            _iGameNightRepo.AddGameNight(gameNight);

            return RedirectToAction("GameNightsOfOrganizer");
        }
    }
}

