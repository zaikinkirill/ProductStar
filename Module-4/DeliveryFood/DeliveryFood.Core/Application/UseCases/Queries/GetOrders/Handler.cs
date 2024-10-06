using DeliveryFood.Core.Ports;
using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Queries.GetOrders;

public class Handler : IRequestHandler<Query, Response>
{
    private readonly IOrderRepository _orderRepository;

    public Handler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllAsync();

        return new Response(orders);
    }
}