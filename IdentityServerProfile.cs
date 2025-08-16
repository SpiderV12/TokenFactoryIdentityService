
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Security.Claims;

public class customIProfileService : IProfileService
{
    public readonly UserManager<IdentityUser> _usermanger;
    public readonly RoleManager<IdentityRole> _rolemanger;

     public customIProfileService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
        _usermanger = userManager;
        _rolemanger = roleManager;
       
    }
     public async Task GetProfileDataAsync(ProfileDataRequestContext context) {
        string userid = context.Subject.FindFirst("sub")?.Value!;
        IdentityUser User = await _usermanger.FindByIdAsync(userid);
        IList<Claim> claims = new List<Claim> { };
        claims.Add(new Claim("nameuser", User.UserName));
                claims.Add(new Claim("Email", User.Email));
        if (User.Email == "mohammadfarraj444@gmail.com")
        {
            claims.Add(new Claim("roleuser", "Admin"));
        }
        else
        {
                     claims.Add(new Claim("roleuser", "customer"));
           
                }
        Console.WriteLine("Calims:" + claims);
        context.IssuedClaims.AddRange(claims);
    }
     public async Task IsActiveAsync(IsActiveContext context)
{
    string userId = context.Subject.FindFirst("sub")?.Value;

    IdentityUser user = await _usermanger.FindByIdAsync(userId);

    if (user != null)
    {
        if (!user.LockoutEnabled)
        {
            context.IsActive = true;
        }
        else if (!user.LockoutEnd.HasValue || user.LockoutEnd.Value <= DateTimeOffset.UtcNow)
        {
            context.IsActive = true;
        }
        else
        {
            context.IsActive = false;
        }
    }
    else
    {
        context.IsActive = false;
    }
}

};