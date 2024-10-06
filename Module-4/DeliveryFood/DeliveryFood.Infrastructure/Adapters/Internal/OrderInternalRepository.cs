using DeliveryFood.Core.Domain.OrderAggregate;
using DeliveryFood.Core.Ports;

namespace DeliveryFood.Infrastructure.Adapters.Internal;

public class OrderInternalRepository : IOrderRepository
{
    private readonly List<Order> _orders = [];
    
    public void Add(Order order)
    {
        _orders.Add(order);
    }

    public void Update(Order order)
    {
        _orders.RemoveAll(t => t.Id == order.Id);
        _orders.Add(order);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await Task.FromResult<IEnumerable<Order>>(_orders);
    }

    public async Task<Order> GetAsync(Guid orderId)
    {
        return await Task.FromResult(_orders.FirstOrDefault(t => t.Id == orderId) ?? throw new InvalidOperationException());
    }

    public async Task<IEnumerable<Order>> GetByStatusAsync(int statusId)
    {
        return await Task.FromResult<IEnumerable<Order>>(_orders.Where(t => t.Status.Id == statusId));
    }
}