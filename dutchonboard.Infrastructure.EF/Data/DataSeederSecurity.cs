using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace dutchonboard.Infrastructure.EF.Data;

public class DataSeederSecurity
{
    private readonly UserManager<IdentityUser> _userManager;
    private const string JilleEmail = "j.struijs@buurtverenigingtkoekje.nl";
    private const string DonatEmail = "d.nowakowski@gmail.com";
    private const string Password = "Hello1234!";

    public DataSeederSecurity(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Seed()
    {
        var jille = await _userManager.FindByEmailAsync(JilleEmail);
        if (jille == null)
        {
            jille = new IdentityUser(JilleEmail){Email = JilleEmail};
            await _userManager.CreateAsync(jille, Password);
            await _userManager.AddClaimAsync(jille, new Claim("Organizer", "true"));
        }

        var donat = await _userManager.FindByEmailAsync(DonatEmail);
        if (donat == null)
        {
            donat = new IdentityUser(DonatEmail){Email = DonatEmail};
            await _userManager.CreateAsync(donat, Password);
        }
    }
}