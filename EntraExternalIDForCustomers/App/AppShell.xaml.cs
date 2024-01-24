namespace EntraExternalIDForCustomers;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(ProductsPage), typeof(ProductsPage));
	}
}
