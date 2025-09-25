namespace MauiBeyondHelloWorld;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(Pages.Layouts.LayoutsPage), typeof(Pages.Layouts.LayoutsPage));
		Routing.RegisterRoute(nameof(Pages.Layouts.AbsoluteLayoutPage), typeof(Pages.Layouts.AbsoluteLayoutPage));
		Routing.RegisterRoute(nameof(Pages.Layouts.GridLayoutPage), typeof(Pages.Layouts.GridLayoutPage));
		Routing.RegisterRoute(nameof(Pages.Layouts.HorizontalStackLayoutPage), typeof(Pages.Layouts.HorizontalStackLayoutPage));
		Routing.RegisterRoute(nameof(Pages.Layouts.FlexLayoutPage), typeof(Pages.Layouts.FlexLayoutPage));
	}
}
