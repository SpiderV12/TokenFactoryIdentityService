using Microsoft.EntityFrameworkCore;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Duende.IdentityServer.Models;

using System.Text.Json;

public class checkuser : IResourceOwnerPasswordValidator
{
    public readonly database _database;
    public readonly IPasswordHasher<IdentityUser> _passwordHasher;
    public readonly UserManager<IdentityUser> _userManager;
    public checkuser(database dbContext, IPasswordHasher<IdentityUser> passwordHasher, UserManager<IdentityUser> userManager)
    {
        _database = dbContext;
        _passwordHasher = passwordHasher;
        _userManager = userManager;

    }
    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
                 Console.WriteLine("cgcking:");

        var User =await   _database.Users.Where((dta) => dta.Email == context.UserName)
        .FirstOrDefaultAsync();
        if (User != null)
        {
            Console.WriteLine("userr:" + User.Email);
            PasswordVerificationResult PassCheck = _passwordHasher.VerifyHashedPassword(User, User.PasswordHash!, context.Password);
            if (PassCheck == PasswordVerificationResult.Success)
            {
                var ss = _database.Roles;
                context.Result = new GrantValidationResult(
                    subject: User.Id,
                    authenticationMethod: "password",
                    claims: new List<Claim>
                    {
                        new Claim("email" ,  User.Email  ),
                        new  Claim("nameuser" , context.UserName)
                    }
                );
            }
            else
            {
                  context.Result = new GrantValidationResult(
                  TokenRequestErrors.InvalidGrant,
                  "error password or username"
               );
            return;
            }

        }
        else
        {
            context.Result = new GrantValidationResult(
                  TokenRequestErrors.InvalidGrant,
                  "error password or username"
               );
            return;
        }

    }

}
