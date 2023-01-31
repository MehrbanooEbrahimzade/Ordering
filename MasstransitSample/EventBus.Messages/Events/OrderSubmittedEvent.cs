﻿namespace EventBus.Messages.Events;

public record OrderSubmittedEvent
{
    public int ProductId { get;init; }
    public int Amount { get;init; }

    public Guid UserId { get;init; }
    public string UserName { get;init; }
    public string Number { get;init; }
    public string EmailAddress { get;init; }

}