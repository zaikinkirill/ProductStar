using TaskManagement.Core.Domain.TaskAggregate;
using TaskManagement.Core.Ports;
using Task = TaskManagement.Core.Domain.TaskAggregate.Task;

namespace TaskManagement.Infrastructure.Adapters.Internal;

public class TaskInternalStorage : ITaskStorage
{
    private readonly List<Task> _tasks = [];
    
    public void Add(Task task)
    {
        _tasks.Add(task);
    }

    public void Update(Task task)
    {
        var currentTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
        _tasks.RemoveAll(t => t.Id == task.Id);
        _tasks.Add(task);
    }

    public async Task<Task> GetAsyncById(Guid taskId)
    {
        return await System.Threading.Tasks.Task.FromResult(_tasks.FirstOrDefault(t => t.Id == taskId) ?? throw new InvalidOperationException());
    }

    public async Task<IEnumerable<Task>> GetAllAsync()
    {
        return await System.Threading.Tasks.Task.FromResult<IEnumerable<Task>>(_tasks);
    }

    public async Task<IEnumerable<Task>> GetAsyncByStatus(Status status)
    {
        return await System.Threading.Tasks.Task.FromResult(_tasks.Where(t => t.Status == status) ?? throw new InvalidOperationException());
    }
}