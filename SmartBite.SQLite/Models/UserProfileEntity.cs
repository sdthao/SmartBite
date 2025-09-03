using SQLite;
using SmartBite.Models.User;

namespace SmartBite.SQLite.Models
{
    public class UserProfileEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserInfoId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string UserId { get; set; }

        [Ignore]
        public UserInfoEntity UserInfo { get; set; }

        public UserProfile ToDomain()
        {
            return new UserProfile(UserId, UserName, Password, UserInfo?.ToDomain());
        }

        public static UserProfileEntity FromDomain(UserProfile domain, int userInfoId, UserInfoEntity userInfoEntity)
        {
            ArgumentNullException.ThrowIfNull(domain);

            return new UserProfileEntity
            {
                UserId = domain.UserId,
                UserName = domain.UserName,
                Password = domain.Password,
                UserInfoId = userInfoId,
                UserInfo = userInfoEntity
            };
        }
    }
}
