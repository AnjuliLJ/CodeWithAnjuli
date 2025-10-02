using Microsoft.Maui.Handlers;

#if ANDROID
using PlatformView = Android.Widget.Button;
#elif IOS || MACCATALYST
using PlatformView = UIKit.UIButton;
#elif WINDOWS
using PlatformView = Microsoft.UI.Xaml.Controls.Button;
#else
using PlatformView = System.Object;
#endif

namespace FinanceBuddy.Controls;

/// <summary>
/// Handler for AnimatedButton - bridges the cross-platform control with platform-specific implementations
/// This demonstrates MAUI's handler pattern for custom controls
/// </summary>
public partial class AnimatedButtonHandler : ViewHandler<AnimatedButton, PlatformView>
{
	public static IPropertyMapper<AnimatedButton, AnimatedButtonHandler> PropertyMapper = new PropertyMapper<AnimatedButton, AnimatedButtonHandler>(ViewHandler.ViewMapper)
	{
		[nameof(AnimatedButton.Text)] = MapText,
		[nameof(AnimatedButton.TextColor)] = MapTextColor,
		[nameof(AnimatedButton.BackgroundColor)] = MapBackgroundColor,
		[nameof(AnimatedButton.CornerRadius)] = MapCornerRadius,
	};

	public AnimatedButtonHandler() : base(PropertyMapper)
	{
	}

	// Mapper methods
	public static void MapText(AnimatedButtonHandler handler, AnimatedButton button)
	{
		handler.UpdateText(button.Text);
	}

	public static void MapTextColor(AnimatedButtonHandler handler, AnimatedButton button)
	{
		handler.UpdateTextColor(button.TextColor);
	}

	public static void MapBackgroundColor(AnimatedButtonHandler handler, AnimatedButton button)
	{
		handler.UpdateBackgroundColor(button.BackgroundColor);
	}

	public static void MapCornerRadius(AnimatedButtonHandler handler, AnimatedButton button)
	{
		handler.UpdateCornerRadius(button.CornerRadius);
	}

	// Partial methods to be implemented per platform
	partial void UpdateText(string text);
	partial void UpdateTextColor(Microsoft.Maui.Graphics.Color color);
	partial void UpdateBackgroundColor(Microsoft.Maui.Graphics.Color color);
	partial void UpdateCornerRadius(float cornerRadius);
}
