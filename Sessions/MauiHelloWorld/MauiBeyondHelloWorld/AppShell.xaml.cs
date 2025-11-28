using MauiBeyondHelloWorld.Views;
using MauiBeyondHelloWorld.Views.Layouts;

namespace MauiBeyondHelloWorld;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(AbsoluteLayoutPage), typeof(AbsoluteLayoutPage));
	}
}
