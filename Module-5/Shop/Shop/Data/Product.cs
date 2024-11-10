namespace Shop.Data;

public class Product
{
    /// <summary>
    /// Наименование продукта.
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// Список ожидающих пользователей.
    /// </summary>
    public List<int> Users { get; set; }
}