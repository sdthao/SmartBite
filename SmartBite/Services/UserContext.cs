using SmartBite.Models.User;

namespace SmartBite.Services
{
    public class UserContext
    {
        public UserProfile? CurrentUser { get; private set; }

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
