using DeliveryFood.Core.Domain.OrderAggregate;

namespace DeliveryFood.Core.Application.UseCases.Queries.GetOrders;

public class Response
{
    public IEnumerable<Order> Orders { get; set; }

    public Response(IEnumerable<Order> orders)
    {
        Orders = orders;
    }
}