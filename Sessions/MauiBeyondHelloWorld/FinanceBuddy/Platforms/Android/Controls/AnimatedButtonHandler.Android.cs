using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Microsoft.Maui.Platform;

namespace FinanceBuddy.Controls;

/// <summary>
/// Android-specific implementation of AnimatedButtonHandler
/// Demonstrates platform-specific code for custom controls
/// </summary>
public partial class AnimatedButtonHandler
{
	protected override Android.Widget.Button CreatePlatformView()
	{
		var button = new Android.Widget.Button(Context);
		button.SetAllCaps(false); // Use original text casing
		return button;
	}

	protected override void ConnectHandler(Android.Widget.Button platformView)
	{
		base.ConnectHandler(platformView);
		
		// Add click handler
		platformView.Click += OnButtonClick;
		
		// Add press animation
		platformView.Touch += OnButtonTouch;
	}

	protected override void DisconnectHandler(Android.Widget.Button platformView)
	{
		platformView.Click -= OnButtonClick;
		platformView.Touch -= OnButtonTouch;
		base.DisconnectHandler(platformView);
	}

	private void OnButtonClick(object? sender, EventArgs e)
	{
		VirtualView?.SendClicked();
	}

	private void OnButtonTouch(object? sender, Android.Views.View.TouchEventArgs e)
	{
		if (sender is not Android.Widget.Button button || e.Event == null)
			return;

		switch (e.Event.Action)
		{
			case MotionEventActions.Down:
				// Scale down animation on press
				button.Animate()
					?.ScaleX(0.80f)
					?.ScaleY(0.80f)
					?.SetDuration(100)
					?.Start();
				break;

			case MotionEventActions.Up:
			case MotionEventActions.Cancel:
				// Scale back to normal
				button.Animate()
					?.ScaleX(1.0f)
					?.ScaleY(1.0f)
					?.SetDuration(100)
					?.Start();
				break;
		}
	}

	partial void UpdateText(string text)
	{
		if (PlatformView != null)
			PlatformView.Text = text;
	}

	partial void UpdateTextColor(Microsoft.Maui.Graphics.Color color)
	{
		if (PlatformView != null)
			PlatformView.SetTextColor(color.ToPlatform());
	}

	partial void UpdateBackgroundColor(Microsoft.Maui.Graphics.Color color)
	{
		if (PlatformView != null)
		{
			var drawable = new GradientDrawable();
			drawable.SetColor(color.ToPlatform());
			drawable.SetCornerRadius(VirtualView?.CornerRadius ?? 8f);
			PlatformView.Background = drawable;
		}
	}

	partial void UpdateCornerRadius(float cornerRadius)
	{
		// Re-apply background with new corner radius
		if (VirtualView != null)
			UpdateBackgroundColor(VirtualView.BackgroundColor);
	}
}
