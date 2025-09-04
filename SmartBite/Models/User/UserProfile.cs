
namespace SmartBite.Models.User
{
    public class UserProfile
    {
        public UserInfo UserInfo { get; set; } = default!;

        public string UserId { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public UserProfile(string userId, string userName, string password, UserInfo userInfo)
        {
            UserId = string.IsNullOrEmpty(userId) ? throw new ArgumentNullException(nameof(userId)) : userId;
            UserName = string.IsNullOrEmpty(userName) ? throw new ArgumentNullException(nameof(userName)) : userName;
            Password = string.IsNullOrEmpty(password) ? throw new ArgumentNullException(nameof(password)) : password;
            UserInfo = userInfo ?? throw new ArgumentNullException(nameof(userInfo));
        }
    }
}
