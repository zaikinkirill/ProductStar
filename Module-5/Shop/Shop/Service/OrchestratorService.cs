using System.Net.Http.Json;

namespace Shop;

public class OrchestratorService
{
    static HttpClient _client = new();
    private static OrchestratorService? instance;

    public OrchestratorService(string url)
    {
        _client.BaseAddress = new Uri(url);
    }
        
    /// <summary>
    /// singleton
    /// </summary>
    public static OrchestratorService GetSingleton(string url)
    {
        if (instance == null)
        {
            instance = new OrchestratorService(url);
        }
        return instance;
    }

    public async void ReserveProductToUser(int userId)
    {
        await _client.PostAsJsonAsync("api/reserve-product", userId);
    }
}