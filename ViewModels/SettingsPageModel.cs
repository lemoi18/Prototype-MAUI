using CommunityToolkit.Mvvm.Input;
using MauiApp8.Views;

namespace MauiApp8.ViewModel
{
    public partial class SettingsPageModel
    {

        [RelayCommand]
        Task NavigateBack() => Shell.Current.GoToAsync("..");
    }
}
