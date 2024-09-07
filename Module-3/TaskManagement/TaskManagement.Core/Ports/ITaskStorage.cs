using TaskManagement.Core.Domain.TaskAggregate;
using Task = TaskManagement.Core.Domain.TaskAggregate.Task;

namespace TaskManagement.Core.Ports;

/// <summary>
/// Storage для Aggregate Task
/// </summary>
public interface ITaskStorage
{
    /// <summary>
    /// Добавить новую задачу
    /// </summary>
    /// <param name="task">Задача</param>
    /// <returns>Задача</returns>
    void Add(Task task);

    /// <summary>
    /// Обновить существующую задачу
    /// </summary>
    /// <param name="task">Задача</param>
    void Update(Task task);

    /// <summary>
    /// Получить задачу по идентификатору
    /// </summary>
    /// <param name="taskId">Идентификатор</param>
    /// <returns>Задача</returns>
    Task<Task> GetAsyncById(Guid taskId);
    
    /// <summary>
    /// Получить все задачи
    /// </summary>
    /// <returns>Список задач</returns>
    Task<IEnumerable<Task>> GetAllAsync();

    /// <summary>
    /// Получить задачу по статусу
    /// </summary>
    /// <param name="status">Статус</param>
    /// <returns>Задача</returns>
    Task<IEnumerable<Task>> GetAsyncByStatus(Status status);
}