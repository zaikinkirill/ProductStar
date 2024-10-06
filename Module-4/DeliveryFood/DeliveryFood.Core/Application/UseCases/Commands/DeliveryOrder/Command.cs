using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Commands.DeliveryOrder;

public class Command : IRequest<string>
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public Guid OrderId { get; }
    
    /// <summary>
    /// Ctr
    /// </summary>
    public Command(Guid orderId)
    {
        if (orderId == Guid.Empty) throw new ArgumentException(nameof(orderId));
        OrderId = orderId;
    }
}