namespace EventBus.Messages.Events
{
    public record OrderSubmitted
    {
        public Guid OrderId { get; init; }
        public string UserName { get; init; }
        public int Amount { get; init; }
        public string EmailAddress { get; init; }
        public string Number { get; set; }
    }
}
