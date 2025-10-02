using Microsoft.Maui.Graphics;
using System.Windows.Input;

namespace FinanceBuddy.Controls;

/// <summary>
/// Custom animated button control that demonstrates MAUI custom components and handlers
/// Features: Press animation, ripple effect, customizable colors
/// </summary>
public class AnimatedButton : View
{
	public static readonly BindableProperty TextProperty =
		BindableProperty.Create(nameof(Text), typeof(string), typeof(AnimatedButton), string.Empty);

	public static readonly BindableProperty TextColorProperty =
		BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(AnimatedButton), Colors.White);

	public static new readonly BindableProperty BackgroundColorProperty =
		BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(AnimatedButton), Colors.Blue);

	public static readonly BindableProperty CornerRadiusProperty =
		BindableProperty.Create(nameof(CornerRadius), typeof(float), typeof(AnimatedButton), 8f);

	public static readonly BindableProperty CommandProperty =
		BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AnimatedButton), null);

	public string Text
	{
		get => (string)GetValue(TextProperty);
		set => SetValue(TextProperty, value);
	}

	public Color TextColor
	{
		get => (Color)GetValue(TextColorProperty);
		set => SetValue(TextColorProperty, value);
	}

	public new Color BackgroundColor
	{
		get => (Color)GetValue(BackgroundColorProperty);
		set => SetValue(BackgroundColorProperty, value);
	}

	public float CornerRadius
	{
		get => (float)GetValue(CornerRadiusProperty);
		set => SetValue(CornerRadiusProperty, value);
	}

	public ICommand? Command
	{
		get => (ICommand?)GetValue(CommandProperty);
		set => SetValue(CommandProperty, value);
	}

	// Event for handling clicks
	public event EventHandler? Clicked;

	public void SendClicked()
	{
		Clicked?.Invoke(this, EventArgs.Empty);
		Command?.Execute(null);
	}
}
