﻿namespace Order.Api
{
    public class OrderCommand
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Number { get; set; }
        public string EmailAddress { get; set; }
    }

}
