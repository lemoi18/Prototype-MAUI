
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Apis.PeopleService.v1.Data;
using MauiApp8.Model;
using MauiApp8.Services.Authentication;
using MauiApp8.Services.DataServices;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility;
using MvvmHelpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiApp8.ViewModel
{
    public partial class FoodPageModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IAuthenticationService authService;
        IDataService dataService;
        private string _searchText;

        [ObservableProperty]
         Account _User;

        [ObservableProperty]
        Food _selectedFood;

        [ObservableProperty]
        MvvmHelpers.ObservableRangeCollection<FoodViewModel> foods;

        // Define the public property for the observable collection

        [ObservableProperty]
        private ObservableCollection<Food> foodCollection;


        [ObservableProperty]
        MvvmHelpers.ObservableRangeCollection<Food> _selectedFoods;


        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    UpdateSearchResultsAsync(value);
                }
            }
        }



        public FoodPageModel(IAuthenticationService authService, IDataService dataService)
        {

            this.authService = authService;

           this.dataService = dataService;
            //foods = new MvvmHelpers.ObservableRangeCollection<FoodViewModel>();

            //LoadFoodsAsync();
            //NavigateToFoodDetailsCommand = new RelayCommand<FoodViewModel>(NavigateToFoodDetails);

        }

        //private async void NavigateToFoodDetails(FoodViewModel selectedFoodViewModel)
        //{
        //    var route = $"{nameof(FoodDetailsModel)}?foodId={selectedFoodViewModel.Food.Id}";
        //    await Shell.Current.GoToAsync(route);
        //}


        //private async void LoadFoodsAsync()
        //{
        //    var foodService = await dataService.GetFoods();
        //    foodCollection = new ObservableCollection<Food>(foodService);

        //    var list = new List<FoodViewModel>();
        //    foreach (var food in foodService)
        //    {
        //        var foodVM = new FoodViewModel(food);

        //        list.Add(foodVM);
        //    }
        //    foods.ReplaceRange(list);
        //}



        public async Task UpdateSearchResultsAsync(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                foods.ReplaceRange(foodCollection.Select(food => new FoodViewModel(food)));
            }
            else
            {
                var filteredResults = foodCollection.Where(p => p.Name.ToLower().Contains(query.ToLower()));
                foods.ReplaceRange(filteredResults.Select(food => new FoodViewModel(food)));
            }
            await Task.CompletedTask;
        }

        //public ICommand NavigateToFoodDetailsCommand { get; }



        

        [RelayCommand]
        public async Task Search(string query) => await UpdateSearchResultsAsync(query);


        [RelayCommand]
        Task NavigateBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        Task AddSelectedFood() => Shell.Current.GoToAsync("..");

    }
}

