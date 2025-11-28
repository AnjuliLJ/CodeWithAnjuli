namespace MauiBeyondHelloWorld.Views.Layouts;

public partial class AbsoluteLayoutPage : ContentPage
{
	public AbsoluteLayoutPage(AbsoluteLayoutViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}