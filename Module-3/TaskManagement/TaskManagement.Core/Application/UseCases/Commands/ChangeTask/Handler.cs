using MediatR;
using TaskManagement.Core.Ports;

namespace TaskManagement.Core.Application.UseCases.Commands.ChangeTask;

public class Handler : IRequestHandler<Command, bool>
{
    private readonly ITaskStorage _taskStorage;

    public Handler(ITaskStorage taskStorage)
    {
        _taskStorage = taskStorage ?? throw new ArgumentNullException(nameof(taskStorage));
    }
    
    public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
    {
        var task = await _taskStorage.GetAsyncById(request.TaskId);

        var taskChangeResult = task.Change(request.Title, request.Description);
        if (taskChangeResult.IsFailure) return false;    
        
        _taskStorage.Update(task);
        
        return true;
    }
}