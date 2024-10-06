using DeliveryFood.Core.Ports;
using MediatR;
using Quartz;

namespace DeliveryFoodApp.UI.Adapters.BackgroundJobs;

public class DeliveryOrderJob : IJob
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;
    
    public DeliveryOrderJob(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderRepository = orderRepository;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        var orders = await _orderRepository.GetByStatusAsync(3);

        foreach (var order in orders)
        {
            var deliveryOrderCommand = new DeliveryFood.Core.Application.UseCases.Commands.DeliveryOrder.Command(order.Id);
            await _mediator.Send(deliveryOrderCommand);
        }
    }
}