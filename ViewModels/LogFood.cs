using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp8.Model;
using MauiApp8.Services;


namespace MauiApp8.ViewModel
{
    public partial class FoodPageModel : ObservableObject
    {
        IAuthenticationService authService;

        public FoodPageModel(IAuthenticationService authService)
        {

            this.authService = authService;
            _user = authService.User;
            // Access the CurrentUser property to get the user object
        }
        public Account User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        private Account _user;



        [RelayCommand]
        Task NavigateBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        Task AddItem() => Shell.Current.GoToAsync("..");

    }
}

