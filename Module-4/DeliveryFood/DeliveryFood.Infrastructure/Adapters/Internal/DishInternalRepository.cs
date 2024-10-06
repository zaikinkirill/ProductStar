using DeliveryFood.Core.Domain.DishAggregate;
using DeliveryFood.Core.Ports;

namespace DeliveryFood.Infrastructure.Adapters.Internal;

public class DishInternalRepository : IDishRepository
{
    private readonly List<Dish> _dishs = new() {Dish.Salad, Dish.FirstDish, Dish.SecondDish};
    
    public async Task<Dish> GetAsync(int dishId)
    {
        return await Task.FromResult(_dishs.FirstOrDefault(t => t.Id == dishId) ?? throw new InvalidOperationException());
    }

    public async Task<IEnumerable<Dish>> GetAllAsync()
    {
        return await Task.FromResult(_dishs);
    }
}