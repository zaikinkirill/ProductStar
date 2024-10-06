using DeliveryFood.Core.Ports;
using MediatR;
using Quartz;

namespace DeliveryFoodApp.UI.Adapters.BackgroundJobs;

[DisallowConcurrentExecution]
public class AcceptOrdersJob : IJob
{
    private readonly IMediator _mediator;
    private readonly IOrderRepository _orderRepository;
    
    public AcceptOrdersJob(IMediator mediator, IOrderRepository orderRepository)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _orderRepository = orderRepository;
    }
    
    public async Task Execute(IJobExecutionContext context)
    {
        var orders = await _orderRepository.GetByStatusAsync(2);

        foreach (var order in orders)
        {
            var acceptOrderCommand = new DeliveryFood.Core.Application.UseCases.Commands.AcceptOrder.Command(order.Id);
            await _mediator.Send(acceptOrderCommand);
        }
    }
}