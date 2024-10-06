using DeliveryFood.Core.Ports;

namespace DeliveryFood.Infrastructure.Adapters.Http;

public class PaymentClient : IPaymentClient
{
    public async Task<bool> PayOrder(Guid orderId, decimal sum, CancellationToken cancellationToken)
    {
        return await Task.FromResult(true);
    }
}