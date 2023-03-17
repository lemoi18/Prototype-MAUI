using MauiApp8.ViewModel;
namespace MauiApp8.Views;

public partial class FoodPage : ContentPage
{
    public FoodPage(FoodPageModel vm)
    {
        InitializeComponent();

        BindingContext = vm;

    }


}