using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace dutchonboard.Infrastructure.EF.Data;

public class DataSeederSecurity
{
    private readonly UserManager<IdentityUser> _userManager;

    public DataSeederSecurity(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Seed()
    {
        var jille = await _userManager.FindByEmailAsync(UserSeedData.Organizer1Email);
        if (jille == null)
        {
            jille = new IdentityUser(UserSeedData.Organizer1FirstName){Email = UserSeedData.Organizer1Email};
            await _userManager.CreateAsync(jille, UserSeedData.Password);
            await _userManager.AddClaimAsync(jille, new Claim("Organizer", "true"));
        }

        var donat = await _userManager.FindByEmailAsync(UserSeedData.Player1Email);
        if (donat == null)
        {
            donat = new IdentityUser(UserSeedData.Player1FirstName){Email = UserSeedData.Player1Email};
            await _userManager.CreateAsync(donat, UserSeedData.Password);
        }

        var donatJr = await _userManager.FindByEmailAsync(UserSeedData.Player2Email);
        if (donatJr == null)
        {
            donat = new IdentityUser(UserSeedData.Player2FirstName){Email = UserSeedData.Player2Email};
            await _userManager.CreateAsync(donat, UserSeedData.Password);
        }
    }
}