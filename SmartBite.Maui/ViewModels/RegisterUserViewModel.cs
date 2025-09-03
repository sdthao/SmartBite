using SmartBite.Services;
using SmartBite.Models.User;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartBite.Maui.ViewModels
{
    public partial class RegisterUserViewModel : ObservableObject
    {
        private readonly IUserDbService _databaseService;

        private readonly ILogger<RegisterUserViewModel> _logger;

        private readonly UserContext _userContext;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private DateTime dateOfBirth;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty] 
        private string message;

        public RegisterUserViewModel(ILogger<RegisterUserViewModel> logger, IUserDbService db, UserContext userContext)
        {
            _databaseService = db ?? throw new ArgumentNullException(nameof(db));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        [RelayCommand]
        private async Task CreateUserAsync()
        {
            IsBusy = true;
            Message = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                    Message = "Username and password are required.";

                    return;
                }

                if (!IsPasswordValid(Password))
                {
                    Message = "Password must be at least 8 characters long, " +
                        "contain at least one number, " +
                        "one special character, and " +
                        "one capital character.";
                    
                    return;
                }

                if (!IsEmailValid(Email))
                {
                    Message = "Invalid email format.";

                    return;
                }

                var userInfo = new UserInfo
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    DateOfBirth = DateOfBirth
                };

                var existingUser = await _databaseService.FindUserAsync(Username);

                if (existingUser.Success)
                {
                    Message = $"Username {Username} already exists. Please choose a different username.";
                    
                    return;
                }

                var newUserId = Guid.NewGuid().ToString();

                var newUser = new UserProfile(newUserId, Username, Password, userInfo);

                var result = await _databaseService.CreateUserAsync(newUser);

                if (!result.Success)
                {
                    Message = "Account creation failed: " + result.Message;

                    return;   
                }

                _userContext.SetUser(newUser);

                _logger.LogInformation("User created successfully.");

                await Shell.Current.GoToAsync("main");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user.");
                Message = "An error occurred: " + ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            return Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase);
        }

        private bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < 8)
                return false;

            var oneCap = password.Any(char.IsUpper);
            var hasNumber = password.Any(char.IsDigit);
            var hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasNumber && hasSpecial && oneCap;
        }

    }
}
