using dutchonboard.Core.Domain.Models;
using dutchonboard.Core.DomainServices.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dutchonboard.Controllers
{
    [Authorize]
    public class GameNightController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IGameNightRepo _iGameNightRepo;
        private readonly IOrganizerRepo _iOrganizerRepo;
        private readonly IPlayerRepo _iPlayerRepo;

        public GameNightController(UserManager<IdentityUser> userManager, IGameNightRepo iGameNightRepo, IOrganizerRepo iOrganizerRepo, IPlayerRepo iPlayerRepo )
        {
            _userManager = userManager;
            _iGameNightRepo = iGameNightRepo;
            _iOrganizerRepo = iOrganizerRepo;
            _iPlayerRepo = iPlayerRepo;
        }

        public IActionResult AllGameNights()
        {
            var nights = _iGameNightRepo.GetAllGameNights();

            ViewData["GameNightsOverviewTitle"] = "Alle bordspellenavonden";
            return View("GameNightsOverview", nights);
        }
        public async Task<IActionResult> GameNightsWhereUserParticipates()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var player = _iPlayerRepo.GetPlayerByEmail(user.Email);

            ViewData["GameNightsOverviewTitle"] = "U heeft zich aangemeld voor de volgende avonden";
            return View("GameNightsOverview", player.JoinedNights);
        }

        [Authorize(Policy = "GameNightOrganizer")]
        public async Task<IActionResult> GameNightsOfOrganizer()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var organizer = _iOrganizerRepo.GetOrganizerByEmail(user.Email); 


            ViewData["GameNightsOverviewTitle"] = "U heeft de volgende avonden georganiseerd";
            return View("GameNightsOverview", organizer.HostedNights);
        }

       
        public IActionResult GameNightDetailPage(int id) => View(_iGameNightRepo.GetGameNightById(id));

      
    }
}
