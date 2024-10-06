using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Queries.GetStatusOrder;

public class Query: IRequest<Response>
{
    public Guid OrderId { get; }

    public Query(Guid orderId)
    {
        if (orderId == Guid.Empty) throw new ArgumentException(nameof(orderId));
        OrderId = orderId;
    }
}