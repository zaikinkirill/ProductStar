using DeliveryFood.Core.Ports;
using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Queries.GetStatusOrder;

public class Handler : IRequestHandler<Query, Response>
{
    private readonly IOrderRepository _orderRepository;

    public Handler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId);

        return new Response(order.Status);
    }
}