using Microsoft.Extensions.Logging;
using SmartBite.Common;
using SQLite;

namespace SmartBite.SQLite
{
    public class SQLiteFactory : IFactory<SQLiteAsyncConnection>
    {
        private readonly ILogger<SQLiteFactory> _logger;

        private readonly SQLiteAsyncConnection _db;

        public SQLiteFactory(ILogger<SQLiteFactory> logger, string dbPath)
        {
            ArgumentNullException.ThrowIfNull(dbPath);

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _db = new SQLiteAsyncConnection(dbPath);
        }

        public IResult<SQLiteAsyncConnection> Create()
        {
            try
            {
                // Try a simple pragma query to check DB connection
                var result = _db.ExecuteScalarAsync<int>("PRAGMA user_version;").Result;

                _logger.LogInformation("SQLite database connection successful. User version: {Version}", result);

                return Result.Successful(_db);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to SQLite database.");

                return Result.Failure<SQLiteAsyncConnection>("Database connection failed: " + ex.Message);
            }
        }
    }
}
