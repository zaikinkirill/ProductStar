using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shop.Data;

namespace Shop.Controller;

[ApiController]
[Route("[product]")]
public class ShopController: ControllerBase
{
    private readonly List<Product> _listProduct;
    private readonly OrchestratorService _orchestratorService;

    public ShopController()
    {
        _listProduct = new List<Product>();
        _orchestratorService = OrchestratorService.GetSingleton("http://localhost:5270/");
    }
    
    /// <summary>
    /// Добавить в продукт в очередь
    /// </summary>
    [HttpPost(Name = "add")]
    public void AddProduct(Product productRequest)
    {
        var product = _listProduct.First(p => p.ProductName == productRequest.ProductName);

        if (product == null)
        {
            _listProduct.Add(new Product
            {
                ProductName = productRequest.ProductName,
                Users = productRequest.Users,
            });
        }
        else
        {
            product.Users = productRequest.Users;
        }
    }
    
    /// <summary>
    /// Резервация продукта
    /// </summary>
    [HttpPost(Name = "reservation")]
    public void SetToProcessing(string productName)
    {
        var productProcessing = _listProduct.First(p => p.ProductName == productName);

        if (productProcessing != null)
        {
            int userId = productProcessing.Users.First();
            _orchestratorService.ReserveProductToUser(userId);
        }
    }
}