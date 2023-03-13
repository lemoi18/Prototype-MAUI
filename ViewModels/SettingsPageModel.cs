using CommunityToolkit.Mvvm.Input;
using MauiApp8.Views;

namespace MauiApp8.ViewModel
{
    public partial class SettingsPageModel
    {

        [RelayCommand]
        Task NavigateBack() => Shell.Current.GoToAsync("..");
        [RelayCommand]
        async Task<Page> SignOut()
        {
            return Application.Current.MainPage = new LoginShell();
        }
    }
}
