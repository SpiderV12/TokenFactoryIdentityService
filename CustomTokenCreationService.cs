using Duende.IdentityServer.Services;
using Duende.IdentityServer.Models;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;

public class CustomTokenCreationService : DefaultTokenCreationService
{
    public CustomTokenCreationService(IClock clock, IKeyMaterialService keys, IdentityServerOptions options, ILogger<DefaultTokenCreationService> logger) : base(clock, keys, options, logger)
    {
    }

    public override async Task<string> CreateTokenAsync(Token token)
    {
        string jwt = await base.CreateTokenAsync(token);



        return jwt;
    }
    }
