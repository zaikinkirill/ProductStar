using MediatR;

namespace TaskManagement.Core.Application.UseCases.Commands.DeleteTask;

public class Command : IRequest<bool>
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid TaskId { get; }
    
    public Command(Guid taskId)
    {
        if (taskId == Guid.Empty) throw new ArgumentException(nameof(taskId));
        TaskId = taskId;
    }
}