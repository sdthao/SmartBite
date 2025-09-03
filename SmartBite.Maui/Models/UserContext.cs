using SmartBite.Models.User;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SmartBite.Maui.Models
{
    public partial class UserContext : ObservableObject
    {
        private UserProfile? _currentUser;

        public UserProfile? CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public void SetUser(UserProfile user)
        {
            CurrentUser = user;
        }

        public void Clear()
        {
            CurrentUser = null;
        }
    }
}
