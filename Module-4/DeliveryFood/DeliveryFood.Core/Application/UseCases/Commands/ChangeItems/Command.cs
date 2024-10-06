using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Commands.ChangeItems;

public class Command : IRequest<string>
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public Guid? OrderId { get; }
        
    /// <summary>
    /// Идентификатор блюда
    /// </summary>
    public int DishId { get; }

    /// <summary>
    /// Количество позиций
    /// </summary>
    public int Quantity { get; }
    
    /// <summary>
    /// Ctr
    /// </summary>
    public Command(Guid? orderId, int dishId, int quantity)
    {
        if (dishId < 0) throw new ArgumentException(nameof(dishId));
        if (quantity < 0) throw new ArgumentException(nameof(quantity));
            
        OrderId = orderId;
        DishId = dishId;
        Quantity = quantity;
    }
}