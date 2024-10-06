using CSharpFunctionalExtensions;
using DeliveryFood.Core.Domain.DishAggregate;
using DeliveryFood.Core.Utils.Primitives;

namespace DeliveryFood.Core.Domain.OrderAggregate;

/// <summary>
/// Позиция блюда
/// </summary>
public class Item
{
    /// <summary>
    /// Идентификатор позиции
    /// </summary>
    public Guid Id { get; protected set; }
    
    /// <summary>
    /// Идентификтор блюда
    /// </summary>
    public int DishId { get; protected set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; protected set; }

    /// <summary>
    /// Количество
    /// </summary>
    public int Quantity { get; protected set; }
    
    /// <summary>
    /// Стоимость
    /// </summary>
    public virtual decimal Price { get; protected set; }
    
    /// <summary>
    /// Ctr
    /// </summary>
    protected Item() { }
    
    /// <summary>
    /// Ctr
    /// </summary>
    /// <param name="dish">Блюдо</param>
    /// <param name="quantity">Количество</param>
    protected Item(Dish dish, int quantity) : this()
    {
        Id = Guid.NewGuid();
        DishId = dish.Id;
        Title = dish.Title;
        Price = dish.Price;
        Quantity = quantity;
    }
    
    /// <summary>
    /// Factory Method
    /// </summary>
    /// <param name="good">Товар</param>
    /// <param name="quantity">Количество</param>
    /// <returns>Результат</returns>
    public static Result<Item, Error> Create(Dish dish, int quantity)
    {
        if (dish==null)  return GeneralErrors.ValueIsRequired(nameof(dish));
        if (quantity<=0)  return GeneralErrors.ValueIsInvalid(nameof(quantity));

        return new Item(dish, quantity);
    }
        
    /// <summary>
    /// Изменить количество
    /// </summary>
    /// <param name="quantity">Количество</param>
    /// <returns>Результат</returns>
    public Result<object, Error> SetQuantity(int quantity)
    {
        if (quantity<=0)  return GeneralErrors.ValueIsInvalid(nameof(quantity));
        Quantity = quantity;
        return new object();
    }
    
    /// <summary>
    /// Рассчитать стоимость позиции
    /// </summary>
    /// <returns>Стоимость</returns>
    public Result<decimal, Error> GetTotal()
    {
        var total = Quantity * Price;
        return total;
    }
}