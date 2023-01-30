using Accounting.Api.Commands;
using Accounting.Api.Entities;

namespace Accounting.Api.Repository.IRepository
{
    public interface IAccountingRepository
    {
        Task<List<AccountingModel>>GetAll();
        Task<bool> AddAccount(AddAccountingCommand account);
    }
}
