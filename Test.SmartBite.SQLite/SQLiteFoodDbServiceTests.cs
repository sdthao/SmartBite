using Moq;
using SQLite;
using Newtonsoft.Json;
using Xunit.Abstractions;
using SmartBite.Models.Food;
using SmartBite.SQLite.Models;
using SmartBite.SQLite.Services;
using Microsoft.Extensions.Logging;
using Test.SmartBit.SQLite.Fixtures;
using SmartBite.Common.WeightMeasurement;
using SmartBite.Common.CalorieMeasurement;
using SmartBite.Common.NutrientMeasurement;

namespace Test.SmartBit.SQLite
{
    public class SQLiteFoodDbServiceTests : IClassFixture<SQLiteDbFixture>, IClassFixture<TestMappersFixture>
    {
        private readonly ITestOutputHelper _output;

        private readonly TestMappersFixture _testFixture;

        private readonly SQLiteAsyncConnection _db;

        private readonly SQLiteFoodDbService _service;

        private readonly string _userId = "test-user";

        private readonly DateTime _testDate = new DateTime(2024, 10, 26);

        public SQLiteFoodDbServiceTests(ITestOutputHelper output, SQLiteDbFixture dbFixture, TestMappersFixture testFixture)
        {
            _output = output;

            _testFixture = testFixture;

            _db = dbFixture.Db;
            
            var loggerMock = new Mock<ILogger<SQLiteFoodDbService>>();
            _service = new SQLiteFoodDbService(
                loggerMock.Object, testFixture.FoodItemMapper,
                testFixture.FoodItemEntityMapper, _db);
        }

        #region GetFoodsForUserAsync Tests
        [Theory]
        [MemberData(nameof(FoodItemEntities))]
        public async Task GetFoodsForUserAsync_InsertFoodItemEntities_ShouldReturnSuccess(List<FoodItemEntity> foodItemEntities)
        {
            foreach (var entity in foodItemEntities)
            {
                entity.UserId = _userId;
                entity.Date = _testDate.Date;
                await _db.InsertAsync(entity);
            }

            var result = await _service.GetFoodsForUserAsync(_userId, _testDate);

            Assert.True(result.Success);

            Assert.NotNull(result.Value);
            
            Assert.Equal(foodItemEntities.Count, result.Value.Count);

            var expectedNames = foodItemEntities.OrderBy(e => e.Name).Select(e => e.Name);
            var actualNames = result.Value.OrderBy(f => f.Name).Select(f => f.Name);

            Assert.Equal(expectedNames, actualNames);

            // Clean up
            var cleanup = await _service.ClearFoodsAsync(_userId);
            Assert.True(cleanup.Success);
        }

        [Fact]
        public async Task GetFoodsForUserAsync_NonExistingUser_EmptFoodList()
        {
            var result = await _service.GetFoodsForUserAsync("nonExistentUser", _testDate);

            Assert.True(result.Success);

            Assert.NotNull(result.Value);
            
            Assert.Empty(result.Value);
        }

        #endregion

        #region AddFoodItemAsync Tests
        [Theory]
        [MemberData(nameof(FoodItems))]
        public async Task AddFoodItemAsync_AddFoodItem_ShouldSucceedAndEqualExpected(List<FoodItem> foodItems)
        {
            foreach (var foodItem in foodItems)
            {
                var add = await _service.AddFoodItemAsync(_userId, foodItem);

                Assert.True(add.Success);

                var dbItem = await _db.Table<FoodItemEntity>()
                    .Where(f => f.Name == foodItem.Name)
                    .FirstOrDefaultAsync();

                Assert.NotNull(dbItem);

                Assert.Equal(foodItem.Name, dbItem.Name);

                Assert.Equal(foodItem.Calories.Value, dbItem.Calories);

                Assert.Equal(foodItem.Weight.Value, dbItem.Weight);

                Assert.Equal(foodItem.Weight.Name, dbItem.WeightScale);

                var expectedNutrients = JsonConvert.SerializeObject(foodItem.FoodNutrients, _testFixture.JsonSettings);

                Assert.Equal(expectedNutrients, dbItem.NutrientsJson);
            }
        }

        #endregion

        #region RemoveFoodItemAsync Tests
        [Theory]
        [MemberData(nameof(FoodItemEntities))]
        public async Task RemoveFoodItemAsync_InsertBeforeRemove_ShouldSucceed(List<FoodItemEntity> foodItemEntities)
        {
            foreach (var entity in foodItemEntities)
            {
                entity.UserId = _userId;
                entity.Date = _testDate.Date;
                await _db.InsertAsync(entity);
            }

            var dbItems = await _db.Table<FoodItemEntity>().ToListAsync();

            var expectedNames = foodItemEntities.OrderBy(e => e.Name).Select(e => e.Name);
            var actualNames = dbItems.OrderBy(f => f.Name).Select(f => f.Name);

            Assert.Equal(expectedNames, actualNames);

            foreach (var name in expectedNames)
            {
                var remove = await _service.RemoveFoodItemAsync(_userId, name);
                
                Assert.True(remove.Success);
                
                var dbItem = await _db.Table<FoodItemEntity>().Where(f => f.Name == name).FirstOrDefaultAsync();
                
                Assert.Null(dbItem);
            }
        }

        [Fact]
        public async Task RemoveFoodItemAsync_NonExistingFood_ShouldFail()
        {
            var result = await _service.RemoveFoodItemAsync(_userId, "NonExistentFood");

            Assert.False(result.Success);

            Assert.Equal("Item not found.", result.Message);
        }

        #endregion

