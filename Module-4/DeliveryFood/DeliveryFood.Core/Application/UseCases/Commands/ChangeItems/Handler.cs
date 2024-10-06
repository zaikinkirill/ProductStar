using DeliveryFood.Core.Domain.OrderAggregate;
using DeliveryFood.Core.Ports;
using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Commands.ChangeItems;

public class Handler : IRequestHandler<Command, string>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDishRepository _dishRepository;

    public Handler(IOrderRepository orderRepository, IDishRepository dishRepository)
    {
        _orderRepository = orderRepository;
        _dishRepository = dishRepository;
    }

    public async Task<string> Handle(Command request, CancellationToken cancellationToken)
    {
        Order order = null;
        if (request.OrderId != null && request.OrderId != Guid.Empty)
        {
            order = await _orderRepository.GetAsync((Guid)request.OrderId);
        }

        if (order == null)
        {
            var orderResult = Order.Create();
            if (orderResult.IsFailure) return "false";
            _orderRepository.Add(orderResult.Value);
            order = orderResult.Value;
        }
        
        var dish = await _dishRepository.GetAsync(request.DishId);
        if (dish == null)
        {
            return $"Блюдо {request.DishId} не найдено";
        }

        var resultChange = order.Change(dish, request.Quantity);
        if (resultChange.IsFailure)
        {
            return $"Ошибка изменения состава заказа {order.Id}";
        }
        _orderRepository.Update(order);
        return "OK";
    }
}