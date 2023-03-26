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



}