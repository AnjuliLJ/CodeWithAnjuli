namespace EntraExternalIDForCustomers;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnLoginClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync(nameof(ProductsPage));
	}
}