        #region ClearFoodsAsync Tests
        [Theory]
        [MemberData(nameof(FoodItemEntities))]
        public async Task ClearFoodsAsync_InsertBeforeClear_ShouldRemoveAllFoodItems(List<FoodItemEntity> foodItemEntities)
        {
            foreach (var entity in foodItemEntities)
            {
                entity.UserId = _userId;
                entity.Date = _testDate.Date;
                await _db.InsertAsync(entity);
            }

            var dbItems = await _db.Table<FoodItemEntity>().ToListAsync();

            var expectedNames = foodItemEntities.OrderBy(e => e.Name).Select(e => e.Name);
            var actualNames = dbItems.OrderBy(f => f.Name).Select(f => f.Name);

            Assert.Equal(expectedNames, actualNames);

            var result = await _service.ClearFoodsAsync(_userId);
            Assert.True(result.Success);

            var remainingItems = await _db.Table<FoodItemEntity>().ToListAsync();
            Assert.Empty(remainingItems);
        }

        #endregion

        #region Test Data
        public static IEnumerable<object[]> FoodItemEntities()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new List<FoodItemEntity>
                    {
                        new FoodItemEntity
                        {
                            Name = "apple",
                            Calories = 70,
                            Weight = 100,
                            WeightScale = "Gram",
                            NutrientsJson = JsonConvert.SerializeObject(new List<Vitamin>
                            {
                                new VitaminA(new MilliGram(10.2)),
                                new VitaminC(new MilliGram(8.8)),
                                new VitaminD(new MilliGram(3.1))
                            })
                        },
                        new FoodItemEntity
                        {
                            Name = "banana",
                            Calories = 105,
                            Weight = 118,
                            WeightScale = "Gram",
                            NutrientsJson = JsonConvert.SerializeObject(new List<Vitamin>
                            {
                                new VitaminD(new MilliGram(0.4)),
                                new VitaminC(new MilliGram(10.3)),
                                new VitaminA(new MilliGram(3.0))
                            })
                        }
                    }
                },
                new object[]
                {
                    new List<FoodItemEntity>
                    {
                        new FoodItemEntity
                        {
                            Name = "beef",
                            Calories = 665,
                            Weight = 319,
                            WeightScale = "Gram",
                            NutrientsJson = JsonConvert.SerializeObject(new List<Vitamin>
                            {
                                new VitaminD(new MilliGram(92.1)),
                                new VitaminC(new MilliGram(1.91)),
                                new VitaminA(new MilliGram(44.01))
                            })
                        },
                        new FoodItemEntity
                        {
                            Name = "chicken",
                            Calories = 400,
                            Weight = 500.912,
                            WeightScale = "Gram",
                            NutrientsJson = JsonConvert.SerializeObject(new List<Vitamin>
                            {
                                new VitaminD(new MilliGram(106.4)),
                                new VitaminC(new MilliGram(9.3)),
                                new VitaminA(new MilliGram(7.0798))
                            })
                        },
                        new FoodItemEntity
                        {
                            Name = "fish",
                            Calories = 349,
                            Weight = 368.17,
                            WeightScale = "Gram",
                            NutrientsJson = JsonConvert.SerializeObject(new List<Vitamin>
                            {
                                new VitaminD(new MilliGram(129.4)),
                                new VitaminC(new MilliGram(0.1)),
                                new VitaminA(new MilliGram(3.912))
                            })
                        }
                    }
                }
            };
        }

        public static IEnumerable<object[]> FoodItems()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new List<FoodItem>
                    {
                        new FoodItem("apple", new Calorie(70), new Gram(100), new List<Vitamin>
                        {
                            new VitaminA(new MilliGram(10.2)),
                            new VitaminC(new MilliGram(8.8)),
                            new VitaminD(new MilliGram(3.1))
                        }),
                        new FoodItem("banana", new Calorie(105), new Gram(118), new List<Vitamin>
                        {
                            new VitaminD(new MilliGram(0.4)),
                            new VitaminC(new MilliGram(10.3)),
                            new VitaminA(new MilliGram(3.0))
                        })
                    }
                },

                new object[]
                {
                    new List<FoodItem>
                    {
                        new FoodItem("beef", new Calorie(665), new Gram(319), new List<Vitamin>
                        {
                            new VitaminD(new MilliGram(92.1)),
                            new VitaminC(new MilliGram(1.91)),
                            new VitaminA(new MilliGram(44.01))
                        }),
                        new FoodItem("chicken", new Calorie(400), new Gram(500.912), new List<Vitamin>
                        {
                            new VitaminD(new MilliGram(106.4)),
                            new VitaminC(new MilliGram(9.3)),
                            new VitaminA(new MilliGram(7.0798))
                        }),
                        new FoodItem("fish", new Calorie(349), new Gram(368.17), new List<Vitamin>
                        {
                            new VitaminD(new MilliGram(129.4)),
                            new VitaminC(new MilliGram(0.1)),
                            new VitaminA(new MilliGram(3.912))
                        })
                    }
                },

                new object[]
                {
                    new List<FoodItem>
                    {
                        new FoodItem("spinach", new Calorie(50), new Gram(291), new List<Vitamin>
                        {
                            new VitaminA(new MilliGram(100.9)),
                            new VitaminC(new MilliGram(89.8)),
                            new VitaminD(new MilliGram(91.9))
                        }),
                        new FoodItem("cabbage", new Calorie(105), new Gram(118), new List<Vitamin>
                        {
                            new VitaminD(new MilliGram(33.19)),
                            new VitaminC(new MilliGram(6679.12345)),
                            new VitaminA(new MilliGram(9876.9876))
                        })
                    }
                },
            };
        }

        #endregion
    }
}
