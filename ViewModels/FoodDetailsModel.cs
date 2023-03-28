using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp8.Model;
using MauiApp8.Services.DataServices;
using MauiApp8.Views;

using System.Collections.ObjectModel;

namespace MauiApp8.ViewModel
{
    [QueryProperty(nameof(Food), nameof(Food))]

    public partial class FoodDetailsModel : ObservableObject
    {
        IDataService dataService;

        [ObservableProperty]
        Food food;


        [ObservableProperty]
        string entry;

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

            // Check if the grams value is valid
            if (Grams.IsZeroOrNaN())
            {
                // Display an alert if the grams value is not valid
                await Shell.Current.DisplayAlert("Error", "Please enter a valid value for grams.", "OK");
                return;
            }

            // Create a new FoodViewModel object with the selected food and the grams value
            var foodVM = new FoodViewModel(Food) { Grams = Grams };


            if (LogFood.SelectedFoodsVM == null)
            {
                LogFood.SelectedFoodsVM = new MvvmHelpers.ObservableRangeCollection<FoodViewModel>();
            }

            // Add the new FoodViewModel object to the SelectedFoodsVM collection
            LogFood.SelectedFoodsVM.Add(foodVM);

            // Navigate to the FoodPage
            await Shell.Current.GoToAsync($"{nameof(FoodPage)}");

            // Clear the grams value
            ClearGrams();
        }

        public void ClearGrams()
        {
            Grams = 0;
        }


        [RelayCommand]
        async Task NavigateBack()
        {
            
            await Shell.Current.GoToAsync("..");
            ClearGrams();

        }

        [RelayCommand]
        Task AddSelectedFood() => Shell.Current.GoToAsync("..");

    }
}
