using SQLite;
using SmartBite.Common;
using SmartBite.Services;
using SmartBite.Models.User;
using SmartBite.SQLite.Models;
using Microsoft.Extensions.Logging;

namespace SmartBite.SQLite.Services
{
    public class SQLiteUserDbService : IUserDbService
    {
        private readonly ILogger<SQLiteUserDbService> _logger;

        private readonly IMapper<UserProfile, UserProfileEntity> _userProfileMapper;

        private readonly IMapper<UserProfileEntity, UserProfile> _userProfileEntityMapper;

        private readonly SQLiteAsyncConnection _db;

        public SQLiteUserDbService(ILogger<SQLiteUserDbService> logger,
            IMapper<UserProfile, UserProfileEntity> userProfileMApper,
            IMapper<UserProfileEntity, UserProfile> userProfileEntityMapper,
            SQLiteAsyncConnection db)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _userProfileMapper = userProfileMApper ?? throw new ArgumentNullException(nameof(userProfileMApper));

            _userProfileEntityMapper = userProfileEntityMapper ?? throw new ArgumentNullException(nameof(userProfileEntityMapper));

            _db = db ?? throw new ArgumentNullException(nameof(db));
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
                    _logger.LogWarning($"User {username} not found.");

                    return Result.Failure<UserProfile>($"User {username} not found.");
                }

                var map = _userProfileEntityMapper.Map(existingUser);

                if (map.Success == false)
                {
                    _logger.LogError(map.Message);

                    return Result.Failure<UserProfile>(map.Message);
                }

                var userProfile = map.Value;

                _logger.LogInformation($"User {username} found.");

                return Result.Successful(userProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Result.Failure<UserProfile>(ex.Message);
            }
        }

        public async Task<IResult<int>> CreateUserAsync(UserProfile profile)
        {
            ArgumentNullException.ThrowIfNull(profile);

            try
            {  
                _logger.LogInformation($"Adding user: {profile.UserName}");

                var map = _userProfileMapper.Map(profile);

                if (map.Success == false)
                {
                    _logger.LogError(map.Message);

                    return Result.Failure<int>(map.Message);
                }

                var profileEntity = map.Value;
                
                var rows = await _db.InsertAsync(profileEntity);

                _logger.LogInformation($"User added with ID: {profileEntity.Id}");

                return Result.Successful(rows);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Result.Failure<int>(ex.Message);
            }
        }

        public async Task<IResult<UserProfile>> LoginAsync(string username, string password)
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
                {
                    _logger.LogWarning($"Login failed for user: {username}");

                    return Result.Failure<UserProfile>("Username or password is incorrect.");
                }

                var map = _userProfileEntityMapper.Map(profileEntity);

                if (map.Success == false)
                {
                    _logger.LogError(map.Message);

                    return Result.Failure<UserProfile>(map.Message);
                }

                var userProfile = map.Value;

                _logger.LogInformation($"User {username} logged in successfully.");

                return Result.Successful(userProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return Result.Failure<UserProfile>(ex.Message);
            }
        }
    }
}
