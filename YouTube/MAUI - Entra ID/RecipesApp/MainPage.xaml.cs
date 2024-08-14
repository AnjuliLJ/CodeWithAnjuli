using Microsoft.Identity.Client;

namespace RecipesApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Connect with Entra 
            var publicClientApplicationBuilder = PublicClientApplicationBuilder
                .Create(EntraConfig.ClientId)
                .WithAuthority(EntraConfig.Authority)
                .WithIosKeychainSecurityGroup(EntraConfig.IOSKeychainSecurityGroup)
                .WithRedirectUri($"msal{EntraConfig.ClientId}://auth")
                .Build();

            try
            {
                var accounts = await publicClientApplicationBuilder.GetAccountsAsync();
                if (accounts.Any())
                {
                     await publicClientApplicationBuilder.AcquireTokenSilent(EntraConfig.Scopes, accounts.First())
                    .ExecuteAsync();
                } else
                {
                    var authResult = await publicClientApplicationBuilder.AcquireTokenInteractive(EntraConfig.Scopes)
                   .WithParentActivityOrWindow(EntraConfig.ParentWindow)
                   .ExecuteAsync().ConfigureAwait(false);
                }

            }
            catch (MsalException ex)
            {
                // Interactive mode
                var authResult = await publicClientApplicationBuilder.AcquireTokenInteractive(EntraConfig.Scopes)
                    .WithParentActivityOrWindow(EntraConfig.ParentWindow)
                    .ExecuteAsync().ConfigureAwait(false);
            }

           

            NameLabel.Text = "Login succesful";
        }
    }

}
