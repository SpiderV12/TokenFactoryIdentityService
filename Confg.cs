using Duende.IdentityServer.Models;
using System.Collections.Generic;

public static class Confg
{
    public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
    {
        new ApiScope("Write"),
        new ApiScope("Read"),
        new ApiScope("ReadWrite")
    };

    public static IEnumerable<Client> Clients => new List<Client>
    {
        new Client
        {
               ClientId = "api_gateway",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
    ClientSecrets = { new Secret("secret123".Sha256()) },
        AlwaysIncludeUserClaimsInIdToken = true, 

        AllowedScopes = { "ReadWrite", "backendapiGate", "openid", "profile" } 
        }
    };

    public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
    {
        new ApiResource("backendapiGate")
        {
            Scopes = { "Write" ,"Read" ,"ReadWrite","openid", "profile"},
            UserClaims = {"nameuser" ,"Email" , "roleuser"}
        }
    };
}
