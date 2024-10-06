using DeliveryFood.Core.Ports;
using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Commands.DeliveryOrder;

public class Handler: IRequestHandler<Command, string>
{
    private readonly IOrderRepository _orderRepository;

    public Handler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<string> Handle(Command request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId);
        if (order == null)
        {
            return "false";
        }

        var result = order.Delivery();
        if (result.IsFailure)
        {
            return result.Error.Message;
        }
        _orderRepository.Update(order);
        return "OK";
    }
}