using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class database : IdentityDbContext<IdentityUser , IdentityRole  , string> 
{
    public database(DbContextOptions<database> dbContextOptions) : base(dbContextOptions)
    {
        
    }
}