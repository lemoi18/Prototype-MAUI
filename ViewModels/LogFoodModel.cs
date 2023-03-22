

using MauiApp8.Model;
using MauiApp8.Services.Authentication;
using MauiApp8.Services.DataServices;
using MvvmHelpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;



namespace MauiApp8.ViewModel
{
    public partial class LogFoodModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        IAuthenticationService authService;
        IDataService dataService;
        
        private string _searchText;







        [ObservableProperty]
        Account _User;

        [ObservableProperty]
        MvvmHelpers.ObservableRangeCollection<FoodViewModel> foodVM;
        [ObservableProperty]
        MvvmHelpers.ObservableRangeCollection<Food> foods;
        //private MvvmHelpers.ObservableRangeCollection<FoodViewModel> foodsVm;
        //public MvvmHelpers.ObservableRangeCollection<FoodViewModel> FoodCollectionVM
        //{
        //    get => foodsVm;

        //    set => SetProperty(ref foodsVm, value);
        //}
        //// Define the public property for the observable collection

        //private MvvmHelpers.ObservableRangeCollection<Food> foods;
        //public MvvmHelpers.ObservableRangeCollection<Food> FoodCollection
        //{
        //    get => foods;
        //    set => SetProperty(ref foods, value);
        //}



        //private MvvmHelpers.ObservableRangeCollection<Food> _selectedFoods;
        //public MvvmHelpers.ObservableRangeCollection<Food> SelectedFood
        //{
        //    get => _selectedFoods;
        //    set=> SetProperty(ref _selectedFoods, value);
        //}

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                Task.Run(()=> UpdateSearchResultsAsync(value));

            }
        }



        public LogFoodModel(IAuthenticationService authService, IDataService dataService)
        {

            this.authService = authService;
            
           this.dataService = dataService;
            this.Foods = new MvvmHelpers.ObservableRangeCollection<Food>();
            this.FoodVM = new MvvmHelpers.ObservableRangeCollection<FoodViewModel>();
            
            InitializeAsync();
            //NavigateToFoodDetailsCommand = new RelayCommand<FoodViewModel>(NavigateToFoodDetails);

        }

        //private async void NavigateToFoodDetails(FoodViewModel selectedFoodViewModel)
        //{
        //    var route = $"{nameof(FoodDetailsModel)}?foodId={selectedFoodViewModel.Food.Id}";
        //    await Shell.Current.GoToAsync(route);
        //}
        private async Task InitializeAsync()
        {
            await LoadFoodsAsync();
            //NavigateToFoodDetailsCommand = new RelayCommand<FoodViewModel>(NavigateToFoodDetails);
        }

        private async Task LoadFoodsAsync()
        {
            var foodService = await dataService.GetFoods();
            if (foodService == null)
            {
                await Shell.Current.DisplayAlert(
                          "Error",
                           "An error occurred.",
                           "Close");
                return;
            }
            Task.Delay(50);
            Console.WriteLine($"Loaded {foodService.Count} food items from the dataService");

            this.Foods = new ObservableRangeCollection<Food>(foodService);

            var list = new List<FoodViewModel>();
            foreach (var food in foodService)
            {
                var foodVM = new FoodViewModel(food);

                list.Add(foodVM);
           }
            Console.WriteLine($"Created {list.Count} FoodViewModel instances");

            this.FoodVM.ReplaceRange(list);
            Console.WriteLine($"FoodCollectionVM contains {this.FoodVM.Count} items");

        }



        public async Task UpdateSearchResultsAsync(string query)
        {
            if (this.Foods == null)
            {
                Console.WriteLine("FoodCollection is null");
                return;
            }

            if (this.FoodVM == null)
            {
                Console.WriteLine("FoodCollectionVM is null");
                return;
            }

            if (string.IsNullOrEmpty(query))
            {
                this.FoodVM.ReplaceRange(this.Foods?.Select(food => new FoodViewModel(food)) ?? Enumerable.Empty<FoodViewModel>());
            }
            else
            {
                var filteredResults = this.Foods
                          .Where(p => !string.IsNullOrEmpty(p.Name) &&
                                 p.Name.ToLower().Contains(query.ToLower()));
                this.FoodVM.ReplaceRange(filteredResults.Select(food => new FoodViewModel(food)));
            }
            await Task.CompletedTask;
        }


        //public ICommand NavigateToFoodDetailsCommand { get; }



        async Task Find(string query) => await UpdateSearchResultsAsync(query);


        [RelayCommand]
        Task NavigateBack() => Shell.Current.GoToAsync("..");

        [RelayCommand]
        async Task AddSelectedFood(string query) => await UpdateSearchResultsAsync(query);

        //[RelayCommand]
        //Task NavigateToDetail() => Shell.Current.GoToAsync($"{nameof(FoodDetailsModel)}?Id={SelectedFoods]}",
        //    new Dictionary<string,object>
        //    (
        //       ["Food"]= SelectedFoods
                
        //    );

    }
}

