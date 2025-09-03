using SQLite;
using SmartBite.Models.User;

namespace SmartBite.SQLite.Models
{
    public class UserInfoEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public UserInfo ToDomain()
        {
            return new UserInfo(FirstName, LastName, Email, DateOfBirth);
        }

        public static UserInfoEntity FromDomain(UserInfo domain)
        {
            ArgumentNullException.ThrowIfNull(domain);

            if (string.IsNullOrEmpty(domain.FirstName) || string.IsNullOrEmpty(domain.LastName))
            {
                throw new ArgumentException("First and last name cannot be null or empty.");
            }

            return new UserInfoEntity
            {
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                Email = domain.Email,
                DateOfBirth = domain.DateOfBirth
            };
        }
    }
}
