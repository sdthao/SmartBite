using SmartBite.Common;
using SmartBite.Models.User;
using SmartBite.SQLite.Models;

namespace SmartBite.SQLite.Mappers
{
    public class UserProfileEntityMapper : IMapper<UserProfileEntity, UserProfile>
    {
        public IResult<UserProfile> Map(UserProfileEntity value, params object[]? parameter)
        {
            ArgumentNullException.ThrowIfNull(value);

            try
            {
                var userProfile = new UserProfile(
                    value.UserId,
                    value.UserName,
                    value.Password,
                    new UserInfo(
                        value.FirstName,
                        value.LastName,
                        value.Email,
                        value.DateOfBirth
                    )
                );

                return Result.Successful(userProfile);
            }
            catch (Exception ex)
            {
                return Result.Failure<UserProfile>($"Mapping failed: {ex.Message}");
            }
        }
    }
}
