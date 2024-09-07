namespace TaskManagement.Core.Domain.TaskAggregate;

/// <summary>
/// Статус задачи
/// </summary>
public class Status
{
    public static readonly Status Created = new Status(1, nameof(Created).ToLowerInvariant());
    public static readonly Status Started = new Status(2, nameof(Started).ToLowerInvariant());
    public static readonly Status Completed = new Status(3, nameof(Completed).ToLowerInvariant());
    public static readonly Status Deleted = new Status(4, nameof(Deleted).ToLowerInvariant());
    
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; protected set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// Ctr
    /// </summary>
    protected Status()
    {}
    
    /// <summary>
    /// Ctr
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="name">Название</param>
    protected Status(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    /// <summary>
    /// Список всех значений списка
    /// </summary>
    /// <returns>Значения списка</returns>
    public static IEnumerable<Status> List()
    {
        yield return Created;
        yield return Completed;
        yield return Deleted;
    }
}