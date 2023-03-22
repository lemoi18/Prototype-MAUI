using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp8.Model;

namespace MauiApp8.ViewModel
{
    public partial class FoodViewModel : ObservableObject
    {

        
        public Food Food { get; set; }
        public string Name => Food?.Name;

        public string Carbohydrates => Food?.Carbohydrates;

        public string Category => Food?.Category;

        public string Description => Food?.Description;

        public FoodViewModel( Food food)
        {
            Food = food;
        }

    }
}
