using Microsoft.Identity.Client;

namespace WebShopMAUI
{
    public partial class App : Application
    {
        public static object ParentWindow { get; set; }
        private static string Authority = "https://YOUR_TENANT.ciamlogin.com/";
        private static string ClientId = "YOUR_CLIENT_ID";
        public static IPublicClientApplication PublicClientApplication { get; set; }
        public App()
        {
            InitializeComponent();
            PublicClientApplication = PublicClientApplicationBuilder.Create(ClientId)
                .WithAuthority(Authority)
                .WithRedirectUri($"msal{ClientId}://auth")
                .Build();
            MainPage = new AppShell();
        }
    }
}
