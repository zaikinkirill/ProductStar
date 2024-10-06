namespace DeliveryFood.Core.Ports;

/// <summary>
/// Клиент для сервиса оплаты
/// </summary>
public interface IPaymentClient
{
    /// <summary>
    /// Оплатить заказ
    /// </summary>
    Task<bool> PayOrder(Guid orderId, decimal sum, CancellationToken cancellationToken);
}