namespace FinanceBuddy.Categories;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(CategoriesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}