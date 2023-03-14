﻿using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp8.Model;
using MauiApp8.Services;
using MauiApp8.Views;
using MauiApp8.Helpers;
using Microsoft.Maui.Networking;

namespace MauiApp8.ViewModel
{

    public partial class LoginPageModel : ObservableObject
    {



        IAuthenticationService _authService;
        private Test _user;


        public LoginPageModel(IAuthenticationService authService)
        {

            this._authService = authService;


        }

        public Test User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }


       
        



        [RelayCommand]
        Task NavigateToSettings() => Shell.Current.GoToAsync(nameof(SettingsPage));
        [RelayCommand]
        Task NavigateToHome() => Shell.Current.GoToAsync(nameof(HomePage));


        [RelayCommand]
        async Task NavigateToGoogle()
        {
            User = await _authService.AuthenticateAsync();
            _authService.User = User;
            if (User.LoginSuccessful)
            {
                Application.Current.MainPage = new AppShell();
            }
            
        }

        

    }

}