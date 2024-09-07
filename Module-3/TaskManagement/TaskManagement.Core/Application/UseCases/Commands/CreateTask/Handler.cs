using MediatR;
using TaskManagement.Core.Ports;
using Task = TaskManagement.Core.Domain.TaskAggregate.Task;

namespace TaskManagement.Core.Application.UseCases.Commands.CreateTask;

public class Handler : IRequestHandler<Command, bool>
{
    private readonly ITaskStorage _taskStorage;

    public Handler(ITaskStorage taskStorage)
    {
        _taskStorage = taskStorage ?? throw new ArgumentNullException(nameof(taskStorage));
    }
    
    public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
    {
        var task = Task.Create(request.Title, request.Description);
        if (task.IsFailure) return false;
        
        _taskStorage.Add(task.Value);
        
        return true;
    }
}