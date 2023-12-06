using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieApp.Models;
using System.Net.Http.Headers;

namespace MovieApp.ViewModels
{
    [QueryProperty(nameof(EntraResponse), "EntraResponse")]
    public partial class BestMovieViewModel : ObservableObject
    {
        [ObservableProperty]  
        EntraResponse entraResponse;

        [ObservableProperty]
        string bestMovie;

        [RelayCommand]
        async void GetMovie()
        {
            var token = await SecureStorage.GetAsync("token");
            using var http = new HttpClient();

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await http.GetAsync("https://movie-fans-api.azurewebsites.net/Movie");

            BestMovie = await response.Content.ReadAsStringAsync();
        }
    }
}
