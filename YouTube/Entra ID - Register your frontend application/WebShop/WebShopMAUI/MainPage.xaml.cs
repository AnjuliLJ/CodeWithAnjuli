using System.Security.Claims;

namespace WebShopMAUI
{
    public partial class MainPage : ContentPage
    {
        private string[] Scopes = { "YOUR_SCOPE_URL" };

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var authResult = await App.PublicClientApplication
                 .AcquireTokenInteractive(Scopes)
                 .WithParentActivityOrWindow(App.ParentWindow)
                 .ExecuteAsync();

            var token = authResult.AccessToken;

            IEnumerable<Claim> claims = authResult.ClaimsPrincipal.Claims;
            var name = claims.First(c => c.Type == "name").Value;
            var userId = claims.First(c => c.Type == "oid").Value;
        }
    }

}
