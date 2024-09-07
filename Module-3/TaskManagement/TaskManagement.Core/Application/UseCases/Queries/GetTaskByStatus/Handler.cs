using MediatR;
using TaskManagement.Core.Ports;

namespace TaskManagement.Core.Application.UseCases.Queries.GetTaskByStatus;

public class Handler : IRequestHandler<Query, Response>
{
    private readonly ITaskStorage _taskStorage;

    public Handler(ITaskStorage taskStorage)
    {
        _taskStorage = taskStorage ?? throw new ArgumentNullException(nameof(taskStorage));
    }

    public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
    {
        var tasks = await _taskStorage.GetAsyncByStatus(request.Status);

        return new Response(tasks);
    }
}