using MauiApp8.ViewModel;
using MauiApp8.Model;
namespace MauiApp8.Views;

public partial class FoodPage : ContentPage
{
    public FoodPage(LogFoodModel vm)
    {
        InitializeComponent();

        BindingContext = vm;

    }


    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = BindingContext as LogFoodModel;
        await viewModel.UpdateSearchResultsAsync(e.NewTextValue);
    }

    void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Food _selectedFoods)
        {
            // Add the selected food to the collection
            var viewModel = BindingContext as LogFoodModel;

            //viewModel.SelectedFoods.Add(selectedFood);

            // Clear the selection
            ((CollectionView)sender).SelectedItem = null;
        }
    }


    

    protected override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            // Code to load data into FoodCollection
        }
        catch (Exception ex)
        {
             DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }


}