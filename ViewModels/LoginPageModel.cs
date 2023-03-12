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



        private  AuthService _authService;
        public Test User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }


        private Test _user;
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
        async Task NavigateToGoogle()
        {
            User = await _authService.AuthenticateAsync();

            await Login();
        }

        [RelayCommand]
        async Task Login()
        {
            // Simulate a successful login
            bool isLoginSuccessful = true;

            if (isLoginSuccessful)
            {
                // Navigate to the Home page using Shell navigation
                await Shell.Current.GoToAsync(nameof(HomePage));

                // Remove all pages from the navigation stack except for the root page
                await Shell.Current.Navigation.PopToRootAsync();
            }
        }

    }

}
