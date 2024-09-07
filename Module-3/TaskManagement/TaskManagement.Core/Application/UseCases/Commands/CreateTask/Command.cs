using MediatR;

namespace TaskManagement.Core.Application.UseCases.Commands.CreateTask;

public class Command : IRequest<bool>
{
    public string Title { get; }
    
    public string Description { get; }
    
    public Command(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException(nameof(title));
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException(nameof(description));
        
        Title = title;
        Description = description;
    }
}