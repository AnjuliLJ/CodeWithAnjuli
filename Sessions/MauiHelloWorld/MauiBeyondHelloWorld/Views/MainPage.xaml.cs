using MauiBeyondHelloWorld.Views;

namespace MauiBeyondHelloWorld;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
