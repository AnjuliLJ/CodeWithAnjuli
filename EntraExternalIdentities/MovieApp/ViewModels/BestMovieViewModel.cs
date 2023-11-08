using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieApp.Models;
using MovieApp.Settings;

namespace MovieApp.ViewModels
{
    [QueryProperty(nameof(User), "User")]
    public partial class BestMovieViewModel : ObservableObject
    {
        [ObservableProperty]  
        User user;

        [ObservableProperty]
        string bestMovie;

        [RelayCommand]
        async void GetMovie()
        {
            using var http = new HttpClient();
            var response = await http.GetAsync(ConnectionSettings.MovieApiUrl);
            BestMovie = await response.Content.ReadAsStringAsync();
        }
    }
}
