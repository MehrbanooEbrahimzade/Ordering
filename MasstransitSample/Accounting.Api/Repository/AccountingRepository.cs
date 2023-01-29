using Accounting.Api.Repository.IRepository;
using Accounting.Api.Entities;
using System.Linq;
using Accounting.Api.Commands;

namespace Accounting.Api.Repository
{
    public class AccountingRepository:IAccountingRepository
    {
        private List<AccountingModel> _accounts = new();

        public async Task<List<AccountingModel>> GetAll()
        {
            return _accounts;
        }

        public async Task<bool> AddAccount(AddAccountingCommand account)
        {
            var newAccount = new AccountingModel(account.UserId, account.UserName, account.Amount, account.ProductId);
            _accounts.Add(newAccount);
            return true;
        }
        public async Task<AccountingModel> GetBy(string userName)
        {
            var account = _accounts.SingleOrDefault(x => x.UserName == userName);
            if (account == null)
            {
                throw new Exception("not found!");
            }

            return account;
        }
    }
}
