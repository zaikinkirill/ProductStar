using MediatR;
using TaskManagement.Core.Domain.TaskAggregate;

namespace TaskManagement.Core.Application.UseCases.Queries.GetTaskByStatus;

public class Query : IRequest<Response>
{
    public Status Status { get; }
    
    /// <summary>
    /// Ctr
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    public Query(Status status)
    {
        Status = status;
    }
}