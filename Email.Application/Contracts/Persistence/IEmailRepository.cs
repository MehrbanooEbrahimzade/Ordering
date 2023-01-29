namespace Email.Application.Contracts.Persistence
{
    public interface IEmailRepository
    {
        Task<Domain.Entities.Email> GetEmailByOrderId(Guid orderId);
        Task<bool> CreateEmail(Domain.Entities.Email email);
        Task<bool> DeleteEmail(Guid id);
    }
}
