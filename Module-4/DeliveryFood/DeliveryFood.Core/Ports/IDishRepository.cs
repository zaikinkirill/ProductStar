using DeliveryFood.Core.Domain.DishAggregate;

namespace DeliveryFood.Core.Ports;

public interface IDishRepository
{
    /// <summary>
    /// Получить блюдо по идентификатору
    /// </summary>
    /// <param name="dishId">Идентификатор</param>
    /// <returns>Блюдо</returns>
    Task<Dish> GetAsync(int dishId);
    
    /// <summary>
    /// Получить все блюда
    /// </summary>
    /// <returns>Блюда</returns>
    Task<IEnumerable<Dish>> GetAllAsync();
}