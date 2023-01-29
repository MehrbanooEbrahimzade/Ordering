namespace Email.Application.Features.Orders.Queries.GetEmail
{
    public class GetEmailDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
