namespace EventBus.Messages.Events;

public interface OrderSubmitted
{
    public Guid UserId { get; set; }
    public string UserName { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
}