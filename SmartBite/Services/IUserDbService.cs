using SmartBite.Common;
using SmartBite.Models.User;

namespace SmartBite.Services
{
    public interface IUserDbService
    {
        /// <summary>
        /// Asynchronously checks if a user exists in the database based on the provided profile.
        /// </summary>
        /// <param name="username">The username to find.</param>
        /// <returns>Success/Fail result of finding the user.</returns>
        Task<IResult<UserProfile>> FindUserAsync(string username);

        /// <summary>
        /// Asynchronously creates a new user in the database based on the provided profile.
        /// </summary>
        /// <param name="profile">The user profile to create.</param>
        /// <returns>Success/Fail result of creating the user profile.</returns>
        Task<IResult<int>> CreateUserAsync(UserProfile profile);

        /// <summary>
        /// Asynchronously logs in a user based on the provided username and password.
        /// </summary>
        /// <param name="username">The user name credential.</param>
        /// <param name="password">The password credential.</param>
        /// <returns>Success/Fail result of loging in with the provided username and password.</returns>
        Task<IResult<UserProfile?>> LoginAsync(string username, string password);
    }
}
