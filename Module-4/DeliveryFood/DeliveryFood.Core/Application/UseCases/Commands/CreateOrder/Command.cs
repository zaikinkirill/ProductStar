using MediatR;

namespace DeliveryFood.Core.Application.UseCases.Commands.CreateOrder;

public class Command : IRequest<Guid>
{
    public Command()
    {
    }
}