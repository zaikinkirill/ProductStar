using MediatR;

namespace TaskManagement.Core.Application.UseCases.Commands.ChangeTask;

public class Command : IRequest<bool>
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid TaskId { get; }

    public string Title { get; }
    
    public string Description { get; }
    
    public Command(Guid taskId, string title, string description)
    {
        if (taskId == Guid.Empty) throw new ArgumentException(nameof(taskId));
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(nameof(title));
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException(nameof(description));
        
        TaskId = taskId;
        Title = title;
        Description = description;
    }
}