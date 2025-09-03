using SmartBite.Common;
using SmartBite.Models.Food;

namespace SmartBite.Services
{
    public interface IFoodDbService
    {
        /// <summary>
        /// Asynchronously gets all food items for a specific user.
        /// </summary>
        /// <param name="userId">The user id to use.</param>
        /// <param name="date">The date associated the food search..</param>
        /// <returns></returns>
        Task<IResult<List<FoodItem>>> GetFoodsForUserAsync(string userId, DateTime date);

        /// <summary>
        /// Asynchronously adds a food item for a specific user.
        /// </summary>
        /// <param name="userId">The user id to use.</param>
        /// <param name="food">The food item to add.</param>
        /// <returns></returns>
        Task<IResult> AddFoodItemAsync(string userId, FoodItem food);

        /// <summary>
        /// Asynchronously removes a food item for a specific user.
        /// </summary>
        /// <param name="userId">The user id to use.</param>
        /// <param name="foodName">The food item name to use.</param>
        /// <returns></returns>
        Task<IResult> RemoveFoodItemAsync(string userId, string foodName);

        /// <summary>
        /// Asynchronously clears all food items for a specific user.
        /// </summary>
        /// <param name="userId">The user id to use.</param>
        /// <returns></returns>
        Task<IResult> ClearFoodsAsync(string userId);
    }
}
