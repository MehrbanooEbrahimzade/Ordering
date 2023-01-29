namespace Order.Api
{
    public class Order
    {
        public Order(int productId, int amount, Guid userId, string userName, string number, string emailAddress)
        {
            ProductId = productId;
            Amount = amount;
            UserId = userId;
            UserName = userName;
            Number = number;
            EmailAddress = emailAddress;
        }

        public int ProductId { get; private set;}
        public int Amount { get; private set;}

        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public string Number { get; private set; }
        public string EmailAddress { get; private set; }
    }
}
