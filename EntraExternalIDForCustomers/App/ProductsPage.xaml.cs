namespace EntraExternalIDForCustomers;

public partial class ProductsPage : ContentPage
{
	public ProductsPage()
	{
		InitializeComponent();
	}

    private void OnGetProductNameClicked(object sender, EventArgs e)
    {
		ProductLabel.Text = "";
    }
}