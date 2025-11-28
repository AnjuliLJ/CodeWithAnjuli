using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiBeyondHelloWorld.Views.Layouts;

public partial class AbsoluteLayoutViewModel : ObservableObject
{

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
