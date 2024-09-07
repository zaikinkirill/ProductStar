using MediatR;

namespace TaskManagement.Core.Application.UseCases.Queries.GetAllTask;

/// <summary>
/// Получить состав корзины
/// </summary>
public class Query : IRequest<Response>
{
    public Query()
    {
    }
}