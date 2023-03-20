using MauiApp8.ViewModel;
using MauiApp8.Model;
namespace MauiApp8.Views;

public partial class FoodPage : ContentPage
{
    public FoodPage(FoodPageModel vm)
    {
        InitializeComponent();

        BindingContext = vm;

    }


    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = BindingContext as FoodPageModel;
        await viewModel.UpdateSearchResultsAsync(e.NewTextValue);
    }

    void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Food selectedFood)
        {
            // Add the selected food to the collection
            var viewModel = BindingContext as FoodPageModel;

            //viewModel.SelectedFoods.Add(selectedFood);

            // Clear the selection
            ((ListView)sender).SelectedItem = null;
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