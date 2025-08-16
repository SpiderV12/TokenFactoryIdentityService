using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Duende.IdentityServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddIdentityServer((opaton ) =>
{
        opaton.AccessTokenJwtType = "JWT";

})
    .AddInMemoryApiResources(Confg.ApiResources)
    .AddInMemoryClients(Confg.Clients)
    .AddInMemoryApiScopes(Confg.ApiScopes)
    .AddProfileService<customIProfileService>()
.AddDeveloperSigningCredential()
    .AddResourceOwnerValidator<checkuser>(); 

builder.Services.AddDbContext<database>((confg) =>
{
    confg.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<database>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseIdentityServer();  


app.Run();

