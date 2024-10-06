using CSharpFunctionalExtensions;
using DeliveryFood.Core.Utils.Primitives;

namespace DeliveryFood.Core.Domain.OrderAggregate;

public class OrderStatus
{
    public static readonly OrderStatus Created = new OrderStatus(1, "Создан");
    public static readonly OrderStatus Issued = new OrderStatus(2, "Оформлен");
    public static readonly OrderStatus Accepted = new OrderStatus(3, "Принят в работу");
    public static readonly OrderStatus Delivered = new OrderStatus(4, "Доставлен");
    
    /// <summary>
    ///  Идентификатор
    /// </summary>
    public int Id { get; protected set; }
    
    /// <summary>
    ///  Название
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Ctr
    /// </summary>
    protected OrderStatus()
    {}
    
    /// <summary>
    /// Ctr
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="name">Название</param>
    protected OrderStatus(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    /// <summary>
    /// Список всех значений списка
    /// </summary>
    /// <returns>Значения списка</returns>
    public static IEnumerable<OrderStatus> List()
    {
        yield return Created;
        yield return Issued;
        yield return Accepted;
        yield return Delivered;
    }

    /// <summary>
    /// Получить статус по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Статус</returns>
    public static Result<OrderStatus, Error> From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);
        if (state == null) return Errors.StatusIsWrong();
        return state;
    }
    
    public static class Errors
    {
        public static Error StatusIsWrong()
        {
            return new($"{nameof(OrderStatus).ToLowerInvariant()}.is.wrong", 
                $"Не верное значение. Допустимые значения: {nameof(OrderStatus).ToLowerInvariant()}: {string.Join(",", List().Select(s => s.Name))}");
        }
    }
}