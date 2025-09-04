using SmartBite.Services;
using SmartBite.Maui.Models;
using SmartBite.Models.Food;
using SmartBite.Models.User;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartBite.Maui.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ILogger<MainViewModel> _logger;

        private readonly IFoodDbService _foodDbService;

        #region Binded Properties
        [ObservableProperty]
        private UserProfile user;

        [ObservableProperty]
        private string greetingMessage = string.Empty;

        [ObservableProperty]
        private string selectedDate;

        [ObservableProperty]
        private bool isDatePickerOpen;

        [ObservableProperty]
        private UserContext userContext;

        [ObservableProperty]
        private ObservableCollection<FoodItem> foodItems;

        #endregion

        #region Commands
        [RelayCommand]
        private void DatePickerOpen() => IsDatePickerOpen = true;

        [RelayCommand]
        private void DatePickerClose() => IsDatePickerOpen = false;

        [RelayCommand]
        private async Task MoreOptions() => await Shell.Current.GoToAsync($"options");

        [RelayCommand]
        private async Task Journal() => await Shell.Current.GoToAsync($"journal");

        [RelayCommand]
        private async Task EditFood() => await Shell.Current.GoToAsync($"editfood");

        [RelayCommand]
        private async Task Camera() => await Shell.Current.GoToAsync($"camera");

        [RelayCommand]
        private async Task AITool() => await Shell.Current.GoToAsync($"aitool");
        
        #endregion

        public MainViewModel(ILogger<MainViewModel> logger, IFoodDbService foodDbService, UserContext user)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _foodDbService = foodDbService ?? throw new ArgumentNullException(nameof(foodDbService));

            UserContext = user ?? throw new ArgumentNullException(nameof(user));

            User = user.CurrentUser ?? throw new ArgumentNullException(nameof(user));

            UserContext.PropertyChanged += (s, a) => { GreetingMessage = $"Hello, {User.UserInfo.FirstName}"; };

            GreetingMessage = $"Hello, {User.UserInfo.FirstName}";

            SelectedDate = DateTime.Now.ToString("yyyy-MM-dd");

            Initialize();
        }

        private void Initialize()
        {
            try
            {
                _logger.LogInformation("Initializing...");

                _ = Task.Run(async () =>
                {
                    var getFoodItems = await _foodDbService.GetFoodsForUserAsync(User.UserId, DateTime.Parse(SelectedDate));

                    if (getFoodItems.Success == false)
                    {
                        _logger.LogWarning(getFoodItems.Message);

                        FoodItems = new ObservableCollection<FoodItem>();

                        return;
                    }

                    FoodItems = new ObservableCollection<FoodItem>(getFoodItems.Value);

                }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing.");
            }
        }
        
    }
}
