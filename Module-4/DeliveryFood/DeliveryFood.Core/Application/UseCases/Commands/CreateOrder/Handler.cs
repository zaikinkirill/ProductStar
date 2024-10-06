using DeliveryFood.Core.Domain.OrderAggregate;
using DeliveryFood.Core.Ports;
using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Commands.CreateOrder;

public class Handler : IRequestHandler<Command, Guid>
{
    private readonly IOrderRepository _orderRepository;

    public Handler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    }

    public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
    {
        var order = Order.Create();
        if (order.IsFailure) return Guid.Empty;
        
        _orderRepository.Add(order.Value);

        return order.Value.Id;
    }
}