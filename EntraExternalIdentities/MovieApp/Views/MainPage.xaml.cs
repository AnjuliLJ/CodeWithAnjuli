using MovieApp.Models;
using MovieApp.MSALClient;
using System.Security.Claims;

namespace MovieApp.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var token = await PublicClientSingleton.Instance.AcquireTokenSilentAsync();
            await SecureStorage.SetAsync("token", token);
            var claims = PublicClientSingleton.Instance.MSALClientHelper.AuthResult.ClaimsPrincipal.Claims;
            var info = GetInfoFromClaim(claims);
            GoToSuccessPage(info);
        }

        //private void OnLoginSilentClicked(object sender, EventArgs e)
        //{

        //}

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