namespace Order.Api
{
    public class Order
    {
        public Order(Guid userId, string userName, int amount, int productId)
        {
            UserId = userId;
            UserName = userName;
            Amount = amount;
            ProductId = productId;
        }
        public Guid UserId { get;private set;}
        public string UserName { get; private set;}
        public int ProductId { get; private set;}
        public int Amount { get; private set;}
    }
}
