namespace WebShopMAUIApp
{
    public partial class App : Application
    {
        public static object ParentWindow { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
