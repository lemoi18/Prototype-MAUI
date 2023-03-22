using MauiApp8.Model;
using MauiApp8.ViewModel;

namespace MauiApp8.Views;

public partial class FoodDetailsPage : ContentPage
{


    public FoodDetailsPage(FoodDetailsModel vm)
    {
        InitializeComponent();

        BindingContext = vm;

    }


}