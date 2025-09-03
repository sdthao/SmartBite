using SQLite;
using SmartBite.Common;
using SmartBite.Services;
using SmartBite.Models.User;
using Microsoft.Extensions.Logging;
using SmartBite.SQLite.Models;

namespace SmartBite.SQLite.Services
{
    public class SQLiteUserDbService : IUserDbService
    {
        private readonly ILogger _logger;

        private readonly SQLiteAsyncConnection _db;

        public SQLiteUserDbService(ILogger<SQLiteUserDbService> logger, SQLiteAsyncConnection db)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _db = db ?? throw new ArgumentNullException(nameof(db));
            _db.CreateTableAsync<UserInfoEntity>().Wait();
            _db.CreateTableAsync<UserProfileEntity>().Wait();
        }

        public async Task<IResult<UserProfile>> FindUserAsync(string username)
        {
            ArgumentNullException.ThrowIfNull(username);

            try
            {
                _logger.LogInformation($"Searching for user: {username}");

                var existingUser = await _db.Table<UserProfileEntity>()
                    .Where(u => u.UserName == username)
                    .FirstOrDefaultAsync();

                if (existingUser is null)
                {
                    _logger.LogInformation($"User {username} not found.");

                    return Result.Failure<UserProfile>($"User {username} not found.");
                }

                _logger.LogInformation($"User {username} found.");

                var userProfile = existingUser.ToDomain();

                return Result.Successful(userProfile);
            }
            catch (Exception ex)
            {
                return Result.Failure<UserProfile>(ex.Message);
            }
        }

        public async Task<IResult<int>> CreateUserAsync(UserProfile profile)
        {
            ArgumentNullException.ThrowIfNull(profile);

            try
            {  
                _logger.LogInformation($"Adding user: {profile.UserName}");

                var userInfoEntity = UserInfoEntity.FromDomain(profile.UserInfo);
                
                await _db.InsertAsync(userInfoEntity);

                var profileEntity = UserProfileEntity.FromDomain(profile, userInfoEntity.Id, userInfoEntity);

                var rows = await _db.InsertAsync(profileEntity);

                _logger.LogInformation($"User added with ID: {profileEntity.Id}");

                return Result.Successful(rows);
            }
            catch (Exception ex)
            {
                return Result.Failure<int>(ex.Message);
            }
        }

        public async Task<IResult<UserProfile?>> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Username or password cannot be empty.");
            }

            try
            {
                _logger.LogInformation($"Attempting login for user: {username}");

                var profileEntity = await _db.Table<UserProfileEntity>()
                    .Where(u => u.UserName == username && u.Password == password)
                    .FirstOrDefaultAsync();

                if (profileEntity == null)
                    return Result.Failure<UserProfile?>("Username or password is incorrect.");

                profileEntity.UserInfo = await _db.Table<UserInfoEntity>()
                    .Where(ui => ui.Id == profileEntity.UserInfoId)
                    .FirstOrDefaultAsync();

                var userProfile = profileEntity.ToDomain();

                _logger.LogInformation($"User {username} logged in successfully.");

                return Result.Successful(userProfile);
            }
            catch (Exception ex)
            {
                return Result.Failure<UserProfile?>(ex.Message);
            }
        }
    }
}
