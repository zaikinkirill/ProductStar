using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using TaskManagement.Utils.Primitives;

namespace TaskManagement.Core.Domain.TaskAggregate;

/// <summary>
///  Задача
/// </summary>
public class Task
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid Id { get; protected set; }
    
    /// <summary>
    /// Название задачи
    /// </summary>
    public string Title { get; protected set; }
    
    /// <summary>
    /// Описание задачи
    /// </summary>
    public string Description { get; protected set; }
    
    /// <summary>
    /// Статус
    /// </summary>
    public Status Status { get; protected set; }
    
    protected Task()
    {}
    
    protected Task(string title, string description)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Status= Status.Created;
    }

    /// <summary>
    /// Factory Method
    /// </summary>
    /// <param name="buyerId">Идентификатор покупателя</param>
    /// <returns>Результат</returns>
    public static Result<Task, Error> Create(string title, string description)
    {
        if (string.IsNullOrEmpty(title)) return GeneralErrors.ValueIsRequired(nameof(title));
        return new Task(title, description);
    }
    
    /// <summary>
    /// Взять задачу в работу.
    /// </summary>
    /// <returns>Результат</returns>
    public Result<object, Error> Started()
    {
        if (Status != Status.Created) return Errors.TaskNotCreated();
        
        //Меняем статус
        Status = Status.Started;
        
        return new object();
    }
    
    /// <summary>
    /// Завершить задачу.
    /// </summary>
    /// <returns>Результат</returns>
    public Result<object, Error> Complete()
    {
        if (Status != Status.Started) return Errors.TaskNotStarted();
        
        //Меняем статус
        Status = Status.Completed;
        
        return new object();
    }
    
    /// <summary>
    /// Удалить задачу.
    /// </summary>
    /// <returns>Результат</returns>
    public Result<object, Error> Delete()
    {
        if (Status == Status.Started || Status == Status.Completed) return Errors.CannotDeleteTask();
        
        //Меняем статус
        Status = Status.Deleted;
        
        return new object();
    }
    
    /// <summary>
    /// Изменить задачу.
    /// </summary>
    /// <returns>Результат</returns>
    public Result<object, Error> Change(string title, string description)
    {
        Title = title;
        Description = description;
        
        return new object();
    }
    
    public static class Errors
    {
        public static Error TaskNotCreated()
        {
            return new($"{nameof(Task).ToLowerInvariant()}.not.created", "Задача не в статусе Создана. Нельзя взять в работу.");
        }
        
        public static Error TaskNotStarted()
        {
            return new($"{nameof(Task).ToLowerInvariant()}.not.started", "Задача не в статусе Взята в работу. Нельзя завершить работу.");
        }
        
        public static Error CannotDeleteTask()
        {
            return new($"{nameof(Task).ToLowerInvariant()}.cannot.delete", "Нельзя удалить задачу в этом статусе.");
        }
    }
}