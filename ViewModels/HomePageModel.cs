using CommunityToolkit.Mvvm.Input;


namespace MauiApp8.ViewModel
{
    public partial class HomePageModel
    {

        [RelayCommand]
        Task NavigateBack() => Shell.Current.GoToAsync("..");
       
    }
}

