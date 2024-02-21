using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace WebShopMAUIApp.Platforms.Android
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal703bfaf6-e792-49ab-b1e9-76f4b1cc299e")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}
