namespace Email.Domain.Entities
{
    public class Email
    {
        public Email(string userName, string emailAddress, int amount, Guid orderId)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            EmailAddress = emailAddress;
            Amount = amount;
            OrderId = orderId;
            CreatedDate = DateTime.Now;
        }
        
        public Guid Id { get;private set; }
        public Guid OrderId { get;private set; }
        public string UserName { get;private set; }
        public string EmailAddress { get;private set; }
        public int Amount { get;private set; }
        public DateTime CreatedDate { get;private set; }
    }
}
