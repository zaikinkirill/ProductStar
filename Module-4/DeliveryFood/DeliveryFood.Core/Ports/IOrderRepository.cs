using DeliveryFood.Core.Domain.OrderAggregate;

namespace DeliveryFood.Core.Ports;

public interface IOrderRepository
{
    /// <summary>
    /// Добавить новый заказ
    /// </summary>
    /// <param name="order">Заказ</param>
    /// <returns>Заказ</returns>
    void Add(Order order);

    /// <summary>
    /// Обновить существующий заказ
    /// </summary>
    /// <param name="order">Заказ</param>
    void Update(Order order);

    /// <summary>
    /// Получить все заказы
    /// </summary>
    /// <returns>Заказы</returns>
    Task<IEnumerable<Order>> GetAllAsync();
    
    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    /// <param name="orderId">Идентификатор</param>
    /// <returns>Заказ</returns>
    Task<Order> GetAsync(Guid orderId);
    
    /// <summary>
    /// Получить заказы по cтатусу
    /// </summary>
    /// <param name="statusId">Статус</param>
    /// <returns>Заказы</returns>
    Task<IEnumerable<Order>> GetByStatusAsync(int statusId);
}