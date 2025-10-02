using UIKit;
using CoreGraphics;
using Microsoft.Maui.Platform;

namespace FinanceBuddy.Controls;

/// <summary>
/// Mac Catalyst-specific implementation of AnimatedButtonHandler
/// Demonstrates platform-specific code for custom controls
/// </summary>
public partial class AnimatedButtonHandler
{
	protected override UIButton CreatePlatformView()
	{
		var button = new UIButton(UIButtonType.System);
		button.Layer.MasksToBounds = true;
		return button;
	}

	protected override void ConnectHandler(UIButton platformView)
	{
		base.ConnectHandler(platformView);
		
		// Add touch handlers for animation
		platformView.TouchDown += OnButtonTouchDown;
		platformView.TouchUpInside += OnButtonTouchUp;
		platformView.TouchUpOutside += OnButtonTouchUp;
		platformView.TouchCancel += OnButtonTouchUp;
	}

	protected override void DisconnectHandler(UIButton platformView)
	{
		platformView.TouchDown -= OnButtonTouchDown;
		platformView.TouchUpInside -= OnButtonTouchUp;
		platformView.TouchUpOutside -= OnButtonTouchUp;
		platformView.TouchCancel -= OnButtonTouchUp;
		base.DisconnectHandler(platformView);
	}

	private void OnButtonTouchDown(object? sender, EventArgs e)
	{
		if (sender is UIButton button)
		{
			// Animate scale down on press
			UIView.Animate(0.1, () =>
			{
				button.Transform = CGAffineTransform.MakeScale(0.95f, 0.95f);
			});
		}
	}

	private void OnButtonTouchUp(object? sender, EventArgs e)
	{
		if (sender is UIButton button)
		{
			// Animate scale back to normal
			UIView.Animate(0.1, () =>
			{
				button.Transform = CGAffineTransform.MakeIdentity();
			});
			
			// Trigger the clicked event
			VirtualView?.SendClicked();
		}
	}

	partial void UpdateText(string text)
	{
		if (PlatformView != null)
			PlatformView.SetTitle(text, UIControlState.Normal);
	}

	partial void UpdateTextColor(Microsoft.Maui.Graphics.Color color)
	{
		if (PlatformView != null)
			PlatformView.SetTitleColor(color.ToPlatform(), UIControlState.Normal);
	}

	partial void UpdateBackgroundColor(Microsoft.Maui.Graphics.Color color)
	{
		if (PlatformView != null)
		{
			PlatformView.BackgroundColor = color.ToPlatform();
			PlatformView.Layer.CornerRadius = VirtualView?.CornerRadius ?? 8f;
		}
	}

	partial void UpdateCornerRadius(float cornerRadius)
	{
		if (PlatformView != null)
			PlatformView.Layer.CornerRadius = cornerRadius;
	}
}
