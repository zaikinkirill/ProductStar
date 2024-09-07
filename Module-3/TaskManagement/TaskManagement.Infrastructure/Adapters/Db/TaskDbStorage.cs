using TaskManagement.Core.Domain.TaskAggregate;
using TaskManagement.Core.Ports;
using Task = TaskManagement.Core.Domain.TaskAggregate.Task;

namespace TaskManagement.Infrastructure.Adapters.Db;

// TODO: не хватило времени на реализацию.
public class TaskDbStorage : ITaskStorage
{
    public void Add(Task task)
    {
        throw new NotImplementedException();
    }

    public void Update(Task task)
    {
        throw new NotImplementedException();
    }

    public async Task<Task> GetAsyncById(Guid taskId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Task>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Task>> GetAsyncByStatus(Status status)
    {
        throw new NotImplementedException();
    }
}