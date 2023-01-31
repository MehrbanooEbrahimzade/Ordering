namespace EventBus.Messages.Events;

public record OrderSubmittedResponse
{
    public bool IsSuccess { get; init; }
}
    
