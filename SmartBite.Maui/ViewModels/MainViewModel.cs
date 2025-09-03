using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SmartBite.Common.CalorieMeasurement;
using SmartBite.Common.Measurement;
using SmartBite.Common.WeightMeasurement;
using SmartBite.Maui.Models;
using SmartBite.Models.Food;
using SmartBite.Models.User;
using System.Collections.ObjectModel;

namespace SmartBite.Maui.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ILogger<MainViewModel> _logger;

        [ObservableProperty]
        private UserProfile user;

        [ObservableProperty]
        private string greetingMessage = string.Empty;

        [ObservableProperty]
        private string selectedDate;

        [ObservableProperty]
        private bool isDatePickerOpen;

        [ObservableProperty]
        private UserContext _userContext;

        [ObservableProperty]
        private ObservableCollection<FoodItem> foodItems;

        public MainViewModel(ILogger<MainViewModel> logger, UserContext user)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            User = user.CurrentUser;

            UserContext = user;
            UserContext.PropertyChanged += (s, a) => { GreetingMessage = $"Hello, {User.UserInfo.FirstName}"; };

            GreetingMessage = $"Hello, {User.UserInfo.FirstName}";

            GenerateMockFoodList();
        }

        [RelayCommand]
        private async Task Settings()
        {
            await Task.Delay(1);
        }

        [RelayCommand]
        private async Task DatePickerOpen()
        {
            IsDatePickerOpen = true;
            await Task.Delay(1);
        }

        [RelayCommand]
        private async Task DatePickerClose()
        {
            IsDatePickerOpen = false;
            await Task.Delay(1);
        }

        [RelayCommand]
        private async Task AddFoodItem(FoodItem foodItem)
        {
            FoodItems.Add(foodItem);
            await Task.Delay(1);
        }

        [RelayCommand]
        private async Task RemoveFoodItem(FoodItem foodItem)
        {
            if (FoodItems != null && FoodItems.Contains(foodItem))
            {
                FoodItems.Remove(foodItem);
                OnPropertyChanged(nameof(FoodItems));
            }
            await Task.Delay(1);
        }

        private void GenerateMockFoodList()
        {
            FoodItems = new ObservableCollection<FoodItem>();
            FoodItems.Add(new FoodItem("Apple", Calorie.FromCalories(52), Gram.FromGrams(100), new List<INutrientMeasurement>()));
            FoodItems.Add(new FoodItem("Yogurt", Calorie.FromCalories(100), Gram.FromGrams(100), new List<INutrientMeasurement>()));
            FoodItems.Add(new FoodItem("Banana", Calorie.FromCalories(105), Gram.FromGrams(118), new List<INutrientMeasurement>()));
            FoodItems.Add(new FoodItem("Chicken Breast", Calorie.FromCalories(300), Ounce.FromOunces(16), new List<INutrientMeasurement>()));
            FoodItems.Add(new FoodItem("Oatmeal", Calorie.FromCalories(147), Gram.FromGrams(234), new List<INutrientMeasurement>()));
            FoodItems.Add(new FoodItem("Egg", Calorie.FromCalories(72), Gram.FromGrams(50), new List<INutrientMeasurement>()));
            FoodItems.Add(new FoodItem("Broccoli", Calorie.FromCalories(34), Gram.FromGrams(91), new List<INutrientMeasurement>()));
            FoodItems.Add(new FoodItem("Salmon", Calorie.FromCalories(400), Ounce.FromOunces(16), new List<INutrientMeasurement>()));

            OnPropertyChanged(nameof(FoodItems));
        }
    }
}
