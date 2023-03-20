using MauiApp8.Model;
using MauiApp8.ViewModel;

namespace MauiApp8.Views;

public partial class FoodDetailsPage : ContentPage
{
    public FoodDetailsPage(Food food, FoodDetailsModel vm)
    {
        InitializeComponent();

        BindingContext = vm;

    }



}