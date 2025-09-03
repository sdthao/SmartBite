using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using SmartBite.Models.Food;
using SmartBite.Models.User;
using SmartBite.Services;
using System.Collections.ObjectModel;

namespace SmartBite.Maui.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ILogger<MainViewModel> _logger;

        [ObservableProperty]
        private UserProfile user;

        [ObservableProperty]
        private string selectedDate;

        [ObservableProperty]
        private bool isDatePickerOpen;

        [ObservableProperty]
        private ObservableCollection<FoodItem> foodItems;

        public MainViewModel(ILogger<MainViewModel> logger, UserContext user)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            User = user.CurrentUser ?? default;
        }
    }
}
