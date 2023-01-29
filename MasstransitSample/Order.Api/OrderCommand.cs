namespace Order.Api
{
    public class OrderCommand
    {
        public Guid UserId { get;  set; }
        public string UserName { get;  set; }
        public int ProductId { get;  set; }
        public int Amount { get;  set; }
    }
}
