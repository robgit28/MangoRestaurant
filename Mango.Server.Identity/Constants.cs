using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mango.Server.Identity
{
    public static class Constants
    {
        // our roles 
        public const string Administrator = "Administrator";
        public const string Customer = "Customer";
        //public const string User = "User";
        //public const string Guest = "Guest";

        // Identity Server  
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            { 
                // initilaises a new instance of openId 
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        // essentially MangoAdmin 
        // allows CRUD for this user 
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                // these names can be anything we want 
                // first argument is the unique name of the API
                // the second argument is the DisplayName. this value can be used on the consent screen.
                new ApiScope("mangoAdmin", "Mango Server."),
                new ApiScope(name: "read", displayName: "Read your data."),
                new ApiScope(name: "write", displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data."),
            };
        
        // this is our app 
        // used for auth a user or accessing a resource 
        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    // in production this would be more secure like a Guid 
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "mango",
                    AllowedGrantTypes = GrantTypes.Code,
                    // in production this would be more secure like a Guid 
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = new List<string> 
                    { 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        "mango"
                        
                    },
                    // the redirect url on login 
                    // from the Mango.web project - launchSettings.json 
                    // for openId Connect 
                    // should be https 
                    RedirectUris = { "https://localhost:44397/signin-oidc" },
                    // the redirect url on logout  
                    // should be https 
                    PostLogoutRedirectUris = { "https://localhost:44397/signout-callback-oidc" },    
                }
            }; 
    }
}
