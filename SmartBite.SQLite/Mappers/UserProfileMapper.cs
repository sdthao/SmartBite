using SmartBite.Common;
using SmartBite.Models.User;
using SmartBite.SQLite.Models;

namespace SmartBite.SQLite.Mappers
{
    public class UserProfileMapper : IMapper<UserProfile, UserProfileEntity>
    {
        public IResult<UserProfileEntity> Map(UserProfile value, params object[]? parameters)
        {
            ArgumentNullException.ThrowIfNull(value);

            try
            {
                var userProfileEntity = new UserProfileEntity
                {
                    UserId = value.UserId,
                    UserName = value.UserName,
                    Password = value.Password,
                    FirstName = value.UserInfo.FirstName,
                    LastName = value.UserInfo.LastName,
                    Email = value.UserInfo.Email,
                    DateOfBirth = value.UserInfo.DateOfBirth
                };

                return Result.Successful(userProfileEntity);
            }
            catch (Exception ex)
            {
                return Result.Failure<UserProfileEntity>($"Mapping failed: {ex.Message}");
            }
        }
    }
}
