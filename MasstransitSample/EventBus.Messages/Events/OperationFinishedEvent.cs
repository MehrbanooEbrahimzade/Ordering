namespace EventBus.Messages.Events;

public record OperationFinishedEvent
{
    public string UserName { get;init;}
    public string Number { get;init;}
}
