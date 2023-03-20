using MauiApp8.ViewModel;
using MauiApp8.Model;
namespace MauiApp8.Views;

public partial class FoodItemView : ContentView
{
    public static readonly BindableProperty FoodItemProperty =
        BindableProperty.Create(nameof(FoodItem), typeof(FoodViewModel), typeof(FoodItemView));

    public FoodViewModel FoodItem
    {
        get => (FoodViewModel)GetValue(FoodItemProperty);
        set => SetValue(FoodItemProperty, value);
    }

    public FoodItemView()
    {
        InitializeComponent();
    }




}