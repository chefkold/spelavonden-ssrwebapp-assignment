using dutchonboard.Core.Domain.Models;
using dutchonboard.Core.DomainServices.Repositories;
using dutchonboard.Core.DomainServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace dutchonboardRESTApi.Controllers;
[ApiController]
[Route("gamenights")]
public class GameNightController : Controller
{
    private readonly IGameNightService _iGameNightService;
    private readonly IPlayerRepo _iPlayerRepo;

    public GameNightController(IGameNightService iGameNightService, IPlayerRepo iPlayerRepo)
    {
        _iGameNightService = iGameNightService;
        _iPlayerRepo = iPlayerRepo;
    }

    // Create and endpoint with a restful route name that receives a player Id and lets that player join a gamenight by id from url
    [HttpPost("{gameNightId}/players/{playerId}")]
    public ActionResult<GameNight> JoinGameNight(int gameNightId, int playerId)
    {
        var gameNight = _iGameNightService.GetGameNightById(gameNightId);

        if (gameNight == null)
        {
            return StatusCode(404, new { status = 404, message = "Spelavond niet gevonden." });
        }

        var player = _iPlayerRepo.GetPlayerById(playerId);
        Result enrollmentResult = _iGameNightService.AddPlayerToGameNight(gameNightId, player);

        if (enrollmentResult.HasError)
        {
            return StatusCode(400, new { status = 400, Error = enrollmentResult.ErrorMessage });

        }

        _iGameNightService.CommitEnrollmentOfPlayer(gameNight, player);
        return StatusCode(200, new { status = 200, message = "Succesvol ingeschreven voor bordspelavond.", gameNight = gameNight });
    }
}