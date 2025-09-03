using SmartBite.Services;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartBite.Maui.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly ILogger<LoginViewModel> _logger;

        private readonly IUserDbService _databaseService;

        private readonly UserContext _userContext;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string loginMessage;

        [ObservableProperty]
        private bool isBusy;

        public LoginViewModel(ILogger<LoginViewModel> logger, IUserDbService db, UserContext userContext)
        {
            _databaseService = db ?? throw new ArgumentNullException(nameof(db));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                LoginMessage = "Please enter username and password.";
                return;
            }

            IsBusy = true;
            LoginMessage = string.Empty;

            try
            {
                _logger.LogInformation($"Attempting login for user: {Username}");

                var user = await _databaseService.LoginAsync(Username, Password);

                if (user.Success == false)
                {
                    LoginMessage = "Invalid username or password.";
                    return;
                }

                _userContext.SetUser(user.Value);

                await Shell.Current.GoToAsync($"main");
            }
            catch (Exception ex)
            {
                LoginMessage = $"An error occurred: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task CreateAccountAsync()
        {
            await Shell.Current.GoToAsync("register");
        }
    }
}
