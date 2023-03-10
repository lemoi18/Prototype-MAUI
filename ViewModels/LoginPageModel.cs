using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp8.Model;
using MauiApp8.Services;
using MauiApp8.Views;
using Microsoft.Maui.Networking;

namespace MauiApp8.ViewModel
{

    public partial class LoginPageModel : ObservableObject
    {



        private AuthService _authService;

        public LoginPageModel(AuthService authService)
        {
            _authService = authService;
        }

        public AuthService AuthService
        {
            get { return _authService; }
            set { SetProperty(ref _authService, value); }
        }



        [RelayCommand]
        Task NavigateToSettings() => Shell.Current.GoToAsync(nameof(SettingsPage));
        [RelayCommand]
        Task NavigateToHome() => Shell.Current.GoToAsync(nameof(HomePage));

        [RelayCommand]
        Task NavigateToGoogle() => _authService.AuthenticateAsync();
        
    }
}
