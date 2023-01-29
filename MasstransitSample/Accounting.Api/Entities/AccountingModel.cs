namespace Accounting.Api.Entities
{
    public class AccountingModel
    {
        public AccountingModel(Guid userId,string userName, int amount, int productId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            UserName = userName;
            Amount = amount;
            ProductId = productId;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string UserName { get; private set; }
        public int ProductId { get; private set; }
        public int Amount { get; private set; }
        
        
    }
}
