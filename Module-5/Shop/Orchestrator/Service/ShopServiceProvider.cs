using System.Net.Http.Json;
using Orchestrator.Data;

namespace Orchestrator.Service;

public class ShopServiceProvider
{
    private static ShopServiceProvider? instance;
    private static HttpClient _client = new();

    public ShopServiceProvider(string url)
    {
        _client.BaseAddress = new Uri(url);
    }

    /// <summary>
    /// singleton
    /// </summary>
    public static ShopServiceProvider GetSingleton(string url)
    {
        if (instance == null)
        { 
            instance = new ShopServiceProvider(url);
        }
        return instance;
    }

    /// <summary>
    /// Обновление очереди.
    /// </summary>
    public async void SetProductProcessing(Shop.Data.Product product)
    {
        await _client.PostAsJsonAsync("product/add", product);
    }
}