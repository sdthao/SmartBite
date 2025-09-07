
namespace SmartBite.Models.User
{
    public class UserInfo
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public UserInfo(string firstname, string lastname, string email, DateTime dateOfBirth)
        {
            FirstName = string.IsNullOrEmpty(firstname) ? throw new ArgumentNullException(nameof(firstname)) : firstname;
            LastName = string.IsNullOrEmpty(lastname) ? throw new ArgumentNullException(nameof(lastname)) : lastname;
            Email = string.IsNullOrEmpty(email) ? throw new ArgumentNullException(nameof(email)) : email;
            DateOfBirth = dateOfBirth;
        }
    }
}
