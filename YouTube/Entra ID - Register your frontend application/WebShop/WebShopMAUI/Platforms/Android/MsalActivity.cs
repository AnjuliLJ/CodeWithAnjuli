using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace WebShopMAUIApp.Platforms.Android
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal6470e5d1-1317-4b4f-b9fb-ff0a83d7e309")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}