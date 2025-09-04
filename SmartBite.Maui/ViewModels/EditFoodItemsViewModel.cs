using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SmartBite.Common;
using SmartBite.Common.CalorieMeasurement;
using SmartBite.Common.NutrientMeasurement;
using SmartBite.Common.WeightMeasurement;
using SmartBite.Enums;
using SmartBite.Maui.Models;
using SmartBite.Models.Food;
using SmartBite.Models.User;
using SmartBite.Services;
using System.Collections.ObjectModel;

namespace SmartBite.Maui.ViewModels
{
    public partial class EditFoodItemsViewModel : ObservableObject
    {
        private readonly ILogger<EditFoodItemsViewModel> _logger;

        private readonly IFoodDbService _foodDbService;

        #region Binded Properties
        [ObservableProperty]
        private UserContext _userContext;

        [ObservableProperty]
        private ObservableCollection<FoodItem> _foodItems;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private string foodName;

        [ObservableProperty]
        private double calories;

        [ObservableProperty]
        private double weight;

        [ObservableProperty]
        private double vitaminA;

        [ObservableProperty]
        private double vitaminC;

        [ObservableProperty]
        private double vitaminD;

        [ObservableProperty]
        private string selectedCalorieScale;

        [ObservableProperty]
        private string selectedWeightScale;

        [ObservableProperty]
        private string selectedVitaminAWeightScale;

        [ObservableProperty]
        private string selectedVitaminCWeightScale;

        [ObservableProperty]
        private string selectedVitaminDWeightScale;

        #endregion

        public List<string> CalorieScales =>
            Enum.GetValues(typeof(CalorieMeasurementScaleEnums))
            .Cast<CalorieMeasurementScaleEnums>()
            .Where(e => e != CalorieMeasurementScaleEnums.UNKNOWN)
            .Select(e => e.ToString().ToLowerInvariant())
            .ToList();

        public List<string> WeightScales =>
            Enum.GetValues(typeof(WeightMeasurementScaleEnums))
            .Cast<WeightMeasurementScaleEnums>()
            .Where(e => e != WeightMeasurementScaleEnums.UNKNOWN)
            .Select(e => e.ToString().ToLowerInvariant())
            .ToList();

        public List<string> NutrientWeightScales =>
            Enum.GetValues(typeof(NutrientWeightMeasurementScaleEnums))
            .Cast<NutrientWeightMeasurementScaleEnums>()
            .Where(e => e != NutrientWeightMeasurementScaleEnums.UNKNOWN)
            .Select(e => e.ToString().ToLowerInvariant())
            .ToList();

        public EditFoodItemsViewModel(ILogger<EditFoodItemsViewModel> logger, IFoodDbService foodDbService, UserContext userContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _foodDbService = foodDbService ?? throw new ArgumentNullException(nameof(foodDbService));

            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));

            _foodItems = new();
        }

        #region Commands
        [RelayCommand]
        private async Task AddFoodItem()
        {
            ErrorMessage = string.Empty;

            try
            {
                if (ValidateFoodItem() == false)
                    return;

                var foodItemToAdd = BuildFoodItem();

                var result = await _foodDbService.AddFoodItemAsync(UserContext.CurrentUser.UserId, foodItemToAdd);

                if (result.Success == false)
                {
                    _logger.LogWarning(result.Message);

                    ErrorMessage = result.Message;

                    return;
                }

                FoodItems.Add(foodItemToAdd);

                _logger.LogInformation($"Food item '{FoodName}' added successfully.");

                ClearFoodItem();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error adding food item: {ex.Message}";

                _logger.LogError(ex, "Error adding food item.");
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            ClearFoodItem();

            await Shell.Current.GoToAsync("main");
        }

        #endregion

        private bool ValidateFoodItem()
        {
            if (string.IsNullOrWhiteSpace(FoodName))
            {
                ErrorMessage = "Food name cannot be empty.";

                _logger.LogWarning(ErrorMessage);

                return false;
            }

            if (string.IsNullOrWhiteSpace(SelectedCalorieScale))
            {
                ErrorMessage = "Calorie scale cannot be empty.";

                _logger.LogWarning(ErrorMessage);

                return false;
            }

            if (string.IsNullOrWhiteSpace(SelectedWeightScale))
            {
                ErrorMessage = "Weight scale cannot be empty";

                _logger.LogWarning(ErrorMessage);

                return false;
            }

            if (string.IsNullOrWhiteSpace(SelectedVitaminAWeightScale))
            {
                ErrorMessage = "Vitamin A weight scale cannot be empty";

                _logger.LogWarning(ErrorMessage);

                return false;
            }

            if (string.IsNullOrWhiteSpace(SelectedVitaminCWeightScale))
            {
                ErrorMessage = "Vitamin C weight scale cannot be empty";

                _logger.LogWarning(ErrorMessage);

                return false;
            }

            if (string.IsNullOrWhiteSpace(SelectedVitaminDWeightScale))
            {
                ErrorMessage = "Vitamin D weight scale cannot be empty";

                _logger.LogWarning(ErrorMessage);

                return false;
            }

            return true;
        }

        private void ClearFoodItem()
        {
            FoodName = string.Empty;
            Calories = 0;
            SelectedCalorieScale = string.Empty;
            Weight = 0;
            SelectedWeightScale = string.Empty;
            VitaminA = 0;
            SelectedVitaminAWeightScale = string.Empty;
            VitaminC = 0;
            SelectedVitaminCWeightScale = string.Empty;
            VitaminD = 0;
            SelectedVitaminDWeightScale = string.Empty;
        }

        private FoodItem BuildFoodItem()
        {
            var calories = GetCalorieMeasurement(SelectedCalorieScale, Calories);

            var weight = GetWeightMeasurement(SelectedWeightScale, Weight);

            var nutrients = new List<Vitamin>
            {
                new VitaminA(GetVitaminMeasurement(SelectedVitaminAWeightScale, VitaminA)),
                new VitaminC(GetVitaminMeasurement(SelectedVitaminCWeightScale, VitaminC)),
                new VitaminD(GetVitaminMeasurement(SelectedVitaminDWeightScale, VitaminD))
            };

            return new FoodItem(FoodName, calories, weight, nutrients);
        }

        private IVitaminMeasurement GetVitaminMeasurement(string scale, double value)
        {
            return scale.ToLower() switch
            {
                "mcg" => new MicroGram(value),
                "mg" => new MilliGram(value),
                _ => throw new InvalidOperationException($"Unknown vitamin scale: {scale}")
            };
        }

        private ICalorieMeasurement GetCalorieMeasurement(string scale, double value)
        {   
            return scale.ToLower() switch
            {
                "cal" => new Calorie(value),
                "kcal" => new KiloCalorie(value),
                "j" => new Joule(value),
                "kj" => new KiloJoule(value),
                _ => throw new InvalidOperationException($"Unknown calorie scale: {scale}")
            };
        }

        private IWeightMeasurement GetWeightMeasurement(string scale, double value)
        {
            return scale.ToLower() switch
            {
                "g" => new Gram(value),
                "kg" => new KiloGram(value),
                "oz" => new Ounce(value),
                "lb" => new Pound(value),
                _ => throw new InvalidOperationException($"Unknown weight scale: {scale}")
            };
        }
    }
}
