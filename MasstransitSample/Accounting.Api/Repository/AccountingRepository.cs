using Accounting.Api.Repository.IRepository;
using Accounting.Api.Entities;
using Accounting.Api.Commands;
using AutoMapper;
using Npgsql;
using Dapper;

namespace Accounting.Api.Repository
{
    public class AccountingRepository:IAccountingRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountingRepository(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        //get all with dapper
        public async Task<List<AccountingModel>> GetAll()
        {
            var connectionString = _configuration.GetConnectionString("AccountingDb");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var allAccount = await connection.QueryAsync<AccountingModel>("SELECT * FROM Accounting");
                return allAccount.ToList();
            }
        }


        public async Task<bool> AddAccount(AddAccountingCommand account)
        {
            var newAccount = _mapper.Map<AccountingModel>(account);
            var connectionString = _configuration.GetConnectionString("AccountingDb");
            using var connection = new NpgsqlConnection(connectionString);

            var affected =
                await connection.ExecuteAsync(
                    "INSERT INTO Accounting(Id, ProductId,Amount, UserId,UserName,Number,EmailAddress,CreatedDate)" +
                    " VALUES (@Id, @ProductId,@Amount, @UserId,@UserName,@Number,@EmailAddress,@CreatedDate) ",
                    new
                    {
                        newAccount.Id,
                        newAccount.ProductId,
                        newAccount.Amount,
                        newAccount.UserId,
                        newAccount.UserName,
                        newAccount.Number,
                        newAccount.EmailAddress,
                        newAccount.CreatedDate
                    });

            return affected != 0;
        }
    }
}
