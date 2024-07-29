namespace RecipesApp
{
    public static class EntraConfig
    {
        public static string Authority = "https://TENANT.ciamlogin.com/";
        public static string ClientId = "CLIENT_ID";
        public static string[] Scopes = { "openid", "offline_access" };
        public static string IOSKeychainSecurityGroup = "com.microsoft.adalcache";
        public static object? ParentWindow { get; set; }
    }
}
