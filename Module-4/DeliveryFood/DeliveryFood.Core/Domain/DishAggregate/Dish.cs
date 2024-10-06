using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using DeliveryFood.Core.Utils.Primitives;

namespace DeliveryFood.Core.Domain.DishAggregate;

/// <summary>
/// Блюдо
/// </summary>
public class Dish
{
    /// <summary>
    /// Салат
    /// </summary>
    public static readonly Dish Salad = new Dish(1
        ,"Салат"
        ,100);

    /// <summary>
    /// Первое блюдо.
    /// </summary>
    public static readonly Dish FirstDish = new Dish(2
        ,"Первое блюдо"
        ,200);
    
    /// <summary>
    /// Второе блюдо.
    /// </summary>
    public static readonly Dish SecondDish = new Dish(3
        ,"Второе блюдо"
        ,300);
    
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; protected set;}
    
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; protected set;}

    /// <summary>
    /// Стоимость
    /// </summary>
    public decimal Price { get; protected set; }
    
    protected Dish()
    {
    }

    protected Dish(int id, string title, decimal price)
        : this()
    {
        Id = id;
        Title = title;
        Price = price;
    }

    /// <summary>
    ///     Factory Method
    /// </summary>
    /// <param name="title">Название</param>
    /// <param name="price">Цена</param>
    /// <returns>Результат</returns>
    public static Result<Dish, Error> Create(int id, string title, decimal price)
    {
        if (string.IsNullOrEmpty(title)) GeneralErrors.ValueIsInvalid(nameof(title));
        if (price <= 0) GeneralErrors.ValueIsInvalid(nameof(price));
            
        return new Dish(id,title, price);
    }

    public static IEnumerable<Dish> List()
    {
        yield return Salad;
        yield return FirstDish;
        yield return SecondDish;
    }
}