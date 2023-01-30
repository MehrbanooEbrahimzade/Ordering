using Dapper;
using Email.Application.Contracts.Persistence;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Email.Infrastructure.Repositories
{
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
        public async Task<bool> CreateEmail(Domain.Entities.Email email)
        {
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
