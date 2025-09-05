using SmartBite.Common;
using SmartBite.Services;
using SmartBite.Models.User;
using SmartBite.Maui.Models;
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

        [ObservableProperty]
        private UserContext _userContext;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string confirmedPassword;

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
                var validate = ValidateUserInfo();

                if (validate.Success == false)
                {
                    _logger.LogDebug(validate.Message);

                    Message = validate.Message;

                    return;
                }

                validate = ValidatePassword();

                if (validate.Success == false)
                {
                    _logger.LogDebug(validate.Message);

                    Message = validate.Message;
                    
                    return;
                }

                validate = ValidateEmail();

                if (validate.Success == false)
                {
                    _logger.LogDebug(validate.Message);

                    Message = validate.Message;

                    return;
                }

                var userInfo = new UserInfo(FirstName, LastName, Email, DateOfBirth);

                var existingUser = await _databaseService.FindUserAsync(Username);

                if (existingUser.Success)
                {
                    _logger.LogDebug(existingUser.Message);

                    Message = $"Username {Username} already exists. Please choose a different username.";
                    
                    return;
                }

                var newUserId = Guid.NewGuid().ToString();

                var newUser = new UserProfile(newUserId, Username, Password, userInfo);

                var result = await _databaseService.CreateUserAsync(newUser);

                if (!result.Success)
                {
                    _logger.LogDebug(result.Message);

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

        #region Validators

        private IResult ValidateUserInfo()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
                return Result.Failure("First name cannot be empty.");

            if (string.IsNullOrWhiteSpace(LastName))
                return Result.Failure("Las name cannot be empty.");

            if (string.IsNullOrWhiteSpace(Username))
                return Result.Failure("User name cannot be empty.");

            if (string.IsNullOrWhiteSpace(Password))
                return Result.Failure("Password cannot be empty.");

            return Result.Successful();
        }

        private IResult ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
                return Result.Failure("Email cannot be empty.");

            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            var match = Regex.IsMatch(Email, emailPattern, RegexOptions.IgnoreCase);

            if (!match)
                return Result.Failure("Email format is wrong.");

            return Result.Successful();
        }

        private IResult ValidatePassword()
        {
            if (string.IsNullOrWhiteSpace(Password))
                return Result.Failure("Password cannot be null or empty.");

            if (Password.Length < 8)
                return Result.Failure("Password must be longer than 8 characters.");

            if (!Password.Any(char.IsUpper))
                return Result.Failure("Password must have at least 1 uppercase letter.");

            if (!Password.Any(char.IsDigit))
                return Result.Failure("Password must have at least 1 number.");

            if (!Password.Any(ch => !char.IsLetterOrDigit(ch)))
                return Result.Failure("Password must have at least 1 special character.");

            if (Password != ConfirmedPassword)
                return Result.Failure("Password and confirmation password must match.");

            return Result.Successful();
        }

        #endregion
    }
}
