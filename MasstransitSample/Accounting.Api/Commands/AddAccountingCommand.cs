namespace Accounting.Api.Commands
{
    public class AddAccountingCommand
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
