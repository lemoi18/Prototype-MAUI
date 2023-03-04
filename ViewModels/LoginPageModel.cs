using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp8.Model;
using MauiApp8.Services;
using MauiApp8.Views;

namespace MauiApp8.ViewModel
{
    
    public partial class LoginPageModel:ObservableObject
    {
        [ObservableProperty]

        User user;

        AuthService authService = new("google");


        
        [RelayCommand]
        Task NavigateToSettings() => Shell.Current.GoToAsync(nameof(SettingsPage));
        [RelayCommand]
        Task NavigateToHome() => Shell.Current.GoToAsync(nameof(HomePage));

        [RelayCommand]
        Task NavigateToGoogle() => authService.AuthenticateAsync();
    }
}
