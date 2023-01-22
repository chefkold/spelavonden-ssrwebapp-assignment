using dutchonboard.Core.DomainServices.Repositories;
using dutchonboardRESTApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dutchonboardRESTApi.Controllers;

[ApiController]
[Route("users")]
public class AuthController : Controller
{

    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IPlayerRepo _iPlayerRepo;


    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IPlayerRepo iPlayerRepo)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _iPlayerRepo = iPlayerRepo;
    }

    // Login as user and receive your player profile ID so that u can join a game night.
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.EmailAddress);
        if (user != null)
        {
            await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);

            if (result.Succeeded)
            {
                var player = _iPlayerRepo.GetPlayerByEmail(user.Email);
                return StatusCode(200, new { status = 200, PlayerId = player.Id });

            }
        }
        return StatusCode(401, new { status = 401, message = "Emailadres of wachtwoord is onjuist." });
    }
}