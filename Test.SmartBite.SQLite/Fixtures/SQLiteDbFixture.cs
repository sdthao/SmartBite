using SQLite;
using SmartBite.SQLite.Models;

namespace Test.SmartBit.SQLite.Fixtures
{
    public class SQLiteDbFixture : IAsyncLifetime
    {
        public SQLiteAsyncConnection Db { get; private set; }

        public async Task InitializeAsync()
        {
            Db = new SQLiteAsyncConnection(":memory:");

            await Db.CreateTableAsync<FoodItemEntity>();

            await Db.CreateTableAsync<UserProfileEntity>();
        }

        public async Task DisposeAsync()
        {
            await Db.CloseAsync();
        }
    }
}
