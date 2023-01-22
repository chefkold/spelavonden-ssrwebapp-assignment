using dutchonboard.Core.Domain.Models;
using dutchonboard.Core.DomainServices.Services;
using dutchonboardRESTApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace dutchonboardRESTApi.Controllers;
[ApiController]
[Route("gamenights")]
public class GameNightController : Controller
{
    private readonly IGameNightService _iGameNightService;

    public GameNightController(IGameNightService iGameNightService)
    {
        _iGameNightService = iGameNightService;
    }


    [HttpGet]
    public ActionResult<ICollection<GameNight>> Get()
    {
        return Ok(_iGameNightService.GetAllGameNights());
    }
}