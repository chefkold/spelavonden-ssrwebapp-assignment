using dutchonboard.Core.Domain.Models;
using dutchonboard.Core.DomainServices.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dutchonboard.Controllers
{
    [Authorize]
    public class GameNightController : Controller
    {
        private readonly IGameNightRepo _iGameNightRepo;

        public GameNightController(IGameNightRepo iGameNightRepo)
        {
            _iGameNightRepo = iGameNightRepo;
        }

        public IActionResult AllGameNights()
        {
            var nights = _iGameNightRepo.GetAllGameNights();
            return View(nights);
        }

        [Authorize(Policy = "GameNightOrganizer")]
        public IActionResult MyGameNights()
        {
            throw new NotImplementedException();
        }

        public IActionResult GameNightDetailPage(int id) => View(_iGameNightRepo.GetGameNightById(id));
    }
}
