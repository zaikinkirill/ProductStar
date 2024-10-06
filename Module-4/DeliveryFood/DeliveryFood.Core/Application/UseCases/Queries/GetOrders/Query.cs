using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Queries.GetOrders;

public class Query: IRequest<Response>
{
    public Query()
    {
    }
}