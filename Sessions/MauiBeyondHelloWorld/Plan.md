# Intro 
Question: Who here is not familiar with .NET MAUI?

# Demo Intro
When building natively running apps, you have two major subjects that you have to think about: Performance and Security. My focus today is on both of these topics, giving examples of the experiences I have. 

Good things to know about the startup of your app:
- MauiProgram.cs: avoid using Singletons because they will eat your memory. Always used Transient. Scope doesn't really have a use here. 
- Avoid things during startup, especially fetching data from the API. It's way better to get the data once a page is loaded.
- Pages: async tasks and business logic in the viewmodel, UI-related tasks to the code-behind. 

# Layouts 
| Layout                                      | Main Strength                                               | Performance Profile                                                                                                                                                                            |
| ------------------------------------------- | ----------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| VerticalStackLayout / HorizontalStackLayout | Simple linear positioning, minimal nesting.                 | Very performant.These new StackLayouts are optimized for speed and should replace legacy StackLayout. Ideal for linear screens like forms.                                                      |
| Grid                                        | Structured row/column alignment.                            | Efficient when flat.Handles complex alignment but can get expensive with many nested grids. Recommended for screen sections with defined structure.                                            |
| FlexLayout                                  | Responsive layout — items wrap, justify, align dynamically. | Moderate performance hitwhen content changes often (e.g., wrapping on resize), because recalculations occur each time alignment properties change. Excellent for adaptive or dynamic screens . |
| AbsoluteLayout                              | Exact positioning and overlap control.                      | Very fastwhen positions are fixed. Great for overlays, animations, or map-like UIs.Poor choicefor scalable UIs or when content changes dynamically — requires manual sizing .                  |
| ScrollView                                  | Wrapping for overflow content.                              | Rendering depends on child layout type. Avoid deep nested containers inside ScrollView for performance.                                                                                        |
# Navigation
Question: who here uses Shell?

Funny thing, last Tuesday I had a discussion with my colleagues at the Tax Administration office. They were discussing why we are not using Shell, and all the arguments used for not using it can be pinpointed to: our applications are too complex for Shell navigation. When you go the MS Documentation, it states that when you want to use complex navigation, like popping two pages and navigating to a new page for example, it is recommended to use Shell over a NavigationPage. NavigationPage is recommended to use when you have just a few layers and will always go back and forth just one page (pop and push).

Most of the time it's not the application that is too complex for Shell, it's the need for making things overly complicated that get in the way. Over-SOLIDifying is mostly the problem, trying to apply backend-habits to a frontend application. 

# Lifecycle 
In the App.xaml.cs you can see the App methods, that are called when the App enters a specific state. The states: Active, Stopped, Destroyed

# Page lifecycle and UI Threading
Dispatcher, InvokeOnMainThread, OnAppearing()

# Custom Controls & Handlers


# Configuration 

# Performance
When you feel like your app is very slow while debugging, first try to test the performance in Release mode. .NET MAUI has the <UseInterpreter> set to True by default, which enables hot reload but can slow down debug performance.

### Compiled Bindings
Compiled bindings improve data binding performance in .NET MAUI apps by resolving binding expressions at compile time, rather than at runtime with reflection. Compiling a binding expression generates compiled code that typically resolves a binding 8-20 times quicker than using a classic binding. 

To use compiled bindings, make sure you set the `x:DataType` property on the VisualElements. This should at least be done on the Page (at the top of the hierarchy), but can also be done locally, for example when you have a collection of a Product type. Try to avoid many different types of DataTypes on a single page.
By default, .NET MAUI produces build warnings for XAML bindings that don't use compiled bindings.

Prior to .NET MAUI 9, the XAML compiler would skip compilation of bindings that define the Source property instead of the BindingContext. From .NET MAUI 9, these bindings can be compiled to take advantage of better runtime performance. However, this optimization isn't enabled by default to avoid breaking existing app code. To enable this optimization, set the $(MauiEnableXamlCBindingWithSourceCompilation) build property to true in your app's project file:
`<MauiEnableXamlCBindingWithSourceCompilation>true</MauiEnableXamlCBindingWithSourceCompilation>`

Then, ensure that all your bindings are annotated with the correct x:DataType and that they don't inherit incorrect data types from their parent scope:

`<Label Text="{Binding Text, Source={x:Reference entry}, x:DataType=Entry}" />`

TODO: list the different types of bindings and the default: OneWay, OneWayToSource & TwoWay.

Just setting `Text="Accept"` is less overhead than `Text="{Binding AcceptText}"` and then one-time set the `AcceptText = "Accept"` in the viewmodel. 

# Outro (40/45)
Summary of the topics discussed.
KISS: What I've learned when I was in school, which wasn't that long ago, is that 
Find me later for questions