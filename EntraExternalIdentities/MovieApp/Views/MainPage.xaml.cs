using MovieApp.Models;
using MovieApp.MSALClient;

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
            await PublicClientSingleton.Instance.AcquireTokenSilentAsync();
            var claims = PublicClientSingleton.Instance.MSALClientHelper.AuthResult.ClaimsPrincipal.Claims.Select(c => c.Value);
            GoToSuccessPage(new User(){Name = "Test" });
        }

        //private void OnLoginSilentClicked(object sender, EventArgs e)
        //{

        //}

        private async void GoToSuccessPage(User user)
        {
            await Shell.Current.GoToAsync($"{nameof(BestMoviePage)}", true, new Dictionary<string, object>
            {
                {"User", user}
            });
        }
    }
}