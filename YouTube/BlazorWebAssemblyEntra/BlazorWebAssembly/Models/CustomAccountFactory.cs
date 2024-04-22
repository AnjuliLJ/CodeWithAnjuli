using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;

namespace BlazorWebAssembly.Models
{
    public class CustomAccountFactory(IAccessTokenProviderAccessor accessor, IServiceProvider serviceProvider)
        : AccountClaimsPrincipalFactory<CustomUserAccount>(accessor)
    {
        private readonly IServiceProvider serviceProvider = serviceProvider;

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(CustomUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var initialUser = await base.CreateUserAsync(account, options);

            if (initialUser.Identity is not null && initialUser.Identity.IsAuthenticated)
            {
                var userIdentity = initialUser.Identity as ClaimsIdentity;

                if (userIdentity is not null)
                {
                    account?.Roles.ForEach((role) =>
                    {
                        userIdentity.AddClaim(new Claim("appRole", role));
                    });
                }
            }
            return initialUser;
        }
    }
}
