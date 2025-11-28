using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiBeyondHelloWorld.Views;
using MauiBeyondHelloWorld.Views.Layouts;

namespace MauiBeyondHelloWorld.Views;

public partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    private async Task NavigateToAbsoluteLayout()
    {
        await Shell.Current.GoToAsync(nameof(AbsoluteLayoutPage));
    }

}
