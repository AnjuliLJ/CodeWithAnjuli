using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Abstractions;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using WebShopMAUIApp.Models;

namespace WebShopMAUIApp
{
    public partial class MainPage : ContentPage
    {
        private readonly EntraIDConfig _entraIDConfig;

        public MainPage()
        {
            InitializeComponent();
            // Load config (normally done in Program.cs or MauiProgram.cs)
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream("WebShopMAUIApp.appsettings.json");
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
            await Shell.Current.GoToAsync($"{nameof(ResultPage)}", true, new Dictionary<string, object>
            {
                {"EntraResponse", info}
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

        private async void OnGetDataClicked(object sender, EventArgs e)
        {
            using var http = new HttpClient();
            var token = await SecureStorage.GetAsync("token");
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await http.GetAsync("https://webshopapi.azurewebsites.net/WeatherForecast");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                labelTest.Text = result;
            } else
            {
                labelTest.Text = "401 Forbidden";
            }
        }
    }

}
