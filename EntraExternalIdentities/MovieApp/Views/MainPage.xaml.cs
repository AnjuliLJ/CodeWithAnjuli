using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Abstractions;
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
            
            // Load config (normally done in Program.cs or MauiProgram.cs)
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("MovieApp.appsettings.json");
            IConfiguration appConfiguration = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            _entraIDConfig = appConfiguration.GetSection("EntraID").Get<EntraIDConfig>();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            IPublicClientApplication publicClientApplication = PublicClientApplicationBuilder.Create(_entraIDConfig.ClientId)
                .WithAuthority(_entraIDConfig.Authority)
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