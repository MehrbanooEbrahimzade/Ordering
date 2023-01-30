namespace EventBus.Messages.Events;

public record OperationFinishedEvent
{
    public string UserName { get; set; }
    public string Number { get; set; }
}
