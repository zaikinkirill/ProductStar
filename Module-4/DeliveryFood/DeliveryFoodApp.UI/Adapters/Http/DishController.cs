using DeliveryFood.Core.Domain.DishAggregate;
using DeliveryFood.Core.Ports;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryFoodApp.UI.Adapters.Http;

/// <summary>
/// Управление блюдами.
/// </summary>
[ApiController]
public class DishController : ControllerBase
{
    private readonly IDishRepository _dishRepository;

    public DishController(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }


    /// <summary>
    /// Получить список блюд.
    /// </summary>
    [HttpGet]
    [Route("/api/v1/dish/get-all")]
    public async Task<IEnumerable<Dish>> GetAll()
    {
        return await _dishRepository.GetAllAsync();
    }
}