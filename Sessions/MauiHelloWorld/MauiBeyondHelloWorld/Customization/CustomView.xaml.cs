namespace MauiBeyondHelloWorld.Customization;

public partial class CustomView : ContentView
{
	public static readonly BindableProperty TitleProperty =
		BindableProperty.Create(nameof(Title), 
		typeof(string), typeof(CustomView), string.Empty);

	public string Title
	{
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

	public CustomView()
	{
		InitializeComponent();
	}
}