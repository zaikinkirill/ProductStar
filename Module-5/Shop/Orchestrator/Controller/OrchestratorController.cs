using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Orchestrator.Data;
using Orchestrator.Service;

namespace Orchestrator.Controller;

[ApiController]
[Route("[controller]")]
public class OrchestratorController : ControllerBase
{
    private readonly List<Shop.Data.Product> _listProduct;
    private readonly ShopServiceProvider _shopFirst;
    private readonly ShopServiceProvider _shopSecond;

    public OrchestratorController()
    {
        _listProduct = new List<Shop.Data.Product>();
        _shopFirst = ShopServiceProvider.GetSingleton("http://localhost:5090/");
        _shopSecond = ShopServiceProvider.GetSingleton("http://localhost:5091/");
    }
    
    /// <summary>
    /// Добавляем в процесс ожидания нового пользователя.
    /// </summary>
    /// <param name="userOrder"></param>
    [HttpGet(Name = "add")]
    public void Add(Product order)
    {
            var product = _listProduct.First(p => p.ProductName == order.ProductName);

            if (product == null)
            {
                _listProduct.Add(new Shop.Data.Product
                {
                    ProductName = order.ProductName,
                    Users = [order.User],
                });
            }
            else
            {
                if (product.Users == null)
                {
                    product.Users = new List<int>();
                }

                product.Users.Add(order.User);
            }
    }

        /// <summary>
        /// Обработка заказа.
        /// </summary>
        [HttpGet(Name = "reserve")]
        public void ReserveProduct(Product order)
        {
            var product = _listProduct.First(p => p.ProductName == order.ProductName);

            if (product != null)
            {
                int? user = null;
                if (product.Users != null)
                {
                    user = product.Users.Single(u => u == order.User);
                    product.Users.Remove(order.User);
                }
                
                if (product.Users == null)
                {
                    product.Users = new List<int>();
                }
                
                product.Users.Add(order.User);
                
                _shopFirst.SetProductProcessing(product);
                _shopSecond.SetProductProcessing(product);
                
                Console.WriteLine($"Заказ {order.ProductName} c {order.User} обработан");
            }
        }
}