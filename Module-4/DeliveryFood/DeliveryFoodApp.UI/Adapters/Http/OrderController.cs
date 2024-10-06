using System.ComponentModel.DataAnnotations;
using DeliveryFood.Core.Domain.OrderAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryFoodApp.UI.Adapters.Http;

/// <summary>
/// Управление заказами
/// </summary>
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Создать заказ
    /// </summary>
    [HttpPost]
    [Route("/api/v1/order/create")]
    public async Task<Guid> Create()
    {
        var createOrderCommand = new DeliveryFood.Core.Application.UseCases.Commands.CreateOrder.Command();
        
        return await _mediator.Send(createOrderCommand);
    }
    
    /// <summary>
    /// Принять заказ в работу
    /// </summary>
    [HttpPost]
    [Route("/api/v1/order/{orderId}/accept")]
    public async Task<string> Accept([FromRoute (Name = "orderId")][Required]Guid orderId)
    {
        var acceptOrderCommand = new DeliveryFood.Core.Application.UseCases.Commands.AcceptOrder.Command(orderId);
        
        return await _mediator.Send(acceptOrderCommand);
    }
    
    /// <summary>
    /// Оплатить заказ
    /// </summary>
    [HttpPost]
    [Route("/api/v1/order/{orderId}/pay")]
    public async Task<string> Pay([FromRoute (Name = "orderId")][Required]Guid orderId)
    {
        var payOrderCommand = new DeliveryFood.Core.Application.UseCases.Commands.PayOrder.Command(orderId);
        
        return await _mediator.Send(payOrderCommand);
    }
    
    /// <summary>
    /// Изменить блюда в заказе
    /// </summary>
    [HttpPost]
    [Consumes("application/json")]
    [Route("/api/v1/order/{orderId?}/items/{dishId}/change/{quantity}")]
    public async Task<string> ChangeItems([FromRoute]Guid? orderId, [FromRoute]int dishId, [FromRoute]int quantity)
    {
        var changeItemsCommand = new DeliveryFood.Core.Application.UseCases.Commands.ChangeItems.Command(orderId, dishId, quantity);
        
        return await _mediator.Send(changeItemsCommand);
    }
    
    /// <summary>
    /// Получить статус заказа
    /// </summary>
    [HttpGet]
    [Route("/api/v1/order/{orderId}/get-status")]
    public async Task<OrderStatus> GetStatus([FromRoute (Name = "orderId")][Required]Guid orderId)
    {
        var getOrderStatusQuery = new  DeliveryFood.Core.Application.UseCases.Queries.GetStatusOrder.Query(orderId);
        
        var response = await _mediator.Send(getOrderStatusQuery);
        return response.Status;
    }
    
    /// <summary>
    /// Получить стоимость заказа
    /// </summary>
    [HttpGet]
    [Route("/api/v1/order/{orderId}/get-sum")]
    public async Task<decimal> GetSum([FromRoute (Name = "orderId")][Required]Guid orderId)
    {
        var getSumQuery = new  DeliveryFood.Core.Application.UseCases.Queries.GetSumOrder.Query(orderId);
        
        var response = await _mediator.Send(getSumQuery);
        return response.Sum;
    }
    
    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    [HttpGet]
    [Route("/api/v1/order/get-all")]
    public async Task<IEnumerable<Order>> GetAll()
    {
        var getOrdersQuery = new  DeliveryFood.Core.Application.UseCases.Queries.GetOrders.Query();
        
        var response = await _mediator.Send(getOrdersQuery);
        return response.Orders;
    }
}