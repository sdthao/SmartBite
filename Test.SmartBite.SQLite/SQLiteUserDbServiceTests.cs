using Moq;
using SQLite;
using Xunit.Abstractions;
using SmartBite.Models.User;
using SmartBite.SQLite.Models;
using SmartBite.SQLite.Services;
using Microsoft.Extensions.Logging;
using Test.SmartBit.SQLite.Fixtures;

namespace Test.SmartBit.SQLite
{
    public class SQLiteUserDbServiceTests : IClassFixture<SQLiteDbFixture>, IClassFixture<TestMappersFixture>
    {
        private readonly ITestOutputHelper _output;

        private readonly SQLiteDbFixture _dbFixture;

        private readonly SQLiteAsyncConnection _db;

        private readonly SQLiteUserDbService _service;

        private const string TestUsername = "username";

        private const string TestPassword = "pAssword1!";

        public SQLiteUserDbServiceTests(ITestOutputHelper output, SQLiteDbFixture dbFixture, TestMappersFixture testFixture)
        {
            _output = output;

            _dbFixture = dbFixture;

            _db = dbFixture.Db;

            var loggerMock = new Mock<ILogger<SQLiteUserDbService>>();
            _service = new SQLiteUserDbService(
                loggerMock.Object, testFixture.UserProfileMapper,
                testFixture.UserProfileEntityMapper, _db);
        }

        #region FindUserAsync Tests
        [Fact]
        public async Task FindUserAsync_CreateTestUser_ShouldReturnSuccess()
        {
            await CreateTestUserAsync();

            var result = await _service.FindUserAsync(TestUsername);

            Assert.True(result.Success);

            Assert.NotNull(result.Value);

            Assert.Equal(TestUsername, result.Value.UserName);
        }

        [Fact]
        public async Task FindUserAsync_NonExistentUser_ShouldReturnFailure()
        {
            var nonexistentUsername = "nonexistentUser";

            var result = await _service.FindUserAsync(nonexistentUsername);

            Assert.False(result.Success);

            Assert.Equal($"User {nonexistentUsername} not found.", result.Message);
        }

        [Fact]
        public async Task FindUserAsync_NullUser_ShouldThrowException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.FindUserAsync(null));
        }

        #endregion

        #region CreateUserAsync Tests
        [Fact]
        public async Task CreateUserAsync_CreateNewUser_ShouldCreateUser()
        {
            var profile = new UserProfile(
                "newUserId",
                TestUsername, 
                TestPassword, 
                new UserInfo("firstname", "lastname", "email@mail.com", DateTime.Now));

            var result = await _service.CreateUserAsync(profile);

            Assert.True(result.Success);

            Assert.Equal(1, result.Value);
            
            var createdProfile = await _dbFixture.Db.Table<UserProfileEntity>().Where(u => u.UserName == TestUsername).FirstOrDefaultAsync();
            
            Assert.NotNull(createdProfile);
        }

        #endregion

        #region LoginAsync Tests
        [Fact]
        public async Task LoginAsync_CorrectCredentials_ShouldSucceed()
        {
            await CreateTestUserAsync();

            var result = await _service.LoginAsync(TestUsername, TestPassword);

            Assert.True(result.Success);

            Assert.NotNull(result.Value);
            
            Assert.Equal(TestUsername, result.Value.UserName);
        }

        [Fact]
        public async Task LoginAsync_IncorrectPassword_ShouldFail()
        {
            await CreateTestUserAsync();

            var result = await _service.LoginAsync(TestUsername, "wrongPassword");

            Assert.False(result.Success);

            Assert.Equal("Username or password is incorrect.", result.Message);
        }

        [Fact]
        public async Task LoginAsync_NotExistentUser_ShouldFail()
        {
            var result = await _service.LoginAsync("nonexistentUser", TestPassword);

            Assert.False(result.Success);

            Assert.Equal("Username or password is incorrect.", result.Message);
        }

        #endregion

        #region Test Data
        private async Task CreateTestUserAsync()
        {
            var userProfile = new UserProfileEntity
            {
                UserName = TestUsername,
                Password = TestPassword,
                UserId = "userId",
                FirstName = "firstname",
                LastName = "lastname",
                Email = "email@mail.com",
                DateOfBirth = DateTime.Now
            };

            await _dbFixture.Db.InsertAsync(userProfile);
        }
        #endregion
    }
}