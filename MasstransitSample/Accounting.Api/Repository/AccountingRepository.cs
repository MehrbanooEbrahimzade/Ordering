using Accounting.Api.Repository.IRepository;
using Accounting.Api.Entities;
using System.Linq;
using Accounting.Api.Commands;
using AutoMapper;
using Npgsql;

namespace Accounting.Api.Repository
{
    public class AccountingRepository:IAccountingRepository
    {
        private readonly IConfiguration _configuration;

        public AccountingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private List<AccountingModel> _accounts = new();
        private readonly IMapper _mapper;


        public AccountingRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<AccountingModel>> GetAll()
        {
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var email = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Email>(
                    "SELECT * FROM Email WHERE OrderId = @OrderId", new { OrderId = orderId });
                return email;
            }
        }

        public async Task<bool> AddAccount(AddAccountingCommand account)
        {
            var newAccount = _mapper.Map<AccountingModel>(account);
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            using var connection = new NpgsqlConnection(connectionString);

            var affected =
                await connection.ExecuteAsync(
                    "INSERT INTO Email (Id,OrderId,UserName, EmailAddress, Amount, CreatedDate)" +
                    " VALUES (@Id,@OrderId, @UserName, @EmailAddress, @Amount , @CreatedDate) ",
                    new
                    {
                        email.Id,
                        email.OrderId,
                        email.UserName,
                        email.EmailAddress,
                        email.Amount,
                        email.CreatedDate
                    });

            return affected != 0;
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
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;

        public EmailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Domain.Entities.Email> GetEmailByOrderId(Guid orderId)
        {
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var email = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Email>(
                    "SELECT * FROM Email WHERE OrderId = @OrderId", new { OrderId = orderId });
                return email;
            }
        }


        public async Task<bool> DeleteEmail(Guid id)
        {
            var connectionString = _configuration.GetConnectionString("ConnectionString");
            using var connection = new NpgsqlConnection(connectionString);

            var affected =
                await connection.ExecuteAsync("DELETE FROM Email WHERE Id = @Id", new { Id = id });
            return affected != 0;
        }
    }
}
