# Custom Component Demo - AnimatedButton

This project includes a **custom animated button control** with **platform-specific handlers** to demonstrate MAUI's extensibility and custom control capabilities.

## 📁 File Structure

```
Controls/
├── AnimatedButton.cs                              # Cross-platform control definition
├── AnimatedButtonHandler.cs                       # Base handler with property mappers
└── Platforms/
    ├── Android/
    │   └── Controls/
    │       └── AnimatedButtonHandler.Android.cs   # Android-specific implementation
    ├── iOS/
    │   └── Controls/
    │       └── AnimatedButtonHandler.iOS.cs       # iOS-specific implementation
    └── MacCatalyst/
        └── Controls/
            └── AnimatedButtonHandler.MacCatalyst.cs # Mac Catalyst implementation
```

## 🎯 What It Demonstrates

### 1. **Custom Control** (`AnimatedButton.cs`)
- Defines bindable properties (Text, TextColor, BackgroundColor, CornerRadius)
- Implements ICommand support for MVVM
- Exposes events (Clicked)
- Cross-platform API that works on all platforms

### 2. **Handler Pattern** (`AnimatedButtonHandler.cs`)
- Uses MAUI's handler architecture
- Property mappers for synchronizing control properties with native views
- Conditional compilation for platform-specific types
- Clean separation of concerns

### 3. **Platform-Specific Implementations**

#### **Android** (`AnimatedButtonHandler.Android.cs`)
- Uses native `Android.Widget.Button`
- Implements touch-based scale animation (press = 0.95x scale)
- Material Design ripple effect (native Android behavior)
- Custom drawable for rounded corners

#### **iOS/Mac Catalyst** (`AnimatedButtonHandler.iOS.cs` / `.MacCatalyst.cs`)
- Uses native `UIKit.UIButton`
- Implements `CGAffineTransform` for smooth scale animations
- Touch event handling with `TouchDown`, `TouchUpInside`, etc.
- `CALayer` for corner radius styling

## 🔧 Registration

The handler is registered in `MauiProgram.cs`:

```csharp
.ConfigureMauiHandlers(handlers =>
{
    handlers.AddHandler<AnimatedButton, AnimatedButtonHandler>();
})
```

## 💡 Usage Example

The custom button is used in `ExpenseDetailsPage.xaml`:

```xml
<AnimatedButton Text="Try Custom Button"
               BackgroundColor="#5B9BED"
               TextColor="White"
               CornerRadius="12"
               HeightRequest="48"
               Clicked="OnAnimatedButtonClicked" />
```

## 🎨 Features

- ✅ **Cross-platform API** - Single XAML declaration works everywhere
- ✅ **Platform-specific animations** - Each platform has native look and feel
- ✅ **Data binding support** - All properties are bindable
- ✅ **MVVM command support** - Can bind to ICommand
- ✅ **Event-based handling** - Traditional Clicked event available
- ✅ **Customizable appearance** - Text, colors, corner radius all configurable

## 🚀 How to Test

1. Build and run the app
2. Navigate to **Expenses** tab
3. Tap any expense to view details
4. Scroll to the bottom to see the "Custom Component Demo" section
5. Press the "Try Custom Button" - notice the platform-specific animation:
   - **Android**: Button scales down with Material ripple
   - **iOS/Mac**: Smooth scale animation with UIView animation

## 📚 Learning Points

This implementation teaches:

1. **Handler Architecture** - How MAUI bridges cross-platform controls to native views
2. **Property Mappers** - Synchronizing .NET properties with native properties
3. **Platform-Specific Code** - Using conditional compilation and partial classes
4. **Native Animations** - Implementing platform-appropriate animations
5. **Custom Controls** - Creating reusable components beyond built-in controls

## 🔍 Key Concepts

### Property Mapper
```csharp
public static IPropertyMapper<AnimatedButton, AnimatedButtonHandler> PropertyMapper = 
    new PropertyMapper<AnimatedButton, AnimatedButtonHandler>(ViewHandler.ViewMapper)
{
    [nameof(AnimatedButton.Text)] = MapText,
    // ... more properties
};
```

### Platform-Specific Types
```csharp
#if ANDROID
using PlatformView = Android.Widget.Button;
#elif IOS || MACCATALYST
using PlatformView = UIKit.UIButton;
#endif
```

### Partial Methods
```csharp
// Declared in base handler
partial void UpdateText(string text);

// Implemented per platform
partial void UpdateText(string text)
{
    if (PlatformView != null)
        PlatformView.Text = text;
}
```

## 🎓 Perfect for Demos

This implementation is ideal for:
- Teaching MAUI custom controls
- Demonstrating handler pattern
- Showing platform-specific customization
- Explaining cross-platform architecture
- Presenting at conferences or in tutorials

---

**Note**: This custom component is added as a demo feature and doesn't interfere with the existing expense tracking functionality of the app.
