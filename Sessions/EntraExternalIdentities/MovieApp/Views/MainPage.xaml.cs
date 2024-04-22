using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using MovieApp.Models;
using MovieApp.MSALClient;
using System.Reflection;
using System.Security.Claims;

namespace MovieApp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly EntraIDConfig _entraIDConfig;

        public MainPage()
        {
            InitializeComponent();

            _entraIDConfig = new EntraIDConfig()
            {
                Authority = "https://moviefans.ciamlogin.com/",
                TenantId = "0d7f6872-c080-48fb-9bc9-04c6da6d127f",
                ClientId = "89e79508-0d81-4da7-9451-d9e88db4bb1b",
                Scopes = "openid offline_access api://23c83759-97a0-4d2a-afc1-b675cba810b9/Movie.Read"
            };
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder
                .Create(_entraIDConfig.ClientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, "common")
                .WithRedirectUri($"msal{_entraIDConfig.ClientId}://auth")
                .Build();

            AuthenticationResult authResult = await publicClientApplication
                .AcquireTokenInteractive(_entraIDConfig.ScopesArray)
                .WithParentActivityOrWindow(App.ParentWindow)
                .ExecuteAsync();

            var token = authResult.AccessToken;
            await SecureStorage.SetAsync("token", token);

            var info = GetInfoFromClaim(authResult.ClaimsPrincipal.Claims);
            GoToSuccessPage(info);

        }


        private async void GoToSuccessPage(EntraResponse response)
        {
            await Shell.Current.GoToAsync($"{nameof(BestMoviePage)}", true, new Dictionary<string, object>
            {
                {"EntraResponse", response}
            });
        }

        private EntraResponse GetInfoFromClaim(IEnumerable<Claim> claims)
        {

            var name = claims.First(c => c.Type == "name").Value;
            var id = claims.First(c => c.Type == "oid").Value;
            return new EntraResponse()
            {
                oid = id,
                Name = name
            };
        }
    }
}