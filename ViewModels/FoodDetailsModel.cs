using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Apis.PeopleService.v1.Data;
using MauiApp8.Model;
using MauiApp8.Services.Authentication;
using MauiApp8.Services.DataServices;
using MauiApp8.Views;
using Microsoft.Maui.Controls.Compatibility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiApp8.ViewModel
{
    [QueryProperty(nameof(Food), nameof(Food))]

    public partial class FoodDetailsModel : ObservableObject
    {
        IDataService dataService;

        [ObservableProperty]
        Food food;
    
        [ObservableProperty]
        double grams;

        [ObservableProperty]
        LogFoodModel logFood;

        public FoodDetailsModel(IDataService dataService, LogFoodModel logFoodModel)
        {
        
            this.dataService = dataService;
            
            this.logFood = logFoodModel;

        }



        [RelayCommand]
        async Task NavigateToBackLog()
        {


            var list = new List<FoodViewModel>();
            
            
                var foodVM = new FoodViewModel(Food);
                
    
                            


            await Shell.Current.GoToAsync($"{nameof(FoodPage)}");
        }




        [RelayCommand]
        Task NavigateBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        Task AddSelectedFood() => Shell.Current.GoToAsync("..");

    }
}
