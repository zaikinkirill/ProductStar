using DeliveryFood.Core.Ports;
using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Commands.PayOrder;

public class Handler : IRequestHandler<Command, string>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentClient _paymentClient;

    public Handler(IOrderRepository orderRepository, IPaymentClient paymentClient)
    {
        _orderRepository = orderRepository;
        _paymentClient = paymentClient;
    }

    public async Task<string> Handle(Command request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId);

        var checkBeforeIssue = order.CheckBeforeIssue();

        if (checkBeforeIssue.IsFailure)
        {
            return checkBeforeIssue.Error.Message;
        }

        var payResult = await _paymentClient.PayOrder(order.Id, order.GetTotalSum().Value, cancellationToken);
        if (!payResult)
        {
            return "Ошибка оплаты заказа";
        }

        var resultCheckout =  order.Checkout();
        if (resultCheckout.IsFailure)
        {
            return resultCheckout.Error.Message;
        }
        _orderRepository.Update(order);
        return "OK";
    }
}