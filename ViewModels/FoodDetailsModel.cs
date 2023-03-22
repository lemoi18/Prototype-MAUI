using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Apis.PeopleService.v1.Data;
using MauiApp8.Model;
using MauiApp8.Services.Authentication;
using MauiApp8.Services.DataServices;
using Microsoft.Maui.Controls.Compatibility;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiApp8.ViewModel
{

    [QueryProperty(nameof(Food), nameof(Food))]
    public partial class FoodDetailsModel : ObservableObject
    {

        public FoodDetailsModel()
        {

            


            // Access the CurrentUser property to get the user object
        }

        private string _searchText;
        [ObservableProperty]
         Account _user;
        [ObservableProperty]
         Food _food;
        [ObservableProperty]
         ObservableCollection<Food> _selectedFoods;


        public ICommand SearchCommand { get; }


        public ICommand PerformSearchCommand { get; set; }

      
       
        [RelayCommand]
        Task NavigateBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        Task AddSelectedFood() => Shell.Current.GoToAsync("..");

    }
}
