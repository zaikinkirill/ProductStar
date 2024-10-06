using DeliveryFood.Core.Domain.OrderAggregate;

namespace DeliveryFood.Core.Application.UseCases.Queries.GetStatusOrder;

public class Response
{
    public OrderStatus Status { get; set; }

    public Response(OrderStatus status)
    {
        Status = status;
    }
}