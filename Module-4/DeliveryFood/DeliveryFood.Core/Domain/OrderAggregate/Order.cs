using CSharpFunctionalExtensions;
using DeliveryFood.Core.Domain.DishAggregate;
using DeliveryFood.Core.Utils.Primitives;

namespace DeliveryFood.Core.Domain.OrderAggregate;

public class Order
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public Guid Id { get; protected set; }
    
    /// <summary>
    /// Позиции в заказе
    /// </summary>
    public virtual List<Item> Items { get; set; } = new();
    
    /// <summary>
    /// Статус заказа.
    /// </summary>
    public OrderStatus Status { get; protected set; }
    
    protected Order()
    {}
    
    protected Order(Guid id)
    {
        Id = id;
        Status = OrderStatus.Created;
    }

    /// <summary>
    /// Factory Method.
    /// </summary>
    /// <returns>Результат</returns>
    public static Result<Order, Error> Create()
    {
        return new Order(Guid.NewGuid());
    }
    
    /// <summary>
    /// Изменить позицию
    /// </summary>
    /// <param name="dish">Блюдо</param>
    /// <param name="quantity">Количество</param>
    /// <returns>Результат</returns>
    public Result<object, Error> Change(Dish dish, int quantity)
    {
        if (Status != OrderStatus.Created) return Errors.OrderAlreadyIssued();
        if (dish == null) return GeneralErrors.ValueIsRequired(nameof(dish));
        if (quantity <0) return GeneralErrors.ValueIsInvalid(nameof(quantity));

        var item = Items.SingleOrDefault(x => x.DishId == dish.Id);
        if (item!=null)
        {
            if (quantity == 0)
            {
                Items.Remove(item);
            }
            else
            {
                item.SetQuantity(quantity);
            }
        }
        else
        {
            var result = Item.Create(dish, quantity);
            if (result.IsFailure)
            {
                return result.Error;
            }
            Items.Add(result.Value);
        }
        return new object();
    }
    
    public Result<object, Error> Accept()
    {
        if (Status != OrderStatus.Issued) return Errors.OrderNotIssued();
        Status = OrderStatus.Accepted;
        return new object();
    }
    public Result<object, Error> Delivery()
    {
        if (Status != OrderStatus.Accepted) return Errors.OrderNotAccept();
        Status = OrderStatus.Delivered;
        return new object();
    }
    
    
    public Result<bool, Error> CheckBeforeIssue()
    {
        if(Status != OrderStatus.Created) return Errors.OrderAlreadyIssued();
        if(Items.Count<=0) return Errors.NotItems();

        return true;
    }
    
    public Result<object, Error> Checkout()
    {
        var checkResult = CheckBeforeIssue();
        if (checkResult.Value)
        {
            Status = OrderStatus.Issued;
            return new object();
        }

        return new object();
    }

    public Result<decimal, Error> GetTotalSum()
    {
        return Items.Sum(o => o.GetTotal().Value);
    }

    public static class Errors
    {
        public static Error OrderAlreadyIssued()
        {
            return new($"{nameof(Order).ToLowerInvariant()}.has.already.issued", "Заказ уж оформлен");
        }
        
        public static Error OrderNotIssued()
        {
            return new($"{nameof(Order).ToLowerInvariant()}.not.issued", "Заказ еще не оплачен");
        }
        public static Error NotItems()
        {
            return new($"{nameof(Order).ToLowerInvariant()}.not.items", "Заказ не имеет товаров");
        }
        public static Error OrderNotAccept()
        {
            return new($"{nameof(Order).ToLowerInvariant()}.not.accept", "Заказ еще не принят в работу");
        }
    }
}